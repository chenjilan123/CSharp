using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharp.Helper
{
    public class FolderHelper
    {
        public void GetChangedFileInMyDocument()
        {
            var myDocumentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var query = from f in Directory.GetFiles(myDocumentPath)
                        let LastWriteTime = File.GetLastWriteTime(f)
                        where LastWriteTime > (DateTime.Now - TimeSpan.FromDays(70D))
                        orderby LastWriteTime ascending
                        select new { Path = f, LastWriteTime };
            foreach (var file in query)
            {
                Console.WriteLine(file.ToString());
            }

        }
    }
}
