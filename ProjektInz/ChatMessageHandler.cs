using ProjektInz.Data.DeserializeOnModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInz
{
    public class HubMessageHandler : WebSocketHandler
    {
        private bool _dataSaved = false;
        public HubMessageHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketId = WebSocketConnectionManager.GetId(socket);
            await SendMessageToAllAsync($"{socketId} is now connected");
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            var message = $"{ Encoding.UTF8.GetString(buffer, 0, result.Count)} ";
            //Warn... if connection closed would be problem...
            if ((DateTime.Now.Minute % 15 == 0) && message.StartsWith("{") && (_dataSaved == false))
                //second condition validate json answer
                //last condition checks - if u added one ex. 15:45 and second 01 dont do the same at 15:45 and 55 second 
            {
                AddSensorReadToList(message, socketId);
                _dataSaved = true;
            }
            else if((DateTime.Now.Minute % 15) !=0)
            {
                _dataSaved = false;
            }
            await SendMessageToAllAsync(message);
        }//TODO:make a flashing red / blue LED when sending data...

        void AddSensorReadToList(string message, string fromWhat)
        {
            ToSensorReadObject.Deserialize(message, fromWhat);
        }
    }
}
