namespace ReHope.Interfaces
{
    public class IContentSafetyRepository
    {
        Task<(bool aprovado, string msg)> ValidarConteudo(string texto);
    }
}
