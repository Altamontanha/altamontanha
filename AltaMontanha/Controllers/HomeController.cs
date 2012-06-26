using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Fachada;
using AltaMontanha.Models.Persistencia.Fabrica;
using AltaMontanha.Models.Persistencia.Abstracao;
using AltaMontanha.Models.Persistencia.Nhibernate;
using System.Collections.Specialized;

namespace AltaMontanha.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult PesquisarNoticia(int? pagina)
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            if (!pagina.HasValue)
                pagina = 1;
            else if (pagina < 1)
                pagina = 1;

            IList<Noticia> noticias = conteudoFacade.PesquisarNoticia(null, pagina.Value, Utilitario.Constante.TamanhoMaterias);

            ViewBag.TotalMaterias = conteudoFacade.PesquisarNoticia(null).Count;
            ViewBag.Pagina = pagina;

            RegistrarBannerInternas();

            return View(noticias);
        }

        public ActionResult VisualizarNoticia(int Codigo)
        {
            IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
            ServicoNHibernate servico = (ServicoNHibernate)fabrica.GetServicoDAO();
            ViewData["ServicoLateral"] = servico.Pesquisar(new Servico() { Ativo = true }, 1, 0, true, new int[0], 1)[0];

            CategoriaEmpresaNHibernate categoriaEmpresas = (CategoriaEmpresaNHibernate)fabrica.GetCategoriaEmpresaDAO();

            IList<CategoriaEmpresa> categorias = categoriaEmpresas.Pesquisar(null);
            ViewBag.Categorias = categorias;

            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            Noticia noticia = conteudoFacade.PesquisarNoticia(Codigo);
            this.RegistrarBannerInternas();

            if (noticia == null)
                return RedirectToAction("Index");

            return View(noticia);
        }

        public ActionResult PesquisarAventura(int? pagina)
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            if (!pagina.HasValue)
                pagina = 1;
            else if (pagina < 1)
                pagina = 1;

            IList<Aventura> aventuras = conteudoFacade.PesquisarAventura(null, Utilitario.Constante.TamanhoMaterias, pagina.Value);

            foreach (Aventura aventura in aventuras)
            {
                aventura.Autor = usuarioFacade.PesquisarUsuario(aventura.Autor.Codigo);
            }

            ViewBag.TotalMaterias = conteudoFacade.PesquisarAventura(null).Count;
            ViewBag.Pagina = pagina;

            RegistrarBannerInternas();

            return View(aventuras);
        }

        public ActionResult VisualizarAventura(int Codigo)
        {
            IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
            ServicoNHibernate servico = (ServicoNHibernate)fabrica.GetServicoDAO();
            ViewData["ServicoLateral"] = servico.Pesquisar(new Servico() { Ativo = true }, 1, 0, true, new int[0], 1)[0];

            CategoriaEmpresaNHibernate categoriaEmpresas = (CategoriaEmpresaNHibernate)fabrica.GetCategoriaEmpresaDAO();

            IList<CategoriaEmpresa> categorias = categoriaEmpresas.Pesquisar(null);
            ViewBag.Categorias = categorias;

            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            Aventura aventura = conteudoFacade.PesquisarAventura(Codigo);
            this.RegistrarBannerInternas();

            if (aventura == null)
                return RedirectToAction("Index");

            UsuarioFacade facade = new UsuarioFacade();
            aventura.Autor = facade.PesquisarUsuario(aventura.Autor.Codigo);

            return View(aventura);
        }


        public ActionResult PesquisarColuna(int? pagina)
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            UsuarioFacade facade = new UsuarioFacade();

            //ViewBag.Codigo = Codigo;

            if (!pagina.HasValue)
                pagina = 1;
            else if (pagina < 1)
                pagina = 1;

            ViewBag.CodigoColunista = 0;

            ViewBag.Usuarios = usuarioFacade.PesquisarColunista();

            IList<Coluna> colunas = conteudoFacade.PesquisarColuna(null, Utilitario.Constante.TamanhoMaterias, pagina.Value, false);

            ViewBag.TotalMaterias = conteudoFacade.PesquisarColuna(null).Count;
            ViewBag.Pagina = pagina;

            this.RegistrarBannerInternas();

            return View(colunas);
        }

        [HttpPost]
        public ActionResult PesquisarColuna(int? pagina, int? CodigoColunista)
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            UsuarioFacade facade = new UsuarioFacade();

            if (!pagina.HasValue)
                pagina = 1;
            else if (pagina < 1)
                pagina = 1;

            if (!CodigoColunista.HasValue)
                CodigoColunista = 0;

            ViewBag.Usuarios = usuarioFacade.PesquisarColunista();

            ViewBag.CodigoColunista = 0;

            int[] Codigos = new int[0];

            foreach (Usuario usuario in (IList<Usuario>)ViewBag.Usuarios)
            {
                if (usuario.Codigo == CodigoColunista)
                {
                    Codigos = new int[1];
                    Codigos[0] = usuario.Codigo;

                    ViewBag.CodigoColunista = usuario.Codigo;
                }
            }

            IList<Coluna> colunas = conteudoFacade.PesquisarColuna(null, Utilitario.Constante.TamanhoMaterias, pagina.Value, false, Codigos);

            ViewBag.TotalMaterias = conteudoFacade.PesquisarColuna(null, 0, 0, false, Codigos).Count;
            ViewBag.Pagina = pagina;

            this.RegistrarBannerInternas();

            return View(colunas);
        }

        public ActionResult PesquisarColunista()
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            Usuario usuario = new Usuario() { Perfil = new Perfil() { Codigo = 3 } };

            IList<Usuario> colunistas = usuarioFacade.PesquisarColunista();
            this.RegistrarBannerInternas();

            return View(colunistas);
        }

        public ActionResult VisualizarColuna(int Codigo)
        {
            IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
            ServicoNHibernate servico = (ServicoNHibernate)fabrica.GetServicoDAO();
            ViewData["ServicoLateral"] = servico.Pesquisar(new Servico() { Ativo = true }, 1, 0, true, new int[0], 1)[0];

            CategoriaEmpresaNHibernate categoriaEmpresas = (CategoriaEmpresaNHibernate)fabrica.GetCategoriaEmpresaDAO();

            IList<CategoriaEmpresa> categorias = categoriaEmpresas.Pesquisar(null);
            ViewBag.Categorias = categorias;

            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            Coluna coluna = conteudoFacade.PesquisarColuna(Codigo);
            this.RegistrarBannerInternas();

            if (coluna == null)
                return RedirectToAction("Index");

            UsuarioFacade facade = new UsuarioFacade();
            coluna.Autor = facade.PesquisarUsuario(coluna.Autor.Codigo);

            return View(coluna);
        }

        public ActionResult RedirecionarConteudo()
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            int codigo = Convert.ToInt32(Request.QueryString["NewsID"]);

            Conteudo conteudo = conteudoFacade.PesquisarConteudo(codigo);

            HttpContext.Response.StatusCode = 302;
            HttpContext.Response.Clear();

            return Redirect(string.Format("{0}/{1}/{2}", conteudo.GetType().ToString().Replace("AltaMontanha.Models.Dominio.", "").Replace("Coluna", "Colunas"), conteudo.Codigo, conteudo.Slug));
        }


        public ActionResult RedirecionarFoto(string Tamanho, string Nome)
        {
            switch (Tamanho)
            {
                case "max":
                    return Redirect("/ManterFoto/Foto/" + Nome + "?Tamanho=full");

                default:
                    return Redirect("/");
            }

        }

        public ActionResult PesquisarArtigo(int? pagina)
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            if (!pagina.HasValue)
                pagina = 1;
            else if (pagina < 1)
                pagina = 1;

            ViewBag.Categorias = conteudoFacade.PesquisarCategoria(null);

            Dictionary<int, bool> selecionados = new Dictionary<int, bool>();

            foreach (Categoria categoria in (IList<Categoria>)ViewBag.Categorias)
            {
                selecionados.Add(categoria.Codigo, false);
            }

            ViewBag.Selecionados = selecionados;

            IList<Artigo> artigos = conteudoFacade.PesquisarArtigo(null, Utilitario.Constante.TamanhoMaterias, pagina.Value, new int[0]);

            ViewBag.TotalMaterias = conteudoFacade.PesquisarArtigo(null).Count;
            ViewBag.Pagina = pagina;

            RegistrarBannerInternas();

            return View(artigos);
        }

        [HttpPost]
        public ActionResult PesquisarArtigo(int? pagina, FormCollection collection)
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            if (!pagina.HasValue)
                pagina = 1;
            else if (pagina < 1)
                pagina = 1;

            ViewBag.Categorias = conteudoFacade.PesquisarCategoria(null);

            Dictionary<int, bool> selecionados = new Dictionary<int, bool>();

            int[] Codigos = new int[collection.Count];
            int i = 0;

            foreach (Categoria categoria in (IList<Categoria>)ViewBag.Categorias)
            {
                if (collection[categoria.Titulo] != null)
                {
                    selecionados.Add(categoria.Codigo, true);
                    Codigos[i] = categoria.Codigo;
                    i++;
                }
                else
                {
                    selecionados.Add(categoria.Codigo, false);
                }
            }

            ViewBag.Selecionados = selecionados;

            IList<Artigo> artigos = conteudoFacade.PesquisarArtigo(null, Utilitario.Constante.TamanhoMaterias, pagina.Value, Codigos);

            IList<Artigo> temp = conteudoFacade.PesquisarArtigo(null, 1, 0, Codigos);

            ViewBag.TotalMaterias = temp.Count;
            ViewBag.Pagina = pagina;

            RegistrarBannerInternas();

            return View(artigos);
        }

        public ActionResult VisualizarArtigo(int Codigo)
        {
            IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();
            ServicoNHibernate servico = (ServicoNHibernate)fabrica.GetServicoDAO();
            ViewData["ServicoLateral"] = servico.Pesquisar(new Servico() { Ativo = true }, 1, 0, true, new int[0], 1)[0];

            CategoriaEmpresaNHibernate categoriaEmpresas = (CategoriaEmpresaNHibernate)fabrica.GetCategoriaEmpresaDAO();

            IList<CategoriaEmpresa> categorias = categoriaEmpresas.Pesquisar(null);
            ViewBag.Categorias = categorias;
            
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            Artigo artigo = conteudoFacade.PesquisarArtigo(Codigo);
            this.RegistrarBannerInternas();

            if (artigo == null)
                return RedirectToAction("Index");

            ConteudoFacade facade = new ConteudoFacade();
            artigo.ObjCategoria = facade.PesquisarCategoria(artigo.ObjCategoria.Codigo);

            return View(artigo);
        }

        public ActionResult VisualizarBusca()
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            RegistrarBannerInternas();

            return View(ViewData);
        }
        //
        // GET: /Home/
        public ActionResult Index()
        {
            ConteudoFacade conteudoFacade = new ConteudoFacade();
            MultimidiaFacade multimidiaFacade = new MultimidiaFacade();
            UsuarioFacade usuarioFacade = new UsuarioFacade();

            IFactoryDAO fabrica = FactoryFactoryDAO.GetFabrica();

            ArtigoNHibernate artigoH = (ArtigoNHibernate)fabrica.GetArtigoDAO();

            ViewData["BannerSuperior"] = multimidiaFacade.PesquisarBannerPorLocal(5);
            ViewData["BannerInferior"] = multimidiaFacade.PesquisarBannerPorLocal(3);

            ViewData["BannerCapaSuperior"] = multimidiaFacade.PesquisarBannerPorLocal(1);
            ViewData["BannerCapaMeio"] = multimidiaFacade.PesquisarBannerPorLocal(2);
            ViewData["BannerMeio"] = multimidiaFacade.PesquisarBannerPorLocal(6);

            ServicoNHibernate servico = (ServicoNHibernate)fabrica.GetServicoDAO();

            ViewData["ServicoLateral"] = servico.Pesquisar(new Servico() { Ativo = true }, 1, 0, true, new int[0], 1)[0];

            CategoriaEmpresaNHibernate categoriaEmpresas = (CategoriaEmpresaNHibernate)fabrica.GetCategoriaEmpresaDAO();

            IList<CategoriaEmpresa> categorias = categoriaEmpresas.Pesquisar(null);
            ViewBag.Categorias = categorias;

            //ViewData["ListaArtigos"] = conteudoFacade.PesquisarArtigoArtigoTecnico(null);
            // Artigos tecnicos: pesquisar artigos aleatorios que nao sejam na categoria 3 (saude)
            IList<Artigo> artigos = artigoH.PesquisarAleatorio(4, null, null, new int[] { 3 });
            ViewData["ListaArtigos"] = artigos;

            int[] Codigos = new int[artigos.Count];

            for (int i = 0; i < artigos.Count; i++)
            {
                Codigos[i] = artigos[i].Codigo;
            }

            ViewData["ListaNoticiasDestaque"] = conteudoFacade.PesquisarNoticia(new Noticia() { Destaque = true, AnteTitulo = "UsarDestaque" }, 7);
            ViewData["ListaNoticias"] = conteudoFacade.PesquisarNoticia(new Noticia() { Destaque = false, AnteTitulo = "UsarDestaque" }, (short)4);

            IList<Coluna> colunas = conteudoFacade.PesquisarColuna(null, 6, true);

            foreach (Coluna item in colunas)
            {
                item.Autor = usuarioFacade.PesquisarUsuario(item.Autor.Codigo);
            }

            ViewData["ListaColunas"] = colunas;


            ViewData["ListaAventuras"] = conteudoFacade.PesquisarAventura(null, 5);


            //ViewData["ListaArtigosSaude"] = conteudoFacade.PesquisarArtigoSaude(null);
            ViewData["ListaArtigosSaude"] = artigoH.PesquisarAleatorio(3, new int[] { 3 }, Codigos, null);

            try
            {
                //ViewData["ListaArtigoEquipamentos"] = conteudoFacade.PesquisarArtigoCategoria(null, new Categoria() { Codigo = 5 }, 1)[0];

                ViewData["ListaArtigoEquipamentos"] = artigoH.PesquisarAleatorio(1, new int[] { 5 }, Codigos, null)[0];
            }
            catch
            {
                ViewData["ListaArtigoEquipamentos"] = new Artigo() { Texto = "", Titulo = "", AnteTitulo = "", FotoCapa = new Foto() };
            }

            try
            {
                //ViewData["ListaArtigoMeioAmbiente"] = conteudoFacade.PesquisarArtigoCategoria(null, new Categoria() { Codigo = 13 }, 1)[0];
                ViewData["ListaArtigoMeioAmbiente"] = artigoH.PesquisarAleatorio(1, new int[] { 13 }, Codigos, null)[0];
            }
            catch
            {
                ViewData["ListaArtigoMeioAmbiente"] = new Artigo() { Texto = "", Titulo = "", AnteTitulo = "", FotoCapa = new Foto() };
            }

            try
            {
                //ViewData["ListaArtigoHistoria"] = conteudoFacade.PesquisarArtigoCategoria(null, new Categoria() { Codigo = 12 }, 1)[0];
                ViewData["ListaArtigoHistoria"] = artigoH.PesquisarAleatorio(1, new int[] { 12 }, Codigos, null)[0];
            }
            catch
            {
                ViewData["ListaArtigoHistoria"] = new Artigo() { Texto = "", Titulo = "", AnteTitulo = "", FotoCapa = new Foto() };
            }

            try
            {
                //ViewData["ListaArtigoEntrevistas"] = conteudoFacade.PesquisarArtigoCategoria(null, new Categoria() { Codigo = 10 }, 1)[0];
                ViewData["ListaArtigoEntrevistas"] = artigoH.PesquisarAleatorio(1, new int[] { 10 }, Codigos, null)[0];
            }
            catch
            {
                ViewData["ListaArtigoEntrevistas"] = new Artigo() { Texto = "", Titulo = "", AnteTitulo = "", FotoCapa = new Foto() };
            }

            return View(ViewData);
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
