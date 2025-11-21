using System.ComponentModel.DataAnnotations;

namespace WorkWell.Domain.Entities.EmpresaOrganizacao
{
    public class Setor
    {
        public long Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; } = null!;

        public long EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
    }
}