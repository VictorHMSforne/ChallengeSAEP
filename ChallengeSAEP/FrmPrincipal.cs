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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            lblNome.Text = Program.ProfessorLogadoNome;
            CarregarDgv();
        }
        public void CarregarDgv()
        {
            int idProfessorLogado = Program.ProfessorLogadoId;

            using (SqlConnection con = Conecta.Conexao())
            {
                string query = "SELECT Id AS Numero, Nome FROM Turmas WHERE IdProfessor = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", idProfessorLogado);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvTurma.Columns.Clear();
                    dgvTurma.DataSource = dt;

                    DataGridViewButtonColumn columnExcluir = new DataGridViewButtonColumn();
                    columnExcluir.Name = "Excluir";
                    columnExcluir.HeaderText = "Excluir";
                    columnExcluir.Text = "Excluir";
                    columnExcluir.UseColumnTextForButtonValue = true;
                    dgvTurma.Columns.Add(columnExcluir);

                    DataGridViewButtonColumn columnVisualizar = new DataGridViewButtonColumn();
                    columnVisualizar.Name = "Visualizar";
                    columnVisualizar.HeaderText = "Visualizar";
                    columnVisualizar.Text = "Visualizar";
                    columnVisualizar.UseColumnTextForButtonValue = true;
                    dgvTurma.Columns.Add(columnVisualizar);

                    Conecta.FecharConexao();
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Program.ProfessorLogadoNome = null;
            this.Close();

            FrmLogin frmlogin = new FrmLogin();
            frmlogin.Show();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            FrmCadastrarTurma frmCadastrarTurma = new FrmCadastrarTurma();
            frmCadastrarTurma.TurmaCadastrada += FrmCadastrarTurma_TurmaCadastrada;
            frmCadastrarTurma.ShowDialog();
        }
        private void FrmCadastrarTurma_TurmaCadastrada(object sender, EventArgs e)
        {
            CarregarDgv();
        }

        private void dgvTurma_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTurma.Columns[e.ColumnIndex].Name == "Excluir")
            {
                var cellValue = dgvTurma.Rows[e.RowIndex].Cells["Numero"].Value;
                if (cellValue != null && int.TryParse(cellValue.ToString(), out int idTurma))
                {
                    ExcluirTurma(idTurma);
                    CarregarDgv(); 
                }
                else
                {
                    MessageBox.Show("Erro ao converter o valor da célula 'Numero' para int.", "Erro de Conversão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (dgvTurma.Columns[e.ColumnIndex].Name == "Visualizar")
            {
                var cellValue = dgvTurma.Rows[e.RowIndex].Cells["Numero"].Value;
                if (cellValue != null && int.TryParse(cellValue.ToString(), out int idTurma))
                {
                    Atividade(idTurma);
                }
                else
                {
                    MessageBox.Show("Erro ao converter o valor da célula 'Numero' para int.", "Erro de Conversão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Atividade(int idTurma)
        {

            FrmAtividade frmAtividade = new FrmAtividade(idTurma);
            frmAtividade.ShowDialog();
        }
        private void ExcluirTurma(int idTurma)
        {
            using (SqlConnection con = Conecta.Conexao())
            {
                string query = "DELETE FROM Turmas WHERE Id = @idTurma";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idTurma", idTurma);
                    if (con.State== ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Conecta.FecharConexao();
                }
            }
        }
    }
}
