using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace InfiniteEntelechy.Components {
    class BaseMovement : Component {
        public Vector2 destination;
        Queue<Vector2> path = new Queue<Vector2>();
        public Vector2 direction;

        float angle = 0;
        public int speed;
        int oldFace;
        bool dir = true;
        public bool arrived = true;

        public BaseMovement(int nSpeed) {
            speed = nSpeed;
            direction = new Vector2();
            destination = new Vector2();
            rotate(0);
        }

        public override void Update() {
            base.Update();
            if(!arrived) {
                Vector2 value = (destination - Entity.Position) / direction;
                int face = (int)value.X - (int)value.Y;
                if(value.X > 0 && value.Y > 0 && face < (int)destination.X - (int)Entity.X) forward();
                else rotate(1);
            }

            if(Entity.Input.KeyDown(Key.W)) forward();
            if(Entity.Input.KeyDown(Key.D)) rotate(-2);
            if(Entity.Input.KeyDown(Key.A)) rotate(2);
            if(Entity.Input.KeyDown(Key.S)) reverse();

            if(Entity.Input.MouseButtonPressed(MouseButton.Right)) {
                path.Clear();
                arrived = true;
            }
            if(Entity.Input.MouseButtonDown(MouseButton.Right)) {
                Vector2 vector = new Vector2(Entity.Input.MouseScreenX, Entity.Input.MouseScreenY);
                set(vector);
                Entity.Scene.Add(new Particle(vector.X, vector.Y, "Assets/Star.png", 10, 10) {
                    LifeSpan = 1000,
                });
            }
        }

        public void set(Vector2 nDestination) {
            if(arrived) destination = nDestination;
            else path.Enqueue(nDestination);
            arrived = false;
        }

        public virtual void forward() {
            Entity.X += direction.X * speed;
            Entity.Y += direction.Y * speed;
            while(!arrived && Math.Abs(destination.X - Entity.Position.X) < speed * 7 && Math.Abs(destination.Y - Entity.Position.Y) < speed * 7) {
                if(path.Count == 0) arrived = true;
                else destination = path.Dequeue();
            }
        }

        public virtual void reverse() {
            Entity.X -= direction.X * speed;
            Entity.Y -= direction.Y * speed;
        }

        public void rotate(int speed) {
            angle += speed;
            direction.X = (float)Math.Sin(angle * Math.PI / 180);
            direction.Y = (float)Math.Cos(angle * Math.PI / 180);
            if(Entity != null) foreach(Graphic graphic in Entity.Graphics) graphic.Angle = angle;
        }
    }
}
