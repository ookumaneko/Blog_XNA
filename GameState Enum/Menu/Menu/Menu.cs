using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menu
{
    class Menu
    {
        Texture2D icon;
        List<string> m_menuItem;
        List<string> m_menuText;
        protected List<Vector2> m_positions;
        List<Vector2> m_textPosition;
        Color m_selectedColour;
        Color m_unSelectedColour;
        int m_currentMenuItem;
        bool selected;

        List<float> m_scales;
        float m_maxScale = 1.5f;
        float m_minScale = 1.0f;

        public Menu(Color selectedColour, Color unSelectedColour)
        {
            m_menuItem = new List<string>();
            m_menuText = new List<string>();
            m_positions = new List<Vector2>();
            m_textPosition = new List<Vector2>();
            m_scales = new List<float>();
            m_selectedColour = selectedColour;
            m_unSelectedColour = unSelectedColour;
            m_currentMenuItem = 0;
        }

        public Menu(Texture2D icon, Color selectedColour, Color unSelectedColour)
        {
            this.icon = icon;
            m_menuItem = new List<string>();
            m_menuText = new List<string>();
            m_positions = new List<Vector2>();
            m_textPosition = new List<Vector2>();
            this.m_selectedColour = selectedColour;
            this.m_unSelectedColour = unSelectedColour;
            m_currentMenuItem = 0;
            selected = false;
        }

        #region Accessor

        public int SelectedNumber
        {
            get { return m_currentMenuItem; }
        }

        /// <summary>
        /// Gets the name of the currently selected item
        /// </summary>
        public string GetSelectedName
        {
            get { return m_menuItem[m_currentMenuItem]; }
        }

        protected List<string> MenuText
        {
            get { return m_menuText; }
        }

        protected List<string> MenuItem
        {
            get { return m_menuItem; }
        }

        protected List<Vector2> TextPosition
        {
            get { return m_textPosition; }
        }

        protected Texture2D Texture
        {
            get { return icon; }
        }

        protected Color UnselectedColour
        {
            get { return m_unSelectedColour; }
        }

        protected Color SelectedColour
        {
            get { return m_selectedColour; }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public Vector2 IconPosition
        {
            get { return m_positions[m_currentMenuItem]; }
        }

        #endregion

        /// <summary>
        /// Add an item to the menu
        /// </summary>
        /// <param name="name">Name of the item (which will be used when displayed)</param>
        /// <param name="position">The position of the item to be drawn</param>
        public virtual void AddMenuItem(string name, Vector2 position)
        {
            m_menuItem.Add(name);
            m_positions.Add(position);
            m_scales.Add(1.0f); // デフォルトのサイズを足す
        }

        public void AddMenuText(string text, Vector2 position)
        {
            m_menuText.Add(text);
            m_textPosition.Add(position);
        }

        public virtual void SelectTop()
        {
            m_currentMenuItem = 0;
        }

        /// <summary>
        /// 次のオプションへ移動する
        /// </summary>
        public virtual void SelectNext()
        {
            if (m_currentMenuItem < m_menuItem.Count - 1)
            {
                m_currentMenuItem++;
            }
            else
            {
                m_currentMenuItem = 0;
            }
        }

        /// <summary>
        /// 前のオプションへ移動する
        /// </summary>
        public virtual void SelectPrevious()
        {
            if (m_currentMenuItem > 0)
            {
                m_currentMenuItem--;
            }
            else
            {
                m_currentMenuItem = m_menuItem.Count - 1;
            }
        }

        public virtual void Update(float delta)
        {
            // サイズ変更の速度
            float speed = 2.5f * delta;

            for (int i = 0; i < m_menuItem.Count; ++i)
            {
                // もし現在選択されている選択肢なら
                if (i == SelectedNumber)
                {
                    if (m_scales[i] < m_maxScale)
                    {
                        //選択肢のサイズを大きくする
                        m_scales[i] += speed;
                    }

                }
                else if (m_scales[i] > m_minScale && i != m_currentMenuItem)
                {
                    //選択されてなく、なおデフォルト値より大きい場合小さくする
                    m_scales[i] -= speed;
                }
            }
        }

        public virtual void Draw(SpriteBatch sp, SpriteFont font)
        {
            // メニューのオプションを描画する
            for (int i = 0; i < m_menuItem.Count; i++)
            {
                // 描画位置を計算する
                Vector2 pos = m_positions[i];
                pos.Y -= (float)(22 * m_scales[i] / 2);
                pos.X -= (float)(22 * m_scales[i] / 2);

                // もし現在カーソルがあっている場合
                if (i == SelectedNumber)
                {
                    sp.DrawString(font, m_menuItem[i], pos, m_selectedColour, 0, Vector2.Zero, m_scales[i], SpriteEffects.None, 0);
                }
                else
                {
                    // 他と同じ色で描画する
                    sp.DrawString(font, m_menuItem[i], pos, m_unSelectedColour, 0, Vector2.Zero, m_scales[i], SpriteEffects.None, 0);
                }
            }

            //draw menu text
            for (int i = 0; i < m_menuText.Count; i++)
            {
                sp.DrawString(font, m_menuText[i], m_textPosition[i], m_unSelectedColour);
            }
        }

        public virtual void Draw(SpriteBatch sp, SpriteFont font, Texture2D icon)
        {
            // メニューが空じゃない場合
            if (m_menuItem.Count != 0)
            {
                // アイコンの描画
                Vector2 pos = new Vector2(m_positions[m_currentMenuItem].X - icon.Width - 15, m_positions[m_currentMenuItem].Y);
                sp.Draw(icon, pos, Color.White);
            }

            // メニューのオプションを描画する
            for (int i = 0; i < m_menuItem.Count; i++)
            {
                // もし現在カーソルがあっている場合
                if (i == m_currentMenuItem)
                {
                    // 違う色で描画する
                    sp.DrawString(font, m_menuItem[i], m_positions[i], m_selectedColour);
                }
                else
                {
                    // 他と同じ色で描画する
                    sp.DrawString(font, m_menuItem[i], m_positions[i], m_unSelectedColour);
                }
            }

            //draw menu text
            for (int i = 0; i < m_menuText.Count; i++)
            {
                sp.DrawString(font, m_menuText[i], m_textPosition[i], m_unSelectedColour);
            }
        }
    }
}
