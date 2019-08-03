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
    class CollisionCircle : CollisionBox
    {
        private int diametre;
        private float rayon
        {
            get
            {
                return this.diametre / 2;
            }
        }

        private Vector2 center
        {
            get
            {
                return new Vector2((int)this.position.X + this.rayon, (int)this.position.Y + this.rayon);
            }
        }

        public CollisionCircle(Vector2 position, int diametre) : base(position)
        {
            this.diametre = diametre;

        }
        public override bool Intersect(CollisionBox collision)
        {
            return true;
        }

        public override bool isInside(Vector2 point)
        {
            bool res = false;
            Vector2 ecart = new Vector2(Math.Abs(point.X - this.position.X),Math.Abs(point.Y - this.position.Y));
            int sommeCarre = (int) ecart.X * (int) ecart.X + (int) ecart.Y * (int) ecart.Y;
            if(Math.Sqrt(sommeCarre) <= this.rayon)
                res = true;
            return res;
        }
    }
}
