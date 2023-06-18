using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace FSchnorrSignatureLogic
{
    public static class BigIntegerUtility
    {

        public static BigInteger GetRandomNonnegative(int numBits)
        {
            byte remBits = (byte)(numBits % 8);
            int numBytes = 1 + numBits / 8;


            byte[] numData = RandomNumberGenerator.GetBytes(numBytes); 
            numData[^1] &= (byte)(((byte)0x7f) >> (0x7 - remBits));
            //numData[^1] |= 0x40; 
            return new BigInteger(numData);
        }


        public static BigInteger GetRandomNonnegative(BigInteger maxExclusive)
        {
            int numBits = (int)BigInteger.Log2(maxExclusive - 1);
            BigInteger b;
            do
            {
                b = GetRandomNonnegative(numBits);
            }
            while (b >= maxExclusive);
            return b;
        }

        private static bool MillerRabinPrimalityTest(BigInteger d, BigInteger n)
        {
            BigInteger a = 2 + GetRandomNonnegative(n - 4);
            BigInteger x = BigInteger.ModPow(a, d, n);
            if (x == 1 || x == n - 1) return true;
            while(d != n - 1)
            {
                x = (x * x) % n;
                d *= 2;
                if (x == 1) return false;
                if (x == n - 1) return true;
            }
            return false;
        }

        public static bool IsProbablePrime(BigInteger n, int k)
        {
            if (n <= 1 || n == 4) return false;
            if (n <= 3) return true;
            BigInteger d = n - 1;
            while(d % 2 == 0)
            {
                d /= 2;
            }
            for(int i=0; i<k; i++)
            {
                if(!MillerRabinPrimalityTest(d, n))
                {
                    return false;
                }
            }
            return true;
        }

        /*
         *  Probability of returning a composite number is at most 4^(-k)
         */
        public static BigInteger GetRandomProbablePrime(int numBits, int k)
        {
            BigInteger b;
            do
            {
                b = GetRandomNonnegative(numBits);

            }
            while (!IsProbablePrime(b, k));
            return b;
        }
    }
}
