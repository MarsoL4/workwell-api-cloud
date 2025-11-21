using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.Enquetes
{
    public class RespostaEnquete
    {
        public long Id { get; set; }
        public long EnqueteId { get; set; }
        public Enquete? Enquete { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }

        [MaxLength(500)]
        public string Resposta { get; set; } = null!;
    }
}