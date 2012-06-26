using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Fachada;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Persistencia.Fabrica;
using AltaMontanha.Models.Persistencia.Nhibernate;

namespace AltaMontanha.Controllers
{
    public class ServicosController : Controller
    {
        public ActionResult Index(int? pagina)
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            if (!pagina.HasValue)
                pagina = 1;
            else if (pagina < 1)
                pagina = 1;

            ViewBag.Categorias = conteudoFacade.PesquisarCategoriaEmpresa(null);

            Dictionary<int, bool> selecionados = new Dictionary<int, bool>();

            foreach (CategoriaEmpresa categoriaEmpresa in (IList<CategoriaEmpresa>)ViewBag.Categorias)
            {
                selecionados.Add(categoriaEmpresa.CodCategoria, false);
            }

            ViewBag.Selecionados = selecionados;

            if (Session["Pagina"] != null)
            {
                int sessionPagina = (int)Session["Pagina"];

                if (sessionPagina == pagina.Value)
                {
                    Session["Servicos"] = null;
                    Session["Servicos"] = conteudoFacade.PesquisarServico(new Servico() { Ativo = true }, 0, 0, new int[0]);
                }
            }
            else
            {
                Session["Servicos"] = null;
                Session["Servicos"] = conteudoFacade.PesquisarServico(new Servico() { Ativo = true }, 0, 0, new int[0]);
            }

            Session["Pagina"] = pagina.Value;

            IList<Servico> servicos = new List<Servico>();

            for (int cont = (pagina.Value - 1) * Utilitario.Constante.TamanhoMaterias; cont < pagina.Value * Utilitario.Constante.TamanhoMaterias; cont++)
            {
                servicos.Add(((IList<Servico>)Session["Servicos"])[cont]);
            }

            ViewBag.TotalServicos = conteudoFacade.PesquisarServico(new Servico() { Ativo = true }).Count;
            ViewBag.Pagina = pagina;

            RegistrarBannerInternas();

            return View(servicos);
        }

        [HttpPost]
        public ActionResult Index(int? pagina, FormCollection collection)
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            if (!pagina.HasValue)
                pagina = 1;
            else if (pagina < 1)
                pagina = 1;

            ViewBag.Categorias = conteudoFacade.PesquisarCategoriaEmpresa(null);

            Dictionary<int, bool> selecionados = new Dictionary<int, bool>();

            int[] Codigos = new int[collection.Count];
            int i = 0;

            foreach (CategoriaEmpresa categoriaEmpresa in (IList<CategoriaEmpresa>)ViewBag.Categorias)
            {
                if (collection == null) { }
                else if (collection[categoriaEmpresa.Categoria] != null)
                {
                    selecionados.Add(categoriaEmpresa.CodCategoria, true);
                    Codigos[i] = categoriaEmpresa.CodCategoria;
                    i++;
                }
                else
                {
                    selecionados.Add(categoriaEmpresa.CodCategoria, false);
                }
            }

            ViewBag.Selecionados = selecionados;


            IList<Servico> sessionServicos = new List<Servico>();

            if (Session["Codigos"] != null)
            {
                int[] sessionCods = (int[])Session["Codigos"];

                bool diferente = false;

                if (sessionCods.Length != Codigos.Count())
                {
                    diferente = true;
                }
                else
                {
                    for (int c = 0; c < Codigos.Length; c++)
                    {
                        if (sessionCods[c].CompareTo(Codigos[c]) != 0)
                        {
                            diferente = true;
                        }
                    }
                }

                int sessionPagina = 0;
                try
                {
                    int.TryParse(Session["Pagina"].ToString(), out sessionPagina);
                }
                catch
                {
                    sessionPagina = 0;
                }

                if (diferente)
                {
                    Session["Codigos"] = Codigos;
                    Session["Servicos"] = null;
                    sessionServicos = conteudoFacade.PesquisarServico(new Servico() { Ativo = true }, 0, 0, Codigos);
                }
                else if (pagina.Value == sessionPagina)
                {
                    Session["Servicos"] = null;
                    sessionServicos = conteudoFacade.PesquisarServico(new Servico() { Ativo = true }, 0, 0, Codigos);
                }
                else
                {
                    sessionServicos = (IList<Servico>)Session["Servicos"];
                }
            }
            else
            {
                Session["Codigos"] = Codigos;
                Session["Servicos"] = null;
                sessionServicos = conteudoFacade.PesquisarServico(new Servico() { Ativo = true }, 0, 0, Codigos);
            }

            IList<Servico> servicos = new List<Servico>();

            for (int cont = (pagina.Value - 1) * Utilitario.Constante.TamanhoMaterias; cont < pagina.Value * Utilitario.Constante.TamanhoMaterias; cont++)
            {
                if (cont < sessionServicos.Count)
                    servicos.Add(sessionServicos[cont]);
            }

            Session["Servicos"] = sessionServicos;

            ViewBag.TotalServicos = conteudoFacade.PesquisarServico(new Servico() { Ativo = true }, 1, -1, Codigos).Count;
            ViewBag.Pagina = pagina;

            Session["Pagina"] = pagina.Value;

            RegistrarBannerInternas();

            return View(servicos);
        }

        public ActionResult Servico(int Codigo)
        {
            IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
            CategoriaEmpresaNHibernate categoriasH = (CategoriaEmpresaNHibernate)fabrica.GetCategoriaEmpresaDAO();

            ViewBag.Categorias = categoriasH.Pesquisar(null);

            ConteudoFacade conteudoFacade = new ConteudoFacade();

            Servico servico = conteudoFacade.PesquisarServico(Codigo);

            RegistrarBannerInternas();

            return View(servico);
        }

        private void RegistrarBannerInternas()
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            ViewData["BannerSuperior"] = multimidiaFacade.PesquisarBannerPorLocal(8);
            ViewData["BannerInferior"] = multimidiaFacade.PesquisarBannerPorLocal(9);

            ViewData["BannerInternaSuperior"] = multimidiaFacade.PesquisarBannerPorLocal(4);
            ViewData["BannerInternaInferior"] = multimidiaFacade.PesquisarBannerPorLocal(7);
        }

    }
}
