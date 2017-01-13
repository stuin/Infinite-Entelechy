using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Otter;

namespace InfiniteEntelechy.Space {
    class Galaxy : Entity {
        public ViewPort viewPort;

        SolarSystem[] systems;
        List<Units.Ship> ships;
        Random rand;
        StarField[][] starFields;

        int x;
        int y;
        int size = 10000;

        public Galaxy(string nName) {
            Name = nName;
            ships = new List<Units.Ship>();
        }

        //Generate solar systems
        public void init() {
            rand = new Random(Name.GetHashCode());
            systems = new SolarSystem[rand.Next(30, 60)];
            viewPort = new ViewPort(size);

            for(int i = 0; i < systems.Length; i++) {
                bool b = true;
                while(b) {
                    systems[i] = new SolarSystem(Namer.get(5,rand) + rand.Next(100), rand.Next(size - 2000) + 1000, rand.Next(size - 2000) + 1000);
                    systems[i].init();
                    b = false;
                    for(int j = 0; j < systems.Length && systems[j] != null && !b; j++) if(i != j) b = overlap(systems[i].Position, systems[i].radius, j);
                }
            }

            viewPort.Add(this);

            ships.Add(new Units.Ship(0, true, size / 2, size / 2));
            foreach(Units.Ship ship in ships) ship.load(viewPort);

            //Thread thread = new Thread(new ThreadStart(showStars));
            //thread.Start();
            //showStars();
        }

        public override void Update() {
            base.Update();

            //Load systems in range
            for(int i = 0; i < systems.Length; i++) {
                if(overlap(viewPort.position, 1000, i)) systems[i].load(viewPort);
                else if(systems[i].loaded) systems[i].remove();
            }

            //for(int x = 0; x < size / 1000; x++) for(int y = 0; y < size / 1000; y++) starFields[x][y].checkPlace();
            Vector2 pos = viewPort.position;
            //if(!starFields[(int)pos.X / 1000][(int)pos.Y / 1000].loaded) starFields[(int)pos.X / 1000][(int)pos.Y / 1000].init();
        }

        bool overlap(Vector2 p, int size, int i) {
            //Check if area contains other systems
            bool found = !(p.X + size < systems[i].X - systems[i].radius || p.X - size > systems[i].X + systems[i].radius);
            if(found) found = !(p.Y + size < systems[i].Y - systems[i].radius || p.Y - size > systems[i].Y + systems[i].radius);
            return found;
        }

        void showStars() {
            //Generate starfield
            starFields = new StarField[20][];
            for(int x = 0; x < 20; x++) {
                starFields[x] = new StarField[20];
                for(int y = 0; y < 20; y++) starFields[x][y] = new StarField(x,y,rand.Next(size),viewPort);
            }
        }
    }
}
