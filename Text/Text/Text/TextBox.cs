using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Text
{
    class TextBox
    {
        protected Rectangle m_bounds;

        public TextBox(Rectangle bounds)
        {
            m_bounds = bounds;
        }

        protected string WrapWords(string text, SpriteFont font)
        {
            string line = "";
            string wrappedText = "";
            string[] words = text.Split(' ');

            foreach (string word in words)
            {
                // if current word hoes of the textbox
                float length = font.MeasureString(line + word).Length();
                if (length > m_bounds.Width)
                {
                    wrappedText += line + '\n';
                    line = "";
                }

                line += word + ' ';
            }

            return wrappedText + line;
        }

        protected string WrapText(string text, SpriteFont font)
        {
            string line = "";
            string wrappedText = "";

            for (int i = 0; i < text.Length; ++i)
            {
                float length = font.MeasureString(line + text[i]).Length();
                if (length > m_bounds.Width)
                {
                    wrappedText += line + '\n';
                    line = "";
                }

                line += text[i];
            }

            return wrappedText + line;
        }

        public virtual void Draw(SpriteBatch sp, SpriteFont font, string text, Texture2D texture, Color backColour, Color fontColour, bool wrapWords)
        {
            if (texture != null)
            {
                sp.Draw(texture, m_bounds, backColour);
            }

            string wrappedText = "";
            if (wrapWords)
            {
                wrappedText = WrapWords(text, font);
            }
            else
            {
                wrappedText = WrapText(text, font);
            }

            sp.DrawString(font, wrappedText, new Vector2(m_bounds.X + 5, m_bounds.Y + 5), fontColour);
        }
    }
}