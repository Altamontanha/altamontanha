using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Fachada;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Persistencia.Nhibernate;

namespace AltaMontanha.Controllers
{
    [HandleError]
    public class ManterServicoController : Utilitario.BaseController
    {
        //
        // GET: /ManterServico/

        [Authorize]
        public ActionResult Index()
        {
            ServicoNHibernate servicoH = new ServicoNHibernate();

            IList<Servico> servicos = servicoH.Pesquisar(null, 0, 0, false, new int[0]);
            return View(servicos);
        }

        [Authorize]
        public ActionResult AlterarServico(int Codigo)
        {

            NHibernate.HttpModule.RecuperarSessao.Flush();
            ConteudoFacade facade = new ConteudoFacade();

            CategoriaEmpresaNHibernate catEmpresa = new CategoriaEmpresaNHibernate();

            IList<CategoriaEmpresa> listCatEmpresa = catEmpresa.Pesquisar(new CategoriaEmpresa());

            ViewBag.Categorias = new SelectList(listCatEmpresa, "CodCategoria", "Categoria");

            Servico servico = facade.PesquisarServico(Codigo);
            return View(servico);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AlterarServico(Servico servico, HttpPostedFileBase file)
        {
            ConteudoFacade facade = new ConteudoFacade();
            MultimidiaFacade facadeMult = new MultimidiaFacade();
            try
            {
                servico.Ativo = (servico.Ativo != null);
                // Se a pessoa nao selecionou uma foto, nao deve apagar o que ja estava cadastrado
                if (file == null)
                {
                    servico.Logomarca = facade.PesquisarServico(servico.Codigo).Logomarca;
                }
                else
                {
                    servico.Logomarca = file.FileName;
                    facadeMult.SalvarFotoServico(file);
                }

                facade.SalvarServico(servico);
                return RedirectToAction("Index");
            }
            catch
            {
                CategoriaEmpresaNHibernate catEmpresa = new CategoriaEmpresaNHibernate();

                IList<CategoriaEmpresa> listCatEmpresa = catEmpresa.Pesquisar(new CategoriaEmpresa());

                ViewBag.Categorias = new SelectList(listCatEmpresa, "CodCategoria", "Categoria");

                return View(servico);
            }
        }

        [Authorize]
        public ActionResult CadastrarServico()
        {

            NHibernate.HttpModule.RecuperarSessao.Flush();
            CategoriaEmpresaNHibernate catEmpresa = new CategoriaEmpresaNHibernate();

            IList<CategoriaEmpresa> listCatEmpresa = catEmpresa.Pesquisar(new CategoriaEmpresa());

            ViewBag.Categorias = new SelectList(listCatEmpresa, "CodCategoria", "Categoria");

            return View(new Servico() { Categoria = new CategoriaEmpresa(), Ativo = false });
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CadastrarServico(Servico servico, HttpPostedFileBase file)
        {
            ConteudoFacade facade = new ConteudoFacade();

            Models.Fachada.MultimidiaFacade facadeMult = new Models.Fachada.MultimidiaFacade();

            try
            {
                servico.Ativo = (servico.Ativo != null);

                servico.Logomarca = file.FileName;

                facadeMult.SalvarFotoServico(file);

                facade.SalvarServico(servico);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(servico);
            }
        }

        [Authorize]
        public ActionResult ExcluirServico(int Codigo)
        {
            Models.Fachada.ConteudoFacade facade = new Models.Fachada.ConteudoFacade();
            facade.ExcluirServico(Codigo);
            return RedirectToAction("Index");
        }

    }
}
