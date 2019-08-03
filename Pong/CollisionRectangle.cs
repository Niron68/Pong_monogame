using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    class CollisionRectangle : CollisionBox 
    {
        private int height;
        private int width;

        public CollisionRectangle(Vector2 position, int width, int height) : base (position)
        {
            this.width = width;
            this.height = height;
        }

        public override bool Intersect(CollisionBox collision)
        {
            return true;
        }

        public override bool isInside(Vector2 point)
        {
            bool res = false;
            if(point.X > this.position.X && point.X < this.position.X + this.width)
                if (point.Y > this.position.Y && point.Y < this.position.Y + this.height)
                    res = true;
            return res;
        }
    }
}
