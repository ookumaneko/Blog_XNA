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

namespace Menu
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Menu menu;
        SpriteFont font;
        Texture2D icon;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            menu = new Menu(Color.Gold, Color.White);

            // ���j���[�ɃI�v�V�����𑫂�
            menu.AddMenuItem("Start", new Vector2(400.0f, 300.0f));
            menu.AddMenuItem("Option", new Vector2(400.0f, 340.0f));
            menu.AddMenuItem("Exit", new Vector2(400.0f, 380.0f));
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");
            icon = Content.Load<Texture2D>("cursor");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update(0.0f);

            // Allows the game to exit
            if (Input.IsPressed(Keys.Escape))
                this.Exit();

            
            if (Input.IsPressed(Keys.Down))
            {
                // ���ֈړ�
                menu.SelectNext();
            }
            
            if (Input.IsPressed(Keys.Up))
            {
                // ��ֈړ�
                menu.SelectPrevious();
            }

            // ���j���[�őI�����ꂽ�ꍇ
            if (Input.IsPressed(Keys.Enter))
            {
                int option = menu.SelectedNumber;

                if (option == 0)
                {
                    // ...�X�^�[�g���I�����ꂽ�Ƃ��̏���
                }
                else if (option == 1)
                {
                    // ...Option���I�����ꂽ�Ƃ��̏���
                }
                else if (option == 2)
                {
                    // ...exit���I�����ꂽ�Ƃ��̏���
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            menu.Draw(spriteBatch, font); //, icon);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
