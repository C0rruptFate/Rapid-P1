﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace RapidP1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region init
        private Texture2D sun, win, pressStart;
        private Texture2D[] planet = new Texture2D[10];
        private Texture2D planet1;
        private Texture2D background;
        private Texture2D startScreen;
        bool isPlayable = false;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private float screenHeight, screenWidth;
        Vector2 ball1Pos = new Vector2(0, 0);
        Vector2 ball2Pos = new Vector2(0, 0);
        static Vector2[] planetPos = new Vector2[3];
        Planet p1,p2,p3,p4,p5,p6;
        PlayerControl control;
        //GamePlay play;
        public string gameState;
        List<Planet> planets = new List<Planet>();
        List<Planet> p1Planets = new List<Planet>();
        List<Planet> p2Planets = new List<Planet>();
        private Texture2D[] playerWinImages = new Texture2D[5];

        Song backgroundMusic;
        public List<SoundEffect> soundEffects = new List<SoundEffect>();

        #endregion

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
            ball1Pos.X = 230;
            ball1Pos.Y = screenHeight / 2;
            ball2Pos.X = screenWidth - 250;
            ball2Pos.Y = (screenHeight)/ 2;
            //planetPos[1].X = ball1Pos.X + 20f;
            //planetPos[1].Y = ball1Pos.Y + 100f;
            //planetPos[2].X = ball2Pos.X - 20f;
            //planetPos[2].Y = ball2Pos.Y + 90f;
            //planetPos[3].X = ball1Pos.X + 80f;
            //planetPos[3].Y = ball1Pos.Y + 90f;

            gameState = GameStates.GameStart.ToString();

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
            for (int i = 0; i < 4; i++)
            {
                int k = i + 1;
                planet[i] = Content.Load<Texture2D>("Button" + k);
                playerWinImages[i] = Content.Load<Texture2D>("win_player" + k);
            }
            //planet = Content.Load<Texture2D>("planet1");
            planet1 = Content.Load<Texture2D>("planet2");
            background = Content.Load<Texture2D>("Background");
            startScreen = Content.Load<Texture2D>("logo_solarSlayers");
            win = Content.Load<Texture2D>("win_wins");
            pressStart = Content.Load<Texture2D>("pressStart");
            //Add sounds
            backgroundMusic = Content.Load<Song>("BackgroundMusic");
            soundEffects.Add(Content.Load<SoundEffect>("Bounce")); //0
            soundEffects.Add(Content.Load<SoundEffect>("HitPlayer")); //1
            soundEffects.Add(Content.Load<SoundEffect>("PlayerShoot")); //2
            soundEffects.Add(Content.Load<SoundEffect>("Teleport2")); //3

            //Music
            MediaPlayer.Stop();
            MediaPlayer.Play(backgroundMusic);


            //p2 = new Planet(planet, planetPos[2]);

            planets.Add(p1);
            planets.Add(p2);
            planets.Add(p3);
            planets.Add(p4);
            planets.Add(p5);
            planets.Add(p6);

            p1Planets.Add(p1);
            p1Planets.Add(p2);
            p1Planets.Add(p3);

            p2Planets.Add(p4);
            p2Planets.Add(p5);
            p2Planets.Add(p6);

            //play = new GamePlay(gameState, startScreen);

            control = new PlayerControl(ball1Pos, ball2Pos, planetPos, sun, planet, planets, playerWinImages,win, soundEffects  /*,p1Planets, p2Planets*/);
            

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

            GamePadState state = GamePad.GetState(PlayerIndex.One);

            if (((Keyboard.GetState().IsKeyDown(Keys.Enter)) || state.Buttons.Start == ButtonState.Pressed) && !isPlayable) //up
            {
                if (gameState == GameStates.GameStart.ToString() || gameState == GameStates.GameOver.ToString())
                {
                    gameState = GameStates.InGame.ToString();
                    isPlayable = true;
                }

            }
            else if (((Keyboard.GetState().IsKeyDown(Keys.Enter)) || state.Buttons.Start == ButtonState.Pressed))
            {
                if (gameState == GameStates.GameOver.ToString())
                {
                    restart();
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Escape) || state.Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }
            else if (isPlayable)
            {
                control.Update(gameTime);

                //Collisions between planet and players
                foreach (Planet planet in planets)
                {

                    CollisionResolutionInfo collsionInfo1 =
                                CollisionUtils.CheckCollision((int)gameTime.ElapsedGameTime.TotalMilliseconds,
                                                              GameConstants.WindowWidth, GameConstants.WindowHeight,
                                                              planet.Velocity, planet.DrawRectangle,
                                                              Vector2.Zero, control.DrawRectangle1
                                                              );

                    if (collsionInfo1 != null)
                    {
                        planet.Velocity = collsionInfo1.FirstVelocity;
                        planet.DrawRectangle = collsionInfo1.FirstDrawRectangle;

                        if (planet.Owner != 1 && control.IsAlive1)
                        {
                            control.IsAlive1 = false;
                            //Play Audio
                            //soundEffects[1].Play();
                            gameState = GameStates.GameOver.ToString();
                        }
                    }

                    CollisionResolutionInfo collsionInfo2 =
                                CollisionUtils.CheckCollision((int)gameTime.ElapsedGameTime.TotalMilliseconds,
                                                              GameConstants.WindowWidth, GameConstants.WindowHeight,
                                                              planet.Velocity, planet.DrawRectangle,
                                                              Vector2.Zero, control.DrawRectangle2
                                                              );

                    if (collsionInfo2 != null)
                    {
                        planet.Velocity = collsionInfo2.FirstVelocity;
                        planet.DrawRectangle = collsionInfo2.FirstDrawRectangle;

                        if (planet.Owner != 1 && control.IsAlive2)
                        {
                            control.IsAlive2 = false;
                            //Play Audio
                            //soundEffects[1].Play();
                            gameState = GameStates.GameOver.ToString();
                        }
                    }
                }

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

                        if (!planets[i].InOrbit && planets[j].InOrbit)
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
                                //Play Audio
                                //soundEffects[0].Play();

                                //planets[j].Velocity = collsionInfo.SecondVelocity;
                                //planets[j].DrawRectangle = collsionInfo.SecondDrawRectangle;

                            }
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
            if (gameState == GameStates.GameStart.ToString())
            {
                spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(startScreen, new Vector2(0, 0), Color.White);
                //spriteBatch.Draw(pressStart, new Vector2(900, 700), Color.White);
                spriteBatch.Draw(pressStart, new Vector2(900, 700), null, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);

            }
            //if (gameState == GameStates.GameOver.ToString())
            //{
            //    spriteBatch.Draw(screenBackground, new Vector2(0, 0), Color.White);
            //}
            else
            {
                spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

                foreach (Planet planet in planets)
                {
                    if (planet.InOrbit)
                        control.Draw(spriteBatch);
                    else
                        planet.Draw(spriteBatch);
                }

                control.Draw(spriteBatch);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void restart()
        {
            Program.shouldRestart = true;
            this.Exit();
        }
    }

    public enum GameStates
    {
        GameStart,
        InGame,
        GameOver
    }
}
