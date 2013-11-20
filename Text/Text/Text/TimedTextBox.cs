using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Text
{
    class TimedTextBox : TextBox
    {
        string m_text;
        float m_timer;      // タイマー
        float m_nextLetter; // 次の文字列を描画する時間
        int m_currentIndex; // 描画する文字の数
        bool m_isEndofText; 

        public TimedTextBox(Rectangle bounds, float speedToAddCharacter)
            : base(bounds)
        {
            m_text = "";
            m_timer = 0.0f;
            m_nextLetter = speedToAddCharacter; // 0.05f;
            m_currentIndex = 0;
            m_isEndofText = true;
        }



        public void Update(float delta)
        {
            if (m_isEndofText)
            {
                return;
            }

            // 次の文字を描画するかの確認
            m_timer += delta;
            if (m_timer >= m_nextLetter)
            {
                // タイマーのリセット
                m_timer = 0.0f;

                // 描画する文字数を増やす
                m_currentIndex++;
                if (m_currentIndex >= m_text.Length)
                {
                    m_currentIndex = m_text.Length;
                    m_isEndofText = true;
                }
            }
        }

        public void Draw(SpriteBatch sp, SpriteFont font, Texture2D texture, Color backColour, Color fontColour, bool wrapWords)
        {
            // 描画する文字列を獲得する
            string textToDraw = m_text.Substring(0, m_currentIndex);

            // 文字列の描画
            base.Draw(sp, font, textToDraw, texture, backColour, fontColour, wrapWords);
        }

        public override void Draw(SpriteBatch sp, SpriteFont font, string text, Texture2D texture, Color backColour, Color fontColour, bool wrapWords)
        {
            // 描画する文字列を獲得する
            string textToDraw = m_text.Substring(0, m_currentIndex);

            // 文字列の描画
            base.Draw(sp, font, textToDraw, texture, backColour, fontColour, wrapWords);
        }
    }
}
