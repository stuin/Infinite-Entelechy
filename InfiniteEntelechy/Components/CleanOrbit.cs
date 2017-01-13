using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace InfiniteEntelechy.Components {
    class CleanOrbit : Component {
        Entity orbitting;
        Entity orbit;
        public ViewPort scene;
        public Surface surface;

        public double speed = .5;
        public int offset = 0;
        int distance;

        public CleanOrbit(Entity nOrbitting, int nDistance) {
            orbitting = nOrbitting;
            distance = nDistance;
        }

        public override void Update() {
            base.Update();

            //Calculate current orbit
            Entity.Position = orbitting.Position;
            float angle = (float)speed * scene.time + offset;
            Entity.X += (float)Math.Sin(angle * Math.PI / 180) * distance;
            Entity.Y += (float)Math.Cos(angle * Math.PI / 180) * distance;
        }

        public void Display() {
            Image image = Image.CreateCircle(distance + 1, Color.Blue);
            image.CenterOrigin();
            orbit = new Entity(surface.HalfWidth, surface.HalfHeight, image);
            image = Image.CreateCircle(distance, Color.Black);
            image.CenterOrigin();
            orbit.AddGraphic(image);
            orbit.Surface = surface;
            scene.Add(orbit);

            Alarm alarm = new Alarm(remove, 100);
            orbit.AddComponent(alarm);
        }

        public void remove() {
            scene.Remove(orbit);
        }
    }
}
