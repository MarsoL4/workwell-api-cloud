using System;
using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.ApoioPsicologico
{
    public class ChatAnonimo
    {
        public long Id { get; set; }
        public long? RemetenteId { get; set; }
        public Funcionario? Remetente { get; set; }
        public long PsicologoId { get; set; }
        public Psicologo? Psicologo { get; set; }

        [MaxLength(2000)]
        public string Mensagem { get; set; } = null!;

        public DateTime DataEnvio { get; set; }
        public bool Anonimo { get; set; }
    }
}