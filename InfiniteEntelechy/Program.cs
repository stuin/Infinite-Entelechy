using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using InfiniteEntelechy.Space;

namespace InfiniteEntelechy {
    class Program {
        static void Main(string[] args) {
            Game game = new Game("Infinite Entelechy", 1280, 960, 60, false);
            Universe universe = new Universe("newLand");
            game.MouseVisible = true;
            game.Start(universe.galaxies[universe.current].viewPort);
        }
    }
}
