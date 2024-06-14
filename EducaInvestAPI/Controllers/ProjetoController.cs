using EducaInvestAPI.Data;
using EducaInvestAPI.Entities;
using EducaInvestAPI.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducaInvestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly DataContext _context;

        public ProjetoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Projeto>>> GetAllProjetos()
        {
            try
            {
                var projetos = await _context.TB_PROJETOS.ToListAsync();
                return Ok(projetos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Projeto>> GetProjeto(int id)
        {
            try
            {
                var projeto = await _context.TB_PROJETOS.FindAsync(id);
                if (projeto == null)
                    return NotFound("Projeto não encontrado.");

                return Ok(projeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByPerfil/{usuarioId}")]
        public async Task<IActionResult> GetByPerfilAsync(int usuarioId)
        {
            try
            {
                var usuario = await _context.TB_USUARIOS
                    .FirstOrDefaultAsync(x => x.Id == usuarioId);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                List<Projeto> lista;

                if (usuario.Perfil == PerfilUsuarioEnum.Administrador || usuario.Perfil == PerfilUsuarioEnum.Investidor)
                {
                    lista = await _context.TB_PROJETOS.ToListAsync();
                }
                else
                {
                    lista = await _context.TB_PROJETOS
                        .Where(p => p.UsuarioId == usuarioId)
                        .ToListAsync();
                }

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Projeto>>> AddProjeto(Projeto projeto)
        {
            try
            {
                var usuario = await _context.TB_USUARIOS.FindAsync(projeto.UsuarioId);
                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                if (usuario.Perfil != PerfilUsuarioEnum.Estudante && usuario.Perfil != PerfilUsuarioEnum.Administrador)
                {
                    return Forbid("Acesso não autorizado.");
                }

                _context.TB_PROJETOS.Add(projeto);
                await _context.SaveChangesAsync();

                return Ok(await _context.TB_PROJETOS.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("uploadFotoProjeto")]
        public async Task<ActionResult> UploadFotoProjeto([FromForm] ICollection<IFormFile> fotoProjeto, int id)
        {
            if (fotoProjeto == null || fotoProjeto.Count == 0)
                return BadRequest();

            var fileProjeto = await _context.TB_PROJETOS.FindAsync(id);
            if (fileProjeto == null)
                return NotFound("Projeto não encontrado.");

            List<byte[]> data = new List<byte[]>();

            foreach (var formFile in fotoProjeto)
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

            fileProjeto.FileBytes = data;

            _context.TB_PROJETOS.Update(fileProjeto);
            await _context.SaveChangesAsync();

            return Ok("Foto adicionada com sucesso.");

        }


        [HttpPut]
        public async Task<ActionResult<List<Projeto>>> UpdateProjeto(Projeto updatedProjeto)
        {
            try
            {
                var tbProjeto = await _context.TB_PROJETOS.FindAsync(updatedProjeto.Id);
                if (tbProjeto == null)
                    return NotFound("Projeto não encontrado.");

                tbProjeto.NomeProjeto = updatedProjeto.NomeProjeto;
                tbProjeto.Subtitulo = updatedProjeto.Subtitulo;
                tbProjeto.DescricaoProjeto = updatedProjeto.DescricaoProjeto;
                tbProjeto.CustoProjeto = updatedProjeto.CustoProjeto;
                tbProjeto.StatusProjeto = updatedProjeto.StatusProjeto;

                _context.TB_PROJETOS.Update(tbProjeto);
                await _context.SaveChangesAsync();

                return Ok(tbProjeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Projeto>>> DeleteProjeto(int id)
        {
            try
            {
                var tbProjeto = await _context.TB_PROJETOS.FindAsync(id);
                if (tbProjeto == null)
                    return NotFound("Projeto não encontrado.");

                _context.TB_PROJETOS.Remove(tbProjeto);
                await _context.SaveChangesAsync();

                return Ok(await _context.TB_PROJETOS.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
