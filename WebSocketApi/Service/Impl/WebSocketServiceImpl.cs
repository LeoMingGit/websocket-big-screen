﻿using Fleck;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace WebSocketApi.Service.Impl
{

    /// <summary>
    /// 
    /// </summary>
    public class WebSocketServiceImpl : IWebSocketService
    {
        #region 单例
        private static object _objLock = new object();
        private static WebSocketServiceImpl _instance = null;

        public static WebSocketServiceImpl Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_objLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new WebSocketServiceImpl();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        ConcurrentDictionary<Guid, IWebSocketConnection> allSockets = new ConcurrentDictionary<Guid, IWebSocketConnection>();
        WebSocketServer sokcetServer = null;
        static object _lock = new object();

        public void Start(string ip,int port)
        {
            sokcetServer = new WebSocketServer($"ws://{ip}:" + port);
            sokcetServer.Start(socket =>
            {
                socket.OnOpen = () => DoOpen(socket);
                socket.OnClose = () => DoClose(socket);
                socket.OnMessage = (msg) => DoMessage(socket, msg);
                socket.OnBinary = (bs) => DoBinary(socket, bs);
                socket.OnError = (e) => DoError(socket, e);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        private void DoOpen(IWebSocketConnection socket)
        {

            allSockets.TryAdd(socket.ConnectionInfo.Id, socket);
            Console.WriteLine("\n[连接] " + "clientId:" + socket.ConnectionInfo.Id);
            Console.WriteLine("\n 当前连接数：" + allSockets.Count);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        private void DoClose(IWebSocketConnection socket)
        {
            IWebSocketConnection so = null;
            allSockets.TryRemove(socket.ConnectionInfo.Id, out so);
            Console.WriteLine("\n[关闭] " + "clientId:" + socket.ConnectionInfo.Id);
            Console.WriteLine("\n 当前连接数：" + allSockets.Count);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="e"></param>
        private void DoError(IWebSocketConnection socket, Exception e)
        {
            Console.WriteLine(e.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="bs"></param>
        private void DoBinary(IWebSocketConnection socket, byte[] bs)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="msg"></param>
        private void DoMessage(IWebSocketConnection socket, string msg)
        {
            Console.WriteLine(msg);
        }

        /// <summary>
        /// 推送数据
        /// </summary>
        /// <param name="data"></param>
        public void PushData(string data)
        {
            lock (_lock)
            {
                foreach (var item in allSockets)
                {
                    item.Value.Send(data);
                }
            }
        }


    }
}
