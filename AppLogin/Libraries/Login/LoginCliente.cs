namespace AppLogin.Libraries.Login
{
    public class LoginCliente
    {
        private string Key = "Login.Cliente";

        private Sessao.Sessao _sessao;

        public LoginCliente(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }
    }
}
