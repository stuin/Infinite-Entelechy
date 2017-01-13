using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace InfiniteEntelechy.Land {
    class Planet {
        int size;
        ViewPort viewPort;
        Random rand;

        public Planet(int nSize, string name) {
            size = nSize;
            viewPort = new ViewPort(size * 100);
        }
    }
}
