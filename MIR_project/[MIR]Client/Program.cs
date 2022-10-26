using MIR_Client;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    //Консольное приложение
    //Запускается с аргументами:
    //-ip=[ip_адрес]
    //-transimt="путь_к файлу_1" "путь_к_файлу_2" ... "путь_к_файлу_n"
    //-receive="путь_к_папке_в_которую_будут_записаны_файлы"
    //Если клиент запущен в режиме -transimt, то он подключается к серверу по указанному ip и порту 10000,
    //и передаёт указанные файлы, выводит сообщение пользователю об успехе и завершает работу.
    //В режиме -receive, клиент подключается к порту 20000 и принимает файлы от сервера в указанную папку,
    //выводит сообщение пользователю и завершает работу.
    public static void Main(string[] args)
    {
        Console.Write("Начало работы");
        Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            //подключаемся к удаленному хосту
            sendSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000));
            Console.Write("Соединение установлено");

            string filePath = "C:\\Users\\Anton\\source\\repos\\MIR_project\\[MIR]Client\\File.txt";
            SendFileOverSocket(sendSocket, filePath);
            Console.Write("Файл отправлен");

            sendSocket.Shutdown(SocketShutdown.Both);
            sendSocket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        Console.Write("Завершение работы");
    }

    public static void SendFileOverSocket(Socket socket, String fileName)
    {
        //[TODO]Проверять существует ли файл с таким названием(сделать отдельный метод)
        FileDto fileDto = FileDtoUtils.CreateFileDto(fileName);
        socket.Send(fileDto.Serialize());
    }
    
}
/* код для подглядывания 
public static void Main(string[] args)
{
    //Socket reseiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    try
    {
        //подключаемся к удаленному хосту
        //reseiveSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000));
        sendSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000));
        Console.Write("Введите сообщение:");
        string message = Console.ReadLine();
        byte[] data = Encoding.Unicode.GetBytes(message);
        sendSocket.Send(data);

        // получаем ответ
        data = new byte[256]; // буфер для ответа
        StringBuilder builder = new StringBuilder();
        int bytes = 0; // количество полученных байт

        do
        {
            bytes = sendSocket.Receive(data, data.Length, 0);
            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
        }
        while (sendSocket.Available > 0);
        Console.WriteLine("ответ сервера: " + builder.ToString());

        // закрываем сокет
        sendSocket.Shutdown(SocketShutdown.Both);
        sendSocket.Close();

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}
*/