using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FSchnorrSignatureLogic
{
    public struct SchnorrSecurityParams
    {
        private int pBitsLength = 512;
        private int qBitsLength = 140;
        public SchnorrSecurityParams()
        {

        }
        public SchnorrSecurityParams(int pBitsLength, int qBitsLength)
        {
            this.pBitsLength = pBitsLength;
            this.qBitsLength = qBitsLength;
        }

        public int PBitsLength { 
            get {  return pBitsLength; }
            set { pBitsLength = value; }
        }
        public int QBitsLength
        {
            get { return qBitsLength; }
            set { qBitsLength = value; }
        }
    }
}
