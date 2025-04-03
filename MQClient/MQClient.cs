// MQClient.cs - Versión corregida
using System;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MQClient
{
    public class MessageQueueClient : IDisposable
    {
        private TcpClient _tcpClient;
        private StreamReader _reader;
        private StreamWriter _writer;
        private readonly object _lock = new();
        private bool _disposed = false;
        public Guid AppID { get; } // Añadido para almacenar el AppID

        public MessageQueueClient(string ip, int port, Guid appID)
        {
            AppID = appID;
            try
            {
                Console.WriteLine($"Intentando conectar a {ip}:{port}...");
                _tcpClient = new TcpClient();
                Connect(ip, port);

                if (_tcpClient.Connected)
                {
                    Console.WriteLine("Conexión establecida con el servidor.");
                    NetworkStream stream = _tcpClient.GetStream();
                    _reader = new StreamReader(stream, Encoding.UTF8);
                    _writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                }
                else
                {
                    throw new InvalidOperationException("No se pudo conectar al servidor");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar: {ex.Message}");
                throw;
            }
        }


        public bool IsConnected()
        {
            return _tcpClient?.Client != null &&
                   _tcpClient.Client.Connected &&
                   _tcpClient.Client.Poll(100, SelectMode.SelectRead);
        }

        private void Connect(string ip, int port, int timeout = 5000)
        {
            _tcpClient = new TcpClient();
            try
            {
                var result = _tcpClient.BeginConnect(ip, port, null, null);
                if (!result.AsyncWaitHandle.WaitOne(timeout))
                    throw new TimeoutException("Tiempo de conexión agotado");

                NetworkStream stream = _tcpClient.GetStream();
                _reader = new StreamReader(stream, Encoding.UTF8);
                _writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public string SendRequest(string request)
        {
            lock (_lock)
            {
                if (_disposed || !(_tcpClient?.Connected ?? false))

                    throw new InvalidOperationException("Cliente no conectado o ya cerrado");

                try
                {
                    _writer.WriteLine(request);
                    return _reader.ReadLine() ?? throw new IOException("Respuesta nula del servidor");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error en comunicación con el servidor: {ex.Message}");
                    return "ERROR|Servidor no disponible";
                }

            }
        }
        

        private void Reconnect()
        {
            if (_tcpClient.Client.RemoteEndPoint is not System.Net.IPEndPoint remoteEndPoint)
                throw new InvalidOperationException("No se puede reconectar: endpoint desconocido");

            DisposeResources();
            Connect(remoteEndPoint.Address.ToString(), remoteEndPoint.Port);
        }

        private void DisposeResources()
        {
            _reader?.Dispose();
            _writer?.Dispose();
            _tcpClient?.Dispose();
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_disposed) return;
                _disposed = true;
                DisposeResources();
            }
        }
    }
}
