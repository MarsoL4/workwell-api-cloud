namespace WorkWell.Application.DTOs.ApoioPsicologico
{
    public class SOSemergenciaDto
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public DateTime? DataAcionamento { get; set; }
        public string Tipo { get; set; } = null!;
        public bool PsicologoNotificado { get; set; }
    }
}