using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoPostRocket.Controllers
{
    /// <summary>
    /// Controller que é responsável pela apresentação das imagens
    /// e também do upload
    /// </summary>
    public class GaleriaController : Controller
    {
        public static Dictionary<string,string>descricoes = new Dictionary<string, string>();
        // GET: Galeria
        public ActionResult Index()
        {
            //Obter arquivos de uma determinada pasta
            var arquivos = Directory.GetFiles(Server.MapPath("~/UploadFiles"));

            //ViewBag com os nomes e extesão do arquivo
            //Exemplo : image.extensão
            ViewBag.Images = arquivos.Select(x => Path.GetFileName(x));
            ViewBag.Descricoes = descricoes;
            //Retornando a view Index.cshtml
            return View();
        }

        //GET: Upload
        /// <summary>
        /// View responsável por fazer upload de imagens
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload()
        {
            //Definir para não existir o link de retorno a galeria
            ViewBag.Success = false;
            return View();
        }

        //POST: Upload
        /// <summary>
        /// Método responsável por salvar a imagem que foi feita upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                //Verifica se foi passado algum arquivo válido
                if (file.ContentLength > 0)
                {
                    //Obtem soment o nome e extensão do arquivo que foi feita upload
                    string arquivo = Path.GetFileName(file.FileName);

                    //Obtém o caminho completo para salvar o arquivo
                    string caminhoASalvar = Path.Combine(Server.MapPath("~/UploadFiles"), arquivo);

                    descricoes.Add(arquivo, Request.Form["descricao"]);

                    //Salva de fato
                    file.SaveAs(caminhoASalvar);
                }
                ViewBag.Message = "Sucesso ao fazer seu upload!";
                ViewBag.Success = true;
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Infelizmente ocorreu um erro, tente novamente mais tarde.";
                ViewBag.Success = false;
                return View();
            }
        }
    }
}