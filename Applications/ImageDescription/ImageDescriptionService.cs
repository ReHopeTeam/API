using Google.GenAI;
using ReHope.Interfaces;

namespace ReHope.Applications.ImageDescription
{
    public class ImageDescriptionService : IImageDescriptionRepository
    {
        private readonly string _apiKey;

        public ImageDescriptionService(IConfiguration configuration)
        {
            _apiKey = configuration["Gemini:ApiKey"] ??
                Environment.GetEnvironmentVariable("GEMINI_API_KEY") ??
                throw new Exception("API Key não configurada.");
        }

        public async Task<string> CriarDescricao(IFormFile imagem)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return (false, "API Key não configurada");
            }

            try
            {
                Client client = new Client(_apiKey: _apiKey);

                string prompt = $@"Você é responsável pelo catálogo de um bazar.

                Sua função é identificar produtos a partir de fotografias e criar descrições para anúncios.

                Regras obrigatórias:

                1. Nunca invente informações que não estejam claramente visíveis.
                2. Se alguma característica não puder ser determinada, omita-a.
                3. Não informe marca, tamanho, material ou estado do produto sem evidência visual.
                4. Utilize linguagem amigável e adequada para e-commerce.
                5. Priorize características que ajudem o comprador a identificar o produto.
                6. Produza textos curtos e objetivos.

                Retorne exclusivamente um JSON no formato:

                {{
                ""descricao"": """",
                }}
                :{texto}";

                var response = await client.Models.GenerateContentAsync(
                    model: "gemini-2.5-flash-lite",
                    contents: prompt
                    );

                string result = response.Text?.Trim().ToUpper() ?? "";

                if (result.StartsWith("INSEGURO"))
                {
                    return (false, result);
                }

                return (true, "Textos seguros!");
            }
            catch (Exception ex)
            {
                return (false, "Erro na IA" + ex.Message);
            }
        }
    }
}
