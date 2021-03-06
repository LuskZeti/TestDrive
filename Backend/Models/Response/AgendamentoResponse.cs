using System;

namespace Backend.Models.Response
{
    public class AgendamentoResponse
    {
        public int Id {get; set;}
        public int Veiculo {get; set;}
        public int Cliente {get; set;}
        public string Acompanhante {get; set;}
        public string Funcionario {get; set;}
        public string Inicial {get; set;}
        public string Final {get; set;}
        public TimeSpan HoraFinal {get; set;}
        public string Status {get; set;}
        public DateTime Data {get; set;}
        public string Feedback {get; set;}
        public int? Avaliacao {get; set;}
    }
}