using ReHope.Exceptions;

namespace ReHope.Applications.Regras
{
    public class Validacao
    {
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }
        }

        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException("E-mail é obrigatório.");
            }
        }
    }
}
