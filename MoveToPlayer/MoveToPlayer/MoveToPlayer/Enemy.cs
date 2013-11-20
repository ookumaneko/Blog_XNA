using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MoveToPlayer
{
    class Enemy
    {
        protected Texture2D m_texture;
        public Vector2 Position;
        public Vector2 PlayerPos;
        protected float m_speed;
        protected float m_layer = 0.5f;

        public Enemy(Texture2D texture, Vector2 pos, Vector2 playerPos, float speed)
        {
            m_texture = texture;
            Position = pos;
            PlayerPos = playerPos;
            m_speed = speed;
        }

        public virtual void Update(float delta)
        {
            // プレイヤーへの向きを獲得
            Vector2 direction = PlayerPos - Position;

            // ベクトルを、正規化し、向きだけを保存させる
            direction.Normalize();

            // 敵の移動
            Position += direction * m_speed * delta;
        }

        public void Draw(SpriteBatch sp)
        {
            //sp.Draw(m_texture, Position, Color.White);
            sp.Draw(m_texture, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, m_layer); 
        }
    }
}
