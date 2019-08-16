using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Framework.Face.V2_2.Error
{
    public enum ASF_ErrorCode
    {
        MOK = 0x0, //成功
        MERR_UNKNOWN = 0x1, //错误原因不明
        MERR_INVALID_PARAM = 0x2, //无效的参数
        MERR_UNSUPPORTED = 0x3, //引擎不支持
        MERR_NO_MEMORY = 0x4, //内存不足
        MERR_BAD_STATE = 0x5, //状态错误
        MERR_USER_CANCEL = 0x6, //用户取消相关操作
        MERR_EXPIRED = 0x7, //操作时间过期
        MERR_USER_PAUSE = 0x8, //用户暂停操作
        MERR_BUFFER_OVERFLOW = 0x9, //缓冲上溢
        MERR_BUFFER_UNDERFLOW = 0xA, //缓冲下溢
        MERR_NO_DISKSPACE = 0xB, //存贮空间不足
        MERR_COMPONENT_NOT_EXIST = 0xC, //组件不存在
        MERR_GLOBAL_DATA_NOT_EXIST = 0xD, //全局数据不存在
        MERR_FSDK_INVALID_APP_ID = 0x7001, //无效的AppId
        MERR_FSDK_INVALID_SDK_ID = 0x7002, //无效的SDKkey
        MERR_FSDK_INVALID_ID_PAIR = 0x7003, //AppId和SDKKey不匹配
        MERR_FSDK_MISMATCH_ID_AND_SDK = 0x7004, //SDKKey和使用的SDK不匹配
        MERR_FSDK_SYSTEM_VERSION_UNSUPPORTED = 0x7005, //系统版本不被当前SDK所支持
        MERR_FSDK_LICENCE_EXPIRED = 0x7006, //SDK有效期过期，需要重新下载更新
        MERR_FSDK_FR_INVALID_MEMORY_INFO = 0x12001, //无效的输入内存
        MERR_FSDK_FR_INVALID_IMAGE_INFO = 0x12002, //无效的输入图像参数
        MERR_FSDK_FR_INVALID_FACE_INFO = 0x12003, //无效的脸部信息
        MERR_FSDK_FR_NO_GPU_AVAILABLE = 0x12004, //当前设备无GPU可用
        MERR_FSDK_FR_MISMATCHED_FEATURE_LEVEL = 0x12005, //待比较的两个人脸特征的版本不一致
        MERR_FSDK_FACEFEATURE_UNKNOWN = 0x14001, //人脸特征检测错误未知
        MERR_FSDK_FACEFEATURE_MEMORY = 0x14002, //人脸特征检测内存错误
        MERR_FSDK_FACEFEATURE_INVALID_FORMAT = 0x14003, //人脸特征检测格式错误
        MERR_FSDK_FACEFEATURE_INVALID_PARAM = 0x14004, //人脸特征检测参数错误
        MERR_FSDK_FACEFEATURE_LOW_CONFIDENCE_LEVEL = 0x14005, //人脸特征检测结果置信度低
        MERR_ASF_EX_FEATURE_UNSUPPORTED_ON_INIT = 0x15001, //Engine不支持的检测属性
        MERR_ASF_EX_FEATURE_UNINITED = 0x15002, //需要检测是属性未初始化
        MERR_ASF_EX_FEATURE_UNPROCESSED = 0x15003, //待获取的属性未在process中处理过
        MERR_ASF_EX_FEATURE_UNSUPPORTED_ON_PROCESS = 0x15004, //PROCESS不支持的检测属性，例如FR，有自己独立的处理函数
        MERR_ASF_EX_INVALID_IMAGE_INFO = 0x15005, //无效的输入图像
        MERR_ASF_EX_INVALID_FACE_INFO = 0x15006, //无效的脸部信息
        MERR_ASF_ACTIVATION_FAIL = 0x16001, //SDK激活失败,请打开读写权限
        MERR_ASF_ALREADY_ACTIVATED = 0x16002, //SDK已激活
        MERR_ASF_NOT_ACTIVATED = 0x16003, //SDK未激活
        MERR_ASF_SCALE_NOT_SUPPORT = 0x16004, //detectFaceScaleVal不支持
        MERR_ASF_VERION_MISMATCH = 0x16005, //SDK版本不匹配
        MERR_ASF_DEVICE_MISMATCH = 0x16006, //设备不匹配
        MERR_ASF_UNIQUE_IDENTIFIER_MISMATCH = 0x16007, //唯一标识不匹配
        MERR_ASF_PARAM_NULL = 0x16008, //参数为空
        MERR_ASF_LIVENESS_EXPIRED = 0x16009, //活体检测功能已过期
        MERR_ASF_VERSION_NOT_SUPPORT = 0x1600A, //版本不支持
        MERR_ASF_SIGN_ERROR = 0x1600B, //签名错误
        MERR_ASF_DATABASE_ERROR = 0x1600C, //数据库插入错误
        MERR_ASF_UNIQUE_CHECKOUT_FAIL = 0x1600D, //唯一标识符校验失败
        MERR_ASF_COLOR_SPACE_NOT_SUPPORT = 0x1600E, //颜色空间不支持
        MERR_ASF_IMAGE_WIDTH_HEIGHT_NOT_SUPPORT = 0x1600F, //图片宽度或高度不支持
        MERR_ASF_READ_PHONE_STATE_DENIED = 0x16010, //android.permission.READ_PHONE_STATE权限被拒绝
        MERR_ASF_ACTIVATION_DATA_DESTROYED = 0x16011, //激活数据被破坏,请删除激活文件，重新进行激活
        MERR_ASF_SERVER_UNKNOWN_ERROR = 0x16012, //服务端未知错误
        MERR_ASF_INTERNET_DENIED = 0x16013, //INTERNET权限被拒绝
        MERR_ASF_ACTIVEFILE_SDK_MISMATCH = 0x16014, //激活文件与SDK版本不匹配,请重新激活
        MERR_ASF_DEVICEINFO_LESS = 0x16015, //设备信息太少，不足以生成设备指纹
        MERR_ASF_REQUEST_TIMEOUT = 0x16016, //客户端时间与服务器时间（即北京时间）前后相差在30分钟之内
        MERR_ASF_APPID_DATA_DECRYPT = 0x16017, //服务端解密失败
        MERR_ASF_APPID_APPKEY_SDK_MISMATCH = 0x16018, //传入的AppId和AppKey与使用的SDK版本不一致
        MERR_ASF_NO_REQUEST = 0x16019, //短时间大量请求会被禁止请求,30分钟之后会解封
        MERR_ASF_NETWORK_COULDNT_RESOLVE_HOST = 0x17001, //无法解析主机地址
        MERR_ASF_NETWORK_COULDNT_CONNECT_SERVER = 0x17002, //无法连接服务器
        MERR_ASF_NETWORK_CONNECT_TIMEOUT = 0x17003, //网络连接超时
        MERR_ASF_NETWORK_UNKNOWN_ERROR = 0x17004, //网络未知错误
    }
}
