using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ArcFaceTest.AFR
{
   public class AFRFunction
    {
       /**
        *Init Engine 
        */
       [System.Runtime.InteropServices.DllImportAttribute("libarcsoft_fsdk_face_recognition.dll", EntryPoint = "AFR_FSDK_InitialEngine", CallingConvention = CallingConvention.Cdecl)]
       public static extern int AFR_FSDK_InitialEngine(string AppId, string SDKKey,  System.IntPtr pMem, int lMemSize, ref System.IntPtr phEngine);


       [System.Runtime.InteropServices.DllImportAttribute("libarcsoft_fsdk_face_recognition.dll", EntryPoint = "AFR_FSDK_ExtractFRFeature", CallingConvention = CallingConvention.Cdecl)]
       public static extern int AFR_FSDK_ExtractFRFeature(System.IntPtr hEngine,  System.IntPtr pInputImage,  System.IntPtr pFaceRes, System.IntPtr pFaceModels);


       [System.Runtime.InteropServices.DllImportAttribute("libarcsoft_fsdk_face_recognition.dll", EntryPoint = "AFR_FSDK_FacePairMatching", CallingConvention = CallingConvention.Cdecl)]
       public static extern int AFR_FSDK_FacePairMatching(System.IntPtr hEngine,  System.IntPtr reffeature,  System.IntPtr probefeature, ref float pfSimilScore);

       [System.Runtime.InteropServices.DllImportAttribute("libarcsoft_fsdk_face_recognition.dll", EntryPoint = "AFR_FSDK_UninitialEngine", CallingConvention = CallingConvention.Cdecl)]
       public static extern int AFR_FSDK_UninitialEngine(System.IntPtr hEngine);

       [System.Runtime.InteropServices.DllImportAttribute("libarcsoft_fsdk_face_recognition.dll", EntryPoint = "AFR_FSDK_GetVersion", CallingConvention = CallingConvention.Cdecl)]
       public static extern System.IntPtr AFR_FSDK_GetVersion(System.IntPtr hEngine);

   
   }
}
