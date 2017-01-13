using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using InfiniteEntelechy.Components;

namespace InfiniteEntelechy.Space {
    class CelestialObject : Entity {
        public int radius;

        Entity orbitting;
        CelestialObject[] satelites;
        CleanOrbit cleanOrbit;
        Land.Planet land;
        Random rand;
        Color color;

        int distance;
        bool planet;
        bool reverse;

        public CelestialObject(Entity nOrbitting, String nName, int nDistance, int nRadius, bool nPlanet, bool nReverse) : base(nOrbitting.X, nOrbitting.Y) {
            orbitting = nOrbitting;
            radius = nRadius;
            distance = nDistance;
            Name = nName;
            planet = nPlanet;
            reverse = nReverse;

            //Load Orbit
            cleanOrbit = new CleanOrbit(orbitting, distance);

            if(planet) color = Color.Green;
            else color = Color.Grey;
        }

        public void init() {
            //Generate numbers for orbit
            rand = new Random(Name.GetHashCode());
            double speed = rand.NextDouble();
            cleanOrbit.offset = rand.Next(360);
            land = new Land.Planet(radius, Name);

            if(planet) satelites = new CelestialObject[rand.Next(radius / 10)];
            else speed *= 2;

            if(reverse) speed *= -1;
            cleanOrbit.speed = speed;

            //Create Moons
            for(int i = 0; planet && i < satelites.Length; i++) {
                int distance = rand.Next((i + 1) * 3 + radius, radius * 2);
                satelites[i] = new CelestialObject(this, Name + ":" + i, distance, rand.Next(1, radius / 3 + 1), false, reverse);
                satelites[i].init();
            }
        }

        public void load(ViewPort scene, Surface surface) {
            cleanOrbit.scene = scene;
            cleanOrbit.surface = surface;
            Layer = 5;

            //Display Planet
            Image image = Image.CreateCircle(radius, color);
            image.CenterOrigin();
            AddGraphic(image);
            scene.Add(this);

            //Load Moons
            for(int i = 0; planet && i < satelites.Length; i++) satelites[i].load(scene,surface);

            //Display circle of orbit
            AddComponent(cleanOrbit);
            cleanOrbit.Display();
        }

        public void remove() {
            //Remove Planet
            Scene.Remove(this);
            for(int i = 0; planet && i < satelites.Length; i++) satelites[i].remove();
        }
    }
}
