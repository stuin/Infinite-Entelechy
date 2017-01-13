using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace InfiniteEntelechy.Space {
    class SolarSystem : Entity {
        public int radius;
        public bool loaded = false;

        Text text;
        CelestialObject[] contents;
        Random rand;
        Surface surface;

        int sunRadius;
        bool reverse = false;

        public SolarSystem(string nName, int nX, int nY) : base(nX, nY) {
            Name = nName;
        }

        public void init() {
            //Generate features
            rand = new Random(Name.GetHashCode());
            contents = new CelestialObject[rand.Next(10)];
            sunRadius = rand.Next(75, 150);
            radius = (int)(sunRadius * 1.25);
            if(rand.Next(2) == 1) reverse = true;

            //Generate planet locations
            int size = 0;
            for(int i = 0; i < contents.Length; i++) {
                size = rand.Next(10, 50);
                int distance = radius + rand.Next(size + 5, 100);
                radius = distance + size;
                contents[i] = new CelestialObject(this, Name + ':' + Namer.get(7,rand) + rand.Next(100), distance, size, true, reverse);
            }
            radius += size * 2;
        }

        public void load(ViewPort scene) {
            if(!loaded) {
                //Display sun
                Image image = Image.CreateCircle(sunRadius, Color.Orange);
                image.CenterOrigin();
                AddGraphic(image);
                scene.Add(this);

                //Set up background surface
                surface = new Surface(radius * 2);
                surface.AutoClear = false;
                surface.SetPosition(Position);
                surface.CenterOrigin();
                scene.AddGraphic(surface);

                //Display Planets
                for(int i = contents.Length - 1; i >= 0; i--) {
                    contents[i].init();
                    contents[i].load(scene,surface);
                }

                //Display name of system
                text = new Text(Name, 32);
                text.SetPosition(X + sunRadius, Y + sunRadius);
                text.CenterOrigin();
                scene.AddGraphic(text);

                loaded = true;
                Layer = 2;
            }
        }

        public void remove() {
            //Remove sun
            Scene.Remove(this);
            Scene.RemoveGraphic(text);
            Scene.RemoveGraphic(surface);
            loaded = false;

            //Remove planets
            for(int i = 0; i < contents.Length; i++) contents[i].remove();
        }
    }
}
