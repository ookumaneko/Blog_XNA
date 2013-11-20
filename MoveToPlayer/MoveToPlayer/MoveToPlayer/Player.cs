using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MoveToPlayer
{
    class Player : Enemy
    {
        public Player(Texture2D texture, Vector2 pos)
            : base(texture, pos, Vector2.Zero, 0.0f)
        {
            m_layer = 0.0f;
        }

        public override void Update(float delta)
        {
            KeyboardState keyState = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;
            float speed = 4.0f;

            m_layer = 0.0f;
            if (keyState.IsKeyDown(Keys.Left))
            {
                direction.X = -speed;
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                direction.X = speed;
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                direction.Y = -speed;
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                direction.Y = speed;
            }

            Position += direction;
        }
    }
}
