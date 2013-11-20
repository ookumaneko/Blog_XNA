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

namespace Vibration
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        float m_timer = 0.0f;           // 振動用のタイマー
        bool m_isVibrating = false;     // 振動しているかどうか
        float m_vibrationLength = 0.35f; // 振動している時間

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Z))
            {
                // コントローラー１を振動させる
                SetVibration(0.5f, 0.5f, 0.35f);
            }

            // もし振動中なら
            if (m_isVibrating)
            {
                // タイマーに経過時間を足す
                m_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // タイマーが設定した時間になった時
                if (m_timer >= m_vibrationLength)
                {
                    // 振動を止める
                    GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
                }
            }

            base.Update(gameTime);
        }

        private void SetVibration(float left, float right, float length)
        {
            GamePad.SetVibration(PlayerIndex.One, left, right);
            m_isVibrating = true;
            m_timer = 0.0f;
            m_vibrationLength = length;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
