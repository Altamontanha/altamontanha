using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;
using AltaMontanha.Models.Dominio;
using AltaMontanha.Models.Persistencia.Fabrica;
using AltaMontanha.Models.Persistencia.Abstracao;
using AltaMontanha.Models.Persistencia.MySQL;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace AltaMontanha.Controllers
{
    [HandleError]
    public class ManterFotoController : Utilitario.BaseController
    {

        //
        // GET: /ManterFoto/
        [Authorize]
        public ActionResult Index()
        {
            Models.Fachada.MultimidiaFacade facade = new Models.Fachada.MultimidiaFacade();
            Foto foto = new Foto();
            foto.Galeria = true;
            int pagina = RecuperarParametroInteiro("pagina");

            if (pagina <= 0)
                pagina = 1;

            IList<Foto> fotos = facade.PesquisarFoto(foto, Utilitario.Constante.TamanhoPagina, pagina);
            // TODO : refactoring.
            ViewData["Total"] = facade.PesquisarFoto(foto, 0, 0).Count;
            ViewData["pagina"] = pagina;

            return View(fotos);
        }
        /// <summary>
        /// Responsável pelo carregamento da tela de cadastro de foto.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult CadastrarFoto(string Id)
        {
            return View();
        }
        /// <summary>
        /// Responsável pelo cadastro da foto
        /// </summary>
        /// <param name="foto"></param>
        /// <param name="arquivo"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        //[ValidateInput(false)]
        public ActionResult CadastrarFoto(Models.Dominio.Foto foto, HttpPostedFileBase file)
        {
            Models.Fachada.MultimidiaFacade facade = new Models.Fachada.MultimidiaFacade();
            if (file == null)
                return View(foto);
            else
            {
                facade.SalvarFotoGaleria(foto, file);
                return RedirectToAction("Index", "ManterFoto");
            }
        }
        //
        // GET: /ManterFoto/AlterarFoto/5
        [Authorize]
        public ActionResult AlterarFoto(int Codigo)
        {
            Models.Fachada.MultimidiaFacade facade = new Models.Fachada.MultimidiaFacade();
            ViewBag.InicioCaminho = "/ManterFoto/Foto/";

            Foto foto = facade.PesquisarFoto(Codigo);
            return View(foto);
        }
        //
        // POST: /ManterFoto/AlterarFoto/5
        [HttpPost]
        [Authorize]
        public ActionResult AlterarFoto(Foto foto, HttpPostedFileBase file)
        {
            Models.Fachada.MultimidiaFacade facade = new Models.Fachada.MultimidiaFacade();
            facade.SalvarFotoGaleria(foto, file);
            return RedirectToAction("Index");
        }
        //
        // GET: /ManterFoto/ExluirFoto/5
        [Authorize]
        public ActionResult ExcluirFoto(int Codigo)
        {
            Models.Fachada.MultimidiaFacade facade = new Models.Fachada.MultimidiaFacade();
            facade.ExcluirFoto(Codigo);
            return RedirectToAction("Index");
        }
        //
        // GET: /VincularFoto/
        [Authorize]
        public ActionResult VincularFoto(int Id)
        {
            ViewBag.Id = Id;

            return View();
        }
        //
        // POST: /ManterFoto/VincularFoto/
        [HttpPost]
        [Authorize]
        public ActionResult VincularFoto(FormCollection collection)
        {
            Models.Persistencia.Fabrica.IFactoryDAO fabrica = Models.Persistencia.Fabrica.FactoryFactoryDAO.GetFabrica();
            Models.Persistencia.Abstracao.IFotoDAO fotoDAO = fabrica.GetFotoDAO(); ;
            Foto foto = new Foto();

            foto.Legenda = collection["txtLegenda"];
            foto.Galeria = true;

            ViewData["Fotos"] = fotoDAO.Pesquisar(foto);

            ViewBag.Id = int.Parse(collection["CodigoConteudo"]);

            return View();
        }

        [Authorize]
        public string CadastrarFotoConteudo(Foto foto, HttpPostedFileBase file, int CodigoConteudo)
        {
            Models.Fachada.MultimidiaFacade facade = new Models.Fachada.MultimidiaFacade();
            facade.SalvarFotoGaleria(foto, file);

            return "Foto inserida com sucesso!! Clique em voltar e pesquise a foto para adicioná-la.<br><br><a href=\"javascript:history.back()\">Voltar</a> ";

            /*
            StringBuilder texto = new StringBuilder();

            texto.Append("<script type='text/javascript'> ");
            texto.Append("j(document).ready(function () {");
            texto.AppendLine("    var codigo  = " + foto.Codigo + @"; ");
            texto.AppendLine("    var legenda = j('input[name=Legenda_' + codigo + ']').val(); ");
            texto.AppendLine("    var caminho = j('input[name=Caminho_' + codigo + ']').val(); ");
            texto.AppendLine("    var autor   = j('input[name=Autor_'   + codigo + ']').val(); ");
            texto.AppendLine("    var fonte   = j('input[name=Fonte_'   + codigo + ']').val(); ");
            texto.AppendLine("    var galeria = j('input[name=Galeria_' + codigo + ']').val(); ");

            texto.AppendLine("    alert('oi'); ");

            texto.AppendLine("    window.parent.j('#listaFotos').append(");
            texto.AppendLine("        '<li id=\"fotoID_' + codigo + '\">' +  ");
            texto.AppendLine("        '  <input type=\"radio\" name=\"FotoCapa.Codigo\" value=\"' + codigo + '\" />' +  ");
            texto.AppendLine("        '  <a href=\"#fotoID_' + codigo + '\">X</a>' +  ");
            //texto.AppendLine("        '  <img alt=\"' + legenda + '\" src=\"" + Server.MapPath("~/ManterFoto/Foto/") + "' + caminho + '?Tamanho=145\"' title=' + legenda + ' />' + ");
            texto.AppendLine("        '  <input type=\"hidden\" name=\"ListaFotos.Index\" value = \"' + codigo + '\" />' + ");
            texto.AppendLine("        '  <input type=\"hidden\" name=\"ListaFotos[' + codigo + '].Codigo\" value=\"' + codigo + '\" />' + ");
            texto.AppendLine("        '  <input type=\"hidden\" name=\"ListaFotos[' + codigo + '].Legenda\" value=\"' + legenda + '\" />' + ");
            texto.AppendLine("        '  <input type=\"hidden\" name=\"ListaFotos[' + codigo + '].Caminho\" value=\"' + caminho + '\" />' + ");
            texto.AppendLine("        '  <input type=\"hidden\" name=\"ListaFotos[' + codigo + '].Autor\" value=\"' + autor + '\" />' + ");
            texto.AppendLine("        '  <input type=\"hidden\" name=\"ListaFotos[' + codigo + '].Fonte\" value=\"' + fonte + '\" />' + ");
            texto.AppendLine("        '  <input type=\"hidden\" name=\"ListaFotos[' + codigo + '].Galeria\" value=\"' + galeria + '\" />' + ");
            texto.AppendLine("        '</li>' ");
            texto.AppendLine("    ); ");
            texto.AppendLine("    window.parent.adicionarAcaoRemover(); ");

            texto.AppendLine("    alert('A foto foi inserida com sucesso'); ");
            texto.AppendLine("    }; ");
            texto.AppendLine("</script>");

            return texto.ToString();
            */



            //ConteudoMySQL conteudo = new ConteudoMySQL();
            //conteudo.VincularFotoConteudo(CodigoConteudo, foto.Codigo);

            //ViewBag.Mensagem = "Foto cadastrada com sucesso! Atualize a página para ela aparecer na listagem de fotos!";

            ////return RedirectToAction(Request.UrlReferrer.AbsolutePath.Split('/')[2], Request.UrlReferrer.AbsolutePath.Split('/')[1], new { Id = CodigoConteudo });

            //return Redirect(Request.UrlReferrer.AbsolutePath + "?Id=" + CodigoConteudo + "&msg=Foto cadastrada com sucesso! Atualize a página para ela aparecer na listagem de fotos!");
        }

        public FileResult Foto(string fotoContent, string Tamanho)
        {
            string caminho = "";

            if (fotoContent.Contains("Usuario/"))
            {
                string NomeArquivo = fotoContent.Split('/')[1].Split('.')[0];
                caminho = "~/AppData/Foto/Usuario/" + fotoContent.Split('/')[1];

                if (NomeArquivo == "")
                    return base.File("/content/themes/publico/img/logoAM.jpg", "image/jpg");
            }
            else if (fotoContent.Contains("Servicos/"))
            {
                if (Tamanho == null)
                    Tamanho = "full";
                else if (Tamanho == "300")
                    Tamanho = "full";

                string NomeArquivo = fotoContent.Split('/')[1].Split('.')[0];
                caminho = "~/AppData/Foto/Logo/Servicos/" + Tamanho + "/" + fotoContent.Split('/')[1];
            }
            else
            {
                if (Tamanho == null)
                    Tamanho = "full";
                else if (Tamanho == "340")
                    Tamanho = "340x240";
                else if (Tamanho == "145")
                    Tamanho = "145x95";

                caminho = "~/AppData/Foto/" + Tamanho + "/" + fotoContent;
            }

            FileInfo info = new FileInfo(Server.MapPath(caminho));

            if (info.Exists)
                return base.File(caminho, "image/jpeg");
            else
                return base.File("/content/themes/publico/img/logoAM.jpg", "image/jpg");
        }
    }
}
