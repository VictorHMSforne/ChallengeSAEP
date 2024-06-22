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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void linkLblCadastrar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRegistrar frmRegister = new FrmRegistrar();
            frmRegister.Show();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtSenha.Text))
                {
                    MessageBox.Show("Por Favor preencha os campos", "Campos vazios!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SqlConnection con = Conecta.Conexao();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Professores WHERE Email=@email AND Senha=@senha";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                    Conecta.Conexao();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        int idProfessor = (int)dr["Id"];
                        string nomeProfessor = dr["Nome"].ToString();

                        Program.ProfessorLogadoId = idProfessor;
                        Program.ProfessorLogadoNome = nomeProfessor;

                        this.Hide();
                        MessageBox.Show("Logado com Sucesso!");
                        FrmPrincipal frmPrincipal = new FrmPrincipal();
                        frmPrincipal.Show();
                    }
                    else
                    {
                        MessageBox.Show("Professor não encontrado!");
                    }
                }
                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }


        private void btnSair_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
