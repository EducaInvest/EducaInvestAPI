using EducaInvestAPI.Data;
using EducaInvestAPI.Entities;
using EducaInvestAPI.Entities.Enums;
using EducaInvestAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EducaInvestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
        }

        private async Task<bool> UsuarioExistente(string email)
        {
            if (await _context.TB_USUARIOS.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Usuario> list = await _context.TB_USUARIOS.ToListAsync();
                return Ok(list);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um problema ao buscar os usuários. Por favor, tente novamente mais tarde.");
            }
        }

        [HttpGet("GetProjetosByUsuario/{usuarioId}")]
        public async Task<ActionResult<List<Projeto>>> GetProjetosByUsuarioId(int usuarioId)
        {
            var projetos = await _context.TB_PROJETOS
                                         .Where(p => p.UsuarioId == usuarioId)
                                         .ToListAsync();

            if (projetos == null || projetos.Count == 0)
            {
                return NotFound("Nenhum projeto encontrado para este usuário.");
            }

            return Ok(projetos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Usuario? usuario = await _context.TB_USUARIOS.FirstOrDefaultAsync(x => x.Id == id);
                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }
                return Ok(usuario);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um problema ao buscar sua conta. Por favor, tente novamente mais tarde.");
            }
        }


        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario(Usuario novoUsuario)
        {
            try
            {
                if (novoUsuario.Email == "" || novoUsuario.PasswordString == "" || novoUsuario.Nome == "" ||
                        novoUsuario.Sobrenome == "" || novoUsuario.CPF == "" || novoUsuario.Telefone == "")
                    throw new Exception("Preencha todas as informações obrigatórias.");

                if (await UsuarioExistente(novoUsuario.Email))
                    throw new Exception("Email já cadastrado.");

                Criptografia.CriarPasswordHash(novoUsuario.PasswordString, out byte[] hash, out byte[] salt);
                novoUsuario.PasswordString = string.Empty;
                novoUsuario.PasswordHash = hash;
                novoUsuario.PasswordSalt = salt;
                novoUsuario.DataAcesso = DateTime.Now;

                await _context.TB_USUARIOS.AddAsync(novoUsuario);
                await _context.SaveChangesAsync();

                string message = $"A conta para o email {novoUsuario.Email} foi cadastrada com sucesso!";

                return Ok(message);

            }
            catch (Exception)
            {
                return BadRequest("Houve um problema ao registrar sua conta. Por favor, tente novamente mais tarde.");
            }
        }

        [HttpPost("Verificar")]
        public async Task<IActionResult> VerificarUsuario(Usuario usuario)
        {
            try
            {
                Usuario? usuarioRegistrado = await _context.TB_USUARIOS
                    .FirstOrDefaultAsync(x =>
                        x.Email.ToLower() == usuario.Email.ToLower()
                    );

                if (usuarioRegistrado == null)
                {
                    return NotFound("Usuário não encontrado.");
                }
                else if (usuarioRegistrado.PasswordHash == null || usuarioRegistrado.PasswordSalt == null)
                {
                    return BadRequest("A conta está com informações incompletas. Não é possível verificar.");
                }
                else if (!Criptografia.VerificarPasswordHash(usuario.PasswordString, usuarioRegistrado.PasswordHash, usuarioRegistrado.PasswordSalt))
                {
                    return BadRequest("Senha incorreta.");
                }
                else
                {
                    usuarioRegistrado.DataAcesso = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return Ok(usuarioRegistrado);
                }
            }
            catch (Exception)
            {
                return BadRequest("Houve um problema ao autenticar sua conta. Por favor, tente novamente.");
            }
        }


        [HttpPost("uploadFotoUsuario")]
        public async Task<ActionResult> UploadFotoUsuario([FromForm] ICollection<IFormFile> fotoUsuario, int id)
        {
            if (fotoUsuario == null || fotoUsuario.Count == 0)
                return BadRequest("Nenhuma foto foi enviada.");

            var fileUsuario = await _context.TB_USUARIOS.FindAsync(id);
            if (fileUsuario == null)
                return NotFound("Usuario não encontrado.");

            List<byte[]> data = new List<byte[]>();

            foreach (var formFile in fotoUsuario)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);

                        data.Add(stream.ToArray());
                    }
                }
            }

            fileUsuario.FileBytes = data;

            _context.TB_USUARIOS.Update(fileUsuario);
            await _context.SaveChangesAsync();

            return Ok("Foto adicionada com sucesso.");

        }

        [HttpPut("AtualizarEmail")]
        public async Task<IActionResult> AtualizarEmail(Usuario u)
        {
            try
            {
                Usuario? usuario = await _context.TB_USUARIOS.FirstOrDefaultAsync(x => x.Id == u.Id);
                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                usuario.Email = u.Email;
                _context.Attach(usuario).Property(x => x.Email).IsModified = true;
                await _context.SaveChangesAsync();

                string message = $"O email '{usuario.Email}' foi alterado com sucesso!";
                return Ok(message);
            }
            catch (Exception)
            {
                return BadRequest("Houve um problema ao atualizar o email. Por favor, tente novamente.");
            }
        }



        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> ChangePassword(Usuario credenciais)
        {

            try
            {
                Usuario? usuario = await _context.TB_USUARIOS
                    .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(credenciais.Email.ToLower()));

                if (usuario == null)
                {
                    return NotFound("Conta não encontrada.");
                }
                else
                {
                    Criptografia.CriarPasswordHash(credenciais.PasswordString, out byte[] hash, out byte[] salt);
                    usuario.PasswordString = string.Empty;
                    usuario.PasswordHash = hash;
                    usuario.PasswordSalt = salt;
                    await _context.SaveChangesAsync();

                    return Ok($"Senha do '{usuario.Email}' alterada com sucesso!");
                }

            }
            catch (Exception)
            {
                return BadRequest("Houve um problema ao alterar a senha. Por favor, tente novamente mais tarde.");
            }
        }

        [HttpPut("AlterarCredenciais")]
        public async Task<IActionResult> AlterarCredenciais(Usuario u)
        {
            try
            {
                var usuario = await _context.TB_USUARIOS.FirstOrDefaultAsync(x => x.Id == u.Id);
                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                usuario.Nome = u.Nome;
                usuario.Sobrenome = u.Sobrenome;
                usuario.CPF = u.CPF;
                usuario.Telefone = u.Telefone;
                usuario.LinkSocial = u.LinkSocial;
                usuario.Cidade = u.Cidade;
                usuario.UF = u.UF;

                _context.Entry(usuario).Property(x => x.Nome).IsModified = true;
                _context.Entry(usuario).Property(x => x.Sobrenome).IsModified = true;
                _context.Entry(usuario).Property(x => x.CPF).IsModified = true;
                _context.Entry(usuario).Property(x => x.Telefone).IsModified = true;
                _context.Entry(usuario).Property(x => x.LinkSocial).IsModified = true;
                _context.Entry(usuario).Property(x => x.Cidade).IsModified = true;
                _context.Entry(usuario).Property(x => x.UF).IsModified = true;
                await _context.SaveChangesAsync();

                return Ok("Alterado com sucesso!");
            }
            catch (Exception)
            {
                return BadRequest("Houve um problema ao alterar seus dados. Por favor, tente novamente.");
            }
        }




        [HttpPost("Delete")]
        public async Task<IActionResult> ExcluirUsuario(Usuario usuarioRegistrado)
        {
            try
            {
                Usuario? usuario = await _context.TB_USUARIOS
                    .FirstOrDefaultAsync(x =>
                        x.Id == usuarioRegistrado.Id &&
                        x.Email.ToLower() == usuarioRegistrado.Email.ToLower()
                    );

                if (usuario == null)
                {
                    return NotFound("Conta não encontrada.");
                }
                else if (usuario.PasswordHash == null || usuario.PasswordSalt == null)
                {
                    return BadRequest("A conta está com informações incompletas. Não é possível excluir.");
                }
                else if (!Criptografia.VerificarPasswordHash(usuarioRegistrado.PasswordString, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    return BadRequest("Senha incorreta.");
                }
                else
                {
                    _context.TB_USUARIOS.Remove(usuario);
                    await _context.SaveChangesAsync();
                    string message = $"A conta {usuario.Email} foi excluída com sucesso.";
                    return Ok(message);
                }
            }
            catch (Exception)
            {
                return BadRequest("Houve um problema ao tentar excluir sua conta. Por favor, tente novamente.");
            }
        }



    }
}
