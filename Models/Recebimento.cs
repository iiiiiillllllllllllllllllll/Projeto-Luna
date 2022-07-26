﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLuna.Models
{
    public class Recebimento
    {
        public int Id { get; set; }
        public DateTime? Data { get; set; }
        public int Parcela { get; set; }
        public double ValorParcela { get; set; }
        public double Valor { get; set; }
        public string Forma { get; set; }
        public DateTime? Vencimento { get; set; }
        public DateTime? Hora { get; set; }
        public Caixa Caixa { get; set; }
        public Venda Venda { get; set; }
    }
}
