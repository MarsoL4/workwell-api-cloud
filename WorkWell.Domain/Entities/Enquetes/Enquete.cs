using System;
using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.Enquetes
{
    public class Enquete
    {
        public long Id { get; set; }
        public long EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }

        [MaxLength(300)]
        public string Pergunta { get; set; } = null!;

        public DateTime DataCriacao { get; set; }
        public bool Ativa { get; set; }
    }
}