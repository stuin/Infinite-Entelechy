using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace InfiniteEntelechy.Space {
    class StarField : Surface {
        Random rand;
        ViewPort viewPort;
        public bool loaded = false;

        public StarField(int x, int y, int seed, ViewPort nViewPort) : base(1000) {
            CameraX = x * 1000;
            CameraY = y * 1000;
            Name = "Stars of " + seed;
            viewPort = nViewPort;
        }

        public void init() {
            Texture texture = new Texture("Assets/Star.png");
            rand = new Random(Name.GetHashCode());
            AutoClear = false;
            FillColor = Color.Cyan;
            viewPort.AddGraphic(this);

            for(int i = 0; i < 100; i++) {
                viewPort.Add(new Particle(rand.Next(1000), rand.Next(1000), texture, 10, 10) {
                    LifeSpan = 100,
                });
            }
            loaded = true;
        }

        public void checkPlace() {
            //Check if close to camera
            bool found = !(viewPort.position.X + 1000 < CameraX || viewPort.position.X - 2000 > CameraX);
            if(found) found = !(viewPort.position.Y + 1000 < CameraY || viewPort.position.Y - 2000 > CameraY);
            else if(!found && loaded) remove();
            if(found && !loaded) init();
        }

        public void remove() {
            viewPort.RemoveGraphic(this);
            loaded = false;
        }
    }
}
