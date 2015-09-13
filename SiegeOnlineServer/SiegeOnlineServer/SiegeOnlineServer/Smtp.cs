//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2015-2016 SiegeOnline
// 版权所有
//
// 文件名：Smtp.cs
//
// 文件功能描述：
//
// 使用 SMTP 进行邮件发送
//
// 创建标识：taixihuase 20150905
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable InconsistentNaming

namespace SiegeOnlineServer
{
    /// <summary>
    /// 类型：类
    /// 名称：Smtp
    /// 作者：taixihuase
    /// 作用：SMTP 邮件发送类
    /// 编写日期：2015/9/5
    /// </summary>
    public class Smtp
    {
        public string FromAddress { get; set; }

        public string SenderName { get; set; }

        public List<string> ToAddresses;

        public string Subject { get; set; }

        public string Body { get; set; }

        public string SmtpAccount { get; protected set; }

        public string SmtpPassword { get; protected set; }

        public int SmtpPort { get; protected set; }

        public string SmtpHost { get; protected set; }

        /// <summary>
        /// 类型：方法
        /// 名称：Smtp
        /// 作者：taixihuase
        /// 作用：构造一个 Smtp 对象，并保存发件人信息
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="from"></param>
        /// <param name="sender"></param>
        public Smtp(string from, string sender)
        {
            FromAddress = from;
            SenderName = sender;
            ToAddresses = new List<string>();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：From
        /// 作者：taixihuase
        /// 作用：更新发件人的地址和信息
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="from"></param>
        /// <param name="sender"></param>
        public void From(string from, string sender)
        {
            FromAddress = from;
            SenderName = sender;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：To
        /// 作者：taixihuase
        /// 作用：填入一个收件人地址
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="to"></param>
        public void To(string to)
        {
            if (!ToAddresses.Contains(to))
            {
                ToAddresses.Add(to);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：InitSmtpOfHotmail
        /// 作者：taixihuase
        /// 作用：初始化 Hotmail 的 SMTP 并登录 Hotmail 账号
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        private void InitSmtpOfHotmail(string account, string password)
        {
            SmtpAccount = account;
            SmtpPassword = password;
            SmtpPort = 587;
            SmtpHost = "smtp.live.com";
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendMailUseHotmail
        /// 作者：taixihuase
        /// 作用：用 Hotmail 邮箱服务发送
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SendMailUseHotmail(string account, string password)
        {
            if (FromAddress.Length > 0 && SenderName.Length > 0)
            {
                InitSmtpOfHotmail(account, password);

                MailMessage msg = new MailMessage {From = new MailAddress(FromAddress, SenderName, Encoding.UTF8)};
                foreach (string address in ToAddresses)
                {
                    string check;
                    if (CheckEmail(address, out check) == 200)
                    {
                        msg.To.Add(address);
                    }
                }
                if (msg.To.Count == 0)
                {
                    return false;
                }

                msg.Subject = Subject;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.Body = Body;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = false;
                msg.Priority = MailPriority.High;

                SmtpClient client = new SmtpClient(SmtpHost, SmtpPort)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(SmtpAccount, SmtpPassword)
                };

                try
                {
                    client.Send(msg);
                    ServerPeer.Log.Debug("emails succeed");
                    return true;
                }
                catch (SmtpException ex)
                {
                    ServerPeer.Log.Debug("emails fail because of " + ex);
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetMailServer
        /// 作者：taixihuase
        /// 作用：尝试获取邮箱服务器
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public string GetMailServer(string strEmail)
        {
            string strDomain = strEmail.Split('@')[1];
            ProcessStartInfo info = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                FileName = "nslookup",
                CreateNoWindow = true,
                Arguments = "-type=mx " + strDomain
            };
            Process ns = Process.Start(info);
            if (ns != null)
            {
                StreamReader sout = ns.StandardOutput;
                Regex reg = new Regex(@"mail exchanger = (?<mailServer>[^\s].*)");
                string strResponse;
                while ((strResponse = sout.ReadLine()) != null)
                {
                    Match amatch = reg.Match(strResponse);
                    if (reg.Match(strResponse).Success)
                    {
                        return amatch.Groups["mailServer"].Value;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CheckEmail
        /// 作者：taixihuase
        /// 作用：判断邮箱的真实性
        /// 编写日期：2015/9/5
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public int CheckEmail(string mailAddress, out string errorInfo)
        {
            string mailServer = GetMailServer(mailAddress);
            if (mailServer == null)
            {
                errorInfo = "Email Server error!";
                return 404;
            }
            errorInfo = String.Empty;
            return 200;

            // 无效代码
            //TcpClient tcpc = new TcpClient
            //{
            //    NoDelay = true,
            //    ReceiveTimeout = 3000,
            //    SendTimeout = 3000
            //};
            //try
            //{
            //    tcpc.Connect(mailServer, 25);
            //    NetworkStream s = tcpc.GetStream();
            //    StreamReader sr = new StreamReader(s, Encoding.Default);
            //    StreamWriter sw = new StreamWriter(s, Encoding.Default);
            //    string strTestFrom = mailAddress;
            //    sw.WriteLine("helo " + mailServer);
            //    sw.WriteLine("mail from:<" + mailAddress + ">");
            //    sw.WriteLine("rcpt to:<" + strTestFrom + ">");
            //    var strResponse = sr.ReadLine();
            //    if (strResponse != null && !strResponse.StartsWith("2"))
            //    {
            //        errorInfo = "UserName error!";
            //        return 403;
            //    }
            //    sw.WriteLine("quit");
            //    errorInfo = String.Empty;
            //    return 200;

            //}
            //catch (Exception e)
            //{
            //    errorInfo = e.Message;
            //    return 403;
            //}
        }
    }
}
