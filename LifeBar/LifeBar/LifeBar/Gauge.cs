using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LifeBar
{
    class Gauge
    {
        Texture2D m_backgroundTexture;
        Texture2D m_pixel;
        float m_maxValue;
        public float m_currentValue;
        float m_width;
        Rectangle m_bounds;
        Color m_colour;

        public Gauge(Texture2D backgroundTex, Texture2D pixel, Rectangle bounds, float startAmount, float maxValue, float width, Color colour)
        {
            m_backgroundTexture = backgroundTex;
            m_pixel = pixel;
            m_bounds = bounds;
            m_width = width;
            m_maxValue = maxValue;
            m_currentValue = startAmount;
            m_colour = colour;
        }

        public float CurrentValue
        {
            get { return m_currentValue; }
            set { m_currentValue = value; }
        }

        public void Draw(SpriteBatch sp)
        {
            // ゲージの量を計算
            int width = (int)((m_currentValue / m_maxValue) * m_width);

            // ゲージの中身を描画
            sp.Draw(m_pixel, new Rectangle(m_bounds.X, m_bounds.Y, width, m_bounds.Height), m_colour);

            // 四角い背景描画
            sp.Draw(m_backgroundTexture, m_bounds, Color.White);
        }
    }
}
