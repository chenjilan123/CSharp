using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public interface ISerializer
    {
        string Serialize<T>(T input);
        T Deserialize<T>(string input);
    }
}
