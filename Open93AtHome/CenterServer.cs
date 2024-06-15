using Open93AtHome.Modules.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open93AtHome
{
    public class CenterServer
    {
        internal DatabaseHandler databaseHandler;

        public CenterServer()
        {
            this.databaseHandler = new DatabaseHandler();
        }

        public void Run()
        {
            Console.WriteLine($"Starting {nameof(Open93AtHome)} center server");
            //TODO: 读取节点信息
            //TODO: 开启 Http 服务和 SocketIO
            //TODO: 启动监听线程
            //TODO: 阻塞主线程使得程序不会立刻退出
        }
    }
}
