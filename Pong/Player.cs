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
    class Player
    {
        private string name;
        public int numero;
        private Texture2D sprite;
        public Vector2 position;
        public float moveSpeed { get; private set; }
        public int Width {
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

        public Player(string name, int numero)
        {
            this.name = name;
            this.numero = numero;
            this.moveSpeed = 8f;
        }

        public void Initialize(Texture2D sprite, Vector2 position)
        {
            this.sprite = sprite;
            this.position = position;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void setMoveSpeed(float moveSpeed)
        {
            this.moveSpeed = moveSpeed;
        }

    }
}
