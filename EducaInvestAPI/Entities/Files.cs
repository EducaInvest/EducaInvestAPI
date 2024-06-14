namespace EducaInvestAPI.Entities
{
    public class Files
    {
        public int Id { get; set; }
        public byte[] FileBytes { get; set; }
        public int ProjetoId { get; set; }


    }
}
