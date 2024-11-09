using Microsoft.AspNetCore.Http;

namespace AppLogin.Libraries.Sessao
{
    public class Sessao
    {
        IHttpContextAccessor _context;

        public Sessao(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void Cadastrar(string Key, string Valor)
        {
            _context.HttpContext.Session.SetString(Key, Valor);
        }

        public string Consultar(string Key)
        {
            return _context.HttpContext.Session.GetString(Key);
        }
        //Verificar se existe a sessão criada
        public bool Existe(string Key)
        {
            if (_context.HttpContext.Session.GetString(Key) == null)
            {
                return false;
            }

            return true;
        }
        //Remover sessão
        public void Remover(string Key)
        {
            _context.HttpContext.Session.Remove(Key);
        }

        public void RemoverTodos()
        {
            _context.HttpContext.Session.Clear();
        }

        public void atualizar(string Key, string Valor) 
        {
            if (Existe(Key)) 
            {
                _context.HttpContext.Session.Remove(Key);
            }

            _context.HttpContext.Session.SetString(Key, Valor);
        }


    }
}
