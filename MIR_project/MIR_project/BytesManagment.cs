using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MIR_Server
{
    internal class BytesManagment
    {
        // знак вопроса после типа данных значит, что переменна может быть Null
        public static Byte[] GetFileNameBytes(String filePath)
        {
            return Encoding.Unicode.GetBytes(Path.GetFileName(filePath));
        }

        public static Byte[] GetFileDataBytes(String filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public static void WriteBytesIntoFile(String filePath, byte[] bytes)
        {
            File.WriteAllBytes(filePath, bytes);
        }

    }
}
