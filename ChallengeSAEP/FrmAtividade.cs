using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChallengeSAEP
{
    public partial class FrmAtividade : Form
    {
        private int _idTurma;
        public FrmAtividade(int idTurma)
        {
            _idTurma = idTurma;
            InitializeComponent();
            CarregarDadosTurma();
            CarregarAtividades();
        }
        private void CarregarDadosTurma()
        {
            using (SqlConnection con = Conecta.Conexao())
            {
                string query = "SELECT Nome FROM Turmas WHERE Id = @idTurma";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idTurma", _idTurma);
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        string nomeTurma = reader["Nome"].ToString();

                        lblTurma.Text = nomeTurma;
                    }
                    con.Close();
                }
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAtividade.Text))
            {
                MessageBox.Show("Por favor, preencha o nome da atividade.", "Campos vazios!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    using (SqlConnection con = Conecta.Conexao())
                    {
                        string query = "INSERT INTO Atividades(Nome, IdTurma) VALUES(@nome, @idTurma)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@nome", txtAtividade.Text);
                            cmd.Parameters.AddWithValue("@idTurma", _idTurma);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    MessageBox.Show("Atividade cadastrada com sucesso!!");
                    CarregarAtividades(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void CarregarAtividades()
        {
            using (SqlConnection con = Conecta.Conexao())
            {
                string query = "SELECT Id as Número, Nome FROM Atividades WHERE IdTurma = @idTurma";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@idTurma", _idTurma);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvAtividade.DataSource = dt;
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
