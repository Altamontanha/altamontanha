using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltaMontanha.Models.Dominio;

namespace AltaMontanha.Models.Persistencia.Abstracao
{
    public interface IServicoDAO : IDAO<Servico>
    {
        IList<Dominio.Servico> Pesquisar(Dominio.Servico servico, int qtde);
        IList<Dominio.Servico> Pesquisar(Dominio.Servico servico, int[] Codigos);
        IList<Dominio.Servico> Pesquisar(Dominio.Servico servico, int qtde, int pagina, int[] Codigos);
        IList<Dominio.Servico> Pesquisar(Dominio.Servico objeto, int qtde, int pagina, bool? aleatorio, int[] Codigos, int? Pagante = null);
    }
}