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

        public Menu(Color selectedColour, Color unSelectedColour)
        {
            m_menuItem = new List<string>();
            m_menuText = new List<string>();
            m_positions = new List<Vector2>();
            m_textPosition = new List<Vector2>();
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
            //if (InputManager.IsPressed(PlayerIndex.One, Buttons.LeftThumbstickUp) ||
            //    InputManager.IsPressed(PlayerIndex.Two, Buttons.LeftThumbstickUp) ||
            //    InputManager.IsPressed(PlayerIndex.Three, Buttons.LeftThumbstickUp) ||
            //    InputManager.IsPressed(PlayerIndex.Four, Buttons.LeftThumbstickUp) ||
            //    InputManager.IsPressed(PlayerIndex.One, Buttons.DPadUp) ||
            //    InputManager.IsPressed(PlayerIndex.Two, Buttons.DPadUp) ||
            //    InputManager.IsPressed(PlayerIndex.Three, Buttons.DPadUp) ||
            //    InputManager.IsPressed(PlayerIndex.Four, Buttons.DPadUp) ||
            //    InputManager.IsPressed(Keys.Up))
            //{
            //    SelectPrevious();
            //}

            //if (InputManager.IsPressed(PlayerIndex.One, Buttons.LeftThumbstickDown) ||
            //    InputManager.IsPressed(PlayerIndex.Two, Buttons.LeftThumbstickDown) ||
            //    InputManager.IsPressed(PlayerIndex.Three, Buttons.LeftThumbstickDown) ||
            //    InputManager.IsPressed(PlayerIndex.Four, Buttons.LeftThumbstickDown) ||
            //    InputManager.IsPressed(PlayerIndex.One, Buttons.DPadDown) ||
            //    InputManager.IsPressed(PlayerIndex.Two, Buttons.DPadDown) ||
            //    InputManager.IsPressed(PlayerIndex.Three, Buttons.DPadDown) ||
            //    InputManager.IsPressed(PlayerIndex.Four, Buttons.DPadDown) ||
            //    InputManager.IsPressed(Keys.Down))
            //{
            //    SelectNext();
            //}

            //if (InputManager.IsPressed(PlayerIndex.One, Buttons.A) ||
            //    InputManager.IsPressed(PlayerIndex.Two, Buttons.A) ||
            //    InputManager.IsPressed(PlayerIndex.Three, Buttons.A) ||
            //    InputManager.IsPressed(PlayerIndex.Four, Buttons.A) ||
            //    InputManager.IsPressed(Keys.Enter))
            //{
            //    selected = true;
            //}
        }

        public virtual void Update(float delta, PlayerIndex index)
        {
        //    if (InputManager.IsPressed(index, Buttons.LeftThumbstickUp) ||
        //        InputManager.IsPressed(index, Buttons.DPadUp) ||
        //        InputManager.IsPressed(Keys.Up))
        //    {
        //        SelectPrevious();
        //    }

        //    if (InputManager.IsPressed(index, Buttons.LeftThumbstickDown) ||
        //        InputManager.IsPressed(index, Buttons.DPadDown) ||
        //        InputManager.IsPressed(Keys.Down))
        //    {
        //        SelectNext();
        //    }

        //    if (InputManager.IsPressed(index, Buttons.A) || InputManager.IsPressed(Keys.Enter))
        //    {
        //        selected = true;
        //    }
        }

        public virtual void Draw(SpriteBatch sp, SpriteFont font)
        {
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

        public virtual void Draw(SpriteBatch sp, SpriteFont font, Texture2D icon)
        {
            if (m_menuItem.Count != 0)
            {
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

        public virtual void Draw(SpriteBatch spriteBatch, SpriteFont font, bool drawIcon)
        {
            //draw icon
            int iconSize = 60;

            if (m_positions.Count != 0 && icon != null && drawIcon)
            {
                spriteBatch.Draw(icon, new Rectangle((int)m_positions[m_currentMenuItem].X - iconSize - 15, (int)m_positions[m_currentMenuItem].Y - 10, iconSize, iconSize), Color.White);
            }

            //draw menu items
            for (int i = 0; i < m_menuItem.Count; i++)
            {
                if (i == m_currentMenuItem)
                {
                    spriteBatch.DrawString(font, m_menuItem[i], m_positions[i], m_selectedColour);
                }
                else
                {
                    spriteBatch.DrawString(font, m_menuItem[i], m_positions[i], m_unSelectedColour);
                }
            }

            //draw menu text
            for (int i = 0; i < m_menuText.Count; i++)
            {
                spriteBatch.DrawString(font, m_menuText[i], m_textPosition[i], m_unSelectedColour);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //draw icon
            if (m_positions.Count != 0 && icon != null)
            {
                spriteBatch.Draw(icon, new Rectangle((int)m_positions[m_currentMenuItem].X - icon.Width, (int)m_positions[m_currentMenuItem].Y, 20, 20), Color.White);
            }
        }
    }
}
