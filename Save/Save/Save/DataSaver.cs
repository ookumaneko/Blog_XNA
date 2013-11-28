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
    class DataSaver : SaveDataHandler
    {
        public DataSaver()
            : base()
        {
        }

        protected override bool Process(SaveGameData data)
        {
            // 存在するデータを消去
            DeleteExistingData(m_filename);

            // 保存処理
            using (Stream stream = m_storageContainer.CreateFile(m_filename))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
                serializer.Serialize(stream, data);
            }

            return true;
        }

        protected override void OnError(string exceptionMessage)
        {
            base.OnError(exceptionMessage);
        }
    }
}
