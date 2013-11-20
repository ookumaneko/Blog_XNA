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

        float m_timer = 0.0f;           // �U���p�̃^�C�}�[
        bool m_isVibrating = false;     // �U�����Ă��邩�ǂ���
        float m_vibrationLength = 0.35f; // �U�����Ă��鎞��

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
                // �R���g���[���[�P��U��������
                SetVibration(0.5f, 0.5f, 0.35f);
            }

            // �����U�����Ȃ�
            if (m_isVibrating)
            {
                // �^�C�}�[�Ɍo�ߎ��Ԃ𑫂�
                m_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // �^�C�}�[���ݒ肵�����ԂɂȂ�����
                if (m_timer >= m_vibrationLength)
                {
                    // �U�����~�߂�
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
