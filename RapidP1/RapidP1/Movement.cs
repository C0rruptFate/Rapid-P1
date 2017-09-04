using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace RapidP1
{
    public class PlayerControl
    {
        #region Declarations
        Vector2 ball1Pos, ball2Pos;
        Vector2[] planetPos = new Vector2[20];
        Texture2D sunSprite1, finalWinSprite;
        Texture2D[] planetSprite1 = new Texture2D[10];
        Game1 g1 = new Game1();
        Vector2[] positionOffset = new Vector2[10];
        //float angle;
        float[] radius = new float[10];
        float[] angle = new float[10];
        Planet p;
        int planetCount1 = 2;
        int planetCount2 = 5;
        List<Planet> planets = new List<Planet>();
        List<int> countList = new List<int>();
        Rectangle drawRectangle1;
        Rectangle drawRectangle2;
        float[] velocity = new float[10];
        bool isAlive1 = true;
        bool isAlive2 = true;
        float fireRate = 50f;
        double currentGameTime;
        float nextFireP1; //When player 1 can next shoot
        float newSpeedP1; //Multiplier for player 1's planets
        bool joyStickRightP1; //Used to track the players right stick movement.
        float nextSpeedLoseP1; //When player 1 will next lose some speed.
        float speedLose = 10f; //How quickly players lose speed
        float speedLoseIncreaseAmount = 0.1f; //How much speed is lost per tick.
        List<Planet> p1Planets = new List<Planet>(); //List of all player 1's planets.
        float[] p1Angles = new float[10]; //used to track the speed of player 1's orbit speed.

        float nextFireP2; //When player 2 can next shoot
        float newSpeedP2; //Multiplier for player 2's planets
        bool joyStickRightP2; //Used to track the players right stick movement.
        float nextSpeedLoseP2; //When player 2 will next lose some speed.
        List<Planet> p2Planets = new List<Planet>(); //the list of all player 2's planets
        float[] p2Angles = new float[10]; //used to track the speed of player 2's orbit speed.
        Texture2D[] playerWins = new Texture2D[5];

        
        float maxNewSpeed = 2; //The max speed the planets can fly at.
        float minNewSpeed = 0.3f; //The min speed they fly at.

        float minOrbit = 1f;
        float maxOrbit = 3f;
        #endregion

        #region Properties

        public Rectangle CollisionRectangle1
        {
            get { return drawRectangle1; }
        }

        public Rectangle CollisionRectangle2
        {
            get { return drawRectangle2; }
        }

        public Rectangle DrawRectangle1
        {
            get { return drawRectangle1; }
            set { drawRectangle1 = value; }
        }

        public Rectangle DrawRectangle2
        {
            get { return drawRectangle2; }
            set { drawRectangle2 = value; }
        }

        public bool IsAlive1
        {
            get { return isAlive1; }
            set { isAlive1 = value; }
        }

        public bool IsAlive2
        {
            get { return isAlive2; }
            set { isAlive2 = value; }
        }

        #endregion


        public void Update(GameTime gameTime)
        {
            try
            {
                GamePadCapabilities capabilities1 = GamePad.GetCapabilities(PlayerIndex.One);
                GamePadCapabilities capabilities2 = GamePad.GetCapabilities(PlayerIndex.Two);
                //currentGameTime = DateTime.Now.Millisecond;
                currentGameTime++;
                //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //    Exit();
                //Planet p;

                if (isAlive1)
                {
                    if ((Keyboard.GetState().IsKeyDown(Keys.W)) && ball1Pos.Y != 0) //up
                    {
                        ball1Pos.Y -= 5;
                        drawRectangle1.Y -= 5;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S) && ball1Pos.Y != (GameConstants.WindowHeight - 100)) //down
                    {
                        ball1Pos.Y += 5;
                        drawRectangle1.Y += 5;
                    }
                    //if (Keyboard.GetState().IsKeyDown(Keys.A) && ball1Pos.X != 0) //left
                    //{
                    //    ball1Pos.X -= 5;
                    //    drawRectangle1.X -= 5;
                    //}
                    //if (Keyboard.GetState().IsKeyDown(Keys.D) && ball1Pos.X != (GameConstants.WindowWidth - 100)) //right
                    //{
                    //    ball1Pos.X += 5;
                    //    drawRectangle1.X += 5;
                    //}
                }
                else
                {
                    foreach (Planet planet in planets)
                    {
                        if (planet.Owner == 1)
                        {
                            planet.InOrbit = false;
                            planet.Owner = 0;
                            planet.GiveAcceleration(ball1Pos + new Vector2(new Random().Next(1, 3), new Random().Next(1, 3)));
                        }
                    }
                }


                //if (Keyboard.GetState().IsKeyDown(Keys.Q) && ball1Pos.X != (GameConstants.WindowWidth - 100)) //right
                //{
                //    Vector2 acc = new Vector2(1, 1);
                //    p.GiveAcceleration(acc);
                //    //shooting
                //}

                if (isAlive2)
                {
                    if ((Keyboard.GetState().IsKeyDown(Keys.NumPad8)) && ball2Pos.Y != 0) //up
                    {
                        ball2Pos.Y -= 5;
                        drawRectangle2.Y -= 5;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.NumPad2) && ball2Pos.Y != (GameConstants.WindowHeight - 100)) //down
                    {
                        ball2Pos.Y += 5;
                        drawRectangle2.Y += 5;
                    }
                    //if (Keyboard.GetState().IsKeyDown(Keys.NumPad4) && ball2Pos.X != 0) //left
                    //{
                    //    ball2Pos.X -= 5;
                    //    drawRectangle2.X -= 5;
                    //}
                    //if (Keyboard.GetState().IsKeyDown(Keys.NumPad6) && ball2Pos.X != (GameConstants.WindowWidth - 100)) //right
                    //{
                    //    ball2Pos.X += 5;
                    //    drawRectangle2.X += 5;
                    //}
                }
                else
                {
                    foreach (Planet planet in planets)
                    {
                        if (planet.Owner == 2)
                        {
                            planet.InOrbit = false;
                            planet.Owner = 0;
                            planet.GiveAcceleration(ball2Pos + new Vector2(new Random().Next(1, 3), new Random().Next(1, 3)));
                        }

                    }
                }


                radius[0] = 120;
                //Default orbit speed values.
                //angle[0] += 0.1f;
                //angle[1] += 0.08f;
                //angle[2] += 0.06f;


                //angle[0] += 0.1f * newSpeedP1;
                //angle[1] += 0.08f * newSpeedP1;
                //angle[2] += 0.06f * newSpeedP1;

                p1Angles[0] += 0.1f * newSpeedP1; //Used to increase the speed of each players orbit.
                p1Angles[1] += 0.08f * newSpeedP1;
                p1Angles[2] += 0.06f * newSpeedP1;

                p2Angles[0] += 0.1f * newSpeedP2;
                p2Angles[1] += 0.08f * newSpeedP2;
                p2Angles[2] += 0.06f * newSpeedP2;

                //angle[0] += ((float)gameTime.ElapsedGameTime.TotalSeconds * 0.5f);
                //angle[1] += ((float)gameTime.ElapsedGameTime.TotalSeconds * 0.8f);
                //angle[2] += ((float)gameTime.ElapsedGameTime.TotalSeconds * 0.1f);


                for (int i = 1; i < 3; i++)
                {
                    radius[i] = radius[i - 1] + 50;
                }

                velocity[0] = radius[0] * 0.5f;
                velocity[1] = radius[1] * 0.8f;
                velocity[2] = radius[2] * 0.1f;


                #region RotationaAndShoot:

                ////angle += 0.03f;
                //Sai's orbit code
                //foreach (Planet planet in planets)
                //{
                //    if ((planet.InOrbit) && (planet.Owner != 0))
                //    {
                //        for (int i = 0; i < planets.Count; i++)
                //        {
                //            ////positionOffset[i] = new Vector2(radius[i] * (float)Math.Sin(angle * 0.1f), radius[i] * (float)Math.Cos(angle * 0.1f));
                //            positionOffset[i] = new Vector2(radius[i] * (float)Math.Sin(angle[i]), radius[i] * (float)Math.Cos(angle[i]));

                //        }
                //    }
                //}

                //Working on Changing planet orbit per player.
                //Just like Sai's code but changes to allow variable speed increase.
                foreach (Planet planet in p1Planets)
                {
                    if ((planet.InOrbit) && (planet.Owner == 1))
                    {
                        for (int i = 0; i < p1Planets.Count; i++)
                        {
                            //positionOffset[i] = new Vector2(radius[i] * (float)Math.Sin(angle * 0.1f), radius[i] * (float)Math.Cos(angle * 0.1f));
                            positionOffset[i] = new Vector2(radius[i] * (float)Math.Sin(p1Angles[i]), radius[i] * (float)Math.Cos(p1Angles[i]));
                            
                        }
                    }
                }

                foreach (Planet planet in p2Planets)
                {
                    if ((planet.InOrbit) && (planet.Owner == 2))
                    {
                        for (int i = 0; i < p2Planets.Count; i++)
                        {
                            //positionOffset[i] = new Vector2(radius[i] * (float)Math.Sin(angle * 0.1f), radius[i] * (float)Math.Cos(angle * 0.1f));
                            positionOffset[i + 3] = new Vector2(radius[i] * (float)Math.Sin(p2Angles[i]), radius[i] * (float)Math.Cos(p2Angles[i]));
                        }
                    }
                }

                //Code to also rotate the draw rectangles of a planet

                for (int i = 0; i< planets.Count; i++)
                {
                    if (planets[i].InOrbit)
                    {
                        if (planets[i].Owner == 1)
                        {
                            planets[i].SetDrawX((int)(positionOffset[i].X + ball1Pos.X));
                            planets[i].SetDrawY((int)(positionOffset[i].Y + ball1Pos.Y));
                        }
                        else
                        {
                            planets[i].SetDrawX((int)(positionOffset[i].X + ball2Pos.X));
                            planets[i].SetDrawY((int)(positionOffset[i].Y + ball2Pos.Y));
                        }
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Q) && nextFireP1 <= currentGameTime) //p1 shoot
                    {
                        //for (int i = 0; i < 1; i++)
                        //{
                        if (planets[planetCount1].Owner == 1 && planets[planetCount1].InOrbit)
                        {
                            planets[planetCount1].InOrbit = false;
                            planets[planetCount1].Owner = 0;
                            Vector2 velocityOffset = new Vector2((float)(0.2f * (float)Math.Sin(0.2f) - 0.1 * (float)Math.Sin(0.1f)), (float)(0.2 * (float)Math.Cos(0.2) - 0.2 * (float)Math.Cos(0.2)));
                            planets[planetCount1].GiveAcceleration(ball1Pos + positionOffset[planetCount1], velocityOffset);
                            if (planetCount1 <= planets.Count - 3 && !(planetCount1 < 0))
                            {
                                planetCount1--;
                            }

                            nextFireP1 = fireRate + (float)currentGameTime;
                        }
                        //}
                        //planetCount++;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.NumPad0) && nextFireP2 <= currentGameTime) //p2 shoot
                    {
                        if (planets[planetCount2].Owner == 2 && planets[planetCount2].InOrbit)
                        {
                            planets[planetCount2].InOrbit = false;
                            planets[planetCount2].Owner = 0;
                            Vector2 velocityOffset = new Vector2(-(float)(0.2f * (float)Math.Sin(0.2f) - 0.1 * (float)Math.Sin(0.1f)), (float)(0.2 * (float)Math.Cos(0.2) - 0.2 * (float)Math.Cos(0.2)));
                            planets[planetCount2].GiveAcceleration(ball2Pos + positionOffset[planetCount2 - 3], velocityOffset);
                            if (planetCount2 <= planets.Count && !(planetCount2 < 3))
                            {
                                planetCount2--;
                            }
                            nextFireP2 = fireRate + (float)currentGameTime;

                        }
                    }

                    #endregion


                    // Check the device for Player One

                    if (capabilities1.IsConnected)
                    {
                        // Get the current state of Controller1
                        GamePadState state = GamePad.GetState(PlayerIndex.One);

                        // You can check explicitly if a gamepad has support for a certain feature
                        if (capabilities1.HasLeftXThumbStick)
                        {
                            // Check for movement
                            ////Move Left
                            //if (state.ThumbSticks.Left.X < -0.5f && ball1Pos.X != (GameConstants.WindowWidth - 100))
                            //    ball1Pos.X -= 10.0f;
                            ////Move Right
                            //else if (state.ThumbSticks.Left.X > 0.5f && ball1Pos.X != (GameConstants.WindowWidth - 100))
                            //    ball1Pos.X += 10.0f;
                        //Move Up
                        if (state.ThumbSticks.Left.Y < -0.5f && ball1Pos.Y != (GameConstants.WindowHeight - 100))
                            {
                                ball1Pos.Y += 5;
                                drawRectangle1.Y += 5;
                            }
                            //Move Down
                            else if (state.ThumbSticks.Left.Y > 0.5f && ball1Pos.Y != 0)
                            {
                                ball1Pos.Y -= 5;
                                drawRectangle1.Y -= 5;
                            }
                        }
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (capabilities1.HasLeftTrigger)
                    {
                        if (state.Triggers.Left >= 1f && !joyStickRightP1 && state.Triggers.Right <= 0 && state.ThumbSticks.Right.X <= 0.5f)//Tracks the movement of p1's right joystick
                        {
                            newSpeedP1 = newSpeedP1 + speedLoseIncreaseAmount; //increase speed
                            if (newSpeedP1 >= maxNewSpeed) //Makes sure we can't go above the max speed.
                            {
                                newSpeedP1 = maxNewSpeed;
                            }
                            else if (newSpeedP1 <= minNewSpeed) //Makes sure we can't go below the min speed.
                            {
                                newSpeedP1 = minNewSpeed;
                            }
                            nextSpeedLoseP1 = speedLose + (float)currentGameTime; //resets the time it takes to tick back down
                            joyStickRightP1 = true; //Makes sure you need to move to the other side first.
                        }
                        else if (state.Triggers.Right >= 1f && joyStickRightP1 && state.Triggers.Left <= 0 && state.ThumbSticks.Right.X >= -0.5f)
                        {
                            newSpeedP1 = newSpeedP1 + speedLoseIncreaseAmount;
                            if (newSpeedP1 >= maxNewSpeed)
                            {
                                newSpeedP1 = maxNewSpeed;
                            }
                            else if (newSpeedP1 <= minNewSpeed)
                            {
                                newSpeedP1 = minNewSpeed;
                            }
                            nextSpeedLoseP1 = speedLose + (float)currentGameTime;
                            joyStickRightP1 = false;

                        }
                    }


                        if (capabilities1.HasRightXThumbStick)//Maybe change this to triggers or something 
                        {
                            if (state.ThumbSticks.Right.X < -0.5f && !joyStickRightP1)//Tracks the movement of p1's right joystick
                            {
                                newSpeedP1 = newSpeedP1 + speedLoseIncreaseAmount; //increase speed
                                if (newSpeedP1 >= maxNewSpeed) //Makes sure we can't go above the max speed.
                                {
                                    newSpeedP1 = maxNewSpeed;
                                }
                                else if (newSpeedP1 <= minNewSpeed) //Makes sure we can't go below the min speed.
                                {
                                    newSpeedP1 = minNewSpeed;
                                }
                                nextSpeedLoseP1 = speedLose + (float)currentGameTime; //resets the time it takes to tick back down
                            joyStickRightP1 = true; //Makes sure you need to move to the other side first.
                            }
                            else if (state.ThumbSticks.Right.X > 0.5f && joyStickRightP1)
                            {
                                newSpeedP1 = newSpeedP1 + speedLoseIncreaseAmount;
                                if (newSpeedP1 >= maxNewSpeed)
                                {
                                    newSpeedP1 = maxNewSpeed;
                                }
                                else if (newSpeedP1 <= minNewSpeed)
                                {
                                    newSpeedP1 = minNewSpeed;
                                }
                                nextSpeedLoseP1 = speedLose + (float)currentGameTime;
                                joyStickRightP1 = false;

                            }

                            if ((float)currentGameTime >= nextSpeedLoseP1)//Ticks down move speed of planets if you arn't moving the joystick.
                            {
                                newSpeedP1 = newSpeedP1 - speedLoseIncreaseAmount;
                                if (newSpeedP1 >= maxNewSpeed)
                                {
                                    newSpeedP1 = maxNewSpeed;
                                }
                                else if (newSpeedP1 <= minNewSpeed)
                                {
                                    newSpeedP1 = minNewSpeed;
                                }

                                if (newSpeedP1 <= 0.3f)
                                {
                                    newSpeedP1 = 0.3f;
                                }
                            nextSpeedLoseP1 = speedLose + (float)currentGameTime;
                        }
                        }
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //Player shoot/dash button
                        if (capabilities1.HasAButton)
                        {
                            //Shoot
                            if (state.Buttons.A == ButtonState.Pressed && nextFireP1 <= currentGameTime)
                            {
                                if (planets[0].Owner == 1 && planets[0].InOrbit)
                                {
                                    planets[0].InOrbit = false;
                                    planets[0].StartOwnerDelay();
                                    Vector2 direction = state.ThumbSticks.Right;
                                    direction.Y *= -1;

                                    //if (state.ThumbSticks.Right.X < -0.5f)
                                    //   direction.X -= 1;
                                    //Move Right
                                    //else if (state.ThumbSticks.Right.X > 0.5f)
                                    //  direction.X += 1;
                                    //Move Up
                                    if (state.ThumbSticks.Left.Y < -0.5f)
                                        direction.Y -= 1;
                                    //Move Down
                                    else if (state.ThumbSticks.Left.Y > 0.5f)
                                        direction.Y += 1;
                                    if (direction.Y == 0)
                                    {
                                        Vector2 velocityOffset = new Vector2((float)(0.2f * (float)Math.Sin(0.2f) - 0.1 * (float)Math.Sin(0.1f)), (float)(0.2 * (float)Math.Cos(0.2) - 0.2 * (float)Math.Cos(0.2)));
                                        planets[0].GiveAcceleration(ball1Pos + positionOffset[0], velocityOffset, newSpeedP1);
                                    }
                                    else
                                        planets[0].GiveAcceleration(ball1Pos + positionOffset[0], direction);
                                //if (planetCount1 <= planets.Count - 3 && !(planetCount1 < 0))
                                //{
                                //    planetCount1--;
                                //}
                                Game1.soundEffects[2].Play();
                                nextFireP1 = fireRate + (float)currentGameTime;
                                }
                            }
                        }
                        if (capabilities1.HasBButton)
                        {
                            //Shoot
                            if (state.Buttons.B == ButtonState.Pressed && nextFireP1 <= currentGameTime)
                            {
                                if (planets[1].Owner == 1 && planets[1].InOrbit)
                                {
                                    planets[1].InOrbit = false;
                                    planets[1].StartOwnerDelay();
                                    Vector2 direction = state.ThumbSticks.Right;
                                    direction.Y *= -1;

                                    //if (state.ThumbSticks.Right.X < -0.5f)
                                    //   direction.X -= 1;
                                    //Move Right
                                    //else if (state.ThumbSticks.Right.X > 0.5f)
                                    //  direction.X += 1;
                                    //Move Up
                                    if (state.ThumbSticks.Left.Y < -0.5f)
                                        direction.Y -= 1;
                                    //Move Down
                                    else if (state.ThumbSticks.Left.Y > 0.5f)
                                        direction.Y += 1;
                                    if (direction.Y == 0)
                                    {
                                        Vector2 velocityOffset = new Vector2((float)(0.2f * (float)Math.Sin(0.2f) - 0.1 * (float)Math.Sin(0.1f)), (float)(0.2 * (float)Math.Cos(0.2) - 0.2 * (float)Math.Cos(0.2)));
                                        planets[1].GiveAcceleration(ball1Pos + positionOffset[1], velocityOffset, newSpeedP1);
                                    }
                                    else
                                        planets[1].GiveAcceleration(ball1Pos + positionOffset[1], direction);
                                //if (planetCount1 <= planets.Count - 3 && !(planetCount1 < 0))
                                //{
                                //    planetCount1--;
                                //}
                                Game1.soundEffects[2].Play();
                                nextFireP1 = fireRate + (float)currentGameTime;
                                }
                            }
                        }
                        if (capabilities1.HasXButton)
                        {
                            //Shoot
                            if (state.Buttons.X == ButtonState.Pressed && nextFireP1 <= currentGameTime)
                            {
                                if (planets[2].Owner == 1 && planets[2].InOrbit)
                                {
                                    planets[2].InOrbit = false;
                                    planets[2].StartOwnerDelay();
                                    Vector2 direction = state.ThumbSticks.Right;
                                    direction.Y *= -1;

                                    //if (state.ThumbSticks.Right.X < -0.5f)
                                    //   direction.X -= 1;
                                    //Move Right
                                    //else if (state.ThumbSticks.Right.X > 0.5f)
                                    //  direction.X += 1;
                                    //Move Up
                                    if (state.ThumbSticks.Left.Y < -0.5f)
                                        direction.Y -= 1;
                                    //Move Down
                                    else if (state.ThumbSticks.Left.Y > 0.5f)
                                        direction.Y += 1;
                                    if (direction.Y == 0)
                                    {
                                        Vector2 velocityOffset = new Vector2((float)(0.2f * (float)Math.Sin(0.2f) - 0.1 * (float)Math.Sin(0.1f)), (float)(0.2 * (float)Math.Cos(0.2) - 0.2 * (float)Math.Cos(0.2)));
                                        planets[2].GiveAcceleration(ball1Pos + positionOffset[2], velocityOffset, newSpeedP1);
                                    }
                                    else
                                        planets[planetCount1].GiveAcceleration(ball1Pos + positionOffset[2], direction);
                                //if (planetCount1 <= planets.Count - 3 && !(planetCount1 < 0))
                                //{
                                //    planetCount1--;
                                //}
                                Game1.soundEffects[2].Play();
                                nextFireP1 = fireRate + (float)currentGameTime;
                                }
                            }
                        }

                        if (state.Buttons.LeftShoulder == ButtonState.Pressed)
                        {
                            //[TODO] Dash.
                        }

                    }

                    // Check the device for Player Two

                    if (capabilities2.IsConnected)
                    {
                        // Get the current state of Controller1
                        GamePadState state = GamePad.GetState(PlayerIndex.Two);

                        // You can check explicitly if a gamepad has support for a certain feature
                        if (capabilities2.HasLeftXThumbStick)
                        {
                        // Check for movement
                        ////Move Left
                        //if (state.ThumbSticks.Left.X < -0.5f && ball2Pos.X != (GameConstants.WindowWidth - 100))
                        //    ball2Pos.X -= 10.0f;
                        ////Move Right
                        //else if (state.ThumbSticks.Left.X > 0.5f && ball2Pos.X != (GameConstants.WindowWidth - 100))
                        //    ball2Pos.X += 10.0f;
                        //Move Up
                        if (state.ThumbSticks.Left.Y < -0.5f && ball2Pos.Y != (GameConstants.WindowHeight - 100))
                            {
                                ball2Pos.Y += 5;
                                drawRectangle2.Y += 5;
                            }
                            //Move Down
                            else if (state.ThumbSticks.Left.Y > 0.5f && ball2Pos.Y != 0)
                            {
                                ball2Pos.Y -= 5;
                                drawRectangle2.Y -= 5;
                            }
                        }
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (capabilities2.HasLeftTrigger)
                    {
                        if (state.Triggers.Left >= 1f && !joyStickRightP2 && state.Triggers.Right <= 0 && state.ThumbSticks.Right.X <= 0.5f)//Tracks the movement of p1's right joystick
                        {
                            newSpeedP2 = newSpeedP2 + speedLoseIncreaseAmount; //increase speed
                            if (newSpeedP2 >= maxNewSpeed) //Makes sure we can't go above the max speed.
                            {
                                newSpeedP2 = maxNewSpeed;
                            }
                            else if (newSpeedP2 <= minNewSpeed) //Makes sure we can't go below the min speed.
                            {
                                newSpeedP2 = minNewSpeed;
                            }
                            nextSpeedLoseP2 = speedLose + (float)currentGameTime; //resets the time it takes to tick back down
                            joyStickRightP2 = true; //Makes sure you need to move to the other side first.
                        }
                        else if (state.Triggers.Right >= 1f && joyStickRightP2 && state.Triggers.Left <= 0 && state.ThumbSticks.Right.X >= -0.5f)
                        {
                            newSpeedP2 = newSpeedP2 + speedLoseIncreaseAmount;
                            if (newSpeedP2 >= maxNewSpeed)
                            {
                                newSpeedP2 = maxNewSpeed;
                            }
                            else if (newSpeedP2 <= minNewSpeed)
                            {
                                newSpeedP2 = minNewSpeed;
                            }
                            nextSpeedLoseP2 = speedLose + (float)currentGameTime;
                            joyStickRightP2 = false;

                        }
                    }

                        if (capabilities2.HasRightXThumbStick)//Maybe change this to triggers or something 
                        {
                            if (state.ThumbSticks.Right.X < -0.5f && !joyStickRightP2)
                            {
                                newSpeedP2 = newSpeedP2 + speedLoseIncreaseAmount;
                                if (newSpeedP2 >= maxNewSpeed)
                                {
                                    newSpeedP2 = maxNewSpeed;
                                }
                                else if (newSpeedP2 <= minNewSpeed)
                                {
                                    newSpeedP2 = minNewSpeed;
                                }
                                nextSpeedLoseP2 = speedLose + (float)currentGameTime;
                                joyStickRightP2 = true;
                            }
                            else if (state.ThumbSticks.Right.X > 0.5f && joyStickRightP2)
                            {
                                newSpeedP2 = newSpeedP2 + speedLoseIncreaseAmount;
                                if (newSpeedP2 >= maxNewSpeed)
                                {
                                    newSpeedP2 = maxNewSpeed;
                                }
                                else if (newSpeedP2 <= minNewSpeed)
                                {
                                    newSpeedP2 = minNewSpeed;
                                }
                                nextSpeedLoseP2 = speedLose + (float)currentGameTime;
                                joyStickRightP2 = false;

                            }

                            if ((float)currentGameTime >= nextSpeedLoseP2)
                            {
                                newSpeedP2 = newSpeedP2 - speedLoseIncreaseAmount;
                                if (newSpeedP2 >= maxNewSpeed)
                                {
                                    newSpeedP2 = maxNewSpeed;
                                }
                                else if (newSpeedP2 <= minNewSpeed)
                                {
                                    newSpeedP2 = minNewSpeed;
                                }

                                if (newSpeedP2 <= 0.3f)
                                {
                                    newSpeedP2 = 0.3f;
                                }
                            nextSpeedLoseP2 = speedLose + (float)currentGameTime;
                        }
                        }
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //Player shoot/Dash button
                        if (capabilities2.HasAButton)
                        {
                            //Shoot
                            if (state.Buttons.A == ButtonState.Pressed && nextFireP1 <= currentGameTime)
                            {
                                if (planets[3].Owner == 2 && planets[3].InOrbit)
                                {
                                    planets[3].InOrbit = false;
                                    planets[3].StartOwnerDelay();
                                    Vector2 direction = state.ThumbSticks.Right;
                                    direction.Y *= -1;

                                    //if (state.ThumbSticks.Right.X < -0.5f)
                                    // direction.X -= 1;
                                    //Move Right
                                    //else if (state.ThumbSticks.Right.X > 0.5f)
                                    // direction.X += 1;
                                    //Move Up
                                    if (state.ThumbSticks.Left.Y < -0.5f)
                                        direction.Y -= 1;
                                    //Move Down
                                    else if (state.ThumbSticks.Left.Y > 0.5f)
                                        direction.Y += 1;
                                    if (direction.Y == 0)
                                    {
                                        Vector2 velocityOffset = new Vector2(-(float)(0.2f * (float)Math.Sin(0.2f) - 0.1 * (float)Math.Sin(0.1f)), (float)(0.2 * (float)Math.Cos(0.2) - 0.2 * (float)Math.Cos(0.2)));
                                        planets[3].GiveAcceleration(ball2Pos + positionOffset[3 - 3], velocityOffset, newSpeedP2);
                                    }
                                    else
                                        planets[3].GiveAcceleration(ball2Pos + positionOffset[3 - 3], direction);
                                //if (planetCount2 <= planets.Count && !(planetCount2 < 3))
                                //{
                                //    planetCount2--;
                                //}
                                Game1.soundEffects[2].Play();
                                nextFireP2 = fireRate + (float)currentGameTime;

                                }
                            }
                        }
                        if (capabilities2.HasBButton)
                        {
                            //Shoot
                            if (state.Buttons.B == ButtonState.Pressed && nextFireP1 <= currentGameTime)
                            {
                                if (planets[4].Owner == 2 && planets[4].InOrbit)
                                {
                                    planets[4].InOrbit = false;
                                    planets[4].StartOwnerDelay();
                                    Vector2 direction = state.ThumbSticks.Right;
                                    direction.Y *= -1;

                                    //if (state.ThumbSticks.Right.X < -0.5f)
                                    // direction.X -= 1;
                                    //Move Right
                                    //else if (state.ThumbSticks.Right.X > 0.5f)
                                    // direction.X += 1;
                                    //Move Up
                                    if (state.ThumbSticks.Left.Y < -0.5f)
                                        direction.Y -= 1;
                                    //Move Down
                                    else if (state.ThumbSticks.Left.Y > 0.5f)
                                        direction.Y += 1;
                                    if (direction.Y == 0)
                                    {
                                        Vector2 velocityOffset = new Vector2(-(float)(0.2f * (float)Math.Sin(0.2f) - 0.1 * (float)Math.Sin(0.1f)), (float)(0.2 * (float)Math.Cos(0.2) - 0.2 * (float)Math.Cos(0.2)));
                                        planets[4].GiveAcceleration(ball2Pos + positionOffset[4 - 3], velocityOffset, newSpeedP2);
                                    }
                                    else
                                        planets[4].GiveAcceleration(ball2Pos + positionOffset[4 - 3], direction);
                                //if (planetCount2 <= planets.Count && !(planetCount2 < 3))
                                //{
                                //    planetCount2--;
                                //}
                                Game1.soundEffects[2].Play();
                                nextFireP2 = fireRate + (float)currentGameTime;

                                }
                            }
                        }
                        if (capabilities2.HasXButton)
                        {
                            //Shoot
                            if (state.Buttons.X == ButtonState.Pressed && nextFireP2 <= currentGameTime)
                            {
                                if (planets[5].Owner == 2 && planets[5].InOrbit)
                                {
                                    planets[5].InOrbit = false;
                                    planets[5].StartOwnerDelay();
                                    Vector2 direction = state.ThumbSticks.Right;
                                    direction.Y *= -1;

                                    //if (state.ThumbSticks.Right.X < -0.5f)
                                    // direction.X -= 1;
                                    //Move Right
                                    //else if (state.ThumbSticks.Right.X > 0.5f)
                                    // direction.X += 1;
                                    //Move Up
                                    if (state.ThumbSticks.Left.Y < -0.5f)
                                        direction.Y -= 1;
                                    //Move Down
                                    else if (state.ThumbSticks.Left.Y > 0.5f)
                                        direction.Y += 1;
                                    if (direction.Y == 0)
                                    {
                                        Vector2 velocityOffset = new Vector2(-(float)(0.2f * (float)Math.Sin(0.2f) - 0.1 * (float)Math.Sin(0.1f)), (float)(0.2 * (float)Math.Cos(0.2) - 0.2 * (float)Math.Cos(0.2)));
                                        planets[5].GiveAcceleration(ball2Pos + positionOffset[5 - 3], velocityOffset, newSpeedP2);
                                    }
                                    else
                                        planets[5].GiveAcceleration(ball2Pos + positionOffset[5 - 3], direction);
                                //if (planetCount2 <= planets.Count && !(planetCount2 < 3))
                                //{
                                //    planetCount2--;
                                //}
                                Game1.soundEffects[2].Play();
                                nextFireP2 = fireRate + (float)currentGameTime;

                                }
                            }
                        }
                        //Dash
                        if (state.Buttons.LeftShoulder == ButtonState.Pressed)
                        {
                            //[TODO] Dash.
                        }
                    }
                }
                catch (Exception e)
            {

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive1)
                spriteBatch.Draw(sunSprite1, ball1Pos, null, Color.White, 0f, Vector2.Zero, 0.08f, SpriteEffects.None, 0f);

            if (IsAlive2)
                spriteBatch.Draw(sunSprite1, ball2Pos, null, Color.White, 0f, Vector2.Zero, 0.08f, SpriteEffects.None, 0f);

            if (!isAlive2)
            {
                spriteBatch.Draw(playerWins[0], new Vector2(500, 200), Color.White);
                spriteBatch.Draw(finalWinSprite, new Vector2(500, 200), Color.White);

            }
            else if (!isAlive1)
            {
                spriteBatch.Draw(playerWins[1], new Vector2(500, 200), Color.White);
                spriteBatch.Draw(finalWinSprite, new Vector2(500, 200), Color.White);

            }

            //spriteBatch.Draw(sunSprite1, drawRectangle1, Color.White);
            //spriteBatch.Draw(sunSprite1, drawRectangle2, Color.White);

            foreach (Planet planet in planets)
            {
                if (planet.InOrbit)
                {


                    //for (int i = 0; i < planetPos.Length; i++)
                    //{
                    for (int k = 0; k < 3; k++)
                    {
                        if (planets[k].InOrbit && planets[k].Owner == 1)
                        {

                            spriteBatch.Draw(planetSprite1[k], ball1Pos + positionOffset[k], null, Color.White, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 0f);
                            //spriteBatch.Draw(planetSprite1, ball2Pos + positionOffset[k], null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
                        }
                    }
                    for (int k = 3; k < 6; k++)
                    {
                        if (planets[k].InOrbit && planets[k].Owner == 2)
                        {
                            //spriteBatch.Draw(planetSprite1, ball1Pos + positionOffset[k], null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(planetSprite1[k-3], ball2Pos + positionOffset[k], null, Color.White, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 0f);
                        }

                        //}
                    }
                }
            }

            /*
            for (int i = 0; i < planetPos.Length; i++)
            {
                foreach (Planet planet in planets)
                {
                    spriteBatch.Draw(planetSprite1, ball1Pos + positionOffset[k], null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
                }
            }
            */

        }

        public PlayerControl(Vector2 sun1Pos, Vector2 sun2Pos, Vector2[] playerPos, Texture2D sunSprite, Texture2D[] planetSprite, List<Planet> planets, Texture2D[] winSprites, Texture2D winSprite /*, List<Planet> p1Planets, List<Planet> p2Planets*/)
        {
            ball1Pos = sun1Pos;
            ball2Pos = sun2Pos;
            for (int i = 0; i < playerPos.Length; i++)
            {
                planetPos[i] = playerPos[i];
            }
            sunSprite1 = sunSprite;
            planetSprite1 = planetSprite;
            playerWins = winSprites;
            finalWinSprite = winSprite;

            this.planets = planets;
            ////Commented out cause these break the playermovement.
            //this.p1Planets = p1Planets;
            //this.p2Planets = p2Planets;
            drawRectangle1 = new Rectangle((int)sun1Pos.X + 10, (int)sun1Pos.Y + 10, (int)(sunSprite.Width * 0.06f), (int)(sunSprite.Height * 0.06f));
            drawRectangle2 = new Rectangle((int)sun2Pos.X + 10, (int)sun2Pos.Y + 10, (int)(sunSprite.Width * 0.06f), (int)(sunSprite.Height * 0.06f));

            planets[0] = new Planet(planetSprite[0], ball1Pos + positionOffset[0], 1, true);
            planets[1] = new Planet(planetSprite[1], ball1Pos + positionOffset[1], 1, true);
            planets[2] = new Planet(planetSprite[2], ball1Pos + positionOffset[2], 1, true);

            //Adds the planets to player 1's list
            p1Planets.Add(planets[0]);
            p1Planets.Add(planets[1]);
            p1Planets.Add(planets[2]);

            planets[3] = new Planet(planetSprite[0], ball2Pos + positionOffset[0], 2, true);
            planets[4] = new Planet(planetSprite[1], ball2Pos + positionOffset[1], 2, true);
            planets[5] = new Planet(planetSprite[2], ball2Pos + positionOffset[2], 2, true);

            //Adds the planets to player 2's list
            p2Planets.Add(planets[3]);
            p2Planets.Add(planets[4]);
            p2Planets.Add(planets[5]);
        }
    }
}