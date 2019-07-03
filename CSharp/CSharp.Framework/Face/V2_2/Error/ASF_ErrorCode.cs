using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Framework.Face.V2_2.Error
{
    public enum ASF_ErrorCode
    {
        MOK = 0,
        MERR_INVALID_PARAM = 2, //无效的参数
        MERR_FSDK_INVALID_SDK_ID = 28674, //无效的SDKkey
        MERR_FSDK_MISMATCH_ID_AND_SDK = 28676, //SDKKey和使用的SDK不匹配
        MERR_ASF_ALREADY_ACTIVATED = 90114, //SDK已激活
        MERR_ASF_IMAGEMODE_0_HIGHER_EXT_UNSUPPORT = 90139, // IMAGE模式下不支持全角度0x1601B 90139

        //        MOK 0x0 0 成功
        //MERR_UNKNOWN 0x1 1 错误原因不明
        //MERR_INVALID_PARAM 0x2 2 无效的参数
        //MERR_UNSUPPORTED 0x3 3 引擎不支持
        //MERR_NO_MEMORY 0x4 4 内存不足
        //MERR_BAD_STATE 0x5 5 状态错误
        //MERR_USER_CANCEL 0x6 6 用户取消相关操作
        //MERR_EXPIRED 0x7 7 操作时间过期
        //MERR_USER_PAUSE 0x8 8 用户暂停操作
        //MERR_BUFFER_OVERFLOW 0x9 9 缓冲上溢
        //MERR_BUFFER_UNDERFLOW 0xA 10 缓冲下溢
        //MERR_NO_DISKSPACE 0xB 11 存贮空间不足
        //MERR_COMPONENT_NOT_EXIST 0xC 12 组件不存在
        //MERR_GLOBAL_DATA_NOT_EXIST 0xD 13 全局数据不存在
        //MERR_FSDK_INVALID_APP_ID 0x7001 28673 无效的 AppId
        //MERR_FSDK_INVALID_SDK_ID 0x7002 28674 无效的 SDKkey
        //MERR_FSDK_INVALID_ID_PAIR 0x7003 28675 AppId 和 SDKKey 不匹配
        //SDKKey 和使用的 SDK 不匹配（注
        //MERR_FSDK_MISMATCH_ID_AND_SD 意：调用初始化引擎接口时，请确 0x7004 28676
        //K 认激活接口传入的参数，并重新激
        //活）
        //MERR_FSDK_SYSTEM_VERSION_UNS
        //0x7005 28677 系统版本不被当前 SDK 所支持 UPPORTED
        //SDK 有效期过期，需要重新下载更 MERR_FSDK_LICENCE_EXPIRED 0x7006 28678 新
        //MERR_FSDK_FR_INVALID_MEMORY_
        //0x12001 73729 无效的输入内存 INFO
        //MERR_FSDK_FR_INVALID_IMAGE_I
        //0x12002 73730 无效的输入图像参数 NFO
        //MERR_FSDK_FR_INVALID_FACE_IN
        //0x12003 73731 无效的脸部信息 FO
        //MERR_FSDK_FR_MISMATCHED_FEAT 待比较的两个人脸特征的版本不 0x12005 73733
        //URE_LEVEL 一致
        //MERR_FSDK_FACEFEATURE_UNKNOW
        //0x14001 81921 人脸特征检测错误未知 N
        //MERR_FSDK_FACEFEATURE_MEMORY 0x14002 81922 人脸特征检测内存错误
        //MERR_FSDK_FACEFEATURE_INVALI
        //0x14003 81923 人脸特征检测格式错误 D_FORMAT
        //MERR_FSDK_FACEFEATURE_INVALI
        //0x14004 81924 人脸特征检测参数错误 D_PARAM
        //MERR_FSDK_FACEFEATURE_LOW_CO
        //0x14005 81925 人脸特征检测结果置信度低 NFIDENCE_LEVEL
        //MERR_ASF_EX_FEATURE_UNSUPPOR
        //0x15001 86017 Engine 不支持的检测属性 TED_ON_INIT
        //MERR_ASF_EX_FEATURE_UNINITED 0x15002 86018 需要检测的属性未初始化
        //MERR_ASF_EX_FEATURE_UNPROCES 待获取的属性未在 process 中处 0x15003 86019
        //SED 理过
        //MERR_ASF_EX_FEATURE_UNSUPPOR PROCESS 不支持的检测属性，例如 0x15004 86020
        //TED_ON_PROCESS FR，有自己独立的处理函数
        //MERR_ASF_EX_INVALID_IMAGE_IN
        //0x15005 86021 无效的输入图像 FO
        //MERR_ASF_EX_INVALID_FACE_INF
        //0x15006 86022 无效的脸部信息 O
        //MERR_ASF_ACTIVATION_FAIL 0x16001 90113 SDK 激活失败，请打开读写权限
        //MERR_ASF_ALREADY_ACTIVATED 0x16002 90114 SDK 已激活
        //MERR_ASF_NOT_ACTIVATED 0x16003 90115 SDK 未激活
        //MERR_ASF_SCALE_NOT_SUPPORT 0x16004 90116 detectFaceScaleVal 不支持
        //MERR_ASF_ACTIVEFILE_SDKTYPE_ 激活文件与 SDK 类型不匹配，请确 0x16005 90117
        //MISMATCH 认使用的 sdk
        //MERR_ASF_DEVICE_MISMATCH 0x16006 90118 设备不匹配
        //MERR_ASF_UNIQUE_IDENTIFIER_I
        //0x16007 90119 唯一标识不合法 LLEGAL
        //MERR_ASF_PARAM_NULL 0x16008 90120 参数为空
        //MERR_ASF_VERSION_NOT_SUPPORT 0x1600A 90122 版本不支持
        //MERR_ASF_SIGN_ERROR 0x1600B 90123 签名错误
        //MERR_ASF_DATABASE_ERROR 0x1600C 90124 激活信息保存异常
        //MERR_ASF_UNIQUE_CHECKOUT_FAI
        //0x1600D 90125 唯一标识符校验失败 L
        //MERR_ASF_COLOR_SPACE_NOT_SUP
        //0x1600E 90126 颜色空间不支持 PORT
        //MERR_ASF_IMAGE_WIDTH_HEIGHT_ 图片宽高不支持，宽度需四字节对 0x1600F 90127
        //NOT_SUPPORT 齐
        //MERR_ASF_ACTIVATION_DATA_DES 激活数据被破坏, 请删除激活文 0x16011 90129
        //TROYED 件，重新进行激活
        //MERR_ASF_SERVER_UNKNOWN_ERRO
        //0x16012 90130 服务端未知错误 R
        //MERR_ASF_ACTIVEFILE_SDK_MISM 激活文件与 SDK 版本不匹配, 请重 0x16014 90132
        //ATCH 新激活
        //设备信息太少，不足以生成设备指 MERR_ASF_DEVICEINFO_LESS 0x16015 90133 纹
        //客户端时间与服务器时间（即北京 MERR_ASF_REQUEST_TIMEOUT 0x16016 90134 时间）前后相差在 30 分钟以上
        //MERR_ASF_APPID_DATA_DECRYPT 0x16017 90135 数据校验异常
        //MERR_ASF_APPID_APPKEY_SDK_MI 传入的 AppId 和 AppKey 与使用的 0x16018 90136
        //SMATCH SDK 版本不一致
        //短时间大量请求会被禁止请求,30
        //MERR_ASF_NO_REQUEST 0x16019 90137 分钟之后解封
        //MERR_ASF_ACTIVE_FILE_NO_EXIS
        //0x1601A 90138 激活文件不存在 T
        //MERR_ASF_IMAGEMODE_0_HIGHER_ IMAGE 模 式 下 不 支 持 全 角 度 0x1601B 90139
        //EXT_UNSUPPORT (ASF_OP_0_HIGHER_EXT)检测
        //MERR_ASF_NETWORK_COULDNT_RES
        //0x17001 94209 无法解析主机地址 OLVE_HOST
        //MERR_ASF_NETWORK_COULDNT_CON 0x17002 94210 无法连接服务器
        //NECT_SERVER
        //MERR_ASF_NETWORK_CONNECT_TIM
        //0x17003 94211 网络连接超时 EOUT
        //MERR_ASF_NETWORK_UNKNOWN_ERR
        //0x17004 94212 网络未知错误 OR
    }
}
