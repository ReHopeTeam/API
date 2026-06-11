using ReHope.Domains;

namespace ReHope.Interfaces
{
    public interface IUsuarioRepository
    {

        List<Usuario> Listar();

        Usuario? ObterPorId(Guid id);

        Usuario? ObterPorEmail(string email);

        bool EmailExiste(string email);

        void Adicionar (Usuario usuario);

        void Atualizar (Usuario usuario);

        void Remover (Guid  id);
    }
}
