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
        private readonly IContentSafetyRepository _contentSafety;

        public ProdutoService(IProdutoRepository repository, IContentSafetyRepository contentSafety)
        {
            _repository = repository;
            _contentSafety = contentSafety;
        }

        private async Task ValidarConteudoProdutoAsync(string nome, string descricao)
        {
            string textoParaValidar = $@"
                Nome do produto: {nome}
                Descrição do produto: {descricao}";

            var resultado = await _contentSafety.ValidarConteudo(textoParaValidar);

            if (!resultado.aprovado)
            {
                throw new DomainException(resultado.msg);
            }
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

            if (produtoDto.Tamanho == null)
            {
                throw new DomainException("Produto precisa de um tamanho.");
            }

            if (produtoDto.CategoriaID == 0)
            {
                throw new DomainException("Produto precisa de uma categoria.");
            }

            if (produtoDto.LocalizacaoID == 0)
            {
                throw new DomainException("Produto precisa de uma localização.");
            }
        }

        //adicionar
                                                //  Guid usuarioId,
        public async Task<LerProdutoDto> Adicionar(CriarProdutoDto produtoDto, Guid usuarioId, int categoriaId, int localizacaoId)
        {
            ValidarCadastro(produtoDto);

            await ValidarConteudoProdutoAsync(produtoDto.NomeProduto, produtoDto.Descricao);

            Produto produto = new Produto
            {
                NomeProduto = produtoDto.NomeProduto,
                Preco = produtoDto.Preco,
                Descricao = produtoDto.Descricao,
                Tamanho = produtoDto.Tamanho,
                Imagem = produtoDto.Imagem,
                StatusProduto = true,
                UsuarioID = usuarioId,
                CategoriaID = produtoDto.CategoriaID,
                LocalizacaoID = produtoDto.LocalizacaoID
            };

            _repository.Adicionar(produto);

            return ConverterProdutoParaDto.ConverterParaDto(produto);
        }

        public LerProdutoDto Atualizar(Guid id, AtualizarProdutoDto produtoDto)
        {
            Produto produtoBanco = _repository.ObterPorId(id);

            if (produtoBanco == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            if (produtoDto.NomeProduto == null)
            {
                throw new DomainException("Produto precisa de um nome.");
            }

            if (produtoDto.Tamanho == null)
            {
                throw new DomainException("Produto precisa de um tamanho.");
            }

            if (produtoDto.LocalizacaoID == 0)
            {
                throw new DomainException("Produto precisa de uma localização.");
            }

            if (produtoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior que zero.");
            }

            if (produtoDto.Descricao.Length < 0)
            {
                throw new DomainException("Produto precisa de uma descrição.");
            }

            produtoBanco.NomeProduto = produtoDto.NomeProduto;
            produtoBanco.Preco = produtoDto.Preco;
            produtoBanco.Descricao = produtoDto.Descricao;
            produtoBanco.CategoriaID = produtoDto.CategoriaID;

            if (produtoDto.Imagem != null && produtoDto.Imagem.Length > 0)
            {
                produtoBanco.Imagem = produtoDto.Imagem;
            }

            if (produtoDto.StatusProduto != null)
            {
                produtoBanco.StatusProduto = produtoDto.StatusProduto.Value;
            }

            _repository.Atualizar(produtoBanco);
            return ConverterProdutoParaDto.ConverterParaDto(produtoBanco);
        }

        public void Remover(Guid id)
        {
            Produto produto = _repository.ObterPorId(id);

            if (produto == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            _repository.Remover(id);
        }
    } 
}
