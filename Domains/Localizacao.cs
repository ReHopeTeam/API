using System;
using System.Collections.Generic;

namespace ReHope.Domains;

public partial class Localizacao
{
    public int LocalizacaoID { get; set; }

    public string NomeLocalizacao { get; set; } = null!;

    public virtual LogProduto? LogProduto { get; set; }

    public virtual ICollection<Produto> Produto { get; set; } = new List<Produto>();
}
