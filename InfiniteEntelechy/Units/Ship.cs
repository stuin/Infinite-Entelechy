using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using InfiniteEntelechy.Components;

namespace InfiniteEntelechy.Units {
    class Ship : Unit {

        public Ship(int nType, bool nFreindly, int nX, int nY) : base(nType, nFreindly, nX, nY) {
            movement = new BaseMovement(10);
        }

        public void load(ViewPort viewPort) {
            Image image = Image.CreateRectangle(50, 100, Color.Gray);
            image.CenterOrigin();
            AddGraphic(image);

            image = Image.CreateCircle(5, Color.Blue);
            image.CenterOrigin();
            AddGraphic(image);

            AddComponent(movement);
            viewPort.Add(this);
        }
    }
}
