using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Backend.Database
{
    public class AgendamentoDatabase
    {
        Models.teste_drive_bdContext ctx = new Models.teste_drive_bdContext();
        public List<Models.TbAgendamento> Consultar(int id, string status)
        {
            return ctx.TbAgendamento.Where(x => x.DsStatus.ToLower() == status.ToLower() &&
                                            x.IdCliente == this.ConsultaCliente(id).IdCliente).ToList();
        }
        public Models.TbCliente ConsultaCliente(int id)
        {
            return ctx.TbCliente.FirstOrDefault(x => x.IdLogin == id);
        }
        public Models.TbLogin ConsultarLogin(int id)
        {
            return ctx.TbLogin.FirstOrDefault(x => x.IdLogin == id);
        }

        public Models.TbAgendamento ConsultarAgendamento(int id)
        {
            return ctx.TbAgendamento.FirstOrDefault(x => x.IdAgendamento == id);
        }

        public Models.TbVeiculo ConsultarVeiculo(int id)
        {
            return ctx.TbVeiculo.FirstOrDefault(x => x.IdVeiculo == id);
        }

        public Models.TbAgendamento AlterarStatus(int id, string status)
        {
            Models.TbAgendamento ag = this.ConsultarAgendamento(id);
            ag.DsStatus = status;
              
            if(status == "concluido" || status == "cancelado")
             {
                 Models.TbVeiculo veiculo = ctx.TbVeiculo.FirstOrDefault(x => x.IdVeiculo == ag.IdVeiculo);
                 veiculo.BtDisponivel = true;
             }

            ctx.SaveChanges();
            return ag;
        }

        public Models.TbAgendamento Cadastrar(Models.TbAgendamento tb)
        {
            ctx.TbAgendamento.Add(tb);
            ctx.SaveChanges();

            this.ConsultarVeiculo(tb.IdVeiculo).BtDisponivel = false;
            ctx.SaveChanges();

            return tb;
        }

        public Models.TbAgendamento AlterarAvaFeed(int id, int avaliacao, string feedback)
        {
            Models.TbAgendamento ag = this.ConsultarAgendamento(id);
            ag.NrAvaliacao = avaliacao;
            ag.DsFeedback = feedback;
            ctx.SaveChanges();
            return ag;
        }

        public List<DateTime> ConsultarHorarios(DateTime dia)
        {
            List<Models.TbAgendamento> ag = ctx.TbAgendamento.Where(x => x.DtAgendamento == dia).ToList();
            List<int> date = new List<int>{ 8,9,10,11,12,14,15,16,17,18 };
            //8,9,10,11,12,14,15,16,17,18
            foreach(int hr in date)
            {
                foreach(Models.TbAgendamento agen in ag)
                {
                    if(hr == agen.DtAgendamento.Hour) 
                    {
                        date.Remove(hr);
                    }
                }
            }
            return date.Select(x => dia.AddHours(x)).ToList();
        }
    }
}