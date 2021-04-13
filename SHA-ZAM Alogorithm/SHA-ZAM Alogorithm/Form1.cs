using System;
using System.Text;
using System.Windows.Forms;
using System.Numerics;

namespace SHA_ZAM_Alogorithm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        BigInteger Pow160 = BigInteger.Pow(2, 160);
        private string SHA_1(string num, uint[] key)    // принимает двоичную строку и ключ, возвращает двоичнуюхэш строку
        {
            int numl = num.Length;
            num += '1';     // добавление 1
            while (num.Length % 512 != 448)
                num += '0';     // добавление нулей
            string len = Convert.ToString(numl, 2);     // длина сообщение в двоичном
            while (len.Length < 64)
                len = "0" + len;    // добавление до 64 незначащими нулями
            num += len;
            int l = num.Length / 512;
            int pos = 0;
            uint[,] numarr = new uint[l, 80];
            for (int i = 0; i < l; i++)
                for (int j = 0; j < 16; j++)
                {
                    numarr[i, j] = Convert.ToUInt32(num.Substring(pos, 32), 2);
                    pos += 32;
                }   // разбиение на блоки по 512 и подблоки по 32
            for (int i = 0; i < l; i++)
                for (int j = 16; j < 80; j++)
                {
                    numarr[i, j] = numarr[i, j - 3] ^ numarr[i, j - 8] ^ numarr[i, j - 14] ^ numarr[i, j - 16];
                    numarr[i, j] = (numarr[i, j] << 1) | ((numarr[i, j] >> 31) & UInt32.MaxValue); // дополнение до 80 чисел
                }
            uint h0 = 0x67452301 ^ key[0], h1 = 0xEFCDAB89 ^ key[1], h2 = 0x98BADCFE ^ key[2], h3 = 0x10325476 ^ key[3], h4 = 0xC3D2E1F0;
            uint a = h0, b = h1, c = h2, d = h3, e = h4, f, k, temp;
            for (int i = 0; i < l; i++)  // основной цикл
            {
                for (int j = 0; j < 80; j++)
                {
                    if (j < 20)
                    {
                        f = (b & c) | (~b & d);
                        k = 0x5A827999;
                    }
                    else
                    if (j < 40)
                    {
                        f = b ^ c ^ d;
                        k = 0x6ED9EBA1;
                    }
                    else
                    if (j < 60)
                    {
                        f = (b & c) | (b & d) | (c & d);
                        k = 0x8F1BBCDC;
                    }
                    else
                    {
                        f = b ^ c ^ d;
                        k = 0xCA62C1D6;
                    }
                    temp = f + e + k + ((a << 5) | ((a >> 27) & UInt32.MaxValue)) + numarr[i, j];
                    e = d;
                    d = c;
                    c = (b << 30) | ((b >> 2) & UInt32.MaxValue);
                    b = a;
                    a = temp;
                }
                h0 += a;
                h1 += b;
                h2 += c;
                h3 += d;
                h4 += e;
            }
            string res = Convert.ToString(h0, 2) + Convert.ToString(h1, 2) + Convert.ToString(h2, 2) + Convert.ToString(h3, 2) + Convert.ToString(h4, 2);
            return res;
        }

        private string ToACSII(string input)
        {
            byte[] input_b = Encoding.GetEncoding("windows-1251").GetBytes(input);
            string[] input_s = new string[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                input_s[i] = Convert.ToString(input_b[i], 2);   // перевод двоичных кодов в строки
                while (input_s[i].Length < 8)
                    input_s[i] = '0' + input_s[i];  // дополнение до 8 незначащими нулями
            }
            string num = "";
            for (int i = 0; i < input.Length; i++)
                num += input_s[i];
            return num;
        }   // принимает строку возвращает двоичную строку кода ACSII
         
        private string ToText(BigInteger input)
        {
            string output = "";
            byte[] code = input.ToByteArray();
            int begin = 0;
            Array.Reverse(code);
            for (int i = 0; i < code.Length; i++)
            {
                begin = i;
                if (code[i] != 0)
                    break;
            }
            output += Encoding.GetEncoding("windows-1251").GetString(code, begin, code.Length - begin);
            return output;
        }

        private BigInteger SquareHash(BigInteger x, BigInteger y)   // преобразование создателей алгоритма
        {
            BigInteger MinSimple = BigInteger.Parse("6957596529882152968992225251835887181478451547013");
            BigInteger SqH = mod(mod(BigInteger.Pow(x + y, 2), MinSimple), Pow160);
            return SqH;
        }

        private string BigIntToX(BigInteger num)    // получает bigint, возвращает двоичную строку
        {
            string line = ""; 
            string numl = num.ToString("X");
            for (int i = 0; i < numl.Length; i++)
                line = HIntToX(numl);
            return line;
        }

        private string HIntToX(string num)    // получает bigint, возвращает двоичную строку
        {
            string line = "";
            for (int i = 0; i < num.Length; i++)
                switch (num.Substring(i, 1))
                {
                    case "0":
                        line += "0000";
                        break;
                    case "1":
                        line += "0001";
                        break;
                    case "2":
                        line += "0010";
                        break;
                    case "3":
                        line += "0011";
                        break;
                    case "4":
                        line += "0100";
                        break;
                    case "5":
                        line += "0101";
                        break;
                    case "6":
                        line += "0110";
                        break;
                    case "7":
                        line += "0111";
                        break;
                    case "8":
                        line += "1000";
                        break;
                    case "9":
                        line += "1001";
                        break;
                    case "A":
                        line += "1010";
                        break;
                    case "B":
                        line += "1011";
                        break;
                    case "C":
                        line += "1100";
                        break;
                    case "D":
                        line += "1101";
                        break;
                    case "E":
                        line += "1110";
                        break;
                    case "F":
                        line += "1111";
                        break;
                }
            return line;
        }

        private BigInteger mod(BigInteger x, BigInteger m)
        {
            BigInteger r = x % m;
            return r < 0 ? r + m : r;
        }

        private BigInteger ToBigInt(string line)    // получает двоичную строку, возвращает bigint
        {
            char[] linech = line.ToCharArray();
            BigInteger num = new BigInteger();
            Array.Reverse(linech);
            for (int i = 0; i < linech.Length; i++)
                if (linech[i].Equals('1'))
                    num += BigInteger.Pow(2, i);
            return num;
        }   

        private BigInteger[] KeyGen (uint[] key)
        {
            uint[] c = { 2800236257, 2961518291, 2427382541, 512905341, 1129189786 }, CC = new uint[8];
            string K = "", C = "11110100101100001000011100000010101111101110110000010101000111011111011011010000011001000110010010111010100011010000100100011101111001000110001011011011000001110010111111010101011000100001001111011001001101101111110111101110100001000100111100000001001101010010010000011011111111111100000010110011010000011111010101110110011101000101001010101111001111110111110011011101001010110011000000000010010111000101110000111000001010100111010011110011001010101111101011111010000101111011100111001110110011001011100110110111";
            for (int i = 0; i < 8; i++)
                CC[i] = Convert.ToUInt32(C.Substring(i * 64, 32), 2);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                    c[i] ^= CC[j];
                K += SHA_1(ToACSII(Convert.ToString(c[i], 2)), key);
            }
            BigInteger[] ExtKey = new BigInteger[3];
            ExtKey[0] = ToBigInt(K.Substring(0, 160));
            ExtKey[1] = ToBigInt(K.Substring(160, 352));
            ExtKey[2] = ToBigInt(K.Substring(512, 160));
            return ExtKey;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private BigInteger[] SHA_ZAM_CR(BigInteger[] Key, BigInteger[] mess)    // получает на вход число и шифрует его алгоритмом SHA-ZAM
        {
            BigInteger[] crypt = new BigInteger[mess.Length];
            for (int i = 0; i < mess.Length; i += 2)
            {
                uint[] key = { 0, 0, 0, 0, 0 };
                BigInteger T1, T2, L1, R1;
                T1 = mod(mess[i] + SquareHash(Key[0], mess[i + 1]), Pow160);
                T2 = mod(mess[i + 1] + ToBigInt(SHA_1(BigIntToX(T1) + BigIntToX(Key[1]), key)), Pow160);
                L1 = mod(T1 + ToBigInt(SHA_1(BigIntToX(T2) + BigIntToX(Key[1]), key)), Pow160);
                R1 = mod(T2 + SquareHash(Key[2], L1), Pow160);
                crypt[i] = L1;
                crypt[i + 1] = R1;
            }
            return crypt;
        }
        private BigInteger[] SHA_ZAM_ENCR(BigInteger[] Key, BigInteger[] mess)      // получает на вход зашифрованное число и расшифровывает его
        {
            BigInteger[] encrypt = new BigInteger[mess.Length];
            for (int i = 1; i < mess.Length; i += 2)
            {
                uint[] key = { 0, 0, 0, 0, 0 };
                BigInteger T1, T2, L, R;
                T2 = mod(mess[i] - SquareHash(Key[2], mess[i - 1]), Pow160);
                T1 = mod(mess[i - 1] - ToBigInt(SHA_1(BigIntToX(T2) + BigIntToX(Key[1]), key)), Pow160);
                R = mod(T2 - ToBigInt(SHA_1(BigIntToX(T1) + BigIntToX(Key[1]), key)), Pow160);
                L = mod(T1 - SquareHash(Key[0], R), Pow160);
                encrypt[i] = R;
                encrypt[i - 1] = L;
            }
            return encrypt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string skey = textBox1.Text;
            uint[] key = new uint[4];
            try
            {
                key[0] = Convert.ToUInt32(skey.Substring(0, 31), 2);
                key[1] = Convert.ToUInt32(skey.Substring(31, 32), 2);
                key[2] = Convert.ToUInt32(skey.Substring(63, 32), 2);
                key[3] = Convert.ToUInt32(skey.Substring(95, 5) + "000000000000000000000000", 2);
                string num = ToACSII(textBox2.Text);
                while (num.Length % 320 != 0)
                    num += "00000000";     // добавление нулей до делимости на блоки по 320
                BigInteger[] mess = new BigInteger[num.Length / 160];
                for (int i = 0; i < num.Length / 160; i++)
                    mess[i] = ToBigInt(num.Substring(i * 160, 160));  //разбиение на подблоки по 160
                BigInteger[] crypt = SHA_ZAM_CR(KeyGen(key), mess);
                string message = "", code = "";
                for (int i = 0; i < crypt.Length; i++)
                {
                    if (crypt[i].ToString("X40").Length > 40)
                        message += crypt[i].ToString("X40").Substring(1, crypt[i].ToString("X").Length - 1);
                    else
                        message += crypt[i].ToString("X40");
                    if (mess[i].ToString("X40").Length > 40)
                        code += mess[i].ToString("X40").Substring(1, mess[i].ToString("X").Length - 1);
                    else
                        code += mess[i].ToString("X40");
                }
                textBox3.Text = code;
                textBox4.Text = message;
            }
            catch(System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Введите в поле исходный ключ 100-значное двоичное число");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string skey = textBox1.Text;
            uint[] key = new uint[4];
            try
            {
                key[0] = Convert.ToUInt32(skey.Substring(0, 31), 2);
                key[1] = Convert.ToUInt32(skey.Substring(31, 32), 2);
                key[2] = Convert.ToUInt32(skey.Substring(63, 32), 2);
                key[3] = Convert.ToUInt32(skey.Substring(95, 5) + "000000000000000000000000", 2);
                string num = HIntToX(textBox4.Text);
                int ost = (num.Length % 320) / 8; //число незаполненности последнего блока
                while (num.Length % 320 != 0)
                    num = num.Substring(0, num.Length - 160) + "0" + num.Substring(num.Length - 160, 160);
                BigInteger[] mess = new BigInteger[num.Length / 160];
                for (int i = 0; i < num.Length / 160; i++)
                    mess[i] = ToBigInt(num.Substring(i * 160, 160));  //разбиение на подблоки по 160
                BigInteger[] encrypt = SHA_ZAM_ENCR(KeyGen(key), mess);
                string message = "";
                num = "";
                for (int i = 0; i < encrypt.Length; i++)
                    message += ToText(encrypt[i]);
            textBox2.Text = message;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Введите в поле исходный ключ 100-значное двоичное число");
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа создана студентом группы БИСТ-18-2 Фунтовым Семёном","О программе");
        }
    }
}