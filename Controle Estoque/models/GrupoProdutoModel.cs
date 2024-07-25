using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Controle_Estoque.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Prencha o nome")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public static List<GrupoProdutoModel> RecuperarLista()
        {
            var ret = new List<GrupoProdutoModel>();

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = @"Server=192.168.222.243; Database=cntl-estoque-YT; User Id=sa; Password=sa; Connect Timeout=30; Encrypt=False; TrustServerCertificate=True; ApplicationIntent=ReadWrite; MultiSubnetFailover=False";
                conn.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conn;
                    comando.CommandText = string.Format("SELECT * FROM GRUPO_PRODUTO ORDER BY NOME ");
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new GrupoProdutoModel
                        {
                            Id = (int)reader["ID"],
                            Nome = (string)reader["NOME"],
                            Ativo = (bool)reader["ATIVO"]
                        });
                    }
                }
            }

            return ret;
        }

        public static GrupoProdutoModel RecuperarPorID(int id)
        {
            GrupoProdutoModel ret = null;

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = @"Server=192.168.222.243; Database=cntl-estoque-YT; User Id=sa; Password=sa; Connect Timeout=30; Encrypt=False; TrustServerCertificate=True; ApplicationIntent=ReadWrite; MultiSubnetFailover=False";
                conn.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conn;
                    comando.CommandText = string.Format("SELECT * FROM GRUPO_PRODUTO WHERE (ID = {0})", id);
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret = new GrupoProdutoModel
                        {
                            Id = (int)reader["ID"],
                            Nome = (string)reader["NOME"],
                            Ativo = (bool)reader["ATIVO"]
                        };
                    }
                }
            }

            return ret;
        }

        public static bool ExcluirPorId(int id)
        {
            var ret = false;

            if(RecuperarPorID(id) != null)
            {
                using (var conn = new SqlConnection())
                {
                    conn.ConnectionString = @"Server=192.168.222.243; Database=cntl-estoque-YT; User Id=sa; Password=sa; Connect Timeout=30; Encrypt=False; TrustServerCertificate=True; ApplicationIntent=ReadWrite; MultiSubnetFailover=False";
                    conn.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conn;
                        comando.CommandText = string.Format("DELETE FROM GRUPO_PRODUTO WHERE (ID = {0})", id);
                        ret = comando.ExecuteNonQuery() > 0;
                    }
                }
            }
            
            return ret;
        }

        public int Salvar()
        {
            var ret = 0;

            var model = RecuperarPorID(this.Id);
            
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = @"Server=192.168.222.243; Database=cntl-estoque-YT; User Id=sa; Password=sa; Connect Timeout=30; Encrypt=False; TrustServerCertificate=True; ApplicationIntent=ReadWrite; MultiSubnetFailover=False";
                conn.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conn;

                    if (model == null)
                    {
                        comando.CommandText = string.Format("INSERT INTO GRUPO_PRODUTO (NOME, ATIVO) VALUES ('{0}', {1}); SELECT CONVERT(INT, SCOPE_IDENTITY())", this.Nome, this.Ativo ? 1 : 0);
                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = string.Format("UPDATE GRUPO_PRODUTO SET NOME='{1}', ATIVO= {2} WHERE ID = {0}",this.Id, this.Nome, this.Ativo ? 1 : 0);
                        if(comando.ExecuteNonQuery() > 0)
                        {
                            ret = this.Id;
                        }
                    }
                }
            }
          return ret;
        }
    }
}
