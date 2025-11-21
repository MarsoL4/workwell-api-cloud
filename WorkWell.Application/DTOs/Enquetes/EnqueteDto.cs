namespace WorkWell.Application.DTOs.Enquetes
{
    public class EnqueteDto
    {
        public long Id { get; set; }
        public long EmpresaId { get; set; }
        public string Pergunta { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }
        public bool Ativa { get; set; }
    }
}