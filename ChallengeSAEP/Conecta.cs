using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChallengeSAEP
{
    public class Conecta
    {
        private static string str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Aluno\\Desktop\\Chale\\ChallengeSAEP\\ChallengeSAEP\\DbTurma.mdf;Integrated Security=True";
        private static SqlConnection con = null;

        public static SqlConnection Conexao()
        {
            con = new SqlConnection(str);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
            }
            catch (Exception sqle)
            {
                con = null;
                MessageBox.Show(sqle.Message);
            }
            return con;
        }
        public static void FecharConexao()
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
}
