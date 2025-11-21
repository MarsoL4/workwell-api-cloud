namespace WorkWell.Application.DTOs.ApoioPsicologico
{
    public class PsicologoDto
    {
        public long Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string TokenEmpresa { get; set; } = null!;
        public string Crp { get; set; } = null!;
        public bool Ativo { get; set; }
        public long SetorId { get; set; }
    }
}