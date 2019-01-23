using System;
namespace QuinMcCluskey
{
    class Program
    {
        static void Main(string[] args)
        {
            string sMinterm;
            Console.WriteLine("Enter the minimum terms separated by commas.");
            sMinterm = Console.ReadLine();
            string[] MintermArray = sMinterm.Split(',');
            int[] minterm = ConvertInteger(MintermArray);
            int max = minterm[minterm.Length - 1];
            int bit = GetBitValue(max);
            string[] BitMinterm = Decimal2Bit(minterm, bit);
            SortbyOne(BitMinterm);
            Array.Reverse(BitMinterm);
            Console.WriteLine("Table1 is Starting");
            foreach (var item in BitMinterm)
            {
                Console.WriteLine(Bit2Decimal(item) + " " + item);
            }
            Console.WriteLine("Table1 Stopped");
            string[] tab = ConvertTable(BitMinterm);
            Console.WriteLine("Table2 is Starting");
            foreach (var item in tab)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Table 2 Stopped");
            int tabloSayaci = 3;
            while (true)
            {
                Console.WriteLine("Table{0} is Starting", tabloSayaci);
                tab = ConvertTable(tab);
                foreach (var item in tab)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("Table{0} Stopped", tabloSayaci);
                tabloSayaci++;
                string[] next = ConvertTable(tab);
                bool difference = false;
                for (int i = 0; i < next.Length; i++)
                {
                    if (tab[i] != next[i])
                        difference = true;
                }
                if (!difference)
                    break;
            }
            string[] statements = ReadTable(tab);
            statements = FixTable(statements);
            string statement = "";
            foreach (string item in statements)
            {
                statement += item + '+';
            }
            if(statement!="")
                Console.WriteLine("Result = " +statement.Substring(0, statement.Length - 1));
            Console.ReadLine();
        }
        static string[] ReadTable(string[] tab)
        {
            string[] statements = new string[tab.Length];
            char a = 'a';
            int i = 0;
            foreach (string item in tab)
            {
                a = 'a';
                string ifade = "";
                foreach (char x in item)
                {
                    if (x == '-')
                    {
                        ifade += a;
                    }
                    a++;
                }
                statements[i] = ifade;
                i++;
            }
            return statements;
        }
        static string[] FixTable(string[] tab)
        {
            string[] array = new string[tab.Length];
            int i = 0;
            foreach (var item in tab)
            {
                bool durum = true;
                foreach (var a in array)
                {
                    if (item == a)
                        durum = false;
                }
                if (durum)
                {
                    array[i] = item;
                    i++;
                }
            }
            string[] gonderilen = new string[i];
            for (int x = 0; x < i; x++)
            {
                gonderilen[x] = array[x];
            }
            return gonderilen;
        }
        static string[] ConvertTable(string[] BitMinterm)
        {
            string[] newBitMinterm = new string[BitMinterm.Length * 2];
            int sayac = 0;
            for (int i = 0; i < BitMinterm.Length; i++)
            {
                for (int j = i; j < BitMinterm.Length; j++)
                {
                    if (DifferenceBits(BitMinterm[i], BitMinterm[j]) == 1)
                    {
                        int konum = DifferenceLocation(BitMinterm[i], BitMinterm[j]);
                        string yeniS1 = "";
                        for (int x = 0; x < BitMinterm[i].Length; x++)
                        {
                            if (x != konum)
                                yeniS1 += BitMinterm[i][x];
                            else
                                yeniS1 += "-";
                        }
                        newBitMinterm[sayac] = yeniS1; sayac++;
                    }
                }
            }
            string[] sent = new string[sayac];  
            for (int i = 0; i < sayac; i++)
            {
                sent[i] = newBitMinterm[i];
            }
            return sent;
        }
        static int DifferenceBits(string s1, string s2) 
        {
            int fark = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    fark += 1;
                }
            }
            return fark;
        }
        static int DifferenceLocation(string s1, string s2) 
        {
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    return i;
                }
            }
            return -1;
        }
        static string[] SortbyOne(string[] BitMinterm) 
        {
            for (int i = 0; i < BitMinterm.Length - 1; i++)
            {
                int maxIndex = i;
                for (int j = i + 1; j < BitMinterm.Length; j++)
                {
                    if (NumberOfOne(BitMinterm[j]) > NumberOfOne(BitMinterm[maxIndex]))
                        maxIndex = j;
                }
                string temp = BitMinterm[i];
                BitMinterm[i] = BitMinterm[maxIndex];
                BitMinterm[maxIndex] = temp;
            }
            return BitMinterm;
        }
        static int NumberOfOne(string a)
        {
            int num = 0;
            foreach (var item in a)
            {
                if (item == '1')
                    num += 1;
            }
            return num;
        }
        static string[] Decimal2Bit(int[] minterm, int bit) 
        {
            string[] newArray = new string[minterm.Length];
            for (int i = 0; i < minterm.Length; i++)
            {
                newArray[i] = ConvertBit(minterm[i], bit);
            }
            return newArray;
        }
        static int Bit2Decimal(string bit)
        {
            int number = 0;
            for (int i = 0; i < bit.Length; i++)
            {
                number += Convert.ToInt32(Math.Pow(2, i)) * (int)Char.GetNumericValue(bit[bit.Length - i - 1]); ;
            }
            return number;
        }
        static int[] ConvertInteger(string[] array)
        {
            int[] newArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = int.Parse(array[i]);
            }
            Array.Sort(newArray);
            return newArray;
        }
        static string ConvertBit(int number, int bit)
        {
            string s = ""; int kalan;
            while (number > 0)
            {
                kalan = number % 2;
                number = number / 2;
                s = kalan.ToString() + s;
            }
            int uzunluk = s.Length;
            if (bit != s.Length)
                for (int i = 0; i < bit - uzunluk; i++)
                {
                    s = '0' + s;
                }
            return s;
        }
        static int GetBitValue(int number)
        {
            int i = 1;
            while (Math.Pow(2, i) <= number)
            {
                i++;
            }
            return i;
        }
    }
}
