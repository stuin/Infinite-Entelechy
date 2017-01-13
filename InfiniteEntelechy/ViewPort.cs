using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using InfiniteEntelechy.Space;

namespace InfiniteEntelechy {
    class ViewPort : Scene {
        public int time;
        public Vector2 position;

        public ViewPort(int size) {
            //Set edge of galaxy
            CameraBounds.Height = size;
            CameraBounds.Width = size;

            //Position camera at center
            CameraX = size / 2;
            CameraY = size / 2;

            time = 1;
            position = new Vector2();
        }

        public override void Update() {
            base.Update();
            int speed = 20;
            time++;
            UseCameraBounds = true;
            
            //Move camera with arrow keys
            if(Input.KeyDown(Key.Up)) CameraY -= speed;
            if(Input.KeyDown(Key.Down)) CameraY += speed;
            if(Input.KeyDown(Key.Left)) CameraX -= speed;
            if(Input.KeyDown(Key.Right)) CameraX += speed;

            position.X = CameraCenterX;
            position.Y = CameraCenterY;
        }
    }
}
