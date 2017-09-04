using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using Microsoft.Xna.Framework.Audio;

namespace RapidP1
{
    public class Planet
    {
        #region fields

        bool inOrbit = true;
        int owner;
        int previousOwner;
        Texture2D sprite;
        Rectangle drawRectangle;
        Vector2 velocity;
        Vector2 acceleration;
        Vector2 location;
        const float ownerDelay = 1;
        const float returnDelay = 10;
        float remainingShootDelay = ownerDelay;
        float remainingReturnDelay = returnDelay;
        bool delayTimer = false;
        bool returnTimer = false;

        public float myNewSpeed = 1;
        float maxNewSpeed = 3;
        float minNewSpeed = 0.3f;
        float minVelocity = 0.1f;
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

        public void SetDrawX(int X)
        {
            drawRectangle.X = X;
        }

        public void SetDrawY(int Y)
        {
            drawRectangle.Y = Y;
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
                                            (int)(sprite.Width*0.2f), (int)(sprite.Height*0.2f));               //Height and Width of rectangle
            this.location = location;

            this.owner = owner;
            previousOwner = owner;
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

            velocityOffset = acceleration - velocityOffset;

            velocityOffset.Normalize();  //Gets the direction only

            velocity.X = velocityOffset.X * GameConstants.speed;
            velocity.Y = velocityOffset.Y * GameConstants.speed;
        }
        public void GiveAcceleration(Vector2 acceleration, Vector2 velocityOffset, float newSpeed)
        {

            drawRectangle.X = (int)acceleration.X;
            drawRectangle.Y = (int)acceleration.Y;

            velocityOffset = acceleration - velocityOffset;

            velocityOffset.Normalize();  //Gets the direction only

            velocity.X = velocityOffset.X * GameConstants.speed;
            velocity.Y = velocityOffset.Y * GameConstants.speed;

            if (newSpeed >= maxNewSpeed)
            {
                myNewSpeed = maxNewSpeed;
            }
            else if (newSpeed <= minNewSpeed)
            {
                myNewSpeed = minNewSpeed;
            }
            else
            {
                myNewSpeed = newSpeed;
            }
            
        }
        public void GiveAcceleration(Vector2 acceleration, float vel)
        {

            drawRectangle.X = (int)acceleration.X;
            drawRectangle.Y = (int)acceleration.Y;

            acceleration.Normalize();  //Gets the direction only

            velocity.X = acceleration.X * vel * 0.01f;
            velocity.Y = acceleration.Y * vel * 0.01f;
        }

        public void StartOwnerDelay()
        {
            delayTimer = true;
            returnTimer = true;
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {

            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(delayTimer)
                remainingShootDelay -= timer;

            if (returnTimer)
                remainingReturnDelay -= timer;

            if(remainingShootDelay <= 0)
            {
                owner = 0;
                remainingShootDelay = ownerDelay;
                delayTimer = false;
            }

            if(remainingReturnDelay <= 0)
            {
                owner = previousOwner;
                inOrbit = true;
                remainingReturnDelay = returnDelay;
                returnTimer = false;
                Game1.soundEffects[3].Play();
            }



            if (!inOrbit)
            {

                drawRectangle.X += (int)(velocity.X * gameTime.ElapsedGameTime.Milliseconds * myNewSpeed);  //Or TotalMilliseconds (need to check)
                drawRectangle.Y += (int)(velocity.Y * gameTime.ElapsedGameTime.Milliseconds * myNewSpeed);

                //velocity.X += velocity.X * 0.01f * GetXDirection() * -1;
                //velocity.Y += velocity.Y * 0.01f * GetYDirection() * -1;

                if (velocity.X > minVelocity)
                {
                    velocity.X *= 0.998f;
                }

                if (velocity.Y > minVelocity)
                {
                    velocity.Y *= 0.998f;
                }

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
                Game1.soundEffects[0].Play();
            }
            else if ((drawRectangle.Y + drawRectangle.Height) > GameConstants.WindowHeight)    //Should set up seperate static class for these constants
            {
                // bounce off bottom
                drawRectangle.Y = GameConstants.WindowHeight - drawRectangle.Height;
                velocity.Y *= -1;
                Game1.soundEffects[0].Play();
            }
        }

        private void BounceLeftRight()
        {
            if (drawRectangle.X < 0)
            {
                // bounc off left
                drawRectangle.X = 0;
                velocity.X *= -1;
                Game1.soundEffects[0].Play();
            }
            else if ((drawRectangle.X + drawRectangle.Width) > GameConstants.WindowWidth)
            {
                // bounce off right
                drawRectangle.X = GameConstants.WindowWidth - drawRectangle.Width;
                velocity.X *= -1;
                Game1.soundEffects[0].Play();
            }


        }

        #endregion

    }
}



