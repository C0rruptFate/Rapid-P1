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
        Texture2D sprite;
        Rectangle drawRectangle;
        Vector2 velocity;
        Vector2 acceleration;
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

        #endregion

        #region constructors

        public Planet(Texture2D sprite, Vector2 location)
        {
            this.sprite = sprite;
            drawRectangle = new Rectangle((int)location.X - sprite.Width/2,             //X-coordinate of the rectangle
                                            (int)location.Y - sprite.Height / 2,        //Y-coordinate of the rectangle
                                            sprite.Width, sprite.Height);               //Height and Width of rectangle
        }

        #endregion

        #region public methods

        public void giveAcceleration(Vector2 acceleration)
        {
            this.acceleration.X += acceleration.X;
            this.acceleration.Y += acceleration.Y;
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (!inOrbit)
            {
                drawRectangle.X += (int)velocity.X * gameTime.ElapsedGameTime.Milliseconds;
                drawRectangle.Y += (int)velocity.Y * gameTime.ElapsedGameTime.Milliseconds;

                velocity.X += acceleration.X * gameTime.ElapsedGameTime.Milliseconds;
                velocity.Y += acceleration.Y * gameTime.ElapsedGameTime.Milliseconds;

                acceleration.X -= 0.1f;
                acceleration.Y -= 0.1f;
            }
        }

        #endregion

        #region private methods

        private int getVelocityX(int xAcceleration)
        {
            //Not implemented yet
            return 0;
        }

        private int getVelocityY(int yAcceleration)
        {
            //Not implemented yet
            return 0;
        }

        #endregion

    }
}



