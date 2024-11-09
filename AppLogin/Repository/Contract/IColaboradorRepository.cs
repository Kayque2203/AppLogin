using AppLogin.Models;

namespace AppLogin.Repository.Contract
{
    public interface IColaboradorRepository
    {
        Colaborador Login(string Email, string Senha);

        void Cadastrar (Colaborador colaborador);

        void Atualizar(Colaborador colaborador);

        void AtualizarSenha(Colaborador colaborador);

        void Excluir(Colaborador colaborador);

        Colaborador ObterColaborador(int Id);

        List<Colaborador> ObterColaboradorPeloEmail(string email);

        IEnumerable<Colaborador> ObterTodosColaboradores();
    }
}
