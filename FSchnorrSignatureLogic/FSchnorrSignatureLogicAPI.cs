using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace FSchnorrSignatureLogic
{
    internal class FSchnorrSignatureLogicAPI : FSchnorrSignatureAbstractLogicAPI
    {
        private const int primalityFactor = 10; 
        public FSchnorrSignatureLogicAPI(FSchnorrSignatureData.FSchnorrSignatureAbstractDataAPI? dataAPI = null) : base(dataAPI)
        {
            
        }

        private void FillPQ(ref SchnorrParams schnorrParams, SchnorrSecurityParams schnorrSecurityParams)
        {
            schnorrParams.P = BigIntegerUtility.GetRandomProbablePrime(schnorrSecurityParams.PBitsLength, primalityFactor);
            BigInteger _q = BigIntegerUtility.GetRandomProbablePrime(schnorrSecurityParams.QBitsLength, primalityFactor);

            BigInteger rem = ((schnorrParams.P - 1) % _q);
            schnorrParams.Q = 2 * _q - rem;

            int lq = (int)BigInteger.LeadingZeroCount(schnorrParams.Q);
            int l_q = (int)BigInteger.LeadingZeroCount(_q);
            if (lq > l_q)
            {
                schnorrParams.Q = _q - rem;

                lq = (int)BigInteger.LeadingZeroCount(schnorrParams.Q);
                l_q = (int)BigInteger.LeadingZeroCount(_q);
                if (lq > l_q)
                {
                    throw new ArgumentException("Invalid/to narrow schnorrSecurityParams given");
                }
            }
        }
        private void FillH(ref SchnorrParams schnorrParams, SchnorrSecurityParams schnorrSecurityParams)
        {
            BigInteger ratio = (schnorrParams.P - 1) % schnorrParams.Q;
            BigInteger hSeed = BigIntegerUtility.GetRandomNonnegative(schnorrSecurityParams.QBitsLength);
            schnorrParams.H = BigInteger.ModPow(hSeed, ratio, schnorrParams.P);
        }

        private void FillAV(ref SchnorrParams schnorrParams, SchnorrSecurityParams schnorrSecurityParams)
        {
            BigInteger _a = BigIntegerUtility.GetRandomNonnegative(schnorrParams.P - 4);
            schnorrParams.A = 2 + _a;
            BigInteger exponent = schnorrParams.P - 1 - schnorrParams.A;
            schnorrParams.V = BigInteger.ModPow(schnorrParams.H, exponent, schnorrParams.P);
        }   


        public override SchnorrParams GenerateParams(SchnorrSecurityParams schnorrSecurityParams)
        {
            SchnorrParams schnorrParams = new SchnorrParams();
            FillPQ(ref schnorrParams, schnorrSecurityParams);
            FillH(ref schnorrParams, schnorrSecurityParams);
            FillAV(ref schnorrParams, schnorrSecurityParams);
            return schnorrParams;

        }

        public override (BigInteger, BigInteger) GenerateSignature(byte[] data, SchnorrParams schnorrParams)
        {
            BigInteger r = 1 + BigIntegerUtility.GetRandomNonnegative(schnorrParams.Q - 1);
            BigInteger X = BigInteger.ModPow(schnorrParams.H, r, schnorrParams.P);
            byte[] MX = data.Concat(X.ToByteArray()).ToArray();
            using SHA256 sha256 = SHA256.Create();
            BigInteger s1 = new BigInteger(sha256.ComputeHash(MX));
            if (s1 < 0) s1 *= -1;

            BigInteger s2 = (r + (schnorrParams.A * s1)) % schnorrParams.Q;
            return (s1, s2);

        }

        public override bool VerifySignature(byte[] data, SchnorrParams schnorrParams, (BigInteger s1, BigInteger s2) signature)
        {
            BigInteger hs2 = BigInteger.ModPow(schnorrParams.H, signature.s2, schnorrParams.P);
            BigInteger hs1 = BigInteger.ModPow(schnorrParams.H, signature.s1, schnorrParams.P);


            BigInteger Z = (hs1 * hs2) % schnorrParams.P;
            byte[] MZ = data.Concat(Z.ToByteArray()).ToArray();
            using SHA256 sha256 = SHA256.Create();
            BigInteger fMZ = new BigInteger(sha256.ComputeHash(MZ));
            if (fMZ < 0) fMZ *= -1;
            return 0 == BigInteger.Compare(signature.s1, fMZ);
        }
    }
}
