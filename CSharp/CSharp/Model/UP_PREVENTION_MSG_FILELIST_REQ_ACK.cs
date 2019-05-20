using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    /// <summary>
    /// 主动安全智能防控报警附件目录请求应答(陕西主动安全809扩展)
    /// </summary>
    [Serializable]
    public class UP_PREVENTION_MSG_FILELIST_REQ_ACK
    {
        /// <summary>
        /// 附件服务器 IP 或域名
        /// </summary>
        public string SERVER { get; set; }
        /// <summary>
        /// 附件服务器 FTP 协议端口号
        /// </summary>
        public ushort TCP_PORT { get; set; }
        /// <summary>
        /// 附件服务器用户名
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        /// 附件服务武器密码
        /// </summary>
        public string PSSSWORD { get; set; }
        /// <summary>
        /// 附件列表
        /// </summary>
        public List<AttachFileInfo> FILE_LIST { get; set; }
    }
}
