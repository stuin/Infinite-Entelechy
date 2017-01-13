using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace InfiniteEntelechy.Components {
    class SpaceMovement : BaseMovement {
        float velocity = 0;
        int acceleration;
        bool brakes = false;

        public SpaceMovement(int nSpeed, int nAccel) : base(nSpeed) {
            acceleration = nAccel;
        }

        public override void Update() {
            base.Update();
            Entity.X += velocity * direction.X;
            Entity.Y += velocity * direction.Y;

            brakes = false;
            if(!arrived && destination.X - Entity.Position.X < velocity && destination.Y - Entity.Position.Y < speed) reverse();
        }

        public override void forward() {
            if(velocity < speed && !brakes) velocity += acceleration;
        }

        public override void reverse() {
            velocity -= (float)acceleration / 2;
            if(velocity < 0) velocity = 0;
            brakes = true;
        }
    }
}
