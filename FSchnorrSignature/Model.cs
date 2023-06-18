using FSchnorrSignatureLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FSchnorrSignature
{
    internal class Model
    {
        private BigInteger v, a, p, q, h, s1, s2;
        private byte[] m = Encoding.ASCII.GetBytes("Sample text");

        private readonly FSchnorrSignatureLogic.FSchnorrSignatureAbstractLogicAPI logicAPI;
        public Model(FSchnorrSignatureLogic.FSchnorrSignatureAbstractLogicAPI? logicAPI = null)
        {
            this.logicAPI = logicAPI ?? FSchnorrSignatureLogic.FSchnorrSignatureAbstractLogicAPI.CreateInstance();
            this.GenerateParams();
            this.GenerateSignature(/*m*/);
        }

        public void GenerateParams()
        {
            FSchnorrSignatureLogic.SchnorrParams schnorrParams = this.logicAPI.GenerateParams(new FSchnorrSignatureLogic.SchnorrSecurityParams(512, 140));
            (v, a, p, q, h) = schnorrParams;
        }

        public void GenerateSignature(/*byte[] data*/)
        {
            SchnorrParams schnorrParams = new SchnorrParams(v, a, p, q, h);
            (s1, s2) = this.logicAPI.GenerateSignature(/*data*/m, schnorrParams);
        }

        public bool VerifySignature(/*byte[] data*/)
        {
            SchnorrParams schnorrParams = new SchnorrParams(v, a, p, q, h);
            return this.logicAPI.VerifySignature(/*data*/m, schnorrParams, (s1, s2));
        }

        public void SetSigningParams(string v, string a, string p, string q, string h)
        {

            this.v = BigInteger.Parse(/*"0" + */v, System.Globalization.NumberStyles.AllowHexSpecifier);
            this.a = BigInteger.Parse(/*"0" + */a, System.Globalization.NumberStyles.AllowHexSpecifier);
            this.p = BigInteger.Parse(/*"0" + */p, System.Globalization.NumberStyles.AllowHexSpecifier);
            this.q = BigInteger.Parse(/*"0" + */q, System.Globalization.NumberStyles.AllowHexSpecifier);
            this.h = BigInteger.Parse(/*"0" + */h, System.Globalization.NumberStyles.AllowHexSpecifier);

        }

        public void GetSigningParams(out string v, out string a, out string p, out string q, out string h)
        {
            v = this.v.ToString("x");
            a = this.a.ToString("x");
            p = this.p.ToString("x");
            q = this.q.ToString("x");
            h = this.h.ToString("x");

        }

        public void SetVerificationParams(string v, string p, string q, string h, string s1, string s2)
        {
            this.v = BigInteger.Parse(/*"0" + */v, System.Globalization.NumberStyles.AllowHexSpecifier);
            this.p = BigInteger.Parse(/*"0" + */p, System.Globalization.NumberStyles.AllowHexSpecifier);
            this.q = BigInteger.Parse(/*"0" + */q, System.Globalization.NumberStyles.AllowHexSpecifier);
            this.h = BigInteger.Parse(/*"0" + */h, System.Globalization.NumberStyles.AllowHexSpecifier);
            this.s1 = BigInteger.Parse(/*"0" + */s1, System.Globalization.NumberStyles.AllowHexSpecifier);
            this.s2 = BigInteger.Parse(/*"0" + */s2, System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        public void GetVerificationParams(out string v, out string p, out string q, out string h, out string s1, out string s2)
        {
            v = this.v.ToString("x");
            p = this.p.ToString("x");
            q = this.q.ToString("x");
            h = this.h.ToString("x");
            s1 = this.s1.ToString("x");
            s2 = this.s2.ToString("x");
        }

        public void GetMessage(out string m)
        {
            m = Encoding.ASCII.GetString(this.m);
        }

        public void GetPrivateKey(out string a)
        {
            a = this.a.ToString("x");
        }

        public void GetAllParams(out string v, out string a, out string p, out string q, out string h, out string s1, out string s2, out string m)
        {
            v = this.v.ToString("x");
            a = this.a.ToString("x");
            p = this.p.ToString("x");
            q = this.q.ToString("x");
            h = this.h.ToString("x");
            s1 = this.s1.ToString("x");
            s2 = this.s2.ToString("x");
            m = Encoding.ASCII.GetString(this.m);
        }


        public void LoadVPQH(string filePath)
        {
            List<BigInteger> list = this.logicAPI.LoadNumbers(filePath);
            this.v = list[0]; //BigInteger.Parse(/*"0" + */list[0], System.Globalization.NumberStyles.AllowHexSpecifier);
            this.p = list[1]; // BigInteger.Parse(/*"0" + */list[1], System.Globalization.NumberStyles.AllowHexSpecifier);
            this.q = list[2]; // BigInteger.Parse(/*"0" + */list[2], System.Globalization.NumberStyles.AllowHexSpecifier);
            this.h = list[3]; // BigInteger.Parse(/*"0" + */list[3], System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        public void LoadA(string filePath)
        {
            List<BigInteger> list = this.logicAPI.LoadNumbers(filePath);
            this.a = list[0];// BigInteger.Parse("0" + list[0], System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        public void StoreVPQH(string filePath)
        {
            this.logicAPI.StoreNumbers(filePath, new List<BigInteger>(new BigInteger[] { v, p, q, h }));
        }

        public void StoreA(string filePath)
        {
            this.logicAPI.StoreNumbers(filePath, new List<BigInteger>(new BigInteger[] { a }));
        }

        public void LoadM(string filePath)
        {
            this.m = this.logicAPI.GetFileBytes(filePath);
        }

        public void StoreM(string filePath)
        {
            this.logicAPI.StoreFileBytes(filePath, this.m);
        }

        public void LoadSignature(string filePath)
        {
            List<BigInteger> list = this.logicAPI.LoadNumbers(filePath);
            this.s1 = list[0];// BigInteger.Parse("0" + list[0], System.Globalization.NumberStyles.AllowHexSpecifier);
            this.s2 = list[1];// bigInteger.Parse("0" + list[1], System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        public void StoreSignature(string filePath)
        {
            this.logicAPI.StoreNumbers(filePath, new List<BigInteger>(new BigInteger[] { s1, s2 }));
        }

        public BigInteger V
        {
            get => this.v;
            set => this.v = value;
        }
        public BigInteger A
        {
            get => this.a;
            set => this.a = value;
        }
        public BigInteger P
        {
            get => this.p;
            set => this.p = value;
        }
        public BigInteger Q
        {
            get => this.q;
            set =>this.q = value;
        }
        public BigInteger H
        {
            get => this.h;
            set => this.h = value;
        }
        public BigInteger S1
        {
            get => this.s1;
            set => this.s1 = value;
        }
        public BigInteger S2
        {
            get => this.s2;
            set => this.s2 = value;
        }
        public byte[] M
        {
            get => this.m;
            set => this.m = value;
        }
    }
}
