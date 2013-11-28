using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections;
using System.Threading;

namespace ThreadTest
{
    public static class TextureManager
    {
        static ContentManager m_content;
        static Dictionary<string, Texture2D> m_textures = new Dictionary<string, Texture2D>();
        static int m_texturesLoaded = 0;

        public static int TexturesLoaded
        {
            get { return m_texturesLoaded; }
        }

        public static void Initialize(Game game)
        {
            m_content = game.Content;
        }

        public static void LoadTexture(string textureName)
        {
            if (textureName != null && !m_textures.ContainsKey(textureName))
            {
                // スレッドの処理する中身を設定
                ThreadStart threadStarter = delegate
                {
                    // ..ココにスレッド内の処理を実装
                    Texture2D texture = m_content.Load<Texture2D>(textureName);
                    m_textures.Add(textureName, texture);
                    m_texturesLoaded++;
                };

                // スレッドを作り、動かす
                Thread loadingThread = new Thread(threadStarter);
                loadingThread.Start();
            }
        }

        public static void RemoveTexture(string toRemove)
        {
            if (toRemove != null && m_textures.ContainsKey(toRemove))
            {
                ThreadStart threadStarter = delegate
                {
                    m_textures[toRemove].Dispose();
                    m_textures.Remove(toRemove);
                    m_texturesLoaded--;
                };

                Thread loadingThread = new Thread(threadStarter);
                loadingThread.Start();
            }
        }

        public static Texture2D GetTexture(string name)
        {
            if (name != null && m_textures.ContainsKey(name))
            {
                return m_textures[name];
            }

            return null;
        }
    }
}