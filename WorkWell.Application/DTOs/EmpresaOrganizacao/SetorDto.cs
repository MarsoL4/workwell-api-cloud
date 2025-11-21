namespace WorkWell.Application.DTOs.EmpresaOrganizacao
{
    public class SetorDto
    {
        public long Id { get; set; }
        public string Nome { get; set; } = null!;
        public long EmpresaId { get; set; }
    }
}