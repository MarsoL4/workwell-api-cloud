using WorkWell.Domain.Enums.EmpresaOrganizacao;

namespace WorkWell.Application.DTOs.EmpresaOrganizacao
{
    public class FuncionarioDto
    {
        public long Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string TokenEmpresa { get; set; } = null!;
        public Cargo Cargo { get; set; }
        public bool Ativo { get; set; }
        public long SetorId { get; set; }
        public long EmpresaId { get; set; } 
    }
}