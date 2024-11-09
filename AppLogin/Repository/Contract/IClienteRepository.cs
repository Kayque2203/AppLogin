using AppLogin.Models;

namespace AppLogin.Repository.Contract
{
    public interface IClienteRepository
    {
        void Cadastrar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Excluir(Cliente cliente);
        Cliente ObterCliente(int id);
        IEnumerable<Cliente> ObterTodosClientes();
    }
}
