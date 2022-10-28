using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIR_Server
{
    internal static class FileDtoUtils
    {
        public static int TotalBytesAmountSize = 4;
        public static int NameBytesAmountSize = 4;
        public static int GetDataBytes(int totalBytes, int nameBytes)
        {
            return totalBytes - nameBytes;
        }

        public static FileDto CreateFileDto(String fileName)
        {
            byte[] totalBytesAmount = new byte[FileDtoUtils.TotalBytesAmountSize];
            byte[] nameBytesAmount = new byte[FileDtoUtils.NameBytesAmountSize];
            byte[] fileNameBytes = BytesManagment.GetFileNameBytes(fileName);
            byte[] fileDataBytes = BytesManagment.GetFileDataBytes(fileName);
            totalBytesAmount = BitConverter.GetBytes(FileDtoUtils.TotalBytesAmountSize + FileDtoUtils.NameBytesAmountSize
                + fileNameBytes.Length + fileDataBytes.Length);
            nameBytesAmount = BitConverter.GetBytes(fileNameBytes.Length);

            return new FileDto(totalBytesAmount, nameBytesAmount, fileNameBytes, fileDataBytes);
        }

    }
}
