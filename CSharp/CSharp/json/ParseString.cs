using CSharp.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.json
{
    public class ParseString
    {
        public void Run()
        {
            var json = "{ \"status\": \"010\" }";
            UserState userState = JsonConvert.DeserializeObject<UserState>(json) as UserState;
            Console.WriteLine(userState.status);
        }

        public void Setting()
        {
            JsonSerializer serializer = new JsonSerializer()
            {
                DateFormatHandling = new DateFormatHandling(),
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Error,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Auto,
            };

            ISerializationBinder binder;
            ITraceWriter tracer;
        }
    }
}
