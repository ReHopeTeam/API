namespace ReHope.Applications.Conversoes
{
    public class ConverterImagemParaBytes
    {
        //não está sendo utilizada em lugar nenhum
        public static byte[] ConverterImagem(IFormFile imagem)
        {
            using var ms = new MemoryStream();
            imagem.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
