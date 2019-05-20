using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    /// <summary>
    /// 附件信息
    /// </summary>
    [Serializable]
    public class AttachFileInfo
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 0x00：图片
        /// 0x01：音频
        /// 0x02：视频
        /// 0x03：记录文件
        /// 0x04：其它
        /// </summary>
        public byte FileType { get; set; }
        /// <summary>
        /// 当前报警附件的大小
        /// </summary>
        public uint FileSize { get; set; }
        /// <summary>
        /// 当前报警附件的完整 URL 地址
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string MD5 { get; set; }
        /// <summary>
        /// 文件格式
        /// Ox0l:jpg
        /// Ox02:gif
        /// Ox03:png
        /// 0x04:wav
        /// 0x05:mp3
        /// 0x06:mp4
        /// 0x07:3gp
        /// 0x08:flv
        /// </summary>
        public byte FileFormat { get; set; }
    }
}
