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

namespace MoveToPlayer
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        Enemy enemy;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            Texture2D playerTex = Content.Load<Texture2D>("player");
            Texture2D enemyTex = Content.Load<Texture2D>("enemy");

            player = new Player(playerTex, new Vector2(300, 300));
            enemy = new Enemy(enemyTex, new Vector2(100, 100), player.Position, 32.0f);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // 前のフレームから経過した時間を獲得する
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // プレイヤーの移動
            player.Update(delta);

            // プレイヤーの位置を敵に保存する
            enemy.PlayerPos = player.Position;

            // 敵の移動
            enemy.Update(delta);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            player.Draw(spriteBatch);

            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            enemy.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
