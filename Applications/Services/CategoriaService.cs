using ReHope.Domains;
using ReHope.DTOs.CategoriaDto;
using ReHope.Interfaces;
using ReHope.Repository;

namespace ReHope.Applications.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public List<LerCategoriaDto> Listar()
        {
            List<Categoria> categorias = _repository.Listar();

            List<LerCategoriaDto> categoriaDto = categorias.Select(categoria => new LerCategoriaDto
            {
                CategoriaID = categoria.CategoriaID,
                NomeCategoria = categoria.NomeCategoria,
                TipoProdutoID = categoria.TipoProdutoID,
                NomeTipo = categoria.TipoProduto.NomeTipo
            }).ToList();

            return categoriaDto;
        }
    }
}
