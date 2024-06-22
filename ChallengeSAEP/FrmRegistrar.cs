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
    public partial class FrmRegistrar : Form
    {
        public FrmRegistrar()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtSenha.Text))
                {
                    MessageBox.Show("Por Favor preencha os campos", "Campos vazios!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SqlConnection con = Conecta.Conexao();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Professores(Nome,Email,Senha) VALUES(@nome,@email,@senha)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                    Conecta.Conexao();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Professor Cadastrado com Sucesso!!");
                    Conecta.FecharConexao();

                    txtNome.Text = "";
                    txtEmail.Text = "";
                    txtSenha.Text = "";
                }
                

            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }
        }
    }
}
