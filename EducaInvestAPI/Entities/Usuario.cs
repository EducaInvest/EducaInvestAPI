using EducaInvestAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EducaInvestAPI.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public PerfilUsuarioEnum Perfil { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Sobrenome { get; set; } = string.Empty;
        [Required]
        public string CPF { get; set; } = string.Empty;
        [Required]
        public string Telefone { get; set; } = string.Empty;
        public string LinkSocial { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public EstadoEnum UF { get; set; }
        public DateTime? DataAcesso { get; set; }
        [JsonIgnore]
        public List<byte[]> FileBytes { get; set; } = null;
        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }
        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }

        [NotMapped]
        public string PasswordString { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Projeto> Projetos { get; set; }

    }
}
