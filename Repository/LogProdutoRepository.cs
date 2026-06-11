using Microsoft.EntityFrameworkCore;
using ReHope.Contexts;
using ReHope.Domains;
using ReHope.Interfaces;

namespace ReHope.Repository
{
    public class LogProdutoRepository : ILogProdutoRepository
    {
        private readonly ReHopeContext _context;
        
        public LogProdutoRepository(ReHopeContext context)
        {
            _context = context;
        }

        public List<LogProduto> Listar()
        {
            List<LogProduto> logProdutos = _context.LogProduto.OrderByDescending(logProduto => logProduto.DataAlteracao).ToList();

            return logProdutos;
        }
        public List<LogProduto> BuscarLogProdutoPorPodutoId(Guid produtoId)
        {
            return _context.LogProduto
                .Include(log => log.LogProdutoID)
                .Include(log => log.NomeAnterior)
                .Include(log => log.PrecoAnterior)
                .Include(log => log.StatusProduto)
                .Include(log => log.Codigo)
                .Include(log => log.LocalizacaoIDAnterior)
                .Include(log => log.UsuarioID)
                .Where(log => log.ProdutoID == produtoId)
                .OrderByDescending(log => log.DataAlteracao)
                .ToList();
        }
    }
}
