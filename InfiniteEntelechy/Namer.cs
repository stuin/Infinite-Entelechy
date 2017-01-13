using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteEntelechy {
    public static class Namer {
        public static string get(int digits, Random rand) {
            String s = (char)('A' + rand.Next(26)) + "";
            while(digits > 0) {
                s += (char)('a' + rand.Next(26));
                digits--;
            }
            return s;
        }
    }
}
