using EducaInvestAPI.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EducaInvestAPI.Entities
{
    public class Atividade
    {
        public int Id { get; set; }
        public string DescricaoAtividade { get; set; } = string.Empty;
        public StatusAtividadeEnum StatusAtividade { get; set; }
        public DateTime DataInicioAtividade { get; set; }
        public DateTime DataTerminoAtividade { get; set; }
        public decimal Percentual { get; set; }
        public int CronogramaId { get; set; }
        [JsonIgnore]
        public Cronograma? Cronograma { get; set; }
    }
}
