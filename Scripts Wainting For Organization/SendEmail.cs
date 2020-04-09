using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class SendEmail : MonoBehaviour
{

    public void Send()
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("jfeliciano@fluxgamestudio.com");
        mail.To.Add("jfeliciano@fluxgamestudio.com");
        mail.Subject = "Teste Mail";
        mail.Body = "Minha mensagem";
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("jfeliciano@fluxgamestudio.com", "********") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);
    }

}
