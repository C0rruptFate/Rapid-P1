using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace RapidP1
{
    public class Planet
    {
        #region fields

        bool inOrbit = true;
        int owner;
        Texture2D sprite;
        Rectangle drawRectangle;
        Vector2 velocity;
        Vector2 acceleration;
        Vector2 location;
        #endregion

        #region properties

        public bool InOrbit
        {
            get { return inOrbit; }
            set { inOrbit = value; }
        }

        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Rectangle DrawRectangle
        {
            get { return drawRectangle; }
            set { drawRectangle = value; }
        }

        public int Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        #endregion

        #region constructors

        public Planet(Texture2D sprite, Vector2 location, int owner, bool inOrbit)
        {
            this.sprite = sprite;
            drawRectangle = new Rectangle((int)location.X - sprite.Width/2,             //X-coordinate of the rectangle
                                            (int)location.Y - sprite.Height / 2,        //Y-coordinate of the rectangle
                                            sprite.Width/10, sprite.Height/10);               //Height and Width of rectangle
            this.location = location;

            this.owner = owner;
            this.inOrbit = inOrbit;
        }

        #endregion

        #region public methods

        public void GiveAcceleration(Vector2 acceleration)
        {
            
            drawRectangle.X = (int)acceleration.X;
            drawRectangle.Y = (int)acceleration.Y;
            
            acceleration.Normalize();  //Gets the direction only

            velocity.X = acceleration.X*GameConstants.speed;
            velocity.Y = acceleration.Y*GameConstants.speed;
        }
        public void GiveAcceleration(Vector2 acceleration, Vector2 velocityOffset)
        {

            drawRectangle.X = (int)acceleration.X;
            drawRectangle.Y = (int)acceleration.Y;

            velocityOffset.Normalize();  //Gets the direction only

            velocity.X = velocityOffset.X * GameConstants.speed;
            velocity.Y = velocityOffset.Y * GameConstants.speed;
        }
        public void GiveAcceleration(Vector2 acceleration, float vel)
        {

            drawRectangle.X = (int)acceleration.X;
            drawRectangle.Y = (int)acceleration.Y;

            acceleration.Normalize();  //Gets the direction only

            velocity.X = acceleration.X * vel * 0.01f;
            velocity.Y = acceleration.Y * vel * 0.01f;
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (!inOrbit && owner == 0)
            {
                drawRectangle.X += (int)(velocity.X * gameTime.ElapsedGameTime.Milliseconds);  //Or TotalMilliseconds (need to check)
                drawRectangle.Y += (int)(velocity.Y * gameTime.ElapsedGameTime.Milliseconds);

                //velocity.X += velocity.X * 0.01f * GetXDirection() * -1;
                //velocity.Y += velocity.Y * 0.01f * GetYDirection() * -1;

                velocity.X *= 0.998f;
                velocity.Y *= 0.998f;

                /*
                if (velocity.X != 0)
                {
                    acceleration.X -= 0.05f;
                }

                if (velocity.Y != 0)
                {
                    acceleration.Y -= 0.05f;
                }

                if (acceleration.X <= 0)
                {
                    acceleration.X = 0;
                    if (velocity.X > 0.01 && velocity.X < -0.01)
                        velocity.X /= 10;
                    else
                        velocity.X = 0;
                }
                    

                if (acceleration.Y <= 0)
                {
                    acceleration.Y = 0;
                    if (velocity.Y > 0.01 && velocity.Y < -0.01)
                        velocity.Y /= 10;
                    else
                        velocity.Y = 0;
                }
                    
                */
                BounceLeftRight();
                BounceTopBottom();
            }
        }

        #endregion

        #region private methods

        private int GetVelocityX(int xAcceleration)
        {
            //Not implemented yet
            return 0;
        }

        private int GetVelocityY(int yAcceleration)
        {
            //Not implemented yet
            return 0;
        }


        //Functions to limit the planets movement to within the playspace

        private float GetXDirection()
        {
            Vector2 direction = velocity;
            direction.Normalize();

            return direction.X;
        }

        private float GetYDirection()
        {
            Vector2 direction = velocity;
            direction.Normalize();

            return direction.Y;
        }

        private void BounceTopBottom()
        {
            if (drawRectangle.Y < 0)
            {
                // bounce off top
                drawRectangle.Y = 0;
                velocity.Y *= -1;
                
            }
            else if ((drawRectangle.Y + drawRectangle.Height) > GameConstants.WindowHeight)    //Should set up seperate static class for these constants
            {
                // bounce off bottom
                drawRectangle.Y = GameConstants.WindowHeight - drawRectangle.Height;
                velocity.Y *= -1;
                
            }
        }

        private void BounceLeftRight()
        {
            if (drawRectangle.X < 0)
            {
                // bounc off left
                drawRectangle.X = 0;
                velocity.X *= -1;
            }
            else if ((drawRectangle.X + drawRectangle.Width) > GameConstants.WindowWidth)
            {
                // bounce off right
                drawRectangle.X = GameConstants.WindowWidth - drawRectangle.Width;
                velocity.X *= -1;
                
            }


        }

        #endregion

    }
}



