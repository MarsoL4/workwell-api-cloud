using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Enums.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.EmpresaOrganizacao
{
    public class Funcionario
    {
        public long Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; } = null!;

        [MaxLength(200)]
        public string Email { get; set; } = null!;

        [MaxLength(256)]
        public string Senha { get; set; } = null!;

        [MaxLength(100)]
        public string TokenEmpresa { get; set; } = null!;

        public Cargo Cargo { get; set; }
        public bool Ativo { get; set; } = true;
        public long SetorId { get; set; }
        public long EmpresaId { get; set; }
        public Setor? Setor { get; set; }
        public PerfilEmocional? PerfilEmocional { get; set; }
    }
}