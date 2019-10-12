using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Xml.Linq;

namespace CSharp.Resource
{
    public class ImageResource
    {
        public void GetImage()
        {
            var manager = new ResourceManager(typeof(int));

            var assembly = typeof(ImageResource).Assembly;
            var resources = assembly.GetManifestResourceNames();
            Console.WriteLine("资源清单: ");
            foreach (var name in resources)
            {
                Console.WriteLine($"\t{name}");
            }

            Console.WriteLine();
            var resourceName = resources[0];
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                var xDoc = XDocument.Load(stream);
                Console.WriteLine($"资源名: {resourceName}");
                Console.WriteLine($"  内容: {xDoc.ToString(SaveOptions.DisableFormatting)}");
            }
        }
    }
}
