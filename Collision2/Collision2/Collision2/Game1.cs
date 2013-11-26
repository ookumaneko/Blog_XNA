using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Collision2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        class Circle
        {
            public Vector2 Center;
            public float Radius;

            public Circle(Vector2 center, float radius)
            {
                Center = center;
                Radius = radius;
            }
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Circle m_circle;
        Rectangle m_bound;
        Texture2D circleTex;
        Texture2D rectTex;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            m_circle = new Circle(new Vector2(100, 100), 20);
            m_bound = new Rectangle(200, 200, 40, 40);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            circleTex = Content.Load<Texture2D>("circle");
            rectTex = Content.Load<Texture2D>("rect");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update(0.0f);

            if (Input.IsPressed(Keys.Escape))
                this.Exit();

            if (Input.IsDown(Keys.Up))
            {
                m_circle.Center.Y -= 4.0f;
            }

            if (Input.IsDown(Keys.Down))
            {
                m_circle.Center.Y += 4.0f;
            }

            if (Input.IsDown(Keys.Left))
            {
                m_circle.Center.X -= 4.0f;
            }

            if (Input.IsDown(Keys.Right))
            {
                m_circle.Center.X += 4.0f;
            }

            base.Update(gameTime);
        }

        //private bool CheckCollision(Circle circle, Rectangle rect)
        //{
        //    //Vector2 circleDistance = Vector2.Zero;
        //    //circleDistance.X = Math.Abs(circle.Center.X - rect.X - rect.Width / 2);
        //    //circleDistance.Y = Math.Abs(circle.Center.Y - rect.Y - rect.Height / 2);            

        //    //if (circleDistance.X > (rect.Width / 2 + circle.Radius)) { return false; }
        //    //if (circleDistance.Y > (rect.Height / 2 + circle.Radius)) { return false; }

        //    //if (circleDistance.X <= (rect.Width / 2)) { return true; }
        //    //if (circleDistance.Y <= (rect.Height / 2)) { return true; }

        //    //float x = circleDistance.X - (float)rect.Width / 2;
        //    //float y = circleDistance.Y - rect.Height / 2;
        //    //float cornerDistance_sq = (x * x) + (y * y);

        //    //return (cornerDistance_sq <= (circle.Radius * circle.Radius));
        //}

        private bool CheckCollision(Circle circle, Rectangle rect)
        {
            // ‰~‚Éˆê”Ô‹ß‚¢ˆÊ’u‚ð’·•ûŒ`‚©‚ç’T‚·
            Vector2 closest = new Vector2(MathHelper.Clamp(circle.Center.X, rect.Left, rect.Right),
                                          MathHelper.Clamp(circle.Center.Y, rect.Top, rect.Bottom));

            // ŒvŽZ‚µ‚½ˆÊ’u‚ÆA‰~‚Ì’†S“_‚Æ‚Ì‹——£‚ð‘ª‚é
            float distanceSquared = Vector2.DistanceSquared(closest, circle.Center);

            // ‹——£‚ª‰~‚Ì”¼Œa‚æ‚è’·‚¢ê‡‚ÍA“–‚½‚Á‚Ä‚¢‚È‚¢
            return (distanceSquared < (circle.Radius * circle.Radius));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Color col = Color.White;
            Vector2 origin = new Vector2(20, 20);

            if (CheckCollision(m_circle, m_bound))
            {
                col = Color.Red;
            }

            spriteBatch.Begin();
            spriteBatch.Draw(circleTex, m_circle.Center, null, col, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(rectTex, m_bound, null, col, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
