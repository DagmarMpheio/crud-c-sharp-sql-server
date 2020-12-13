using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;

namespace CRUDSQLServer
{
    public partial class frmCadastroCliente : Office2007Form /*Form*/
    {
        string connectionString = @"Server=.\sqlexpress;Database=CRUDCSharp;Trusted_Connection=True;";//conexao com base de dados
        bool novo;//variavel para saber se eh um novo registo ou actualizacao de um registo     

        public frmCadastroCliente()
        {
            InitializeComponent();
            carregarTabela();
            restaurarBDPadraoCasoNaoExista();
        }

        private void restaurarBDPadraoCasoNaoExista()
        {
            try
            {
                var bdExiste = verficaSeBDJaExiste();

                if (!bdExiste)
                {
                    restaurarBDPadrao();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tstId.Enabled = false;
            tsbBuscar.Enabled = false;
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCEP.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtUF.Enabled = false;
            mskTelefone.Enabled = false;

        }

        private void tsbNovo_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = true;
            tsbCancelar.Enabled = true;
            tsbExcluir.Enabled = true;
            tstId.Enabled = true;
            tsbBuscar.Enabled = true;
            txtNome.Enabled = true;
            txtEndereco.Enabled = true;
            mskCEP.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            txtUF.Enabled = true;
            mskTelefone.Enabled = true;
            txtNome.Focus();
            novo = true;

            txtId.Text = "";
            txtNome.Text = "";
            txtEndereco.Text = "";
            mskCEP.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtUF.Text = "";
            mskTelefone.Text = "";
        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            if (novo)
            {
                string sql = "INSERT INTO CLIENTE (NOME, ENDERECO, CEP, BAIRRO, CIDADE, UF, TELEFONE) "+
                    "VALUES (@Nome, @Endereco, @Cep, @Bairro, @Cidade, @Uf, @Telefone)";

                SqlConnection con = new SqlConnection(connectionString);//fazer a conexao
                SqlCommand cmd = new SqlCommand(sql, con);//executar o comando
                /*passar os parametros*/
                cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@Endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@Cep", mskCEP.Text);
                cmd.Parameters.AddWithValue("@Bairro", txtBairro.Text);
                cmd.Parameters.AddWithValue("@Cidade", txtCidade.Text);
                cmd.Parameters.AddWithValue("@Uf", txtUF.Text);
                cmd.Parameters.AddWithValue("@Telefone", mskTelefone.Text);
                cmd.CommandType = CommandType.Text;//tipo de comando texto
                con.Open();//iniciar(abrir) a conexao
                try
                {
                    int i = cmd.ExecuteNonQuery();//retorna o numero de linhas afectadas na tabela(0, nao inseriu. 1, inseriu)
                    if (i > 0) { 
                        MessageBox.Show("Cadastro realizado com sucesso!");//se tudo correr bem, mostrar a mensagem de sucesso e fechar a conexao com bd
                    carregarTabela();
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("Erro: "+ex.ToString());//se ocorrer exibir mensagem de erro
                }
                finally
                {
                    con.Close();//se tudo correr bem, mostrar a mensagem de sucesso e fechar a conexao com bd
                }
            }
            else
            {
                string sql = "UPDATE CLIENTE SET NOME=@Nome, ENDERECO=@Endereco, CEP=@Cep, "+
                    "BAIRRO=@Bairro, CIDADE=@Cidade, UF=@Uf, TELEFONE=@Telefone WHERE ID=@Id";

                SqlConnection con = new SqlConnection(connectionString);//fazer a conexao
                SqlCommand cmd = new SqlCommand(sql, con);//executar o comando
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@Endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@Cep", mskCEP.Text);
                cmd.Parameters.AddWithValue("@Bairro", txtBairro.Text);
                cmd.Parameters.AddWithValue("@Cidade", txtCidade.Text);
                cmd.Parameters.AddWithValue("@Uf", txtUF.Text);
                cmd.Parameters.AddWithValue("@Telefone", mskTelefone.Text);
                cmd.CommandType = CommandType.Text;//tipo de comando texto
                con.Open();//iniciar(abrir) a conexao
                try
                {
                    int i = cmd.ExecuteNonQuery();//retorna o numero de linhas afectadas na tabela(0, nao inseriu. 1, inseriu)
                    if (i > 0) { 
                        MessageBox.Show("Dados actualizados com suceso!");//se tudo correr bem, mostrar a mensagem de sucesso e fechar a conexao com bd
                        carregarTabela();
                    }
                }
                catch(Exception ex) {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }

            }

            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tstId.Enabled = true;
            tsbBuscar.Enabled = true;
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCEP.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtUF.Enabled = false;
            mskTelefone.Enabled = false;
            txtNome.Text = "";
            txtEndereco.Text = "";
            mskCEP.Text = "";
            txtBairro.Text = "";
            txtCidade.Text ="";
            txtUF.Text = "";
            mskTelefone.Text = "";
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tstId.Enabled = true;
            tsbBuscar.Enabled = true;
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCEP.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtUF.Enabled = false;
            mskTelefone.Enabled = false;
            txtId.Text = "";
            txtNome.Text = "";
            txtEndereco.Text = "";
            mskCEP.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtUF.Text = "";
            mskTelefone.Text = "";
        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM CLIENTE WHERE ID=@Id";

            SqlConnection con = new SqlConnection(connectionString);//fazer a conexao
            SqlCommand cmd = new SqlCommand(sql, con);//executar o comando
            cmd.Parameters.AddWithValue("@Id", txtId.Text);
            cmd.CommandType = CommandType.Text;//tipo de comando texto
            con.Open();//iniciar(abrir) a conexao

            try
            {
                int i = cmd.ExecuteNonQuery();//retorna o numero de linhas afectadas na tabela(0, nao inseriu. 1, inseriu)
                if (i > 0)
                    MessageBox.Show("Dados excluídos com suceso!");//se tudo correr bem, mostrar a mensagem de sucesso e fechar a conexao com bd

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }

            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tstId.Enabled = true;
            tsbBuscar.Enabled = true;
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCEP.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtUF.Enabled = false;
            mskTelefone.Enabled = false;
            txtNome.Text = "";
            txtEndereco.Text = "";
            mskCEP.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtUF.Text = "";
            mskTelefone.Text = "";
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM CLIENTE WHERE ID=@Id";

            SqlConnection con = new SqlConnection(connectionString);//fazer a conexao
            SqlCommand cmd = new SqlCommand(sql, con);//executar o comando
            cmd.Parameters.AddWithValue("@Id", tstId.Text);
            cmd.CommandType = CommandType.Text;//tipo de comando texto
            SqlDataReader reader;//armazena dados obtidos pela consulta na bd
            con.Open();//iniciar(abrir) a conexao

            try
            {
                reader = cmd.ExecuteReader();//trazer os dados em um array
                if (reader.Read())
                {
                    tsbNovo.Enabled = false;
                    tsbSalvar.Enabled = true;
                    tsbCancelar.Enabled = true;
                    tsbExcluir.Enabled = true;
                    tstId.Enabled = false;
                    tsbBuscar.Enabled = false;
                    txtNome.Enabled = true;
                    txtEndereco.Enabled = true;
                    mskCEP.Enabled = true;
                    txtBairro.Enabled = true;
                    txtCidade.Enabled = true;
                    txtUF.Enabled = true;
                    mskTelefone.Enabled = true;
                    txtNome.Focus();
                    txtId.Text = reader[0].ToString();
                    txtNome.Text = reader[1].ToString();
                    txtEndereco.Text = reader[2].ToString();
                    mskCEP.Text = reader[3].ToString();
                    txtBairro.Text = reader[4].ToString();
                    txtCidade.Text = reader[5].ToString();
                    txtUF.Text = reader[6].ToString();
                    mskTelefone.Text = reader[7].ToString();
                    novo = false;

                }
                else
                {
                    MessageBox.Show("Nenhum registo encontrado com o ID informado!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }

            tstId.Text = "";
        }

        public void carregarTabela()
        {
            string sql = "SELECT * FROM CLIENTE";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();

            //cria umobjecto Adapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            //cria um objecto DataTable 
            DataTable clientes = new DataTable();

            //prencher com dados
            adapter.Fill(clientes);

            //atribui o datable ao datagridview para exibir os dados
            dgvDados.DataSource = clientes;
        }

        private void dgvDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var Row = dgvDados.CurrentRow;//pegar a linha actual


            foreach (DataGridViewRow linha in dgvDados.Rows)
            {
                txtId.Text = Row.Cells[0].Value.ToString();
                txtNome.Text = Row.Cells[1].Value.ToString();
                txtEndereco.Text = Row.Cells[2].Value.ToString();
                mskCEP.Text = Row.Cells[3].Value.ToString();
                txtBairro.Text = Row.Cells[4].Value.ToString();
                txtCidade.Text = Row.Cells[5].Value.ToString();
                txtUF.Text = Row.Cells[6].Value.ToString();
                mskTelefone.Text = Row.Cells[7].Value.ToString();
            }

            tsbBuscar.Enabled = true;
            txtNome.Enabled = true;
            txtEndereco.Enabled = true;
            mskCEP.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            txtUF.Enabled = true;
            mskTelefone.Enabled = true;
            txtNome.Focus();





        }

        //verificar se a bd existe
        private bool verficaSeBDJaExiste()
        {
            bool retorno = false;

            try
            {
                using(var conn = new System.Data.SqlClient.SqlConnection(@"Server=.\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT 1 FROM SYS.DATABASES WHERE NAME LIKE 'CRUDCSharp'";
                        var valor = cmd.ExecuteScalar();

                        if (valor != null && valor != DBNull.Value && Convert.ToInt32(valor).Equals(1))
                        {
                            retorno = true;
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retorno;
        }

        //descobrir os directorios padrao
        private void descobrirDirectoriosPadrao(out string directorioDados, out string directorioLog, out string directorioBackup)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(@"Server=.\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
            {
                var serverConnection = new Microsoft.SqlServer.Management.Common.ServerConnection(connection);
                var server = new Microsoft.SqlServer.Management.Smo.Server(serverConnection);
                directorioDados = !string.IsNullOrWhiteSpace(server.Settings.DefaultFile) ? server.Settings.DefaultFile : (!string.IsNullOrWhiteSpace(server.DefaultFile) ? server.DefaultFile : server.MasterDBPath);
                directorioLog = !string.IsNullOrWhiteSpace(server.Settings.DefaultLog) ? server.Settings.DefaultLog : (!string.IsNullOrWhiteSpace(server.DefaultLog) ? server.DefaultLog:server.MasterDBLogPath);
                directorioBackup = !string.IsNullOrWhiteSpace(server.Settings.BackupDirectory) ? server.Settings.BackupDirectory : server.BackupDirectory;
            }
        }

        //restaurar a bd padrao
        private void restaurarBDPadrao()
        {
            try
            {
                string directorioDados, directorioLog, directorioBackup;
                descobrirDirectoriosPadrao(out directorioDados, out directorioLog, out directorioBackup);

                using (var conn = new System.Data.SqlClient.SqlConnection(@"Server=.\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        var caminhoCompletoBackup = System.IO.Path.Combine(directorioBackup, "CRUDCSharp.bak");
                        var caminhoCompletoDados = System.IO.Path.Combine(directorioDados, "CRUDCSharp.mdf");
                        var caminhoCompletoLog = System.IO.Path.Combine(directorioBackup, "CRUDCSharp_Log.ldf");
                        System.IO.File.Copy("CRUDCSharp.bak", caminhoCompletoBackup, true);
                        cmd.CommandText =
                            @"RESTORE DATABASE CRUDCSharp " +
                            @"FROM DISK = C'" + caminhoCompletoBackup + "'" +
                            @"WITH FILE = 1, " +
                            @"MOVE C'CRUDCSharp' TO C'" + caminhoCompletoDados + "' " +
                            @"MOVE C'CRUDCSharp_LOG' C'" + caminhoCompletoLog + "' " +
                            @"NOUNLOAD, REPLACE, STATS = 10";
                        cmd.ExecuteNonQuery();
                    }

                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
