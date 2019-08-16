using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Framework.Face.V2_2.Error
{
    public static class ASF_ErrorDescription
    {
        private static readonly Dictionary<ASF_ErrorCode, string> _dicDesc;

        static ASF_ErrorDescription()
        {
            _dicDesc = new Dictionary<ASF_ErrorCode, string>
            {
                { ASF_ErrorCode.MOK, "成功" },
                { ASF_ErrorCode.MERR_UNKNOWN, "错误原因不明" },
                { ASF_ErrorCode.MERR_INVALID_PARAM, "无效的参数" },
                { ASF_ErrorCode.MERR_UNSUPPORTED, "引擎不支持" },
                { ASF_ErrorCode.MERR_NO_MEMORY, "内存不足" },
                { ASF_ErrorCode.MERR_BAD_STATE, "状态错误" },
                { ASF_ErrorCode.MERR_USER_CANCEL, "用户取消相关操作" },
                { ASF_ErrorCode.MERR_EXPIRED, "操作时间过期" },
                { ASF_ErrorCode.MERR_USER_PAUSE, "用户暂停操作" },
                { ASF_ErrorCode.MERR_BUFFER_OVERFLOW, "缓冲上溢" },
                { ASF_ErrorCode.MERR_BUFFER_UNDERFLOW, "缓冲下溢" },
                { ASF_ErrorCode.MERR_NO_DISKSPACE, "存贮空间不足" },
                { ASF_ErrorCode.MERR_COMPONENT_NOT_EXIST, "组件不存在" },
                { ASF_ErrorCode.MERR_GLOBAL_DATA_NOT_EXIST, "全局数据不存在" },
                { ASF_ErrorCode.MERR_FSDK_INVALID_APP_ID, "无效的AppId" },
                { ASF_ErrorCode.MERR_FSDK_INVALID_SDK_ID, "无效的SDKkey" },
                { ASF_ErrorCode.MERR_FSDK_INVALID_ID_PAIR, "AppId和SDKKey不匹配" },
                { ASF_ErrorCode.MERR_FSDK_MISMATCH_ID_AND_SDK, "SDKKey和使用的SDK不匹配" },
                { ASF_ErrorCode.MERR_FSDK_SYSTEM_VERSION_UNSUPPORTED, "系统版本不被当前SDK所支持" },
                { ASF_ErrorCode.MERR_FSDK_LICENCE_EXPIRED, "SDK有效期过期，需要重新下载更新" },
                { ASF_ErrorCode.MERR_FSDK_FR_INVALID_MEMORY_INFO, "无效的输入内存" },
                { ASF_ErrorCode.MERR_FSDK_FR_INVALID_IMAGE_INFO, "无效的输入图像参数" },
                { ASF_ErrorCode.MERR_FSDK_FR_INVALID_FACE_INFO, "无效的脸部信息" },
                { ASF_ErrorCode.MERR_FSDK_FR_NO_GPU_AVAILABLE, "当前设备无GPU可用" },
                { ASF_ErrorCode.MERR_FSDK_FR_MISMATCHED_FEATURE_LEVEL, "待比较的两个人脸特征的版本不一致" },
                { ASF_ErrorCode.MERR_FSDK_FACEFEATURE_UNKNOWN, "人脸特征检测错误未知" },
                { ASF_ErrorCode.MERR_FSDK_FACEFEATURE_MEMORY, "人脸特征检测内存错误" },
                { ASF_ErrorCode.MERR_FSDK_FACEFEATURE_INVALID_FORMAT, "人脸特征检测格式错误" },
                { ASF_ErrorCode.MERR_FSDK_FACEFEATURE_INVALID_PARAM, "人脸特征检测参数错误" },
                { ASF_ErrorCode.MERR_FSDK_FACEFEATURE_LOW_CONFIDENCE_LEVEL, "人脸特征检测结果置信度低" },
                { ASF_ErrorCode.MERR_ASF_EX_FEATURE_UNSUPPORTED_ON_INIT, "Engine不支持的检测属性" },
                { ASF_ErrorCode.MERR_ASF_EX_FEATURE_UNINITED, "需要检测是属性未初始化" },
                { ASF_ErrorCode.MERR_ASF_EX_FEATURE_UNPROCESSED, "待获取的属性未在process中处理过" },
                { ASF_ErrorCode.MERR_ASF_EX_FEATURE_UNSUPPORTED_ON_PROCESS, "PROCESS不支持的检测属性，例如FR，有自己独立的处理函数" },
                { ASF_ErrorCode.MERR_ASF_EX_INVALID_IMAGE_INFO, "无效的输入图像" },
                { ASF_ErrorCode.MERR_ASF_EX_INVALID_FACE_INFO, "无效的脸部信息" },
                { ASF_ErrorCode.MERR_ASF_ACTIVATION_FAIL, "SDK激活失败,请打开读写权限" },
                { ASF_ErrorCode.MERR_ASF_ALREADY_ACTIVATED, "SDK已激活" },
                { ASF_ErrorCode.MERR_ASF_NOT_ACTIVATED, "SDK未激活" },
                { ASF_ErrorCode.MERR_ASF_SCALE_NOT_SUPPORT, "detectFaceScaleVal不支持" },
                { ASF_ErrorCode.MERR_ASF_VERION_MISMATCH, "SDK版本不匹配" },
                { ASF_ErrorCode.MERR_ASF_DEVICE_MISMATCH, "设备不匹配" },
                { ASF_ErrorCode.MERR_ASF_UNIQUE_IDENTIFIER_MISMATCH, "唯一标识不匹配" },
                { ASF_ErrorCode.MERR_ASF_PARAM_NULL, "参数为空" },
                { ASF_ErrorCode.MERR_ASF_LIVENESS_EXPIRED, "活体检测功能已过期" },
                { ASF_ErrorCode.MERR_ASF_VERSION_NOT_SUPPORT, "版本不支持" },
                { ASF_ErrorCode.MERR_ASF_SIGN_ERROR, "签名错误" },
                { ASF_ErrorCode.MERR_ASF_DATABASE_ERROR, "数据库插入错误" },
                { ASF_ErrorCode.MERR_ASF_UNIQUE_CHECKOUT_FAIL, "唯一标识符校验失败" },
                { ASF_ErrorCode.MERR_ASF_COLOR_SPACE_NOT_SUPPORT, "颜色空间不支持" },
                { ASF_ErrorCode.MERR_ASF_IMAGE_WIDTH_HEIGHT_NOT_SUPPORT, "图片宽度或高度不支持" },
                { ASF_ErrorCode.MERR_ASF_READ_PHONE_STATE_DENIED, "android.permission.READ_PHONE_STATE权限被拒绝" },
                { ASF_ErrorCode.MERR_ASF_ACTIVATION_DATA_DESTROYED, "激活数据被破坏,请删除激活文件，重新进行激活" },
                { ASF_ErrorCode.MERR_ASF_SERVER_UNKNOWN_ERROR, "服务端未知错误" },
                { ASF_ErrorCode.MERR_ASF_INTERNET_DENIED, "INTERNET权限被拒绝" },
                { ASF_ErrorCode.MERR_ASF_ACTIVEFILE_SDK_MISMATCH, "激活文件与SDK版本不匹配,请重新激活" },
                { ASF_ErrorCode.MERR_ASF_DEVICEINFO_LESS, "设备信息太少，不足以生成设备指纹" },
                { ASF_ErrorCode.MERR_ASF_REQUEST_TIMEOUT, "客户端时间与服务器时间（即北京时间）前后相差在30分钟之内" },
                { ASF_ErrorCode.MERR_ASF_APPID_DATA_DECRYPT, "服务端解密失败" },
                { ASF_ErrorCode.MERR_ASF_APPID_APPKEY_SDK_MISMATCH, "传入的AppId和AppKey与使用的SDK版本不一致" },
                { ASF_ErrorCode.MERR_ASF_NO_REQUEST, "短时间大量请求会被禁止请求,30分钟之后会解封" },
                { ASF_ErrorCode.MERR_ASF_NETWORK_COULDNT_RESOLVE_HOST, "无法解析主机地址" },
                { ASF_ErrorCode.MERR_ASF_NETWORK_COULDNT_CONNECT_SERVER, "无法连接服务器" },
                { ASF_ErrorCode.MERR_ASF_NETWORK_CONNECT_TIMEOUT, "网络连接超时" },
                { ASF_ErrorCode.MERR_ASF_NETWORK_UNKNOWN_ERROR, "网络未知错误" },
            };
        }

        public static string GetErrorDescription(ASF_ErrorCode errorCode)
        {
            if (_dicDesc.ContainsKey(errorCode))
            {
                return _dicDesc[errorCode];
            }
            return null;
        }
    }
}
