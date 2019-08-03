using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        
        private List<Player> playerList;
        private Ball ball1;
        private GraphicsDeviceManager graphics;
        private  SpriteBatch spriteBatch;
        private Texture2D background;
        private Texture2D ball;
        private Texture2D player;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        private List<GamePadState> currentGamePadState;
        private List<GamePadState> previousGamePadState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            playerList = new List<Player>();
            playerList.Add(new Player("Joueur 1", 1));
            playerList.Add(new Player("Joueur 2", 2));
            currentGamePadState = new List<GamePadState>();
            previousGamePadState = new List<GamePadState>();
            currentGamePadState.Add(new GamePadState());
            currentGamePadState.Add(new GamePadState());
            previousGamePadState.Add(new GamePadState());
            previousGamePadState.Add(new GamePadState());
            ball1 = new Ball(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("bg");
            ball = Content.Load<Texture2D>("balle_pong");
            player = Content.Load<Texture2D>("barre_pong");
            Vector2 positionP1 = new Vector2(5, 0);
            Vector2 positionP2 = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width - (player.Width + 5), 0);
            playerList[0].Initialize(player, positionP1);
            playerList[1].Initialize(player, positionP2);
            Vector2 positionBall = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - ball.Width / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - ball.Height / 2);
            ball1.Initialize(ball, positionBall);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousGamePadState[0] = currentGamePadState[0];
            previousGamePadState[1] = currentGamePadState[1];
            previousKeyboardState = currentKeyboardState;

            currentGamePadState[0] = GamePad.GetState(PlayerIndex.One);
            currentGamePadState[1] = GamePad.GetState(PlayerIndex.Two);
            currentKeyboardState = Keyboard.GetState();

            ball1.Update(gameTime);
            UpdatePlayer(gameTime);
            UpdateCollision(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 600), Color.White);
            playerList[0].Draw(spriteBatch);
            playerList[1].Draw(spriteBatch);
            ball1.Draw(spriteBatch);
            spriteBatch.End();

            // TODO: Add your drawing code here
            //base.BeginDraw();
            base.Draw(gameTime);
            //base.EndDraw();
        }

        public void UpdatePlayer(GameTime gametime)
        {
            if (currentGamePadState[0].Buttons.LeftShoulder == ButtonState.Pressed)
            {
                playerList[0].setMoveSpeed(16f);
            }
            else
            {
                playerList[0].setMoveSpeed(8f);
            }

            if (currentGamePadState[1].Buttons.LeftShoulder == ButtonState.Pressed)
            {
                playerList[1].setMoveSpeed(16f);
            }
            else
            {
                playerList[1].setMoveSpeed(8f);
            }


            playerList[0].position.Y -= currentGamePadState[0].ThumbSticks.Left.Y * playerList[0].moveSpeed;
            playerList[1].position.Y -= currentGamePadState[1].ThumbSticks.Left.Y * playerList[1].moveSpeed;

            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                playerList[1].position.Y += playerList[1].moveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                playerList[1].position.Y -= playerList[1].moveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                playerList[0].position.Y += playerList[0].moveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Z))
            {
                playerList[0].position.Y -= playerList[0].moveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Space) || currentGamePadState[0].Buttons.Start == ButtonState.Pressed)
            {
                ball1.hasStarted = true;
            }

            playerList[0].position.Y = MathHelper.Clamp(playerList[0].position.Y, 0, GraphicsDevice.Viewport.Height - playerList[0].Height);
            playerList[1].position.Y = MathHelper.Clamp(playerList[1].position.Y, 0, GraphicsDevice.Viewport.Height - playerList[1].Height);
        }

        public void UpdateCollision(GameTime gameTime)
        {
            Rectangle ballRectangle = new Rectangle((int) ball1.position.X, (int) ball1.position.Y, ball1.Width, ball1.Height);
            Rectangle playerRectangle;
            foreach(Player elements in this.playerList)
            {
                playerRectangle = new Rectangle((int) elements.position.X, (int) elements.position.Y, elements.Width, elements.Height);
                if (ballRectangle.Intersects(playerRectangle))
                {
                    ball1.directionX *= -1;
                    ball1.moveSpeed += 0.1f;
                    if(currentGamePadState[elements.numero-1].ThumbSticks.Right.Y < 0f)
                    {
                        ball1.directionY = 1;
                    }else if(currentGamePadState[elements.numero - 1].ThumbSticks.Right.Y > 0f)
                    {
                        ball1.directionY = -1;
                    }
                }
            }
        }

    }
}
