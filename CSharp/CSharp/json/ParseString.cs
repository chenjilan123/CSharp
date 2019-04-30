using CSharp.Entity;
using Newtonsoft.Json;
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
    }
}
