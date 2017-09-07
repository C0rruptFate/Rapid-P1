using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;


namespace RapidP1
{

    public class ScrollingBackground
    {
        private Vector2 screenpos, origin, texturesize;
        private Texture2D mytexture;
        private int screenheight;
        int screenwidth;
        int direction;

        public ScrollingBackground(int direction)
        {
            this.direction = direction;
        }

        public void Load(GraphicsDevice device, Texture2D backgroundTexture)
        {
            mytexture = backgroundTexture;
            screenheight = device.Viewport.Height;
            screenwidth = device.Viewport.Width;
            // Set the origin so that we're drawing from the 
            // center of the top edge.
            origin = new Vector2(0, mytexture.Height / 2);
            // Set the screen position to the center of the screen.
            screenpos = new Vector2(screenwidth / 2, screenheight / 2);
            // Offset to draw the second texture, when necessary.
            texturesize = new Vector2(mytexture.Width, 0);
        }

        public void Update(float deltaX)
        {
            if (direction == 1)
            {
                screenpos.X += deltaX;
            }
            else
            {
                screenpos.X -= deltaX;
            }
            
            screenpos.X = screenpos.X % mytexture.Width;
        }

        public void Draw(SpriteBatch batch)
        {
            // Draw the texture, if it is still onscreen.

            if (direction == 1)
            {
                if (screenpos.X < screenwidth)
                {
                    // Draw the texture, if it is still onscreen.
                    batch.Draw(mytexture, screenpos, null,
                         Color.White, 0, origin, 1, SpriteEffects.None, 0f);
                }

                // Draw the texture a second time, behind the first,
                // to create the scrolling illusion.
                batch.Draw(mytexture, screenpos - texturesize, null,
                     Color.White, 0, origin, 1, SpriteEffects.None, 0f);

            }
            else
            {
                if (screenpos.X > 0)
                {
                    // Draw the texture, if it is still onscreen.
                    batch.Draw(mytexture, screenpos - texturesize, null,
                         Color.White, 0, origin, 1, SpriteEffects.None, 0f);
                }

                // Draw the texture a second time, behind the first,
                // to create the scrolling illusion.
                batch.Draw(mytexture, screenpos, null,
                     Color.White, 0, origin, 1, SpriteEffects.None, 0f);
            }
        }
    }
}