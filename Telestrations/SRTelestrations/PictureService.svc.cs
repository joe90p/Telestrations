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
using PictureLink.GameLogic;

namespace PictureLink.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PictureService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string UploadImage(string image, string id)
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
            var context = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
            var player = new Player(id);
            var guess = new Guess(player, fileName);
            GameHub.GameManager.AddGuess(guess);
            return "great";

        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public IPlaySession GetPlaySession(string id)
        {
            var player = new Player(id);
            var session = GameHub.GameManager.GetPlaySession(player);
            return session;
        }

        private string GetImageName()
        {
            return DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".png"; 
        }
    }
}
