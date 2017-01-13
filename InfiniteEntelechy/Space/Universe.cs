using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace InfiniteEntelechy.Space {
    class Universe {
        string name;
        public int current;
        public Galaxy[] galaxies;

        Random rand;

        public Universe(string nName) {
            name = nName;
            rand = new Random(name.GetHashCode());

            Image.CirclePointCount = 50;
            galaxies = new Galaxy[1];
            current = 0;

            for(int i = 0; i < galaxies.Length; i++) galaxies[i] = new Galaxy(Namer.get(20,rand));
            galaxies[current].init();
        }
    }
}
