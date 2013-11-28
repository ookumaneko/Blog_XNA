using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleParticle
{
    enum ParticleType
    {
        Linear, // 同じ速度のエフェクト
        Spread, // ランダムな速度で移動
    }

    class ParticleEmitter
    {
        protected List<Particle> m_particles;
        protected Random m_rand;

        public ParticleEmitter()
        {
            m_particles = new List<Particle>();
            m_rand = new Random();
        }

        public void Clear()
        {
            m_particles.Clear();
        }

        public void Emit(Texture2D tex, Vector2 pos, float scale, float shrinkRate, 
                          float duration, int amount, int maxSpeed, Color colour)
        {
            Particle p;

            for (int i = 0; i < amount; i++)
            {
                // ランダムな角度を獲得する
                int angle = m_rand.Next(0, 360);

                // ランダムな速度を獲得する
                float speed = m_rand.Next(1, maxSpeed);

                // 新しいパーティクルを作る
                p = new Particle(tex, pos, speed, angle, scale, shrinkRate, duration, colour);

                // パーティクルを足す
                m_particles.Add(p);
            }
        }

        public void Emit(Texture2D tex, Vector2 pos, int minAngle, int maxAngle, float scale,
                          float shrinkRate, float duration, int amount, int maxSpeed, Color colour)
        {
            Particle p;

            for (int i = 0; i < amount; i++)
            {
                // ランダムな角度を獲得する
                int angle = m_rand.Next(minAngle, maxAngle);

                // ランダムな速度を獲得する
                float speed = m_rand.Next(1, maxSpeed);

                // 新しいパーティクルを作る
                p = new Particle(tex, pos, speed, angle, scale, shrinkRate, duration, colour);

                // パーティクルを足す
                m_particles.Add(p);
            }
        }

        public void Emit(ParticleType type, Texture2D tex, Vector2 pos, int minAngle, int maxAngle, float scale,
                          float shrinkRate, float duration, int amount, int maxSpeed, Color colour)
        {
            Particle p;
            float angle = 0.0f;
            float speed = 0.0f;

            for (int i = 0; i < amount; i++)
            {
                // ランダムな角度を獲得する
                angle = m_rand.Next(minAngle, maxAngle);

                if (type == ParticleType.Linear)
                {
                    // 一定の速度を設定する。
                    speed = maxSpeed;
                }

                else if (type == ParticleType.Spread)
                {
                    // ランダムな速度を獲得する
                    speed = m_rand.Next(1, maxSpeed);
                }

                // 新しいパーティクルを作る
                p = new Particle(tex, pos, speed, angle, scale, shrinkRate, duration, colour);

                // パーティクルを足す
                m_particles.Add(p);
            }
        }

        public void Update(float delta)
        {
            // 破棄されているパーティクル
            List<Particle> toRemove = new List<Particle>();

            for (int i = 0; i < m_particles.Count; i++)
            {
                // パーティクルの更新
                m_particles[i].Update(delta);

                // もし現在のパーティクルが破棄されていた場合
                if (!m_particles[i].m_isActive)
                {
                    // 破棄するリストに追加
                    toRemove.Add(m_particles[i]);
                }
            }

            for (int i = 0; i < toRemove.Count; i++)
            {
                // パーティクルをリストから除外する
                m_particles.Remove(toRemove[i]);
            }
        }

        public void Draw(SpriteBatch sp)
        {
            foreach (Particle p in m_particles)
            {
                p.Draw(sp);
            }
        }

        protected class Particle
        {
            public Texture2D m_texture;
            public Vector2   m_position;
            public Vector2   m_direction;
            public Vector2   m_origin;
            public float     m_duration;
            public float     m_scale;
            public float     m_shrinkRate;
            public float     m_speed;      
            public bool      m_isActive;
            public Color     m_colour;

            public Particle(Texture2D tex, Vector2 pos, float speed, float angle, float scale, float shrinkRate, float duration, Color colour)
            {
                m_texture = tex;
                m_position = pos;
                m_scale = scale;
                m_shrinkRate = shrinkRate;
                m_isActive = true;
                m_duration = duration;
                m_colour = colour;
                m_speed = speed;
                m_origin = new Vector2(tex.Width / 2, tex.Height / 2);

                // 角度をラジアンに変更する
                angle = MathHelper.ToRadians(angle);

                // 角度から、向きを獲得する
                Vector2 up = new Vector2(0, -1.0f);
                Matrix rot = Matrix.CreateRotationZ(angle);
                m_direction = Vector2.Transform(up, rot);
            }

            public void Update(float delta)
            {
                // パーティクルを移動する
                m_position += m_direction * m_speed * delta;
                
                // パーティクルを小さくする
                m_scale -= m_shrinkRate * delta;

                // 寿命を減らす
                m_duration -= delta;

                // 大きさが小さすぎるか、寿命が尽きたら
                if (m_scale <= 0.0f || m_duration <= 0.0f)
                {
                    // パーティクルを破棄する
                    m_isActive = false;
                    m_position = new Vector2(-100, -100);
                }
            }

            public void Draw(SpriteBatch sp)
            {
                sp.Draw(m_texture, m_position, null, m_colour, 0, m_origin, m_scale, SpriteEffects.None, 0);
            }
        };

    }
}
