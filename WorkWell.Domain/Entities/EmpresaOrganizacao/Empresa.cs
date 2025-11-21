using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkWell.Domain.Entities.EmpresaOrganizacao
{
    public class Empresa
    {
        public long Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; } = null!;

        [MaxLength(200)]
        public string EmailAdmin { get; set; } = null!;

        [MaxLength(256)]
        public string SenhaAdmin { get; set; } = null!;

        [MaxLength(100)]
        public string TokenAcesso { get; set; } = null!;

        [MaxLength(300)]
        public string LogoUrl { get; set; } = null!;

        [MaxLength(10)]
        public string CorPrimaria { get; set; } = null!;

        [MaxLength(10)]
        public string CorSecundaria { get; set; } = null!;

        [MaxLength(1000)]
        public string Missao { get; set; } = null!;

        [MaxLength(1000)]
        public string PoliticaBemEstar { get; set; } = null!;

        public List<Funcionario> Funcionarios { get; set; } = [];
        public List<Setor> Setores { get; set; } = [];
    }
}