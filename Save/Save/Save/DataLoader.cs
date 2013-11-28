// to get this working on XBox add references to :
// Microsoft.Xna.Framework.GamerServices & System.Xml.Serialization.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Save
{
    class DataLoader : SaveDataHandler
    {
        public DataLoader()
            : base()
        {
        }

        protected override bool Process(SaveGameData data)
        {
            // ファイルが存在しない場合、処理を終了する
            if (!m_storageContainer.FileExists(m_filename))
            {
                return false;
            }

            // ロード処理
            using (Stream stream = m_storageContainer.OpenFile(m_filename, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));

                // 読み込んだファイルをSaveGameDataに変換
                data = (serializer.Deserialize(stream) as SaveGameData);
            }

            return true;
        }

        protected override void OnError(string exceptionMessage)
        {
            base.OnError(exceptionMessage);
        }
    }
}
