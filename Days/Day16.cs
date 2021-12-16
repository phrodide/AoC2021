using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day16
    {
        public class Packet
        {
            public int V { get; set; }
            public int T { get; set; }
            public ulong Literal { get; set; }
            public List<Packet> SubPackets { get; set; } = new List<Packet>();
        }
        public Day16()
        {

        }

        int GetNibbles(byte[] input, int bits, ref int offset, ref int nibbles)
        {
            int[] And = new int[] { 255, 1, 3, 7, 15, 31, 63, 127, 255 }; 
            int retVal = input[offset] & And[nibbles];
            if (nibbles >= bits)
            {
                retVal >>= (nibbles - bits);
                retVal &= And[bits];
                nibbles -= bits;
                if (nibbles==0)
                {
                    offset++;
                    nibbles = 8;
                }
                return retVal;
            }
            else
            {
                //we have to cross a byte boundary.
                retVal <<= 8;
                retVal += input[++offset] & And[0];
                retVal >>= (nibbles+8)-bits;
                retVal &= And[bits];
                nibbles = nibbles+8-bits;
                return retVal;
            }
        }

        ulong VersionAdd = 0;

        public string SolvePart1()
        {
            byte[] input = System.Convert.FromHexString(data);
            int offset = 0;
            int nibbles = 8;
            do
            {
                //get three nibbles
                var Packet = PacketParse(input, ref offset, ref nibbles);

            } while (offset < input.Length);

            return VersionAdd.ToString();
        }

        private Packet PacketParse(byte[] input, ref int offset, ref int nibbles, bool isPadded = true)
        {
            var V = GetNibbles(input, 3, ref offset, ref nibbles);
            var T = GetNibbles(input, 3, ref offset, ref nibbles);
            ulong value = 0;
            List<Packet> packets = new List<Packet>();
            switch (T)
            {
                case 4:
                    {
                        value = 0;
                        while (true)
                        {
                            var literal = GetNibbles(input, 5, ref offset, ref nibbles);
                            value <<= 4;
#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
                            value |= (ulong)(literal & 0x0f);
#pragma warning restore CS0675 // Bitwise-or operator used on a sign-extended operand
                            if ((literal&0x10)!=0x10)
                            {
                                break;
                            }
                        }
                    }
                    break;
                default:
                    {
                        var I = GetNibbles(input, 1, ref offset, ref nibbles);
                        if (I==1)
                        {
                            //11 bits
                            int len = (GetNibbles(input, 5, ref offset, ref nibbles) << 6) | GetNibbles(input, 6, ref offset, ref nibbles);
                            for (int i = 0; i < len; i++)
                            {
                                packets.Add(PacketParse(input, ref offset, ref nibbles, false));
                            }
                        }
                        else
                        {
                            //15 bits
                            int len = (GetNibbles(input, 5, ref offset, ref nibbles) << 10) | (GetNibbles(input, 5, ref offset, ref nibbles) << 5) | GetNibbles(input, 5, ref offset, ref nibbles);
                            int destOffset = ((len-nibbles) / 8) + offset + (((len-nibbles) % 8)!=0 ? 1 : 0) ;
                            do
                            {
                                packets.Add(PacketParse(input, ref offset, ref nibbles, false));
                            } 
                            while (destOffset > offset);
                            
                        }
                    }
                    break;
            }
            VersionAdd += (ulong)V;
            if (isPadded && nibbles != 8 && nibbles != 0)
                offset++;


            return new Packet
            {
                V = V,
                T = T,
                Literal = value,
                SubPackets = packets,

            };
        }

        public string SolvePart2()
        {
            byte[] input = Convert.FromHexString(data);
            int offset = 0;
            int nibbles = 8;
            var Packet = PacketParse(input, ref offset, ref nibbles);

            var result = PacketCompute(Packet);

            return result.ToString();
        }

        public ulong PacketCompute(Packet p)
        {
            switch (p.T)
            {
                case 0: return p.SubPackets.Select(sub => PacketCompute(sub)).Aggregate((result,item) => result + item);
                case 1: return p.SubPackets.Select(sub => PacketCompute(sub)).Aggregate((ulong)1, (result, item) => result * item);
                case 2: return p.SubPackets.Select(sub => PacketCompute(sub)).Min();
                case 3: return p.SubPackets.Select(sub => PacketCompute(sub)).Max();
                case 4: return (ulong)p.Literal;
                case 5: return (ulong)(PacketCompute(p.SubPackets[0]) > PacketCompute(p.SubPackets[1]) ? 1 : 0);
                case 6: return (ulong)(PacketCompute(p.SubPackets[0]) < PacketCompute(p.SubPackets[1]) ? 1 : 0);
                case 7: return (ulong)(PacketCompute(p.SubPackets[0]) == PacketCompute(p.SubPackets[1]) ? 1 : 0);
            }
            //should never reach here. T is 3 bits and all bits are handled above.
            return 0;
        }

        public static string tdata = @"9C0141080250320F1802104A08";

        public static string data = @"005173980232D7F50C740109F3B9F3F0005425D36565F202012CAC0170004262EC658B0200FC3A8AB0EA5FF331201507003710004262243F8F600086C378B7152529CB4981400B202D04C00C0028048095070038C00B50028C00C50030805D3700240049210021C00810038400A400688C00C3003E605A4A19A62D3E741480261B00464C9E6A5DF3A455999C2430E0054FCBE7260084F4B37B2D60034325DE114B66A3A4012E4FFC62801069839983820061A60EE7526781E513C8050D00042E34C24898000844608F70E840198DD152262801D382460164D9BCE14CC20C179F17200812785261CE484E5D85801A59FDA64976DB504008665EB65E97C52DCAA82803B1264604D342040109E802B09E13CBC22B040154CBE53F8015796D8A4B6C50C01787B800974B413A5990400B8CA6008CE22D003992F9A2BCD421F2C9CA889802506B40159FEE0065C8A6FCF66004C695008E6F7D1693BDAEAD2993A9FEE790B62872001F54A0AC7F9B2C959535EFD4426E98CC864801029F0D935B3005E64CA8012F9AD9ACB84CC67BDBF7DF4A70086739D648BF396BFF603377389587C62211006470B68021895FCFBC249BCDF2C8200C1803D1F21DC273007E3A4148CA4008746F8630D840219B9B7C9DFFD2C9A8478CD3F9A4974401A99D65BA0BC716007FA7BFE8B6C933C8BD4A139005B1E00AC9760A73BA229A87520C017E007C679824EDC95B732C9FB04B007873BCCC94E789A18C8E399841627F6CF3C50A0174A6676199ABDA5F4F92E752E63C911ACC01793A6FB2B84D0020526FD26F6402334F935802200087C3D8DD0E0401A8CF0A23A100A0B294CCF671E00A0002110823D4231007A0D4198EC40181E802924D3272BE70BD3D4C8A100A613B6AFB7481668024200D4188C108C401D89716A080";
    }
}
