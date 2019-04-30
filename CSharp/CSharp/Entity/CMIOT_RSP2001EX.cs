namespace CSharp.Entity
{
    public class CMIOT_RSP2001EX
    {
        public CMIOT_RST2001EX[] result { get; set; }
    }

    public class CMIOT_RST2001EX
    {
        public string msisdn { get; set; }
        public string GPRSSTATUS { get; set; }
    }
}
