namespace WorkWell.Application.DTOs.AtividadesBemEstar
{
    public class ParticipacaoAtividadeDto
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public long AtividadeId { get; set; }
        public bool Participou { get; set; }
        public DateTime? DataParticipacao { get; set; }
    }
}