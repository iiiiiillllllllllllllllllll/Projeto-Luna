﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoLuna.DataBase;
using ProjetoLuna.Helpers;
using MySql.Data.MySqlClient;

namespace ProjetoLuna.Models
{
    internal class FuncionarioDAO
    {
        private static Conexao _conn = new Conexao();

        public void Insert(Funcionario funcionario)
        {

            try
            {
                var comando = _conn.Query();

                comando.CommandText = "call inserirFuncionario(@Nome, @DataNasc, @Salario,@CPF, @Email, @Telefone, @Sexo, @Funcao);";

                comando.Parameters.AddWithValue("@Nome", funcionario.Nome);
                comando.Parameters.AddWithValue("@DataNasc", funcionario.DataNasc?.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@Salario", funcionario.Salario);            
                comando.Parameters.AddWithValue("@CPF", funcionario.CPF);
                comando.Parameters.AddWithValue("@Email", funcionario.Email);
                comando.Parameters.AddWithValue("@Telefone", funcionario.Telefone);
                comando.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                comando.Parameters.AddWithValue("@Funcao", funcionario.Funcao);

                var resultado = comando.ExecuteNonQuery();

                if (resultado == 0)
                {
                    throw new Exception("Ocorreram erros ao salvar as informações!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Funcionario> List()
        {
            try
            {
                var lista = new List<Funcionario>();
                var comando = _conn.Query();

                comando.CommandText = "SELECT * FROM Funcionario";

                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    var funcionario= new Funcionario();

                    funcionario.Id = reader.GetInt32("id_fun");
                    funcionario.Nome = DAOHelper.GetString(reader, "nome_fun");
                    funcionario.DataNasc = DAOHelper.GetDateTime(reader, "dataNasc_fun");
                    funcionario.Salario = DAOHelper.GetDouble(reader, "salario_fun");                  
                    funcionario.CPF = DAOHelper.GetString(reader, "cpf_fun");
                    funcionario.Email = DAOHelper.GetString(reader, "email_fun");
                    funcionario.Telefone = DAOHelper.GetString(reader, "telefone_fun");
                    funcionario.Sexo = DAOHelper.GetString(reader, "sexo_fun");
                    funcionario.Funcao = DAOHelper.GetString(reader, "funcao_fun");
                    lista.Add(funcionario);
                }
                reader.Close();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Funcionario funcionario)
        {
            try
            {
                var comando = _conn.Query();
                comando.CommandText = "DELETE FROM Funcionario WHERE id_fun = @id";
                comando.Parameters.AddWithValue("@id", funcionario.Id);
                var resultado = comando.ExecuteNonQuery();
                if (resultado == 0)
                {
                    throw new Exception("Ocorreram problemas ao deletar as informações");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Funcionario funcionario)
        {
            try
            {
                var comando = _conn.Query();

                comando.CommandText = "call atualizarFuncionario(@id, @Nome, @DataNasc, @Salario,@CPF, @Email, @Telefone, @Sexo, @Funcao);";

                comando.Parameters.AddWithValue("@Nome", funcionario.Nome);
                comando.Parameters.AddWithValue("@DataNasc", funcionario.DataNasc?.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@Salario", funcionario.Salario);             
                comando.Parameters.AddWithValue("@CPF", funcionario.CPF);
                comando.Parameters.AddWithValue("@Email", funcionario.Email);
                comando.Parameters.AddWithValue("@Telefone", funcionario.Telefone);
                comando.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                comando.Parameters.AddWithValue("@Funcao", funcionario.Funcao);
                comando.Parameters.AddWithValue("@id", funcionario.Id);

                var resultado = comando.ExecuteNonQuery();

                if (resultado == 0)
                {
                    throw new Exception("Ocorreram erros ao atualizar as informações");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
