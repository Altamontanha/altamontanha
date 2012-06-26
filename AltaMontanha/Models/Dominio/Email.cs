using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace AltaMontanha.Models.Dominio
{
    public class Email
    {
        [Required(ErrorMessage = "Preencha o nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o e-mail!")]
        [Display(Name = "E-mail")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Preencha o assunto!")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "Preencha o comentário!")]
        [Display(Name = "Comentário")]
        public string Comentario { get; set; }

        public int Enviar()
        {
            MailMessage oEmail = new MailMessage();
            MailAddress sDe = new MailAddress(this.Nome + "<" + this.EMail + ">"); /*COLOQUE AQUI UMA CAIXA VALIDA @seudomínio PARA QUE O ENVIO SEJA REALIZADO DE MODO NORMALIZADO*/
            MailAddress sRpt = new MailAddress(this.EMail);

            oEmail.To.Add("altamontanha@altamontanha.com"); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
            oEmail.From = sDe;
            oEmail.ReplyTo = sRpt;
            oEmail.Priority = MailPriority.Normal;
            oEmail.IsBodyHtml = false;
            oEmail.Subject = this.Comentario;

            oEmail.Body = this.Comentario;

            SmtpClient oEnviar = new SmtpClient();
            oEnviar.Host = "smtp.altamontanha.com"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
            oEnviar.Credentials = new System.Net.NetworkCredential("altamontanha@altamontanha.com", "gentedem"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
            try
            {
                oEnviar.Send(oEmail);
                return 0;
            }
            catch (ArgumentNullException)
            {
                return 1;
            }
            catch (ObjectDisposedException)
            {
                return 2;
            }
            catch (InvalidOperationException)
            {
                return 3;
            }
            catch (SmtpException)
            {
                return 4;
            }
            oEmail.Dispose();

            /*


            SmtpClient cliente = new SmtpClient("smtp.altamontanha.com", 587);
            cliente.EnableSsl = true;

            MailAddress remetente = new MailAddress(this.EMail, this.Nome);
            MailAddress destinatario = new MailAddress("altamontanha@altamontanha.com", "Contato");

            MailMessage mensagem = new MailMessage(remetente, destinatario);

            mensagem.Body = this.Comentario;
            mensagem.Subject = this.Assunto;

            NetworkCredential credenciais = new NetworkCredential(
              "altamontanha@altamontanha.com",
              "gentedem");

            cliente.Credentials = credenciais;

            try
            {
                cliente.Send(mensagem);
                return 0;
            }
            catch (ArgumentNullException)
            {
                return 1;
            }
            catch (ObjectDisposedException)
            {
                return 2;
            }
            catch (InvalidOperationException)
            {
                return 3;
            }
            catch (SmtpException)
            {
                return 4;
            }

            return 0;
             * */







            /*
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

            message.To.Add("geasimarcos@gmail.com");
            message.Subject = this.Assunto;
            message.From = new MailAddress("altamontanha@altamontanha.com");
            message.Body = this.Comentario;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.altamontanha.com");

            try
            {
                smtp.Send(message);
                return 0;
            }
            catch (ArgumentNullException)
            {
                return 1;
            }
            catch (ObjectDisposedException)
            {
                return 2;
            }
            catch (InvalidOperationException)
            {
                return 3;
            }
            catch (SmtpException)
            {
                return 4;
            }
            */
        }
    }
}