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
        enum GameStates
        {
            Menu,
            InGame,
            Pause,
        };

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Menu menu;
        SpriteFont font;
        SpriteFont largeFont;

        GameStates state = GameStates.Menu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            menu = new Menu(Color.Gold, Color.White);

            // メニューにオプションを足す
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
            largeFont = Content.Load<SpriteFont>("Large");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Input.Update(delta);

            // Allows the game to exit
            if (Input.IsPressed(Keys.Escape))
                this.Exit();

            // 現在の状態によって呼ぶupdateメソッドを
            // 変更する
            switch (state)
            {
                case GameStates.Menu:
                    UpdateMenu(delta);
                    break;
                case GameStates.InGame:
                    UpdateInGame(delta);
                    break;
                case GameStates.Pause:
                    UpdatePause(delta);
                    break;
            }

            base.Update(gameTime);
        }

        private void UpdateMenu(float delta)
        {
            menu.Update(delta);
            if (Input.IsPressed(Keys.Down))
            {
                // 下へ移動
                menu.SelectNext();
            }

            if (Input.IsPressed(Keys.Up))
            {
                // 上へ移動
                menu.SelectPrevious();
            }

            // メニューで選択された場合
            if (Input.IsPressed(Keys.Enter))
            {
                int option = menu.SelectedNumber;

                if (option == 0)
                {
                    state = GameStates.InGame;
                }
                else if (option == 1)
                {
                    // ...Optionが選択されたときの処理
                }
                else if (option == 2)
                {
                    this.Exit();
                }
            }
        }

        private void UpdateInGame(float delta)
        {
            if (Input.IsPressed(Keys.Space))
            {
                // ポーズ画面へ移動
                state = GameStates.Pause;
            }
            else if (Input.IsPressed(Keys.Enter))
            {
                // メニュー画面へ移動
                state = GameStates.Menu;
            }
        }

        private void UpdatePause(float delta)
        {
            if (Input.IsPressed(Keys.Space))
            {
                // ゲーム画面へ戻る
                state = GameStates.InGame;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (state)
            {
                case GameStates.Menu:
                    DrawMenu();
                    break;
                case GameStates.InGame:
                    DrawInGame();
                    break;
                case GameStates.Pause:
                    DrawPause();
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawMenu()
        {
            menu.Draw(spriteBatch, font);
            spriteBatch.DrawString(largeFont, "Menu Screen", new Vector2(300, 20), Color.Silver);
        }

        private void DrawInGame()
        {
            spriteBatch.DrawString(largeFont, "In Game", new Vector2(300, 20), Color.Silver);
            spriteBatch.DrawString(font, "Press Space to Pause", new Vector2(300, 300), Color.Silver);
            spriteBatch.DrawString(font, "Press Enter to return to Menu", new Vector2(300, 340), Color.Silver);
        }

        private void DrawPause()
        {
            spriteBatch.DrawString(font, "Pause Screen", new Vector2(300, 20), Color.Silver);
            spriteBatch.DrawString(font, "Press Space to resume", new Vector2(300, 300), Color.Silver);
        }
    }
}
