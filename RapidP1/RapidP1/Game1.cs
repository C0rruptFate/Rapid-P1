using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;

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
        PlayerControl control;

        List<Planet> planets = new List<Planet>();

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
            sun = Content.Load<Texture2D>("sunP2");
            planet = Content.Load<Texture2D>("planet1");
            planet1 = Content.Load<Texture2D>("planet2");
            background = Content.Load<Texture2D>("Background");
            p = new Planet(planet, planetPos[1]);

            p2 = new Planet(planet, planetPos[2]);

            planets.Add(p);

            control = new PlayerControl(ball1Pos, ball2Pos, planetPos, sun, planet);




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

            control.Update(gameTime);

            foreach (Planet planet in planets)
            {
                planet.Update(gameTime);
            }

            for (int i = 0; i < planets.Count; i++)
            {
                for (int j = i + 1; j < planets.Count; j++)
                {
                    if (!planets[i].InOrbit && !planets[j].InOrbit)
                    {
                        CollisionResolutionInfo collsionInfo =
                            CollisionUtils.CheckCollision((int)gameTime.ElapsedGameTime.TotalMilliseconds,
                                                          GameConstants.WindowWidth, GameConstants.WindowHeight,
                                                          planets[i].Velocity, planets[i].DrawRectangle,
                                                          planets[j].Velocity, planets[j].DrawRectangle
                                                          );

                        //To detect collison between planet i and planet j 
                        if (collsionInfo != null)
                        {

                            planets[i].Velocity = collsionInfo.FirstVelocity;
                            planets[i].DrawRectangle = collsionInfo.FirstDrawRectangle;

                            planets[j].Velocity = collsionInfo.SecondVelocity;
                            planets[j].DrawRectangle = collsionInfo.SecondDrawRectangle;

                        }
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

            foreach(Planet planet in planets)
            {
                planet.Draw(spriteBatch);
            }

            control.Draw(spriteBatch);

            //spriteBatch.Draw(planet, planet2Pos, null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
