using Open93AtHome.Modules.Database;

namespace Open93AtHome
{
    public class Program
    {
        static void Main(string[] args)
        {
            CenterServer server = new CenterServer();
            server.Run();
        }
    }
}
