using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace RapidP1
{
    public class PlayerControl
    {
        Vector2 ball1Pos, ball2Pos;
        Vector2[] planetPos = new Vector2[20];
        Texture2D sunSprite1, planetSprite1;
        Game1 g1 = new Game1();
        public void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            //Planet p;
            if ((Keyboard.GetState().IsKeyDown(Keys.W)) && ball1Pos.Y != 0) //up
            {
                ball1Pos.Y -= 5;
                planetPos[1].Y -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && ball1Pos.Y != (GameConstants.WindowHeight - 100)) //down
            {
                ball1Pos.Y += 5;
                planetPos[1].Y += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && ball1Pos.X != 0) //left
            {
                ball1Pos.X -= 5;
                planetPos[1].X -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && ball1Pos.X != (GameConstants.WindowWidth - 100)) //right
            {
                ball1Pos.X += 5;
                planetPos[1].X += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q) && ball1Pos.X != (GameConstants.WindowWidth - 100)) //right
            {
                Vector2 acc = new Vector2(1, 1);
                //p.GiveAcceleration(acc);
                //shooting
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.NumPad8)) && ball2Pos.Y != 0) //up
            {
                ball2Pos.Y -= 5;
                planetPos[2].Y -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2) && ball2Pos.Y != (GameConstants.WindowHeight - 100)) //down
            {
                ball2Pos.Y += 5;
                planetPos[2].Y += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad4) && ball2Pos.X != 0) //left
            {
                ball2Pos.X -= 5;
                planetPos[2].X -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad6) && ball2Pos.X != (GameConstants.WindowWidth - 100)) //right
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
                    if (state.ThumbSticks.Left.X < -0.5f && ball1Pos.X != (GameConstants.WindowWidth - 100))
                        ball1Pos.X -= 10.0f;
                    //Move Right
                    else if (state.ThumbSticks.Left.X > 0.5f && ball1Pos.X != (GameConstants.WindowWidth - 100))
                        ball1Pos.X += 10.0f;
                    //Move Up
                    if (state.ThumbSticks.Left.Y < -0.5f && ball1Pos.Y != (GameConstants.WindowHeight - 100))
                        ball1Pos.Y += 10.0f;
                    //Move Down
                    else if (state.ThumbSticks.Left.Y > 0.5f && ball1Pos.Y != (GameConstants.WindowHeight - 100))
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
                    if (state.ThumbSticks.Left.X < -0.5f && ball2Pos.X != (GameConstants.WindowWidth - 100))
                        ball2Pos.X -= 10.0f;
                    //Move Right
                    else if (state.ThumbSticks.Left.X > 0.5f && ball2Pos.X != (GameConstants.WindowWidth - 100))
                        ball2Pos.X += 10.0f;
                    //Move Up
                    if (state.ThumbSticks.Left.Y < -0.5f && ball2Pos.Y != (GameConstants.WindowHeight - 100))
                        ball2Pos.Y += 10.0f;
                    //Move Down
                    else if (state.ThumbSticks.Left.Y > 0.5f && ball2Pos.Y != (GameConstants.WindowHeight - 100))
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sunSprite1, ball1Pos,  null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
            for (int i = 0; i < planetPos.Length; i++)
            {
                spriteBatch.Draw(planetSprite1, planetPos[i],  null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
            }
        }

        public PlayerControl(Vector2 sun1Pos, Vector2 sun2Pos, Vector2[] playerPos, Texture2D sunSprite, Texture2D planetSprite)
        {
            ball1Pos = sun1Pos;
            ball2Pos = sun2Pos;
            for (int i = 0; i < playerPos.Length; i++)
            {
                planetPos[i] = playerPos[i];
            }
            sunSprite1 = sunSprite;
            planetSprite1 = planetSprite;

        }

    }
}