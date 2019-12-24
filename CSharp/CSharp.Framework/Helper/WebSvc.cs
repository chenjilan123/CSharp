using CSharp.Framework.WebReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Framework.Helper
{
    public class WebSvc
    {
        public void Run()
        {
            BusinessOrderHttpService service = new BusinessOrderHttpService();
            service.transServices("s");
        }
    }
}
