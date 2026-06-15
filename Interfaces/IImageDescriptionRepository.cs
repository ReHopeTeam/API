namespace ReHope.Interfaces
{
    public interface IImageDescriptionRepository
    {
        // retorno

        Task<string> CriarDescricao(IFormFile imagem);
    }
}
