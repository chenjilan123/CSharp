using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Linq;

namespace CSharp.File
{
    public class ImageFormatSave
    {
        private readonly List<string> imgs = new List<string>()
        {
            "0_13639545731_1_191024142237949.png",
            "贵J29172_191024160509.jpg",
            "贵JT0135_191024160453.jpg",
        };

        public void SaveImage()
        {
            //for (int i = 0; i < imgs.Count; i++)
            //{
            //    var sPath = $@"C:\Users\11\Desktop\贵州\图片\{imgs[i]}";
            //    Image img = Image.FromFile(sPath);
            //    if (img == null)
            //        return;
            //    img.Save($"img{i}.jpg");
            //}

            //var path = $@"C:\Users\11\Desktop\贵州\图片\png.txt";
            //var path = $@"png.txt";
            var path = $@"C:\Users\11\Desktop\贵州\图片\{imgs[0]}";
            path = SaveData(path);
            //SaveImage(path);
        }

        private void SaveImage(string txtPath)
        {
            const string savePath = "img1001.png";
            using (var sr = new StreamReader(txtPath))
            {
                var img = Image.FromStream(sr.BaseStream);
                img.Save(savePath);
            }
        }

        private string SaveData(string dataPath)
        {
            const string savePath = "img10011.jpg";
            using (Stream stream = new FileStream(dataPath, FileMode.Open))
            using (var tr = new StreamReader(stream))
            {
                //var s = tr.ReadToEnd();
                var s = ImageFileInfo.File1;
                //Console.WriteLine(s);
                List<char> c = new List<char>();
                s = s.TrimStart('0', 'x', 'X');
                var data = s.ToCharArray().Aggregate<char, List<byte>>(new List<byte>(), (prev, cur) =>
                {
                    c.Add(cur);
                    if (c.Count >= 2)
                    {
                        var curS = c[0].ToString() + c[1].ToString();
                        try
                        {
                            prev.Add(byte.Parse(curS, System.Globalization.NumberStyles.HexNumber));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        c.Clear();
                    }
                    return prev;
                });

                using (var fs = new FileStream(savePath, FileMode.OpenOrCreate))
                {
                    fs.Write(data.ToArray(), 0, data.Count);
                }
            }
            return savePath;
        }
    }
}
