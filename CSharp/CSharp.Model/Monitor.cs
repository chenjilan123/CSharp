using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Model
{
    public class Monitor: MarshalByRefObject
    {
        public Size GetDesktopBitmapSize()
        {
            return new Size(1, 1);
        }

        public byte[] GetDesktopBitmapBytes()
        {
            return new byte[0];
        }
    }
}
