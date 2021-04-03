using System;

namespace DES
{
    static class calc
    {
        public static void PrintBin(int[] hex)
        {
            string strhex = "";
            string strbin;
            for (int i = 0; i < hex.Length; i++)
                strhex += Convert.ToString(hex[i]);
            strbin = ToBin(strhex);
            Console.Write(strbin);
        }
        public static void PrintBin(string hex)
        {
            string strbin;
            strbin = ToBin(hex);
            Console.Write(strbin);
        }
        public static void PrintHex(int[] bin)
        {
            string strbin = "";
            string strhex;
            for (int i = 0; i < bin.Length; i++)
                strbin += Convert.ToString(bin[i]);
            strhex = ToHex(strbin);
            Console.Write(strhex);
        }
        public static void PrintHex(string bin)
        {
            string strhex;
            strhex = ToHex(bin);
            Console.Write(strhex);
        }
        public static string ToHex(string str2)
        {
            int n = str2.Length;
            string strhex = "";
            for (int i = 0; i < str2.Length / 4; i++)
            {
                switch (str2.Substring(i * 4, 4))
                {
                    case "0000":
                        strhex += "0";
                        break;
                    case "0001":
                        strhex += "1";
                        break;
                    case "0010":
                        strhex += "2";
                        break;
                    case "0011":
                        strhex += "3";
                        break;
                    case "0100":
                        strhex += "4";
                        break;
                    case "0101":
                        strhex += "5";
                        break;
                    case "0110":
                        strhex += "6";
                        break;
                    case "0111":
                        strhex += "7";
                        break;
                    case "1000":
                        strhex += "8";
                        break;
                    case "1001":
                        strhex += "9";
                        break;
                    case "1010":
                        strhex += "A";
                        break;
                    case "1011":
                        strhex += "B";
                        break;
                    case "1100":
                        strhex += "C";
                        break;
                    case "1101":
                        strhex += "D";
                        break;
                    case "1110":
                        strhex += "E";
                        break;
                    case "1111":
                        strhex += "F";
                        break;
                }
            }
            return strhex;
        }
        public static int[] Shift(int[] bin, int n)
        {
            int tmp;
            for (int i = 0; i < n; i++)
            {
                tmp = bin[0];
                for (int j = 0; j < bin.Length - 1; j++)
                {
                    bin[j] = bin[j + 1];
                }
                bin[bin.Length - 1] = tmp;
            }
            return bin;
        }
        public static string ToBin(string str16)
        {
            int n = str16.Length;
            string strbin = "";
            for (int i = 0; i < n; i++)
            {
                switch (str16[i])
                {
                    case '0':
                        strbin += "0000";
                        break;
                    case '1':
                        strbin += "0001";
                        break;
                    case '2':
                        strbin += "0010";
                        break;
                    case '3':
                        strbin += "0011";
                        break;
                    case '4':
                        strbin += "0100";
                        break;
                    case '5':
                        strbin += "0101";
                        break;
                    case '6':
                        strbin += "0110";
                        break;
                    case '7':
                        strbin += "0111";
                        break;
                    case '8':
                        strbin += "1000";
                        break;
                    case '9':
                        strbin += "1001";
                        break;
                    case 'A':
                        strbin += "1010";
                        break;
                    case 'B':
                        strbin += "1011";
                        break;
                    case 'C':
                        strbin += "1100";
                        break;
                    case 'D':
                        strbin += "1101";
                        break;
                    case 'E':
                        strbin += "1110";
                        break;
                    case 'F':
                        strbin += "1111";
                        break;
                }

            }
            return strbin;
        }
    }
    class DES
    {
        string key;
        string plaintext;
        int[] plaintextBin = new int[64];
        static int[] IP = GetIP();
        static int[] EP = GetEP();
        static int[] P = GetPermutation();
        static int[] InvIP = GetInvIP();
        int[] cyperBin;
        string[] roundKeys;
        public DES(string key, string plaintext)
        {
            string bintext;
            this.key = key;
            this.plaintext = plaintext;
            bintext = calc.ToBin(plaintext);
            for (int i = 0; i < 64; i++)
                plaintextBin[i] = Convert.ToInt32(bintext[i]) - 48 ;
            roundKeys = GetRoundKeys(key);
        }
        public int[] SBox(int[] xor)
        {
            int[] sbox = new int[32];
            string sboxstring="";
            int row, col;
            int tmp=0;
            int[,] s1 = new int[4, 16]{{14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7 },
        { 0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8 },
        { 4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0 },
        { 15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13}};
            int[,] s2 = new int[4, 16]{{15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10 },
        { 3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5 },
        { 0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15 },
        { 13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9 }
            };
            int[,] s3 = new int[4, 16]{{10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8 },
        { 13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1 },
        { 13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7},
        { 1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12}
            };
            int[,] s4 = new int[4, 16]{{7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15 },
        { 13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9 },
        { 10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4},
        { 3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14} };
            int[,] s5 = new int[4, 16]{{2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9 },
        { 14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6 },
        { 4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14},
        { 11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3} };
            int[,] s6 = new int[4, 16] { {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
        { 10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8 },
        { 9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6},
        { 4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13} };
            int[,] s7 = new int[4, 16]{{4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1 },
        { 13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6 },
        { 1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2},
        { 6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12} };
            int[,] s8 = new int[4, 16] {{ 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
        { 1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2},
        { 7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8},
        { 2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11}};
            for(int i = 0; i < 8; i++)
            {
                row = xor[i * 6] * 2 + xor[i * 6 + 5];
                col = xor[i * 6 + 1] * 8 + xor[i * 6 + 2] * 4 + xor[i * 6 + 3] * 2 + xor[i * 6 + 4];
                switch (i)
                {
                    case 0: tmp = s1[row, col]; break;
                    case 1: tmp = s2[row, col]; break;
                    case 2: tmp = s3[row, col]; break;
                    case 3: tmp = s4[row, col]; break;
                    case 4: tmp = s5[row, col]; break;
                    case 5: tmp = s6[row, col]; break;
                    case 6: tmp = s7[row, col]; break;
                    case 7: tmp = s8[row, col]; break;
                }
                switch (tmp)
                {
                    case 0: sboxstring += "0000"; break;
                    case 1: sboxstring += "0001"; break;
                    case 2: sboxstring += "0010"; break;
                    case 3: sboxstring += "0011"; break;
                    case 4: sboxstring += "0100"; break;
                    case 5: sboxstring += "0101"; break;
                    case 6: sboxstring += "0110"; break;
                    case 7: sboxstring += "0111"; break;
                    case 8: sboxstring += "1000"; break;
                    case 9: sboxstring += "1001"; break;
                    case 10: sboxstring += "1010"; break;
                    case 11: sboxstring += "1011"; break;
                    case 12: sboxstring += "1100"; break;
                    case 13: sboxstring += "1101"; break;
                    case 14: sboxstring += "1110"; break;
                    case 15: sboxstring += "1111"; break;
                }
            }
            for (int i = 0; i < 32; i++)
                sbox[i] = Convert.ToInt32(sboxstring[i])-48;
            return sbox;
        }
        public void Encrypction()
        {
            int[] left = new int[32], right = new int[32], nextleft = new int[32], xor48 = new int[48], pright = new int[32], xor32 = new int[32], sbox, encrypted = new int[64];
            Console.WriteLine("-----------------Encrypction-----------------");
            Console.WriteLine("             LEFT       RIGHT      ROUNDKEY");

            for (int i = 0; i < IP.Length; i++) //IP
            {
                if (i < 32)
                {
                    left[i] = plaintextBin[IP[i] - 1];
                }
                else
                {
                    right[i - 32] = plaintextBin[IP[i] - 1];
                    nextleft[i - 32] = plaintextBin[IP[i] - 1];
                }
            }
            for (int r = 0; r < 16; r++)
            {
                for (int i = 0; i < EP.Length; i++) //EP, XOR
                    xor48[i] = (Convert.ToInt32(roundKeys[r][i])-48 == right[EP[i] - 1]) ? 0 : 1;
                sbox = SBox(xor48);
                for (int i = 0; i < P.Length; i++)
                    pright[i] = sbox[P[i] - 1];
                for (int i = 0; i < left.Length; i++)
                    xor32[i] = (left[i] == pright[i]) ? 0 : 1;
                Console.Write("{0,2} round | ",r+1);
                calc.PrintHex(nextleft);
                Console.Write(" | ");
                calc.PrintHex(xor32);
                Console.Write(" | ");
                calc.PrintHex(roundKeys[r]);
                Console.WriteLine();
                right = (int[])xor32.Clone();
                left = (int[])nextleft.Clone();
                nextleft = (int[])xor32.Clone();
            }
            Console.Write("   Swap  | ");
            calc.PrintHex(right);
            Console.Write(" | ");
            calc.PrintHex(left);
            Console.WriteLine();
            for (int i = 0; i < InvIP.Length; i++)
                encrypted[i] = InvIP[i] > 32 ? left[InvIP[i]-33] : right[InvIP[i]-1];
            Console.WriteLine("Complete.");
            Console.WriteLine("             Hex                Binary");
            Console.Write("Plain Text : {0} | ", plaintext);
            calc.PrintBin(plaintext);
            Console.WriteLine();
            Console.Write("Key        : {0} | ", key);
            calc.PrintBin(key);
            Console.WriteLine();
            Console.Write("Cyper Text : ");
            calc.PrintHex(encrypted);
            Console.Write(" | ");
            for (int i = 0; i < encrypted.Length; i++)
                Console.Write(encrypted[i]);
            Console.WriteLine();
            cyperBin = (int[])encrypted.Clone();
        }
        public void Decrypction()
        {
            int[] left = new int[32], right = new int[32], nextleft = new int[32], xor48 = new int[48], pright = new int[32], xor32 = new int[32], sbox, decrypted = new int[64];
            Console.WriteLine();
            Console.WriteLine("-----------------Decrypction-----------------");
            Console.WriteLine("             LEFT       RIGHT      ROUNDKEY");

            for (int i = 0; i < InvIP.Length; i++) //IP
            {
                if (i < 32)
                {
                    left[i] = cyperBin[IP[i] - 1];
                }
                else
                {
                    right[i-32] = cyperBin[IP[i] - 1];
                    nextleft[i-32] = cyperBin[IP[i] - 1];
                }
            }
            for (int r = 0; r < 16; r++)
            {
                for (int i = 0; i < EP.Length; i++) //EP, XOR
                    xor48[i] = (Convert.ToInt32(roundKeys[15-r][i]) - 48 == right[EP[i] - 1]) ? 0 : 1;
                sbox = SBox(xor48);
                for (int i = 0; i < P.Length; i++)
                    pright[i] = sbox[P[i] - 1];
                for (int i = 0; i < left.Length; i++)
                    xor32[i] = (left[i] == pright[i]) ? 0 : 1;
                Console.Write("{0,2} round | ", r + 1);
                calc.PrintHex(nextleft);
                Console.Write(" | ");
                calc.PrintHex(xor32);
                Console.Write(" | ");
                calc.PrintHex(roundKeys[15-r]);
                Console.WriteLine();
                right = (int[])xor32.Clone();
                left = (int[])nextleft.Clone();
                nextleft = (int[])xor32.Clone();
            }
            Console.Write("   Swap  | ");
            calc.PrintHex(right);
            Console.Write(" | ");
            calc.PrintHex(left);
            Console.WriteLine();
            for (int i = 0; i < InvIP.Length; i++)
                decrypted[i] = InvIP[i] > 32 ? left[InvIP[i] - 33] : right[InvIP[i] - 1];
            Console.WriteLine("Complete.");
            Console.WriteLine("             Hex                Binary");
            Console.Write("Cyper Text : ");
            calc.PrintHex(cyperBin);
            Console.Write(" | ");
            for (int i = 0; i < decrypted.Length; i++)
                Console.Write(decrypted[i]);
            Console.WriteLine();
            Console.Write("Key        : {0} | ", key);
            calc.PrintBin(key);
            Console.WriteLine();
            Console.Write("Decrypted  : ");
            calc.PrintHex(decrypted);
            Console.Write(" | ");
            for (int i = 0; i < decrypted.Length; i++)
                Console.Write(decrypted[i]);
            Console.WriteLine();
            Console.Write("Plain Text : {0} | ", plaintext);
            calc.PrintBin(plaintext);
            Console.WriteLine();
        }
        public static int[] GetPermutation()
        {
            int[] P = {16 ,07, 20, 21 ,29 ,12, 28 ,17 ,01 ,15 ,23 ,26, 05 ,18 ,31, 10,
02 ,08 ,24 ,14, 32, 27, 03, 09, 19 ,13 ,30, 06, 22 ,11, 04, 25 };
            return P;
        }
        public static string[] GetRoundKeys(string key)
        {
            string keytmp = "";
            string strkey2 = calc.ToBin(key);
            int[] k = new int[64]; //이진수 키를 배열로 저장
            int[] schedule = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
            for(int i = 0; i < 64; i++)
                k[i] = strkey2[i];
            //init key
            int[] init = {57, 49, 41, 33, 25, 17, 9,
                1, 58, 50, 42, 34, 26, 18,
                10, 2, 59, 51, 43, 35, 27,
                19, 11, 3, 60, 52, 44, 36,
                63, 55, 47, 39, 31, 23, 15,
                7, 62, 54, 46, 38, 30, 22,
                14, 6, 61, 53, 45, 37, 29,
                21, 13, 5, 28, 20, 12, 4};
            int[] comp = {14,17,11,24, 01, 05, 03, 28, 15, 06 ,21 ,10,
            41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48,
            23, 19, 12, 04, 26, 08, 16, 07, 27, 20, 13, 02,
            44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32,
            };
            int[,] ikey = new int[16,58];
            int[] initleft = new int[28], initright = new int[28];
            int[] compkey = new int[48];
            int[] left;
            int[] right;
            string[] keys = new string[16];
            for(int i = 0; i < init.Length; i++)
            {
                if(i<28)
                    initleft[i] = k[init[i]-1];
                else
                    initright[i-28] = k[init[i]-1];
            }
            for (int i = 0; i < 16; i++)
            {
                left = calc.Shift(initleft, schedule[i]);
                right = calc.Shift(initright, schedule[i]);
                for (int m = 0; m < 56; m++)
                {
                    ikey[i, m] = m < 28 ? left[m] : right[m - 28];
                }
            }
            string t;
            for (int r = 0; r < 16; r++)
            {
                keytmp = "";
                for (int i = 0; i < comp.Length; i++)
                {
                    compkey[i] = ikey[r, comp[i] - 1];
                    t = Convert.ToString(compkey[i] - 48);
                    keytmp += t;
                }
                keys[r] = keytmp[0..12] + keytmp[24..36] + keytmp[12..24] + keytmp[36..48];
            }
            
            return keys;
        }
        public static int[] GetEP()
        {
            int[] EP = {32, 1, 2, 3 ,4 ,5 ,4 ,5 ,6 ,7 ,8 ,9,
                        8, 9, 10 ,11, 12, 13, 12 ,13 ,14 ,15, 16, 17,
                        16 ,17 ,18 ,19, 20, 21 ,20, 21, 22 ,23, 24, 25,
                        24, 25 ,26 ,27, 28, 29,28, 29, 30 ,31, 32, 1};
            return EP;
        }
        public static int[] GetIP()
        {
            int[] IP = {58, 50, 42, 34, 26 ,18 ,10, 2,
                        60, 52 ,44, 36, 28 ,20 ,12, 4,
                        62 ,54, 46 ,38 ,30 ,22, 14, 6,
                        64, 56, 48, 40, 32 ,24 ,16 ,8,
                        57 ,49, 41 ,33, 25, 17, 9 ,1,
                        59 ,51, 43, 35, 27 ,19, 11, 3,
                        61, 53 ,45, 37, 29, 21, 13, 5,
                        63, 55 ,47 ,39 ,31 ,23 ,15 ,7};
            return IP;
        }
        public static int[] GetInvIP()
        {
            int[] InvIP = {40, 8, 48 ,16, 56, 24 ,64 ,32,
                            39 ,7 ,47, 15 ,55 ,23, 63, 31,
                            38, 6, 46, 14, 54, 22, 62, 30,
                            37, 5, 45, 13, 53, 21, 61, 29,
                            36, 4, 44, 12, 52, 20, 60, 28,
                            35, 3, 43, 11, 51, 19, 59 ,27,
                            34, 2, 42, 10 ,50, 18, 58, 26,
                            33, 1, 41, 9, 49, 17, 57, 25 };
            return InvIP;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DES des = new DES("85E813540F0AB405", "0123456789ABCDEF");
            des.Encrypction();
            des.Decrypction();
        }
    }
}
