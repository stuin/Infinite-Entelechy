using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using InfiniteEntelechy.Components;

namespace InfiniteEntelechy.Units {
    class Unit : Entity {
        public BaseMovement movement;

        int health = 100;
        int type;
        bool freindly;
        bool attack = false;
        bool selected = false;

        public Unit(int nType, bool nFreindly, int nX, int nY) : base(nX, nY) {
            type = nType;
            freindly = nFreindly;
            Layer = 100;
        }
    }
}
