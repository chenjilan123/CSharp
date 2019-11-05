using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CSharp.JTB
{
    public class J808ProtocolRulerProvider
    {
        #region 单例
        private static J808ProtocolRulerProvider _instance;
        private static object _objLock = new object();
        public static J808ProtocolRulerProvider Instance
        {
            get
            {
                if (_instance == null)
                    lock (_objLock)
                        if (_instance == null)
                            _instance = new J808ProtocolRulerProvider();
                return _instance;
            }
        }
        #endregion

        const string J808_2013 = "J808_V2013";
        const string J808_2019 = "J808_V2019";
        const string J809_2013 = "J809_V2013";
        const string J809_2019 = "J809_V2019";

        Dictionary<string, CommandSet> CommandSets;
        CommandSet J808V2013CommandSet => this.CommandSets[J808_2013];
        CommandSet J808V2019CommandSet => this.CommandSets[J808_2019];
        CommandSet J809V2013CommandSet => this.CommandSets[J809_2013];
        CommandSet J809V2019CommandSet => this.CommandSets[J809_2019];

        public J808ProtocolRulerProvider Load()
        {
            using (var fs = new FileStream("./JTB/JTB.json", FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs))
            {
                var json = sr.ReadToEnd();
                var serializer = new JsonSerializer();

                var commandSets = JsonConvert.DeserializeObject<List<CommandSet>>(json);
                this.CommandSets = (from i in commandSets
                                    select i).ToDictionary<CommandSet, string>(i => $"{i.Type}_{i.Version}");
            }
            return this;
        }

        public Command GetJ808V2013Command(uint msgId)
        {
            return this.J808V2013CommandSet.GetCommand(msgId);
        }

        #region XmlImplement
        //private const string ConfigFilePath = "JTB/J808.xml";
        //private const string JTBCommandSetPath = "JTBCommand//CommandSet";
        //private const string CommandSetType = "J808";
        public void LoadXml()
        {
            //Xml
            //var doc = new XmlDocument();
            //doc.Load(ConfigFilePath);

            //var commandSets = doc.SelectNodes(CommandSetPath.J808);
            //foreach (XmlNode commandSet in commandSets)
            //{
            //    var command = XDocument.Parse(commandSet.OuterXml);
            //    //Console.WriteLine(command.ToString(SaveOptions.None));

            //    var builder = from i in command.Elements()
            //                  select new Command
            //                  {
            //                      Name = i.Attribute("Name").Value,
            //                      Fields = new List<Field>(),
            //                      //{
            //                      //    i.Elements("Field")
            //                      //}
            //                  };
            //    foreach (var cmd in builder)
            //    {
            //        Console.WriteLine($"Name: {cmd.Name}, Fields: {cmd.Fields.Count}");
            //    }
            //}

            //var doc = XDocument.Load(ConfigFilePath);
            //var j808CommandSet = from s in doc.Elements(JTBCommandSetPath)
            //                     where s.Attribute("Type").Value == CommandSetType
            //                     select s;
            //foreach (var commandSet in j808CommandSet)
            //{
            //    foreach (var command in commandSet.Elements("Field"))
            //    {
            //        Console.WriteLine(command.Attribute("Name"));
            //    }
            //}

            //Console.WriteLine(doc.Root);
        }

        //class Command
        //{
        //    public string Name { get; set; }
        //    public List<Field> Fields { get; set; }
        //}

        //class Field
        //{
        //    public Type Type { get; set; }
        //    public string Name { get; set; }
        //}
        #endregion
    }
}
