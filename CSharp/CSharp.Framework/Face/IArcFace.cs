using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Framework.Face
{
    public interface IArcFace
    {
        void InitEngine(string appId, string sdkKey);
        void ExtractFeature(Bitmap bitmap);
        void Compare(byte[] feature1, byte[] feature2);
    }
}
