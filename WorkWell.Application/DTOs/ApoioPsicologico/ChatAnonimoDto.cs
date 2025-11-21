namespace WorkWell.Application.DTOs.ApoioPsicologico
{
    public class ChatAnonimoDto
    {
        public long Id { get; set; }
        public long? RemetenteId { get; set; }
        public long PsicologoId { get; set; }
        public string Mensagem { get; set; } = null!;
        public DateTime? DataEnvio { get; set; }
        public bool Anonimo { get; set; }
    }
}