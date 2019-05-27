#define qqEnterprise2

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CSharp.Framework.Email
{
    public class EmailHelper
    {
        private const string emailFromName = "你好";

#if qq
        private const string emailSever = "smtp.qq.com";
        private const string emailFrom = "357592895@qq.com";
        private const string passWord = "mnlqtfdfrawsbiji"; //该密码为授权码, 非邮箱密码
#elif _163
        private const string emailSever = "smtp.163.com";
        private const string emailFrom = "13625024073@163.com";
        private const string passWord = "amnenriyiwamzcfy"; //该密码为授权码, 非邮箱密码
#elif qqEnterprise1
        private const string emailSever = "smtp.exmail.qq.com";
        private const string emailFrom = "chenjilan@c-go.com.cn";
        private const string passWord = "qt4go7ki3MdAtD8o"; //该密码为授权码, 非邮箱密码


#elif qqEnterprise2
        private const string emailSever = "smtp.exmail.qq.com";
        private const string emailFrom = "tpm@c-go.com.cn";
        private const string passWord = "HaNeuXSTiLhDQPbM"; //该密码为授权码, 非邮箱密码

#elif gmail_无效
        private const string emailSever = "smtp.gmail.com";
        private const string emailFrom = "wanpidan1234@gmail.com";
        private const string passWord = "357592895"; //该密码为授权码, 非邮箱密码
#endif


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="emailSubject">主题</param>
        /// <param name="emailBody">内容</param>
        /// <param name="mailAttach">附件</param>
        /// <param name="mailTo">收件人邮箱</param>
        /// <param name="mailToName">收件人</param>
        /// <param name="covertHtml">内容部分是否为html</param>
        public void SendEmail(string id, string emailSubject, string emailBody,string mailAttach, string mailTo, string mailToName, bool covertHtml = false)
        {
            string[] mailList = mailTo.ToString().Split(new char[] { ',' });
            string[] mailNameList = mailToName.ToString().Split(new char[] { ',' });
            string[] mailAttachList = mailAttach.ToString().Split(new char[] { ',' });

            MailMessage myMail = new MailMessage();
            try
            {
                //int i;
                //MailAddress mad;
                Encoding enc = Encoding.GetEncoding(936);
                myMail.From = new MailAddress(emailFrom, emailFromName, enc);
                for (int i = 0; i < mailList.Length; i++)
                {
                    //mad = new MailAddress(mailList[i], mailNameList[i], enc);
                    //if (!myMail.To.Contains(mad))
                    //{
                    myMail.To.Add(new MailAddress(mailList[i], mailNameList[i], enc));
                    Logger.TimerLog.Info(string.Format("第{0}个 收件人：{1}，收件人名：{2}", i + 1, mailList[i],mailNameList[i]));
                    //}
                }
                for (int i = 0; i < mailAttachList.Length; i++)
                {
                    if (!string.IsNullOrEmpty(mailAttachList[i]))
                    {
                        string filePath = mailAttachList[i];
                        filePath = filePath.Replace("\\","/");
                        if (!File.Exists(filePath))
                        {
                            Logger.TimerLog.Error(string.Format("未找到附件：{0}", filePath));
                            continue;
                        }
                        string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1, filePath.Length - filePath.LastIndexOf("/") - 1);
                        Attachment attachment = new Attachment(filePath);
                        attachment.Name = fileName;
                        attachment.NameEncoding = enc;
                        myMail.Attachments.Add(attachment);
                        Logger.TimerLog.Info(string.Format("添加第{0}个附件，附件路径：{1}，附件别名：{2}", i+1,filePath,fileName));
                    }
                        
                }

                myMail.Subject = emailSubject.ToString();
                myMail.Body = covertHtml ? this.CovertToHtmlLineBreak(emailBody.ToString()) : emailBody.ToString();
                myMail.SubjectEncoding = enc;
                myMail.BodyEncoding = enc;
                myMail.Priority = MailPriority.Normal;
                myMail.IsBodyHtml = covertHtml;

                SmtpClient smtpServer = new SmtpClient();
                smtpServer.UseDefaultCredentials = false;
                smtpServer.EnableSsl = true;
                smtpServer.Credentials = new NetworkCredential(emailFrom, passWord);
                smtpServer.Host = emailSever;
                smtpServer.Port = 587; //25, 587, 465

                //加这段之前用公司邮箱发送报错：根据验证过程，远程证书无效
                //加上后解决问题
//                ServicePointManager.ServerCertificateValidationCallback =
//delegate (Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };

                smtpServer.Send(myMail);

                Logger.TimerLog.Info(string.Format("邮件id：{0} 收件人：{1}，主题：{2}，内容：{3}, 发送成功",id, mailTo,myMail.Subject,myMail.Body));
            }
            catch (Exception ex)
            {
                Logger.TimerLog.Error(string.Format("邮件id：{0} 收件人：{1}，主题：{2}，内容：{3}, 发送失败", id, mailTo, emailSubject, emailBody), ex);
                throw ex;
            }
            finally
            {
                myMail.Attachments.Dispose();  //释放资源（关闭附件文件）
            }
        }
        private string CovertToHtmlLineBreak(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            return str.Trim().Replace("\r\n", "<br />");
        }
    }

}
