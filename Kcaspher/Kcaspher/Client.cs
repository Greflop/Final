﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Projet_2._0;

namespace Kcaspher
{
    public class Client
    {
        Socket server_socket;

        StreamWriter servWriter;
        StreamReader servReader;
        public string pos2;//, pos2x, pos2y;
//        public Queue<string> pos2q;

        public string s;

        public bool Connected { get { return server_socket.Connected; } }

        public Client()
        {
            server_socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            pos2 = "10";
  //          pos2q = new Queue<string>();
        }

        public void Connect(string host, int port, string name)
        {
            try
            {
                server_socket.Connect(host, port);
            }
            catch (SocketException)
            {
                Console.WriteLine("Connexion to server failed");
                return;
            }


            servWriter = new StreamWriter(new NetworkStream(server_socket));
            servReader = new StreamReader(new NetworkStream(server_socket));


            servWriter.WriteLine(name);
            servWriter.Flush();
            string confirm = servReader.ReadLine();
            if (confirm.CompareTo("Bienvenue") == 0)
                Console.WriteLine("Confirmation  : " + confirm);
            else
            {
                Close();
                server_socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
                Console.WriteLine("You're blacklisted, get lost");
            }
        }

        public void Run()
        {
            Thread reader = new Thread(new ThreadStart(Read));
            Thread writer = new Thread(new ThreadStart(Write));
            reader.Start();
            writer.Start();

            reader.Join();
            writer.Join();
            Close();
        }

        private void Read()
        {
            while (true)
            {
                if (server_socket.Poll(10, SelectMode.SelectRead))
                {
                    Console.WriteLine(pos2);//servReader.ReadLine());
                    pos2 = servReader.ReadLine();
                    //if (!pos2.Contains("error"))
    //                pos2q.Enqueue(pos2);
                }
                // recoie pos
            }
        }

        private void Write()
        {
            while (true)
            {
                s = Game1.GetGame().posClient(); // envoie de pos
                servWriter.WriteLine(s);
                servWriter.Flush();
            }
        }

        private void Close()
        {
            servWriter.Flush();
            servWriter.Close();
            servReader.Close();
            server_socket.Close();
        }
    }
}
