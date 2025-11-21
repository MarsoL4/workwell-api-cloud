using System;
using System.Collections.Generic;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.Agenda
{
    public class AgendaFuncionario
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }
        public DateTime Data { get; set; }
        public List<ItemAgenda> Itens { get; set; } = [];
    }
}