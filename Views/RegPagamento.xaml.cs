﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjetoLuna.Models;
using MySql.Data.MySqlClient;

namespace ProjetoLuna.Views
{
    public partial class RegPagamento : Window
    {
        private Pagamento _pag = new Pagamento();

        public RegPagamento()
        {
            InitializeComponent();
            cbFormaPag.Items.Add("Cartão");
            cbFormaPag.Items.Add("Dinheiro");
            cbFormaPag.Items.Add("Cheque");
            cbFormaPag.Items.Add("Outro");
            Loaded += RegPagamento_Loaded;
        }

        public RegPagamento(Pagamento pagamento)
        {
            InitializeComponent();
             Loaded += RegPagamento_Loaded;
            _pag = pagamento;
        }

        private void RegPagamento_Loaded(object sender, RoutedEventArgs e)
        {
            cbCaixa.ItemsSource = new CaixaDAO().List();
            cbDespesa.ItemsSource = new DespesaDAO().List();

            dtData.SelectedDate = DateTime.Now;
            Thora.SelectedTime = DateTime.Now;

            if (_pag.Id > 0)
                preencherForm();
        }

        private void preencherForm()
        {
            dtData.SelectedDate = _pag.Data;
            Thora.SelectedTime = _pag.Hora;

            if (double.TryParse(txtValor.Text, out double Valor))
                _pag.Valor = Valor;

            dtVenc.SelectedDate = _pag.Vencimento;
            cbFormaPag.SelectedItem = _pag.FormaPag;

            if (_pag.Caixa != null)
                cbCaixa.SelectedValue = _pag.Caixa.Id;

            if (_pag.Despesa != null)
                cbDespesa.SelectedValue = _pag.Despesa.Id;
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            _pag.Data = dtData.SelectedDate;
            if (double.TryParse(txtValor.Text, out double Valor))
                _pag.Valor = Valor;
            _pag.FormaPag = cbFormaPag.Text;
            _pag.Vencimento = dtVenc.SelectedDate;
            _pag.Hora = Thora.SelectedTime;
            _pag.Caixa = cbCaixa.SelectedItem as Caixa;
            _pag.Despesa = cbDespesa.SelectedItem as Despesa;

            try
            {
                var dao = new PagamentoDAO();
                if (_pag.Id > 0)
                {
               //     dao.Update(_pag);
                    MessageBox.Show("Registro do Pagamento atualizado com sucesso!");

                }
                else
                {
                    dao.Insert(_pag);
                    MessageBox.Show("Registro do Pagamento inseridos com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            var form = new Views.PagamentoFormWindow();
            form.Show();
            this.Close();
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
           
            txtValor.Clear();
            cbCaixa.SelectedIndex = -1;
            cbDespesa.SelectedIndex = -1;
            cbFormaPag.SelectedIndex = -1;
        }
    }
}
