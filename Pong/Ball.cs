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
    class Ball
    {

        private Texture2D sprite;
        public Vector2 basePosition;
        public Vector2 position;
        public int directionX = 1;
        public int directionY = 1;
        private float baseMoveSpeed = 3f;
        public float moveSpeed = 3f;

        public int windowWidth;
        public int windowHeight;
        public bool hasStarted = false;

        public int Width
        {
            get
            {
                return this.sprite.Width;
            }
        }

        public int Height
        {
            get
            {
                return this.sprite.Height;
            }
        }

        public Ball(int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public void Initialize(Texture2D sprite, Vector2 position)
        {
            this.sprite = sprite;
            this.position = position;
            this.basePosition = new Vector2(position.X, position.Y);
        }

        public void Update(GameTime gameTime)
        {
            if (hasStarted)
            {
                this.position.X += this.moveSpeed * this.directionX;
                this.position.Y += this.moveSpeed * this.directionY;
                if (this.position.Y >= this.windowHeight - this.Height || this.position.Y <= 0)
                {
                    this.directionY *= -1;
                }
                if(this.position.X <= 0 || this.position.X >= this.windowWidth)
                {
                    this.hasStarted = false;
                    this.position.X = this.basePosition.X;
                    this.position.Y = this.basePosition.Y;
                    this.moveSpeed = this.baseMoveSpeed;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.sprite, this.position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

    }
}
