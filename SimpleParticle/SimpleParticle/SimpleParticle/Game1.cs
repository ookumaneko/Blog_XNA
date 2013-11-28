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

namespace SimpleParticle
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ParticleEmitter emitter;
        Texture2D particleTex;

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

            emitter = new ParticleEmitter();
            particleTex = Content.Load<Texture2D>("particle");            
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

            // スペースキーが押されたら
            if (Input.IsPressed(Keys.Space))
            {
                // パーティクルを作る
                emitter.Emit(ParticleType.Linear, particleTex, new Vector2(300, 300), 0, 360, 1.0f, 1.0f, 1.5f, 100, 250, Color.Gold);
                //emitter.Emit(ParticleType.Spread, particleTex, new Vector2(300, 300), 0, 360, 0.5f, 1.0f, 1.5f, 100, 250, Color.Gold);
            }

            if (Input.IsPressed(Keys.Z))
            {
                float scale = 0.5f;
                float shrink = 0.5f;
                int speed = 600;
                int min = 0; // 150;
                int max = 360; // 230;
                Vector2 pos = new Vector2(350, 200);
                emitter.Emit(ParticleType.Spread, particleTex, pos, min, max, scale, shrink, 1.0f, 40, speed, Color.Salmon);
                emitter.Emit(ParticleType.Spread, particleTex, pos, min, max, scale, shrink, 2.5f, 40, speed, Color.Pink);
                emitter.Emit(ParticleType.Spread, particleTex, pos, min, max, scale, shrink, 2.0f, 40, speed, Color.Crimson);
                //emitter.Emit(ParticleType.Linear, particleTex, pos, min, max, scale, shrink, 1.0f, 40, speed, Color.Salmon);
                //emitter.Emit(ParticleType.Linear, particleTex, pos, min, max, scale, shrink, 2.5f, 40, speed, Color.Pink);
                //emitter.Emit(ParticleType.Linear, particleTex, pos, min, max, scale, shrink, 2.0f, 40, speed, Color.Crimson);
            }

            // パーティクルの更新
            emitter.Update(delta);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, 

            spriteBatch.Begin();

            // パーティクルの描画
            emitter.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
