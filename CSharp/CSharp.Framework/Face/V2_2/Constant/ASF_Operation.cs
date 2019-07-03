using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Framework.Face.V2_2.Constant
{
    public enum ASF_Operation
    {
        ASF_NONE = 0x00000000,	//无属性
        ASF_FACE_DETECT = 0x00000001,	//此处detect可以是tracking或者detection两个引擎之一，具体的选择由detect mode 确定
        ASF_FACERECOGNITION = 0x00000004,	//人脸特征
        ASF_AGE = 0x00000008,	//年龄
        ASF_GENDER = 0x00000010,	//性别
        ASF_FACE3DANGLE = 0x00000020,	//3D角度
        ASF_LIVENESS = 0x00000080,	//RGB活体
        ASF_IR_LIVENESS = 0x00000400,	//红外活体
    }
}
