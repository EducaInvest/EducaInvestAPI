using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EducaInvestAPI.Entities
{
    public class Cronograma
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        [JsonIgnore]
        public Projeto Projeto { get; set; }
        [JsonIgnore]
        public List<Atividade> Atividades { get; set; }
    }
}
