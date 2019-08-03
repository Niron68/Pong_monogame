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
    abstract class CollisionBox
    {

        protected Vector2 position;

        public CollisionBox(Vector2 position)
        {
            this.position = position;
        }

        abstract public bool Intersect(CollisionBox collision);
        abstract public bool isInside(Vector2 point);
    }
}
