using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Framework.Face.V2_2.Data
{
    public struct ASF_MultiFaceInfo
    {
        public IntPtr faceRect;
        public IntPtr faceOrient;
        public int faceNum;
        public IntPtr faceID;

        public List<MRECT> GetFaceArea()
        {
            if (faceNum == 0) return null;
            var lst = new List<MRECT>();
            for (int i = 0; i < faceNum; i++)
            {
                lst.Add(Marshal.PtrToStructure<MRECT>(faceRect + 16 * i));
            }
            return lst;
        }
        public void PrintInfo()
        {
            Console.WriteLine("脸部信息: ");
            Console.WriteLine($"\t个数: {faceNum}");
            var rectSize = Marshal.SizeOf<MRECT>();
            for (int i = 0; i < faceNum; i++)
            {
                var ptr = faceRect + rectSize * i;
                Console.WriteLine(ptr.ToString());
                var rect = Marshal.PtrToStructure<MRECT>(ptr);
                var orient = Marshal.PtrToStructure<int>(faceOrient + 4 * i);
                Console.WriteLine($"\t\tFace{i + 1} - Left: {rect.left}, Right: {rect.right}, Top: {rect.top}, Bottom: {rect.bottom}, Orient: {orient}");
            }

            {
                //测试溢出位
                Console.WriteLine("\t\t溢出位置: ");
                for (int i = faceNum; i < faceNum + 2; i++)
                {
                    var rect = Marshal.PtrToStructure<MRECT>(faceRect + rectSize * i);
                    var orient = Marshal.PtrToStructure<int>(faceOrient + 4 * i);
                    Console.WriteLine($"\t\tFace{i + 1} - Left: {rect.left}, Right: {rect.right}, Top: {rect.top}, Bottom: {rect.bottom}, Orient: {orient}");
                }
            }
        }
    }
}
