using ReHope.Applications.Conversoes;
using ReHope.Domains;
using ReHope.DTOs.ProdutoDto;
using ReHope.Exceptions;
using ReHope.Interfaces;

namespace ReHope.Applications.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public List<LerProdutoDto> Listar()
        {
            List<Produto> produtos = _repository.Listar();

            List<LerProdutoDto> produtoDto = produtos.Select(ConverterProdutoParaDto.ConverterParaDto).ToList();

            return produtoDto;
        }

        public LerProdutoDto ObterPorId(Guid id)
        {
            Produto produto = _repository.ObterPorId(id);

            if (produto == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            return ConverterProdutoParaDto.ConverterParaDto(produto);
        }

        public LerProdutoDto ObterPorCodigo(int codigo)
        {
            Produto produto = _repository.ObterPorCodigo(codigo);

            if (produto == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            return ConverterProdutoParaDto.ConverterParaDto(produto);
        }

        private static void ValidarCadastro(CriarProdutoDto produtoDto)
        {
            if (string.IsNullOrWhiteSpace(produtoDto.NomeProduto))
            {
                throw new DomainException("Nome é obrigatório.");
            }

            if (produtoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior que zero.");
            }

            if (string.IsNullOrWhiteSpace(produtoDto.Descricao))
            {
                throw new DomainException("Descrição é obrigatória.");
            }

            if (produtoDto.CategoriaID == null)
            {
                throw new DomainException("Produto precisa de uma categoria.");
            }

            if (produtoDto.LocalizacaoID == null)
            {
                throw new DomainException("Produto precisa de uma localização.");
            }
        }

        //adicionar
    }
}
