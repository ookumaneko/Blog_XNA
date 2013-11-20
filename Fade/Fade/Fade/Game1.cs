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

namespace Fade
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D m_back;

        Texture2D m_pixel;
        float m_alpha;
        float m_alphaIncAmount;
        bool m_isFadeOut;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            m_alpha = 0.0f;
            m_alphaIncAmount = 0.004f;
            m_isFadeOut = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            m_pixel = Content.Load<Texture2D>("pixel");
            m_back = Content.Load<Texture2D>("back");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
                this.Exit();

            UpdateFade();

            base.Update(gameTime);
        }

        private void UpdateFade()
        {
            if (m_isFadeOut)
            {
                m_alpha += m_alphaIncAmount;
                if (m_alpha >= 1.0f)
                {
                    m_alpha = 1.0f;
                    m_isFadeOut = false;
                }
            }
            else
            {
                m_alpha -= m_alphaIncAmount;
                if (m_alpha <= 0.0f)
                {
                    m_alpha = 0.0f;
                    m_isFadeOut = true;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // 画面のサイズ
            Rectangle screenBound = new Rectangle(0, 0, 800, 600);

            // フェードの色
            Color colour = new Color(0.0f, 0.0f, 0.0f, m_alpha);

            // `描画
            spriteBatch.Begin();
            spriteBatch.Draw(m_back, Vector2.Zero, Color.White);
            spriteBatch.Draw(m_pixel, screenBound, colour);            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
