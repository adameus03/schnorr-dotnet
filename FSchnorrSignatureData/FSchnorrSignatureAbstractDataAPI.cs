using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FSchnorrSignatureData
{
    public abstract class FSchnorrSignatureAbstractDataAPI
    {
        public FSchnorrSignatureAbstractDataAPI() { }
        public static FSchnorrSignatureAbstractDataAPI CreateInstance()
        {
            return new FSchnorrSignatureDataAPI();
        }
        public abstract List<BigInteger> LoadNumbers(string filePath);
        public abstract void StoreNumbers(string filePath, List<BigInteger> bigIntegers);

        public abstract byte[] GetFileBytes(string filePath);
        public abstract void StoreFileBytes(string filePath, byte[] data);
    }
}
