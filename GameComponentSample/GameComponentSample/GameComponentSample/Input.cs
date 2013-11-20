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

namespace GameComponentSample
{
    class Input : GameComponent
    {
        public Input(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            // ... このクラスの初期化
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // ... 入力処理の更新
            base.Update(gameTime);
        }
    }
}
