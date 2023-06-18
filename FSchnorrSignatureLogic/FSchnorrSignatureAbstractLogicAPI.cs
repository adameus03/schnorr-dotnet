using FSchnorrSignatureData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace FSchnorrSignatureLogic
{
    public abstract class FSchnorrSignatureAbstractLogicAPI
    {
        protected readonly FSchnorrSignatureData.FSchnorrSignatureAbstractDataAPI dataAPI;
        public FSchnorrSignatureAbstractLogicAPI(FSchnorrSignatureData.FSchnorrSignatureAbstractDataAPI? dataAPI = null)
        {
            this.dataAPI = dataAPI ?? FSchnorrSignatureData.FSchnorrSignatureAbstractDataAPI.CreateInstance();
        }
        public static FSchnorrSignatureAbstractLogicAPI CreateInstance(FSchnorrSignatureData.FSchnorrSignatureAbstractDataAPI? dataAPI = null)
        {
            return new FSchnorrSignatureLogicAPI(dataAPI);
        }

        public abstract SchnorrParams GenerateParams(SchnorrSecurityParams schnorrSecurityParams);
        public abstract (BigInteger, BigInteger) GenerateSignature(byte[] data, SchnorrParams schnorrParams);

        public abstract bool VerifySignature(byte[] data, SchnorrParams schnorrParams, (BigInteger s1, BigInteger s2) signature);

        public List<BigInteger> LoadNumbers(string filePath) => this.dataAPI.LoadNumbers(filePath);
        public void StoreNumbers(string filePath, List<BigInteger> bigIntegers) => this.dataAPI.StoreNumbers(filePath, bigIntegers);

        public byte[] GetFileBytes(string filePath) => this.dataAPI.GetFileBytes(filePath);

        public void StoreFileBytes(string filePath, byte[] data) => this.dataAPI.StoreFileBytes(filePath, data);


    }
}
