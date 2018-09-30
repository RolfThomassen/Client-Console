using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Socket.Shared;

namespace Client_Console.Net.SocketV2
{
	public class ClientSocket
	{
		//private NetworkStream _networkStream;
		private TcpClient _clientSocket;
		private string _hostName;
		private int _port;

		public ClientSocket(string hostName, int port)
		{
			_hostName = hostName;
			_port = port;
			_clientSocket = new TcpClient();
		}

		/// <summary>
		/// Connects to the host
		/// </summary>
		/// <exception cref="POCSocketException"></exception>
		public void Connect()
		{
			try
			{
				_clientSocket.Connect(_hostName, _port);
				Console.WriteLine($"client connected!! {_hostName}:{_port}");

				var networkStream = _clientSocket.GetStream();

				var t = new Thread(o => ReceiveData((TcpClient)o));
				t.Start(_clientSocket);


				var s = string.Empty;
				while (!string.IsNullOrWhiteSpace((s = Console.ReadLine())))
				{
                    
					var buffer = Encoding.ASCII.GetBytes(s);
					networkStream.Write(buffer, 0, buffer.Length);
				}

				_clientSocket.Client.Shutdown(SocketShutdown.Send);
				t.Join();

				networkStream.Close();
				_clientSocket.Close();

				Console.WriteLine("disconnect from server!!");
				Console.ReadKey();
			}
			catch (SocketException socketEx)
			{
				throw new POCSocketException(socketEx.Message, socketEx);
			}
		}

		private void ReceiveData(TcpClient client)
		{
			var networkStream = client.GetStream();
			var receivedBytes = new byte[1024];
			int byteCount = 0;

            try
            {
                while ((byteCount = networkStream.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
                {
                    Console.WriteLine(Encoding.ASCII.GetString(receivedBytes, 0, byteCount));
                }
            }
            catch (System.IO.IOException IOE)
            {
                Console.WriteLine($"Error message:{IOE.Message}");

            }

        }
	}
}