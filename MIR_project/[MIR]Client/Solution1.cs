using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

internal class Solution1
{
    //async void Start()
    //{
    //    IPAddress serverAddress = IPAddress.Loopback;
    //    const int serverPort = 7777;
    //    string filename = "test.txt";

    //    using var client = new TcpClient(serverAddress.ToString(), serverPort);
    //    var stream = client.GetStream();

    //    byte[] buf = new byte[65536];
    //    await ReadBytes(sizeof(long));
    //    long remainingLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buf, 0));

    //    using var file = File.Create(filename);
    //    while (remainingLength > 0)
    //    {
    //        int lengthToRead = (int)Math.Min(remainingLength, buf.Length);
    //        await ReadBytes(lengthToRead);
    //        await file.WriteAsync(buf, 0, lengthToRead);
    //        remainingLength -= lengthToRead;
    //    }
    //}
    //async Task ReadBytes(int howmuch)
    //{
    //    int readPos = 0;
    //    while (readPos < howmuch)
    //    {
    //        var actuallyRead = await Stream.ReadAsync(buf, readPos, howmuch - readPos);
    //        if (actuallyRead == 0)
    //            throw new EndOfStreamException();
    //        readPos += actuallyRead;
    //    }
    //}
}