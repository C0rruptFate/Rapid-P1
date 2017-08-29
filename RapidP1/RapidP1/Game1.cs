using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace RapidP1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private Texture2D sun;
        private static Texture2D planet;
        private static Texture2D planet1;
        private Texture2D background;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private float screenHeight, screenWidth;
        Vector2 ball1Pos = new Vector2(0, 0);
        Vector2 ball2Pos = new Vector2(0, 0);
        static Vector2[] planetPos = new Vector2[5];
        Planet p;

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
            screenHeight = GameConstants.WindowHeight;
            screenWidth = GameConstants.WindowWidth;
            ball1Pos.X = 0;
            ball1Pos.Y = screenHeight / 2;
            ball2Pos.X = screenWidth - 100;
            ball2Pos.Y = (screenHeight)/ 2;
            planetPos[1].X = ball1Pos.X + 20f;
            planetPos[1].Y = ball1Pos.Y + 100f;
            planetPos[2].X = ball2Pos.X - 20f;
            planetPos[2].Y = ball2Pos.Y + 90f;
            planetPos[3].X = ball1Pos.X + 80f;
            planetPos[3].Y = ball1Pos.Y + 90f;
            

            //Planet P1 = new Planet(planet,ball1Pos);
            //Vector2 acceleration = new Vector2(5, 5);
            //P1.giveAcceleration(acceleration);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sun = Content.Load<Texture2D>("ball");
            planet = Content.Load<Texture2D>("planet1");
            planet1 = Content.Load<Texture2D>("planet2");
            background = Content.Load<Texture2D>("Background");
            //p = new Planet(planet, planetPos[1]);




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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            //p.Update(gameTime);
            if (planetPos[1].X <= ball1Pos.X + 200f)
            {



                if ((Keyboard.GetState().IsKeyDown(Keys.W)) && ball1Pos.Y != 0) //up
                {
                    ball1Pos.Y -= 5;
                    planetPos[1].Y -= 5;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S) && ball1Pos.Y != (GraphicsDevice.DisplayMode.Height - 100)) //down
                {
                    ball1Pos.Y += 5;
                    planetPos[1].Y += 5;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A) && ball1Pos.X != 0) //left
                {
                    ball1Pos.X -= 5;
                    planetPos[1].X -= 5;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) && ball1Pos.X != (GraphicsDevice.DisplayMode.Width - 100)) //right
                {
                    ball1Pos.X += 5;
                    planetPos[1].X += 5;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Q) && ball1Pos.X != (GraphicsDevice.DisplayMode.Width - 100)) //right
                {
                    //Draw(gameTime, 1);

                //shooting
                }
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.NumPad8)) && ball2Pos.Y != 0) //up
            {
                ball2Pos.Y -= 5;
                planetPos[2].Y -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2) && ball2Pos.Y != (GraphicsDevice.DisplayMode.Height - 100)) //down
            {
                ball2Pos.Y += 5;
                planetPos[2].Y += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad4) && ball2Pos.X != 0) //left
            {
                ball2Pos.X -= 5;
                planetPos[2].X -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad6) && ball2Pos.X != (GraphicsDevice.DisplayMode.Width - 100)) //right
            {
                ball2Pos.X += 5;
                planetPos[2].X += 5;
            }


            // Check the device for Player One
            GamePadCapabilities capabilities1 = GamePad.GetCapabilities(PlayerIndex.One);

            if (capabilities1.IsConnected)
            {
                // Get the current state of Controller1
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                // You can check explicitly if a gamepad has support for a certain feature
                if (capabilities1.HasLeftXThumbStick)
                {
                    // Check for movement
                    //Move Left
                    if (state.ThumbSticks.Left.X < -0.5f && ball1Pos.X != (GraphicsDevice.DisplayMode.Width - 100))
                        ball1Pos.X -= 10.0f;
                    //Move Right
                    else if (state.ThumbSticks.Left.X > 0.5f && ball1Pos.X != (GraphicsDevice.DisplayMode.Width - 100))
                        ball1Pos.X += 10.0f;
                    //Move Up
                    if (state.ThumbSticks.Left.Y < -0.5f && ball1Pos.Y != (GraphicsDevice.DisplayMode.Height - 100))
                        ball1Pos.Y += 10.0f;
                    //Move Down
                    else if (state.ThumbSticks.Left.Y > 0.5f && ball1Pos.Y != (GraphicsDevice.DisplayMode.Height - 100))
                        ball1Pos.Y -= 10.0f;
                }

                //Player shoot/dash button
                if (capabilities1.HasAButton)
                {
                    //Shoot
                    if (state.Buttons.A == ButtonState.Pressed)
                    {
                        planetPos[1].X += 1;
                    }
                    //Dash
                    if (state.Buttons.X == ButtonState.Pressed)
                    {
                        //[TODO] Dash.
                    }
                }

            }


            // Check the device for Player Two
            GamePadCapabilities capabilities2 = GamePad.GetCapabilities(PlayerIndex.Two);

            if (capabilities2.IsConnected)
            {
                // Get the current state of Controller1
                GamePadState state = GamePad.GetState(PlayerIndex.Two);

                // You can check explicitly if a gamepad has support for a certain feature
                if (capabilities2.HasLeftXThumbStick)
                {
                    // Check for movement
                    //Move Left
                    if (state.ThumbSticks.Left.X < -0.5f && ball2Pos.X != (GraphicsDevice.DisplayMode.Width - 100))
                        ball2Pos.X -= 10.0f;
                    //Move Right
                    else if (state.ThumbSticks.Left.X > 0.5f && ball2Pos.X != (GraphicsDevice.DisplayMode.Width - 100))
                        ball2Pos.X += 10.0f;
                    //Move Up
                    if (state.ThumbSticks.Left.Y < -0.5f && ball2Pos.Y != (GraphicsDevice.DisplayMode.Height - 100))
                        ball2Pos.Y += 10.0f;
                    //Move Down
                    else if (state.ThumbSticks.Left.Y > 0.5f && ball2Pos.Y != (GraphicsDevice.DisplayMode.Height - 100))
                        ball2Pos.Y -= 10.0f;
                }

                //Player shoot/Dash button
                if (capabilities2.HasAButton)
                {
                    //Shoot
                    if (state.Buttons.A == ButtonState.Pressed)
                    {
                        //[TODO] shoot planet like missle
                    }
                    //Dash
                    if (state.Buttons.X == ButtonState.Pressed)
                    {
                        //[TODO] Dash.
                    }
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            

            // TODO: Add your drawing code here
            
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0,0),Color.White);
            spriteBatch.Draw(sun, ball1Pos, null, Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
            spriteBatch.Draw(sun, ball2Pos, null, Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
            spriteBatch.Draw(planet, planetPos[1], null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(planet, planetPos[3], null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(planet1, planetPos[2], null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);

            //p.Draw(spriteBatch);

            //spriteBatch.Draw(planet, planet2Pos, null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //public void Draw(GameTime gameTime, int planetCount)
        //{
        //    spriteBatch.Begin();
        //    while (planetPos[1].X!=ball2Pos.X)
        //    {
        //        spriteBatch.Draw(planet, planetPos[planetCount], null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
        //        planetPos[planetCount].X += 1;
        //    }
        //    spriteBatch.End();
        //}
    }
}
