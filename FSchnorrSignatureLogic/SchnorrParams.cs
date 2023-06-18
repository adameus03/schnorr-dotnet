using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace FSchnorrSignatureLogic
{
    public struct SchnorrParams
    {
        private BigInteger v, a, p, q, h;
        public SchnorrParams()
        {

        }
        public SchnorrParams(BigInteger v, BigInteger a, BigInteger p, BigInteger q, BigInteger h)
        {
            this.v = v;
            this.a = a;
            this.p = p;
            this.q = q;
            this.h = h;
        }

        public void Deconstruct(out BigInteger v, out BigInteger a, out BigInteger p, out BigInteger q, out BigInteger h)
        {
            v = this.v;
            a = this.a;
            p = this.p;
            q = this.q;
            h = this.h;
        }

        public BigInteger V {
            get => v;
            set => this.v = value;
        }
        public BigInteger A
        {
            get => a;
            set => this.a = value;
        }
        public BigInteger P
        {
            get => p; 
            set => this.p = value;
        }
        public BigInteger Q
        {
            get => q; 
            set => this.q = value;
        }
        public BigInteger H
        {
            get => h;
            set => this.h = value;
        }

    }
}
