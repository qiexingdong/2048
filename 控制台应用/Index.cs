using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    struct Index
    {
        public int RIndex { get; set; }
        public int CIndex { get; set; }
        public Index(int rIndex,int cIndex) {
            RIndex = rIndex;
            CIndex = cIndex;
        }
    }
}
