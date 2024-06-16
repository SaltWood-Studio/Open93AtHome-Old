using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Open93AtHome.Modules.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Open93AtHome
{
    public class CenterServer
    {
        internal DatabaseHandler databaseHandler;
        private WebApplication? application;

        public CenterServer()
        {
            this.databaseHandler = new DatabaseHandler();
        }

        public void Run()
        {
            Console.WriteLine($"Starting {nameof(Open93AtHome)} center server");

            InitializeService();

            Task appTask = application!.RunAsync();

            while (!(appTask.IsCompleted || appTask.IsCanceled || appTask.IsFaulted))
            {
                Thread.Sleep(1000);
            }

            //TODO: 启动监听线程
            //TODO: 阻塞主线程使得程序不会立刻退出
        }

        protected void InitializeService()
        {
            X509Certificate2 cert = null!;
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            builder.WebHost.UseKestrel(options =>
            {
                options.ListenAnyIP(9388);
            });
            application = builder.Build();

            var webSocketOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromMinutes(30)
            };

            application.UseWebSockets(webSocketOptions);

            application.MapGet("/400", async (context) =>
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Bad request");
            });
            application.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        HandleWebSocketConnection(webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                }
                else
                {
                    await next(context);
                }

            });
        }

        public void HandleWebSocketConnection(WebSocket webSocket)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    byte[] buffer = new byte[8];
                    await webSocket.ReceiveAsync(buffer, CancellationToken.None);

                    ulong length = Utils.ByteArrayToUInt64(buffer);

                    byte[] data = new byte[length];
                    await webSocket.ReceiveAsync(data, CancellationToken.None);

                    Console.WriteLine(Convert.ToHexString(data));
                    var message = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(data));

                    await webSocket.SendAsync(Encoding.UTF8.GetBytes(message?["message"].ToString()!), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }).Wait();
        }
    }
}
