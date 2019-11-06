//public void uf_SendMailToAgents(string as_DutyName,string as_DutyMail, string as_BeginDate, string as_EndDate, string as_AgentName,string as_AgentMail)
//         {
//             string ls_Server, ls_SendFrom, ls_Body = "";
//             ls_Server = ConfigurationSettings.AppSettings["SmtpServer"].Trim();
//             ls_SendFrom = ConfigurationSettings.AppSettings["MailSender"].Trim();
//             SmtpMail.SmtpServer = ls_Server;
//             MailMessage lo_Message = new MailMessage();
//             //MailAttachment lo_Attach = new MailAttachment("C:\\Compal\\SIT Mail Release\\Mail Attachment\\A31\\2011\\2011-5-28 14-33-34\\postback.doc");
//             //lo_Message.Attachments.Add(lo_Attach);
//             lo_Message.To = as_AgentMail;
//             lo_Message.Cc = as_DutyMail;
//             lo_Message.From = ls_SendFrom;
//             lo_Message.Subject = "SW SITMailRelease System---- 設定代理人提醒";
//             lo_Message.Priority = MailPriority.High;
//             lo_Message.BodyFormat = MailFormat.Html;
//             lo_Message.BodyEncoding = System.Text.Encoding.UTF8;
//             ls_Body = uf_CreateAgentBody(as_DutyName, as_AgentName, as_BeginDate, as_EndDate);
//             lo_Message.Body = ls_Body;
//             SmtpMail.Send(lo_Message);
//         }

//         public string uf_CreateAgentBody(string as_DutyName, string as_AgentName, string as_BeginDate, string as_EndDate)
//         {
//             XmlDataDocument lo_Document = new XmlDataDocument();
//             XmlDeclaration lo_Declaration = lo_Document.CreateXmlDeclaration("1.0", "UTF-8", null);
//             XmlElement lo_Root = lo_Document.CreateElement("SITMailRelease");
//             lo_Document.AppendChild(lo_Root);

//             XmlElement lo_EmpName = lo_Document.CreateElement("Name_Eng");
//             lo_Document.DocumentElement.AppendChild(lo_EmpName);
//             lo_EmpName.InnerText = as_AgentName;

//             XmlElement lo_Agent = lo_Document.CreateElement("Duty");
//             lo_Document.DocumentElement.AppendChild(lo_Agent);
//             lo_Agent.InnerText = as_DutyName;

//             XmlElement lo_BeginDate = lo_Document.CreateElement("BeginDate");
//             lo_Document.DocumentElement.AppendChild(lo_BeginDate);
//             lo_BeginDate.InnerText = as_BeginDate;
//             XmlElement lo_EndDate = lo_Document.CreateElement("EndDate");
//             lo_Document.DocumentElement.AppendChild(lo_EndDate);
//             lo_EndDate.InnerText = as_EndDate;

//             //XmlElement lo_Link = lo_Document.CreateElement("Link");
//             //lo_Document.DocumentElement.AppendChild(lo_Link);
//             //lo_Link.InnerText = ConfigurationSettings.AppSettings["href"].ToString();

//             XmlElement lo_Date = lo_Document.CreateElement("DateNow");
//             lo_Document.DocumentElement.AppendChild(lo_Date);
//             lo_Date.InnerText = DateTime.Now.ToShortDateString();

//             lo_Document.InsertBefore(lo_Declaration, lo_Root);
//             string ls_file = this.up_MailToAgent;
//             string ls_Body = this.uf_MailFormat(lo_Document, ls_file);
//             return ls_Body;
//         }