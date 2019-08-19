using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Utility
{
    public class NewtonSerializer : ISerializer
    {
        public T Deserialize<T>(string input)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(input);
        }

        public string Serialize<T>(T input)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(input, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented });
        }
    }
}
