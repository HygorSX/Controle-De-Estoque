using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace Controle_Estoque.Models
{
    public class UsuarioModel
    {
        public static bool ValidarUsuario(string login, string senha)
        {
            var ret = false;

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = @"Server=DESKTOP-73M6QT8\MSSQLSERVER01;Database=controle-estoque;Integrated Security=True;";
                conn.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conn;
                    comando.CommandText = string.Format("SELECT COUNT(*) FROM USUARIO WHERE LOGIN='{0}' AND SENHA='{1}'", login, senha);
                    ret = ((int)comando.ExecuteScalar() > 0);
                }
            }

            return ret;
        }
    }
}