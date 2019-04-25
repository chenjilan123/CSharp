using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.SimpleConfig
{
    /// <summary>
    /// 配置类型
    /// </summary> 
    public enum SysConfigType
    {
        /// <summary>
        /// 平台配置--前
        /// </summary>
        TopMonitorConfigXml = 1,
        /// <summary>
        /// 用户配置
        /// </summary>
        UserFunctionAuthConfig = 2,
        /// <summary>
        /// 只存于数据库的配置
        /// </summary>
        SqlConfigDesign = 4,
        /// <summary>
        /// 平台配置--后
        /// </summary>
        TopMonitorConfigXmlAfter = 8,
        /// <summary>
        /// 系统配置(config表中单独一条)
        /// </summary>
        PlaySysConfig = 16,
        /// <summary>
        /// 本地配置
        /// </summary>
        LocalConfigXml = 32,
    }

}
