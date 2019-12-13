using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    [Flags]
    public enum FlagBits1
    {
        B1 = 0x01,
        B2 = 0x02,
        //B3 = 0x03,
        //B3 = FlagBits2.B1 | FlagBits1.B2,
        //B3 = FlagBits1.B1 | FlagBits1.B2,
        B3 = B1 | B2,
        B4 = 0x04,
    }
    [Flags]
    public enum FlagBits2
    {
        B1 = 0x01,
        B2 = 0x02,
        B3 = 0x04,
        B4 = 0x08,
    }
}
