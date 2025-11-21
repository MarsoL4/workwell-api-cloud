namespace WorkWell.Application.DTOs.Enquetes
{
    public class RespostaEnqueteDto
    {
        public long Id { get; set; }
        public long EnqueteId { get; set; }
        public long FuncionarioId { get; set; }
        public string Resposta { get; set; } = null!;
    }
}