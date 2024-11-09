using AppLogin.Models;
using AppLogin.Models.Constants;
using AppLogin.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;

namespace AppLogin.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _conexaoMySql;
        public ClienteRepository(IConfiguration conf) // Método Construtor
        {
            _conexaoMySql = conf.GetConnectionString("ConexaoMySql");
        }

        public void Excluir(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        //public Cliente ObterCliente(int id)
        //{
        //    throw new NotImplementedException();
        //}


        //Logim Cliente
        public Cliente Login(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from cliente where Email = @Email and Senha = @Senha", conexao);

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    cliente.Id = Convert.ToInt32(dr["Id"]);
                    cliente.Nome = Convert.ToString(dr["Nome"]);
                    cliente.Nascimento = Convert.ToDateTime(dr["Nascimento"]);

                    cliente.Sexo = Convert.ToString(dr["Sexo"]);
                    cliente.CPF = Convert.ToString(dr["CPF"]);
                    cliente.Telefone = Convert.ToString(dr["Telefone"]);
                    cliente.Situacao = Convert.ToString(dr["Situacao"]);

                    cliente.Email = Convert.ToString(dr["Email"]);
                    cliente.Senha = Convert.ToString(dr["Senha"]);
                }
                return cliente;
            }
        }


        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> cliList = new List<Cliente>();
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM CLIENTE", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    cliList.Add(
                        new Cliente
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Nascimento = Convert.ToDateTime(dr["Nascimento"]),
                            Sexo = Convert.ToString(dr["Sexo"]),
                            CPF = Convert.ToString(dr["CPF"]),
                            Telefone = Convert.ToString(dr["Telefone"]),
                            Email = Convert.ToString(dr["Email"]),
                            Senha = Convert.ToString(dr["Senha"]),
                            Situacao = Convert.ToString(dr["Situacao"])
                        });
                }
                return cliList;
            }
        }

        public void Cadastrar(Cliente cliente)
        {
            string Situacao = SituacaoConstant.Ativo;

            try
            {
                using (var conexao = new MySqlConnection())
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into cliente(nome, nascimento, sexo, cpf, telefone, email, senha, situacao) values (@Nome , @Nascimento, @Sexo, @CPF, @Telefone, @Email, @Senha, @Situacao)", conexao);

                    cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = cliente.Nome;
                    cmd.Parameters.Add("@Nascimento", MySqlDbType.Datetime).Value = cliente.Nascimento.ToString("yyy/MM/dd");
                    cmd.Parameters.Add("@Sexo", MySqlDbType.VarChar).Value = cliente.Sexo;
                    cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                    cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cliente.Email;
                    cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = cliente.Senha;
                    cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = cliente.Situacao;
                    cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = cliente.CPF;

                    cmd.ExecuteNonQuery();
                    conexao.Close();
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("Erro no cadastro cliente" + ex.Message);
            }
        }


        public void Atualizar(Cliente cliente)
        {
            string Situacao = SituacaoConstant.Ativo;
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update Cliente set Nome=@Nome, Nascimento=@Nascimento, Sexo=@Sexo,  CPF=@CPF, " +
                    " Telefone=@Telefone, Email=@Email, Senha=@Senha, Situacao=@Situacao WHERE Id=@Id ", conexao);

                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = cliente.Id;
                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@Nascimento", MySqlDbType.DateTime).Value = cliente.Nascimento.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@Sexo", MySqlDbType.VarChar).Value = cliente.Sexo;
                cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = cliente.CPF;
                cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cliente.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = cliente.Senha;
                cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir (int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from Cliente WHERE Id=@Id", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Ativar(int Id)
        {
            string Situacao = SituacaoConstant.Ativo;
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update Cliente set Situacao=@Situacao WHERE Id=@Id ", conexao);

                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = Id;
                cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        // Desativar
        public void Desativar(int Id)
        {
            string Situacao = SituacaoConstant.Desativado;
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update Cliente set Situacao=@Situacao WHERE Id=@Id ", conexao);

                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = Id;
                cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Cliente ObterCliente(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Cliente WHERE Id=@Id ", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.Id = (Int32)(dr["Id"]);
                    cliente.Nome = (string)(dr["Nome"]);
                    cliente.Nascimento = (DateTime)(dr["Nascimento"]);
                    cliente.Sexo = (string)(dr["Sexo"]);
                    cliente.CPF = (string)(dr["CPF"]);
                    cliente.Telefone = (string)(dr["Telefone"]);
                    cliente.Email = (string)(dr["Email"]);
                    cliente.Senha = (string)(dr["Senha"]);
                    cliente.Situacao = (string)(dr["Situacao"]);

                }
                return cliente;
            }
        }
    }
}
