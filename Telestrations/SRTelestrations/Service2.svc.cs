using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Web.Routing;
using System.Reflection;

namespace SRTelestrations
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service2
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        private string UploadImage(string image, string id)
        {
            var imagesFolder = "C:\\Phil\\Telestrations\\Telestrations\\SRTelestrations\\Images";
            var fileName = this.GetImageName();
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }
            var fileNameWitPath = imagesFolder + "\\" + this.GetImageName();
            using (var fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    var data = Convert.FromBase64String(image);
                    bw.Write(data);
                    bw.Close();
                }
            }
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            ChatHub.GameManager.AddItemForPlayer(fileName, id);
            return "great";

        }

        private string GetImageName()
        {
            return DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".png"; 
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
