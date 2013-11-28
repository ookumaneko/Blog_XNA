using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Sample
{
    abstract class GameState
    {
        GameStateManager manager; // このクラスを保存しているクラス
        ContentManager content;
        GraphicsDeviceManager graphics;
        Game game;
        SpriteBatch spriteBatch;

        public GameState(Game game, GraphicsDeviceManager graphics, GameStateManager owner)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = game.Content;
            this.spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            this.manager = owner;
        }

        public Game Game
        {
            get { return game; }
        }

        public GameStateManager Manager
        {
            get { return manager; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return graphics.GraphicsDevice; }
        }

        public GraphicsDeviceManager GraphicsManager
        {
            get { return graphics; }
        }

        public ContentManager Content
        {
            get { return content; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        // 継承するクラスはこのメソッドをオーバーライドしなければいけない
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

    }
}
