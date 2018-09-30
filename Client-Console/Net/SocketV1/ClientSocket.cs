//using System;
//using System.Net.Sockets;
//using System.Text;
//using Socket.Shared;

//namespace Client_Console.Net.SocketV1
//{
//	public class ClientSocket
//	{
//		private NetworkStream _networkStream;
//		private TcpClient _clientSocket;
//		private string _hostName;
//		private int _port;

//		public ClientSocket(string hostName, int port)
//		{
//			_hostName = hostName;
//			_port = port;
//			_clientSocket = new TcpClient();
//		}

//		/// <summary>
//		/// Connects to the host
//		/// </summary>
//		/// <exception cref="POCSocketException"></exception>
//		public void Connect()
//		{
//			try
//			{
//				_clientSocket.Connect(_hostName, _port);
//			}
//			catch (SocketException socketEx)
//			{
//				throw new POCSocketException(socketEx.Message, socketEx);
//			}
//		}

//		public void Send()
//		{
//			var serverStream = _clientSocket.GetStream();
//			var outStream = Encoding.ASCII.GetBytes("Message from Client$");
//			serverStream.Write(outStream, 0, outStream.Length);
//			serverStream.Flush();

//			var inStream = new byte[10025];
//			serverStream.Read(inStream, 0, _clientSocket.ReceiveBufferSize);

//			var returnData = Encoding.ASCII.GetString(inStream);

//			Console.WriteLine("Data from Server: " + returnData);
//		}
//	}
//}