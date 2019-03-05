using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class JetExtensionOptions
    {
        public int Id { get; set; }
        public double Weight { get; set; }

        public JetExtensionOptions()
        {
            this.Weight = 90.5;
            this.Id = 878;
        }
    }
}
