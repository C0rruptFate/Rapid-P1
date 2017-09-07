﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace RapidP1
{
    /// <summary>
    /// An animated explosion object
    /// </summary>
    public class Explosion
    {
        #region Fields

        // object location
        Rectangle drawRectangle;

        // animation strip info
        Texture2D strip;
        int frameWidth;
        int frameHeight;
        Color color;

        // fields used to track and draw animations
        Rectangle sourceRectangle;
        int currentFrame;
        int elapsedFrameTime = 0;

        // playing or not
        bool playing = false;
        bool finished = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new explosion object
        /// </summary>
        /// <param name="spriteStrip">the sprite strip for the explosion</param>
        /// <param name="x">the x location of the center of the explosion</param>
        /// <param name="y">the y location of the center of the explosion</param>
        public Explosion(Texture2D spriteStrip, int x, int y, Color color)
        {
            // initialize animation to start at frame 0
            currentFrame = 0;

            Initialize(spriteStrip);
            Play(x, y);

            this.color = color;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the collision rectangle for the explosion
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Gets whether or not the explosion is finished
        /// </summary>
        public bool Finished
        {
            get { return finished; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the explosion. This only has an effect if the explosion animation is playing
        /// </summary>
        /// <param name="gameTime">the game time</param>
        public void Update(GameTime gameTime)
        {
            if (playing)
            {
                // check for advancing animation frame
                elapsedFrameTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedFrameTime > 50)
                {
                    // reset frame timer
                    elapsedFrameTime = 0;

                    // advance the animation
                    if (currentFrame < 16 - 1)
                    {
                        currentFrame++;
                        SetSourceRectangleLocation(currentFrame);
                    }
                    else
                    {
                        // reached the end of the animation
                        playing = false;
                        finished = true;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the explosion. This only has an effect if the explosion animation is playing
        /// </summary>
        /// <param name="spriteBatch">the spritebatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (playing)
            {
                spriteBatch.Draw(strip, drawRectangle, sourceRectangle, color);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the content for the explosion
        /// </summary>
        /// <param name="spriteStrip">the sprite strip for the explosion</param>
        private void Initialize(Texture2D spriteStrip)
        {
            // load the animation strip
            strip = spriteStrip;

            // calculate frame size
            frameWidth = strip.Width / 4;
            frameHeight = strip.Height / 4;

            // set initial draw and source rectangles
            drawRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
        }

        /// <summary>
        /// Starts playing the animation for the explosion
        /// </summary>
        /// <param name="x">the x location of the center of the explosion</param>
        /// <param name="y">the y location of the center of the explosion</param>
        private void Play(int x, int y)
        {
            // reset tracking values
            playing = true;
            elapsedFrameTime = 0;
            currentFrame = 0;

            // set draw location and source rectangle
            drawRectangle.X = x - drawRectangle.Width / 2;
            drawRectangle.Y = y - drawRectangle.Height / 2;
            SetSourceRectangleLocation(currentFrame);
        }

        /// <summary>
        /// Sets the source rectangle location to correspond with the given frame
        /// </summary>
        /// <param name="frameNumber">the frame number</param>
        private void SetSourceRectangleLocation(int frameNumber)
        {
            // calculate X and Y based on frame number
            sourceRectangle.X = (frameNumber % 4) * frameWidth;
            sourceRectangle.Y = (frameNumber / 4) * frameHeight;
        }

        #endregion

    }
}
