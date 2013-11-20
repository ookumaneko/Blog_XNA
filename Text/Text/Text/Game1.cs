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

namespace Text
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D m_background;
        Texture2D m_pixel;
        SpriteFont m_font;
        TextBox m_textbox;
        string m_text = "This is a test to display text in the text layer Testing Line break and stuff....CBA to work...orz";

        float m_timer;      // ƒ^ƒCƒ}[
        float m_nextLetter; // Ÿ‚Ì•¶š—ñ‚ğ•`‰æ‚·‚éŠÔ
        int m_currentIndex; // •`‰æ‚·‚é•¶š‚Ì”

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            int height = 150;
            m_textbox = new TextBox(new Rectangle(0, graphics.PreferredBackBufferHeight - height, graphics.PreferredBackBufferWidth, height));

            m_timer = 0.0f;
            m_nextLetter = 0.05f;
            m_currentIndex = 0;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            m_background = Content.Load<Texture2D>("background");
            m_pixel = Content.Load<Texture2D>("pixel");
            m_font = Content.Load<SpriteFont>("Font");
        }

        protected override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Ÿ‚Ì•¶š‚ğ•`‰æ‚·‚é‚©‚ÌŠm”F
            m_timer += delta;
            if (m_timer >= m_nextLetter)
            {
                // ƒ^ƒCƒ}[‚ÌƒŠƒZƒbƒg
                m_timer = 0.0f;

                // •`‰æ‚·‚é•¶š”‚ğ‘‚â‚·
                m_currentIndex++;
                if (m_currentIndex >= m_text.Length)
                {
                    m_currentIndex = m_text.Length;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // ”wŒi‚ğ•`‰æ‚·‚é
            spriteBatch.Draw(m_background, Vector2.Zero, Color.White);

            // •`‰æ‚·‚é•¶š—ñ‚ğŠl“¾‚·‚é
            string text = m_text.Substring(0, m_currentIndex);

            // •¶š—ñ‚Ì•`‰æ
            m_textbox.Draw(spriteBatch, m_font, text, m_pixel, new Color(0, 0, 0, 0.65f), Color.White, true);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
