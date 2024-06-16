using EducaInvestAPI.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EducaInvestAPI.Entities
{
    public class Projeto
    {
        public int Id { get; set; }
        public StatusProjetoEnum StatusProjeto { get; set; }
        [Required]
        public string NomeProjeto { get; set; } = string.Empty;
        public string Subtitulo { get; set; } = string.Empty;
        public string DescricaoProjeto { get; set; } = string.Empty;
        public decimal CustoProjeto { get; set; }
        public bool Investido { get; set; }
        public DateTime DataPublicacao { get; set; }
        public int UsuarioId { get; set; }
        public int CronogramaId { get; set; }
        [JsonIgnore]
        public List<byte[]>? FileBytes { get; set; } = null;
        [JsonIgnore]
        public Usuario? Usuario { get; set; }
        [JsonIgnore]
        public Cronograma? Cronograma { get; set; }

    }
}
