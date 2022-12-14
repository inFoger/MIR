using MIR_Server;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
public class Program
{
    //поэтапно:
    //1.возможность передавать клиенту файлы [-]
    //2.сервер принимает файлы [-]
    //3.сервер отправляет обратно


    //Ожидает подключений на TCP-порты 10000 и 20000.
    //При подключении к серверу на TCP-порт 10000: сервер принимает файлы во внутренний буффер. Сервер запоминает имена и содержимое файлов
    //При подключении к серверу на TCP-порт 20000: сервер передаёт все файлы из буфера.
    static void Main(string[] args)
    {
        ServerStart();
    }

    static void ServerStart()
    {
        Socket reseiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            reseiveSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000));
            sendSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000));

            reseiveSocket.Listen(10); //10 - количество входящих подключений, которые могут быть поставлены в очередь сокета
            sendSocket.Listen(10);

            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            while (true)
            {
                Socket clientSendingSocket = reseiveSocket.Accept();
                Console.WriteLine("Чот подключилось");
                FileReceivingFromClient(clientSendingSocket);
                
                break; //потом убрать
            }

            //sendSocket.Accept();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void FileReceivingFromClient(Socket clientSendingSocket)
    {
        byte[] totalBytesAmount = new byte[FileDtoUtils.TotalBytesAmountSize];
        byte[] nameBytesAmount = new byte[FileDtoUtils.NameBytesAmountSize];

        clientSendingSocket.Receive(totalBytesAmount);
        clientSendingSocket.Receive(nameBytesAmount);

        int totalBytesAmountInt = BitConverter.ToInt32(totalBytesAmount);
        int nameBytesAmountInt = BitConverter.ToInt32(nameBytesAmount);

        byte[] nameBytes = new byte[nameBytesAmountInt];
        byte[] dataBytes = new byte[totalBytesAmountInt - nameBytesAmountInt];

        clientSendingSocket.Receive(nameBytes);
        clientSendingSocket.Receive(dataBytes);

        string name = Encoding.Unicode.GetString(nameBytes);
        Console.WriteLine("Название: " + name + "\nTotal bytes amount: " + totalBytesAmountInt);
        Console.WriteLine("Name bytes amount: " + nameBytesAmountInt + "\nData bytes: " + dataBytes);

        string filePath = "C:\\Users\\Anton\\source\\repos\\MIR\\MIR_project\\MIR_project\\SavedFiles\\" + name;
        BytesManagment.WriteBytesIntoFile(filePath, dataBytes);

        byte[] result = BytesManagment.GetFileDataBytes(filePath);
        Console.WriteLine(Encoding.UTF8.GetString(result));

        // закрываем сокет
        clientSendingSocket.Shutdown(SocketShutdown.Both);
        clientSendingSocket.Close();
    }

    static void FileSendingToClient(Socket clientSendingSocket)
    {
        //[TODO]Проверять существует ли файл с таким названием(сделать отдельный метод)
        string filePath = "C:\\Users\\Anton\\source\\repos\\MIR\\MIR_project\\MIR_project\\SavedFiles\\File.txt";
        FileDto fileDto = FileDtoUtils.CreateFileDto(filePath);
        clientSendingSocket.Send(fileDto.Serialize());
        Console.Write("Файл отправлен");

        clientSendingSocket.Shutdown(SocketShutdown.Both);
        clientSendingSocket.Close();
    }

}

/* код для подглядывания
 Socket reseiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            reseiveSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000));
            //sendSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000));

            reseiveSocket.Listen(10); //10 - количество входящих подключений, которые могут быть поставлены в очередь сокета
            //sendSocket.Listen(10);

            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            while (true)
            {
                Socket clientSendingSocket = reseiveSocket.Accept();
                Console.WriteLine("Чот подключилось");
                StringBuilder builder = new StringBuilder();
                int recievingBytes = 0; // количество полученых байтов
                byte[] data = new byte[256]; // буффер для полученных данных

                do
                {
                    recievingBytes = clientSendingSocket.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, recievingBytes));

                } while (clientSendingSocket.Available > 0);
                Console.WriteLine("Полученное сообщение: " + builder.ToString());

                // отправляем ответ
                string message = "ваше сообщение доставлено";
                data = Encoding.Unicode.GetBytes(message);
                clientSendingSocket.Send(data);
                // закрываем сокет
                clientSendingSocket.Shutdown(SocketShutdown.Both);
                clientSendingSocket.Close();
            }

            //sendSocket.Accept();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
 */