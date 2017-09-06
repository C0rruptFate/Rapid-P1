using Microsoft.Xna.Framework;
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
        private Texture2D sunAnimationSpriteSheet;
        bool isPlayable = false;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private float screenHeight, screenWidth;
        Vector2 ball1Pos = new Vector2(0, 0);
        Vector2 ball2Pos = new Vector2(0, 0);
        static Vector2[] planetPos = new Vector2[3];
        Planet p1,p2,p3,p4,p5,p6;
        PlayerControl control;
        SpriteFont spriteFont;
        public string gameState;
        List<Planet> planets = new List<Planet>();
        List<Planet> p1Planets = new List<Planet>();
        List<Planet> p2Planets = new List<Planet>();
        private Texture2D[] playerWinImages = new Texture2D[5];
        Texture2D[] playerScores = new Texture2D[10];
        static int player1Score, player2Score;
        Texture2D spriteSheet1, spriteSheet2, spriteSheet3, spriteSheetLaunch;
        float time;
        float frameTime = 0.1f;
        int frameIndex;
        const int totalFrames = 16;
        int frameHeight = 512;
        int frameWidth = 512;
        int frameHeight2 = 1024;
        int frameWidth2 = 1024;
        float countdownTimer = 10;
        const float something = 0.02f;
        static int gameCount = 0;

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
            sun = Content.Load<Texture2D>("sunMaskOuter");
            for (int i = 0; i < 4; i++)
            {
                int k = i + 1;
                planet[i] = Content.Load<Texture2D>("Button" + k);
                playerWinImages[i] = Content.Load<Texture2D>("win_player" + k);
            }

            for (int i = 0; i < 10; i++)
            {
                playerScores[i] = Content.Load<Texture2D>("score_" + i);
            }
            //planet = Content.Load<Texture2D>("planet1");
            planet1 = Content.Load<Texture2D>("planet2");   
            background = Content.Load<Texture2D>("Background");
            startScreen = Content.Load<Texture2D>("logo_solarSlayers");
            win = Content.Load<Texture2D>("win_wins");
            pressStart = Content.Load<Texture2D>("pressStart");
            spriteFont = Content.Load<SpriteFont>("Score");
            spriteSheet1 = Content.Load<Texture2D>("countdown1");
            spriteSheet2 = Content.Load<Texture2D>("countdown2");
            spriteSheet3 = Content.Load<Texture2D>("countdown3");
            spriteSheetLaunch = Content.Load<Texture2D>("countdownLaunch");
            sunAnimationSpriteSheet = Content.Load<Texture2D>("sunSpriteSheet2");

            //Add sounds
            backgroundMusic = Content.Load<Song>("BackgroundMusic");
            soundEffects.Add(Content.Load<SoundEffect>("Bounce")); //0
            soundEffects.Add(Content.Load<SoundEffect>("HitPlayer")); //1
            soundEffects.Add(Content.Load<SoundEffect>("PlayerShoot")); //2
            soundEffects.Add(Content.Load<SoundEffect>("Teleport2")); //3

            //Music
            //MediaPlayer.Stop();
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

            control = new PlayerControl(ball1Pos, ball2Pos, planetPos, sun, planet, planets, playerWinImages,win, soundEffects, sunAnimationSpriteSheet /*,p1Planets, p2Planets*/);
            

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
                if (gameState == GameStates.GameStart.ToString())
                {
                    gameState = GameStates.Countdown.ToString();
                    //isPlayable = true;
                }
                if (gameState == GameStates.GameOver.ToString())
                {
                    restart();
                }
                if (gameState == GameStates.GameOver.ToString() && (player1Score == 3 || player2Score == 3))
                {
                    newGame();
                }

            }
            //else if (((Keyboard.GetState().IsKeyDown(Keys.Enter)) || state.Buttons.Start == ButtonState.Pressed))
            //{
            //    if (gameState == GameStates.GameOver.ToString())
            //    {
            //        restart();
            //    }
            //    if (gameState == GameStates.GameOver.ToString() && (player1Score == 3 || player2Score == 3))
            //    {
            //        newGame();
            //    }
            //}
            else if (Keyboard.GetState().IsKeyDown(Keys.Escape) || state.Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }
            //if (Keyboard.GetState().IsKeyDown(Keys.Enter) || state.Buttons.Start == ButtonState.Pressed && (gameState == GameStates.Countdown.ToString()) && !isPlayable)
            //{
            //    isPlayable = true;
            //}
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
                            //control.IsAlive1 = false;
                            if (control.IsAlive2)
                            {
                                player2Score += 1;
                                gameCount += 1;
                            }
                            //player2Score += 1;
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
                            //control.IsAlive2 = false;
                            if (control.IsAlive1)
                            {
                                player1Score += 1;
                                gameCount += 1;
                            }
                            //player1Score += 1;
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

            //if (countdownTimer >=0)
            //{
            //    countdownTimer--;
            //}

            //time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //while (time > frameTime)
            //{
            //    frameIndex++;
            //    time = 0f;
            //}
            //if (frameIndex > totalFrames) frameIndex = 1;
            //Rectangle source1 = new Rectangle(frameIndex * frameWidth, 0, frameWidth, frameHeight);
            ////Rectangle source2 = new Rectangle(frameIndex * frameWidth, frameHeight/4, frameWidth, frameHeight);
            ////Rectangle source3 = new Rectangle(frameIndex * frameWidth, frameHeight/2, frameWidth, frameHeight);
            ////Rectangle source4 = new Rectangle(frameIndex * frameWidth, frameHeight * 3/4, frameWidth, frameHeight);
            //Vector2 position = new Vector2(500, 500);

            //    if (gameState == GameStates.Countdown.ToString())
            //    {
            //    spriteBatch.Begin();
            //        spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            //        if (countdownTimer <= 300 && countdownTimer > 200)
            //        {
            //            spriteBatch.Draw(spriteSheet3, position, source1, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            //            //countdownTimer -= 10;
            //        }
            //        else if (countdownTimer <= 200 && countdownTimer > 100)
            //        {
            //            spriteBatch.Draw(spriteSheet2, position, source1, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            //            //countdownTimer -= 10;
            //        }
            //        else if (countdownTimer < 100 && countdownTimer > 0)
            //        {
            //            spriteBatch.Draw(spriteSheet1, position, source1, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            //        }
            //        else//don't show sprite
            //        {
            //            gameState = GameStates.InGame.ToString();
            //        isPlayable = true;
            //        }
            //    spriteBatch.End();
            //    }
                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > frameTime)
            {
                frameIndex++;
                time = 0f;
            }
            if (frameIndex > totalFrames) frameIndex = 1;
            Rectangle source1 = new Rectangle(frameIndex * frameWidth, 0, frameWidth, frameHeight);
            Rectangle source2 = new Rectangle(frameIndex * frameWidth2, frameHeight2, frameWidth2, frameHeight2);
            //Rectangle source3 = new Rectangle(frameIndex * frameWidth, frameHeight/2, frameWidth, frameHeight);
            //Rectangle source4 = new Rectangle(frameIndex * frameWidth, frameHeight * 3/4, frameWidth, frameHeight);
            Vector2 position1 = new Vector2(700, 300);
            Vector2 position2 = new Vector2(500, 0);
            // TODO: Add your drawing code here

            spriteBatch.Begin();
            if (gameState == GameStates.GameStart.ToString())
            {
                if (gameCount==0)
                {
                    spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startScreen, new Vector2(0, 0), Color.White);
                    //spriteBatch.Draw(pressStart, new Vector2(900, 700), Color.White);
                    spriteBatch.Draw(pressStart, new Vector2(900, 700), null, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(pressStart, new Vector2(900, 700), null, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
                }
                //spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
                //spriteBatch.Draw(startScreen, new Vector2(0, 0), Color.White);
                ////spriteBatch.Draw(pressStart, new Vector2(900, 700), Color.White);
                //spriteBatch.Draw(pressStart, new Vector2(900, 700), null, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);

                //spriteBatch.Draw(spriteSheet, position, source1, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }
            else if (gameState==GameStates.Countdown.ToString())
            {
                spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
                if (gameCount == 0)
                {
                    if (countdownTimer <= 10 && countdownTimer > 08)
                    {
                        spriteBatch.Draw(spriteSheet3, position1, source1, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                        countdownTimer -= something;
                    }
                    else if (countdownTimer <= 08 && countdownTimer > 06)
                    {
                        spriteBatch.Draw(spriteSheet2, position1, source1, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                        countdownTimer -= something;
                    }
                    else if (countdownTimer <= 06 && countdownTimer > 04)
                    {
                        spriteBatch.Draw(spriteSheet1, position1, source1, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                        countdownTimer -= something;
                    }
                    else if (countdownTimer <= 04 && countdownTimer > 02)
                    {
                        spriteBatch.Draw(spriteSheetLaunch, position2, source2, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                        countdownTimer -= something;
                    }
                    else
                    {
                        gameState = GameStates.InGame.ToString();
                        isPlayable = true;
                    }
                }
                else
                {
                    if (countdownTimer <= 10 && countdownTimer > 08)
                    {
                        spriteBatch.Draw(spriteSheetLaunch, position2, source2, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                        countdownTimer -= something;
                    }
                    else
                    {
                        gameState = GameStates.InGame.ToString();
                        isPlayable = true;
                    }
                }
                
                //spriteBatch.Draw(spriteSheet2, position, source1, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                //isPlayable = true;
            }
            else
            {
                spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
                switch (player1Score)
                {
                    case 1:
                        spriteBatch.Draw(playerScores[1], new Vector2(30, 0), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(playerScores[2], new Vector2(30, 0), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(playerScores[3], new Vector2(30, 0), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(playerScores[4], new Vector2(30, 0), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(playerScores[5], new Vector2(30, 0), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(playerScores[6], new Vector2(30, 0), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(playerScores[7], new Vector2(30, 0), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(playerScores[8], new Vector2(30, 0), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(playerScores[9], new Vector2(30, 0), Color.White);
                        break;
                    case 0:
                        spriteBatch.Draw(playerScores[0], new Vector2(30, 0), Color.White);
                        break;
                    default:
                        break;
                }
                switch (player2Score)
                {
                    case 1:
                        spriteBatch.Draw(playerScores[1], new Vector2(GameConstants.WindowWidth - 100, 0), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(playerScores[2], new Vector2(GameConstants.WindowWidth - 100, 0), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(playerScores[3], new Vector2(GameConstants.WindowWidth - 100, 0), Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(playerScores[4], new Vector2(GameConstants.WindowWidth - 100, 0), Color.White);
                        break;
                    case 5:
                        spriteBatch.Draw(playerScores[5], new Vector2(GameConstants.WindowWidth - 100, 0), Color.White);
                        break;
                    case 6:
                        spriteBatch.Draw(playerScores[6], new Vector2(GameConstants.WindowWidth - 100, 0), Color.White);
                        break;
                    case 7:
                        spriteBatch.Draw(playerScores[7], new Vector2(GameConstants.WindowWidth - 100, 0), Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(playerScores[8], new Vector2(GameConstants.WindowWidth - 100, 0), Color.White);
                        break;
                    case 9:
                        spriteBatch.Draw(playerScores[9], new Vector2(GameConstants.WindowWidth - 100, 0), Color.White);
                        break;
                    case 0:
                        spriteBatch.Draw(playerScores[0], new Vector2(GameConstants.WindowWidth - 100, 0), Color.White);
                        break;
                    default:
                        break;
                }
                //spriteBatch.DrawString(spriteFont, player2Score.ToString(), new Vector2(GameConstants.WindowWidth - 100, 0),Color.White);
                //spriteBatch.DrawString(spriteFont, player1Score.ToString(), Vector2.Zero, Color.White);
                if (player1Score == 3)
                {
                    spriteBatch.Draw(playerWinImages[0], new Vector2(500, 200), Color.White);
                    spriteBatch.Draw(win, new Vector2(500, 200), Color.White);
                    gameState = GameStates.GameOver.ToString();
                    isPlayable = false;
                }
                if (player2Score == 3)
                {
                    spriteBatch.Draw(playerWinImages[1], new Vector2(500, 200), Color.White);
                    spriteBatch.Draw(win, new Vector2(500, 200), Color.White);
                    gameState = GameStates.GameOver.ToString();
                    isPlayable = false;

                }
                //if ((player1Score > 0 && player1Score <3) && (player2Score > 0 && player2Score <3))
                //{
                //    spriteBatch.Draw(win, new Vector2(500, 200), Color.White);
                //    //restart();
                //}

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
            soundEffects.Clear();
            Program.shouldRestart = true;
            this.Exit();
        }
        public void newGame()
        {
            player1Score = 0;
            player2Score = 0;
            gameCount = 0;
            control.resetVariables();
            Program.shouldRestart = true;
            gameState = GameStates.GameStart.ToString();
            this.Exit();
        }
    }

    public enum GameStates
    {
        Countdown,
        GameStart,
        InGame,
        GameOver
    }
}
