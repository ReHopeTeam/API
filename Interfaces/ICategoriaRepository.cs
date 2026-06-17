using ReHope.Domains;

namespace ReHope.Interfaces
{
    public interface ICategoriaRepository
    {
        List<Categoria> Listar();

        Categoria? ObterPorId(int id);

        Categoria? ObterCategoriaPorTipo(string nomeTipo);

        bool NomeCategoriaExiste(string nome, int? categoriaIdAtual = null);

        void Adicionar (Categoria categoria);
        void Atualizar (Categoria categoria);
        void Remover (int id);
    }
}
