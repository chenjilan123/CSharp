using NJMSservice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.WebService
{
    public class CAMSservice
    {
        public async Task SendAsync()
        {
            var svr = new NJMSservice.ServiceSoapClient(NJMSservice.ServiceSoapClient.EndpointConfiguration.ServiceSoap);

            var msgs = new string[]
                {
                    "Truck_No1,Code1,JIN1,WEI1,Location1,2010-1-1 0:00:00,Describe1,Kind1,N,speed",
                };
            Console.WriteLine($"Send Msg: ");
            foreach (var msg in msgs)
            {
                Console.WriteLine("     " + msg);
            }
            var result = await svr.Insert_GpsOnway_infoAsync(msgs);
            Console.WriteLine($"Recv Msg: {result}");
        }
    }
}
