using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Sample
{
    class MenuState : GameState
    {
        Menu menu;
        SpriteFont font;
        SpriteFont largeFont;

        public MenuState(Game game, GraphicsDeviceManager graphics, GameStateManager owner)
            : base(game, graphics, owner)
        {
            // ... 初期化処理
            menu = new Menu(Color.Gold, Color.White);

            // メニューにオプションを足す
            menu.AddMenuItem("Start", new Vector2(400.0f, 300.0f));
            menu.AddMenuItem("Option", new Vector2(400.0f, 340.0f));
            menu.AddMenuItem("Exit", new Vector2(400.0f, 380.0f));

            font = Content.Load<SpriteFont>("font");
            largeFont = Content.Load<SpriteFont>("Large");
        }

        public override void Update(GameTime gameTime)
        {
            // ..メニュー画面の更新処理
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

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
                    Manager.ChangeState(GameStates.Playing);
                }
                else if (option == 1)
                {
                    // ...Optionが選択されたときの処理
                }
                else if (option == 2)
                {
                    Game.Exit();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // ..メニュー画面の描画処理
            SpriteBatch.Begin();
            menu.Draw(SpriteBatch, font);
            SpriteBatch.DrawString(largeFont, "Menu Screen", new Vector2(300, 20), Color.Silver);
            SpriteBatch.End();
        }
    }
}
