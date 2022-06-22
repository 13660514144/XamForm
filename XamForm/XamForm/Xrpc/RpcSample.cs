using BeetleX.XRPC.Clients;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamForm.Xrpc
{
   
    public class RpcSample
    {
        static XRPCClient Client;
        static IsampleCap ISampleCap;
        public string lastid;
        public string PageMothed;
        public JArray SearchField;
        public RpcSample()
        {
            lastid = string.Empty;
            PageMothed = "first";
            SearchField = new JArray() ;
            Client = new XRPCClient("124.223.82.154", 9090);
            Client.Options.ParameterFormater = new JsonPacket();//default messagepack
        }
        public async Task<string> GetMap()
        {            
            ISampleCap = Client.Create<IsampleCap>();
            var o = new {
                IdCode= "62412c5f83e3ebef97021241",
			    Role= "",
			    DelFlg= 1,
                GroupFlg= "",
                LastId= lastid,
			    PageNextOrPre= PageMothed,
                WhereCollection= SearchField,
			    rows= 10,
			    pages= 10
            };
            var Task = await ISampleCap.GetPage(JsonConvert.SerializeObject(o));        
            return Task;
        }
    }
    public interface IsampleCap
    {
        Task<string> GetPage(dynamic Obj);
    }
}
