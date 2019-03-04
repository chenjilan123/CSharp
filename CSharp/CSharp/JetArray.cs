using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CSharp
{
    public class JetArray : KeyedCollection<string, Jet>
    {
        protected override string GetKeyForItem(Jet jet)
        {
            return jet.Code;
        }

        public JetArray Add(JetArray jetArr)
        {
            foreach (var jet in jetArr)
            {
                base.Add(jet);
            }
            return this;
        }
    }
}
