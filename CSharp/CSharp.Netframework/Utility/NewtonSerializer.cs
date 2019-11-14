using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharp.Netframework.Utility
{
    public class NewtonSerializer 
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        public T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public string Serialize<T>(T input)
        {
            return JsonConvert.SerializeObject(input, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented });
        }

        public T Deserialize<T>(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            using (var jsr = new JsonTextReader(sr))
            {
                return _serializer.Deserialize<T>(jsr);
            }
        }
    }
}
