using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FSchnorrSignatureData
{
    internal class FSchnorrSignatureDataAPI : FSchnorrSignatureAbstractDataAPI
    {
        public FSchnorrSignatureDataAPI() : base()
        {

        }

        public override byte[] GetFileBytes(string filePath)
        {
            //StreamReader fsr = new StreamReader(filePath);
            return File.ReadAllBytes(filePath);
        }

        public override List<BigInteger> LoadNumbers(string filePath)
        {
            StreamReader fsr = new StreamReader(filePath);
            List<BigInteger> bigIntegers = new List<BigInteger>();
            string? line;
            while ((line=fsr.ReadLine()) != null)
            {
                bigIntegers.Add(BigInteger.Parse(/*"0"+*/line, System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            fsr.Close();
            return bigIntegers;
        }

        public override void StoreFileBytes(string filePath, byte[] data)
        {
            File.WriteAllBytes(filePath, data);
        }

        public override void StoreNumbers(string filePath, List<BigInteger> bigIntegers)
        {
            StreamWriter fsw = new StreamWriter(filePath, append: false);
            for(int i=0; i<bigIntegers.Count; i++) {
                fsw.WriteLine(bigIntegers[i].ToString("x"));
            }
            fsw.Close();
        }

    }
}
