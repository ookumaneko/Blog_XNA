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

        float m_timer;      // タイマー
        float m_nextLetter; // 次の文字列を描画する時間
        int m_currentIndex; // 描画する文字の数

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

            // 次の文字を描画するかの確認
            m_timer += delta;
            if (m_timer >= m_nextLetter)
            {
                // タイマーのリセット
                m_timer = 0.0f;

                // 描画する文字数を増やす
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

            // 背景を描画する
            spriteBatch.Draw(m_background, Vector2.Zero, Color.White);

            // 描画する文字列を獲得する
            string text = m_text.Substring(0, m_currentIndex);

            // 文字列の描画
            m_textbox.Draw(spriteBatch, m_font, text, m_pixel, new Color(0, 0, 0, 0.65f), Color.White, true);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
