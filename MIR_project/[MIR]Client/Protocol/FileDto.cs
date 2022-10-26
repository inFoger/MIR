using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _MIR_Client
{
    internal class FileDto
    {
        private FileDto() { }

        public FileDto(byte[] totalBytesAmount, byte[] nameBytesAmount, byte[] name, byte[] data)
        {
            TotalBytesAmount = totalBytesAmount;
            NameBytesAmount = nameBytesAmount;
            Name = name;
            Data = data;
        }

        public byte[] TotalBytesAmount { get; } 

        public byte[] NameBytesAmount { get; }

        public byte[] Name { get; }

        public byte[] Data { get; }

        public byte[] Serialize()
        {
            return TotalBytesAmount.Concat(NameBytesAmount).Concat(Name).Concat(Data).ToArray();
        }

        public override string ToString()
        {
            return TotalBytesAmount.ToString() + NameBytesAmount.ToString() + Name.ToString() + Data.ToString();
        }

    }
}
