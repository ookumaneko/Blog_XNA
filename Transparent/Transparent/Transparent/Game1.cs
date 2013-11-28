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

namespace Transparent
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D mouse;

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // �e�N�X�`���A�̓ǂݍ���
            mouse = Content.Load<Texture2D>("mouse");

            // ���F�̃s�N�Z���𓧖�������
            SetAlpha(mouse, Color.White);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(mouse, new Vector2(100, 100), Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }

        private void SetAlpha(Texture2D texture, Color toClear)
        {
            Color[] original = new Color[texture.Width * texture.Height];
            Color[] newColour = new Color[texture.Height * texture.Width];

            // �e�N�X�`���A�̐F��S�Ċl��
            texture.GetData<Color>(original);

            for (int i = 0; i < newColour.Length; ++i)
            {
                // �������݂̐F�������Ǝw�肳�ꂽ�F�̏ꍇ
                if (original[i] == toClear)
                {
                    // �F�𖳐F�����ɕς���
                    newColour[i] = Color.Transparent;
                }
                else
                {
                    // �Ⴄ�ꍇ�́A���̂܂܃R�s�[����
                    newColour[i] = original[i];
                }
            }

            // �w�i�F���������摜���e�N�X�`���ɕۑ�����
            texture.SetData<Color>(newColour);
        }
    }
}
