using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SingletonExample
{
    class Program
    {

        static void Main(string[] args)
        {

            ParameterizedThreadStart ts = new ParameterizedThreadStart(EnterPlayer);

            for (int i = 0; i < 20; i++)
            {

                Thread t = new Thread(ts);

                t.Start("player" + i);

            }



            LoadBalanceServer.GetLoadBalanceServer().ShowServerInfo();

            Console.ReadKey();

        }



        static void EnterPlayer(object playerName)
        {

            LoadBalanceServer lbs = LoadBalanceServer.GetLoadBalanceServer();

            lbs.GetLobbyServer().EnterPlayer(playerName.ToString());

        }

    }



    class LoadBalanceServer
    {

        private const int SERVER_COUNT = 3;

        private List<LobbyServer> serverList = new List<LobbyServer>();



        private static volatile LoadBalanceServer lbs;

        private static object syncLock = new object();



        private LoadBalanceServer()
        {

            for (int i = 0; i < SERVER_COUNT; i++)
            {

                serverList.Add(new LobbyServer("LobbyServer" + i));

            }

        }



        public static LoadBalanceServer GetLoadBalanceServer()
        {

            if (lbs == null)
            {

                lock (syncLock)
                {

                    if (lbs == null)
                    {

                        Thread.Sleep(1000);

                        lbs = new LoadBalanceServer();

                    }

                }

            }

            return lbs;

        }



        public LobbyServer GetLobbyServer()
        {

            LobbyServer ls = serverList[0];

            for (int i = 1; i < SERVER_COUNT; i++)
            {

                if (serverList[i].PlayerList.Count < ls.PlayerList.Count)

                    ls = serverList[i];

            }

            return ls;

        }



        public void ShowServerInfo()
        {

            foreach (LobbyServer ls in serverList)
            {

                Console.WriteLine("=================" + ls.ServerName + "=================");

                foreach (string player in ls.PlayerList)
                {

                    Console.WriteLine(player);

                }

            }

        }

    }



    class LobbyServer
    {

        private List<string> playerList = new List<string>();



        public List<string> PlayerList
        {

            get { return playerList; }

        }



        private string serverName;



        public string ServerName
        {

            get { return serverName; }

        }



        public LobbyServer(string serverName)
        {

            this.serverName = serverName;

        }



        public void EnterPlayer(string playerName)
        {

            playerList.Add(playerName);

        }

    }

}

