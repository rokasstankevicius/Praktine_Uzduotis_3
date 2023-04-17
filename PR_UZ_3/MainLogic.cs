using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PR_UZ_3
{
    public class MainLogic
    {
        private static int n, public_key, private_key;
        
        public static void SetKeys(int primeOne, int primeTwo)
        {
            int prime1 = primeOne;
            int prime2 = primeTwo;
            
            n = prime1 * prime2;
            int Φn = (prime1 - 1) * (prime2 - 1);
            int e = 2;
            while (true)
            {
                if (FindE(e, Φn) == 1)
                    break;
                e++;
            }
            public_key = e;
            int d = 2;
            while (true)
            {
                if ((d * e) % Φn == 1) // d = (k*Φ(n) + 1) / e 
                    break;
                d++;
            }
            private_key = d;
        }

        private static int FindE(int a, int b) // 1 < e < Φ(n)
        {
            if (b == 0)
                return a;
            return FindE(b, a % b);
        }
        
        private static int Encrypt(double message)
        {
            int e = public_key;
            int encrypted_text = 1;
            while (e-- > 0)
            {
                encrypted_text *= (int)message;
                encrypted_text %= n;
            }
            return encrypted_text;
        }
        
        private static long Decrypt(long encryptedText)
        {
            int d = private_key;
            long decrypted = 1;
            while (d-- > 0)
            {
                decrypted *= encryptedText;
                decrypted %= n;
            }
            return decrypted;
        }
        
        public static List<int> Encoder(string message)
        {
            List<int> form = new List<int>();
            foreach (char letter in message)
                form.Add(Encrypt(letter));
            return form;
        }

        public static string Decoder(List<int> encoded)
        {
            string s = "";
            foreach (int num in encoded)
                s += (char)Decrypt(num);
            return s;
        }

        public static int N => n;

        public static int PublicKey => public_key;

        public static int PrivateKey => private_key;
        
        private static int Encrypt_Limited(double message, int N, int PK)
        {
            int e = PK;
            int encrypted_text = 1;
            while (e-- > 0)
            {
                encrypted_text *= (int)message;
                encrypted_text %= N;
            }
            return encrypted_text;
        }
        
        public static List<int> Encoder_Limited(string message, int N, int PK)
        {
            List<int> form = new List<int>();
            foreach (char letter in message)
                form.Add(Encrypt_Limited(letter, N, PK));
            return form;
        }
    }
}