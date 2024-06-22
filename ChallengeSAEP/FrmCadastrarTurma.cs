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
    public partial class FrmCadastrarTurma : Form
    {
        public event EventHandler TurmaCadastrada;
        public FrmCadastrarTurma()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTurma.Text))
                {
                    MessageBox.Show("Por Favor preencha os campos", "Campos vazios!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SqlConnection con = Conecta.Conexao();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Turmas(Nome,IdProfessor) VALUES(@nome,@professor)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@nome", txtTurma.Text);
                    cmd.Parameters.AddWithValue("@professor", Program.ProfessorLogadoId);
                    Conecta.Conexao();
                    cmd.ExecuteNonQuery();
                    FrmPrincipal frmPrincipal = new FrmPrincipal();
                    frmPrincipal.CarregarDgv();
                    MessageBox.Show("Turma Cadastrada com Sucesso!!");
                    Conecta.FecharConexao();
                    TurmaCadastrada?.Invoke(this, EventArgs.Empty);

                    this.Close();
                }
                
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
