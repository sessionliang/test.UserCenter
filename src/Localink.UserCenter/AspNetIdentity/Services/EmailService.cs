using Localink.UserCenter.Configurations;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Localink.UserCenter.AspNetIdentity.Services
{
    /// <summary>
    /// 邮箱验证Service
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            MailConfig mailConfig = (MailConfig)ConfigurationManager.GetSection("application/mail");
            if (mailConfig.RequireValid)
            {
                // 设置邮件内容
                var mail = new MailMessage(
                    new MailAddress(mailConfig.Uid, "no-reply"),
                    new MailAddress(message.Destination)
                    );
                mail.Subject = message.Subject;
                mail.Body = message.Body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                // 设置SMTP服务器
                var smtp = new SmtpClient(mailConfig.Server, mailConfig.Port);
                smtp.EnableSsl = mailConfig.EnableSSL;
                smtp.Credentials = new System.Net.NetworkCredential(mailConfig.Uid, mailConfig.Pwd);

                await smtp.SendMailAsync(mail);
            }
            await Task.FromResult(0);
        }
    }
}