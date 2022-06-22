
using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;
using Xamarin.Forms;
using XamForm.Class;
using XamForm.Views;

namespace XamForm.WsSocket
{
    public class WsSocketMethod
    {
        public WebSocket4Net.WebSocket _webSocket = null;
        public bool Flg = false;
        public string MachineIp = string.Empty;
        public WsSocketMethod()
        {
            
        }
        public void WsIint()
        {
            MachineIp = NetWork.GetIp();
            WebSocketInit();
            Device.StartTimer(TimeSpan.FromMilliseconds(30000.0), TimerElapsed);           
        }
        public async Task WebSocketInit()
        {
            try
            {
                string Ip = "124.223.82.154";
                int Port = 24220;
                string Url = $"ws://{Ip}:{Port}";
                _webSocket = new WebSocket4Net.WebSocket(Url);
                _webSocket.Opened += WebSocket_Opened;
                _webSocket.Error += WebSocket_Error;
                _webSocket.Closed += WebSocket_Closed;
                _webSocket.MessageReceived += WebSocket_MessageReceived;
                bool Flg = await Start();
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new object(), "ErrMsg", ex.Message.ToString());
            }
        }
        /// <summary>
        /// 连接方法
        /// <returns></returns>
        public async Task<bool> Start()
        {
            bool result = true;
            try
            {
                result = await _webSocket.OpenAsync();
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Socket关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebSocket_Closed(object sender, EventArgs e)
        {
            //_logger.LogInformation("Socket关闭事件");
            
        }
        /// <summary>
        /// Socket报错事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebSocket_Error(object sender, ErrorEventArgs e)
        {
            //MessagingCenter.Send(new object(), "ErrMsg", e.ToString());
        }
        /// <summary>
        /// Socket打开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebSocket_Opened(object sender, EventArgs e)
        {
            try
            {
                Thread.Sleep(1000);
                SendClientMsg("ReturnClientModel","app");
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new object(), "ErrMsg", ex.Message.ToString());
            }
        }
        private void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
           // _logger.LogInformation($"客户端:收到数据:{e.Message}");
           // DataChange(e.Message);
        }

        private void SendClientMsg(string RMothed,string DeviceType)
        {
            Wsmodel ws = new Wsmodel();
            ws.ConnectId = string.Empty;
            ws.ClientIp = MachineIp;
            ws.ClientPort = "242*";
            ws.DeviceType = DeviceType;
            var obj = new {
                Mothed = "ReturnClientModel",
                Data = ws
            };
            _webSocket.Send(JsonConvert.SerializeObject(obj));
        }
        public class Wsmodel
        {
            public string ConnectId { get; set; }
            public string ClientIp { get; set; }
            public string ClientPort { get; set; }
            public string DeviceType { get; set; }
        }
        private bool TimerElapsed()
        {
            
            Device.BeginInvokeOnMainThread(()=> {
                try
                {
                    if (_webSocket.State != WebSocket4Net.WebSocketState.Open &&
                        _webSocket.State != WebSocket4Net.WebSocketState.Connecting)
                    {
                        _webSocket.Close();
                        _webSocket.Open();
                        Flg = true;
                    }
                    else
                    {
                        SendClientMsg("Heart", "app");
                    }
                }
                catch (Exception ex)
                {
                    Flg = false;
                }
            });            
            return Flg;
        }
    }
}
