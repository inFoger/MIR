using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

internal class Solution1
{
    /*
    static async void Start()
    {
        IPAddress localAddress = IPAddress.Loopback;
        const int localPort = 7777;
        const string filename = "test.txt";

        var server = new TcpListener(localAddress, localPort);
        server.Start();

        while (true)
        {
            var client = await server.AcceptTcpClientAsync();
            _ = Task.Run(() => Serve(client, filename));
        }
    }
    static async Task Serve(TcpClient client, string filename)
    {
        using var _ = client;
        var stream = client.GetStream();
        using var file = File.OpenRead(filename);
        var length = file.Length;
        byte[] lengthBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(length));
        await stream.WriteAsync(lengthBytes);
        await file.CopyToAsync(stream);
    }
    */
}


