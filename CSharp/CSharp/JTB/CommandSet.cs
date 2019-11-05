using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.JTB
{
    public class CommandSet
    {
        public string Type { get; set; }
        public string Version { get; set; }
        public List<Command> Commands { get; set; }

        public Command GetCommand(uint msgId)
        {
            return Commands.Where(cmd => cmd.MsgId == msgId).FirstOrDefault();
        }
    }

    public class Command
    {
        public string Name { get; set; }
        public uint MsgId { get; set; }
        public List<Field> Fields { get; set; }
    }

    public class Field
    {
        public uint Length { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }

    }
}
