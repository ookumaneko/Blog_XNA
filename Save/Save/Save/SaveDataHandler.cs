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
    [XmlInclude(typeof(Unit)), XmlInclude(typeof(Player))]
    public class GameObject
    {
        public Vector3 Position { get; set; }
    }

    public class Unit : GameObject
    {
        public float Life { get; set; }
    }

    public class Player : GameObject
    {
        public string Name { get; set; }
    }

    public class SaveGameData
    {
        public string PlayerName;
        public Vector2 AvatarPosition;
        public int Level;
        public int Score;
        public List<GameObject> GameObjects;
    }

    class SaveDataHandler
    {
        protected enum States
        {
            None,                        // 何も無い状態
            ReadyToSelectStorageDevice,  // デバイス選択を出来る状態
            SelectingStorageDevice,      // デバイス選択中
            ReadyToOpenStorageContainer, // コンテナをあける準備が出来た状態
            OpeningStorageContainer,     // コンテナを開いている最中
            HandlingFile                 // ファイル操作中
        }

        protected StorageDevice m_storageDevice;
        protected States m_state;
        protected IAsyncResult m_asyncResult;
        protected StorageContainer m_storageContainer;
        protected PlayerIndex m_controller;
        protected string m_filename;    // 保存されるファイル名
        protected string m_diplayName;  // 使用されるコンテナ（ディレクトリ）名
        protected SaveGameData m_data;

        public SaveDataHandler()
        {
            m_data = null;
            m_state = States.None;
            m_storageContainer = null;
            m_storageDevice = null;
            m_filename = "";
            m_diplayName = "";
        }

        public SaveGameData Data
        {
            get { return m_data; }
            set { m_data = value; }
        }

        public void Update(float delta)
        {
            switch (m_state)
            {
                case States.ReadyToSelectStorageDevice:
                    #if XBOX
                    if (!Guide.IsVisible)
                    #endif
                    {
                        // デバイス選択を表示（Windowだと表示されない)
                        m_asyncResult = StorageDevice.BeginShowSelector(m_controller, null, null);
                        m_state = States.SelectingStorageDevice;
                    }
                    break;

                case States.SelectingStorageDevice:
                    if (m_asyncResult.IsCompleted)
                    {
                        // デバイス選択を閉じる（Windowだと表示されない)
                        m_storageDevice = StorageDevice.EndShowSelector(m_asyncResult);
                        m_state = States.ReadyToOpenStorageContainer;
                    }
                    break;

                case States.ReadyToOpenStorageContainer:

                    if (m_storageDevice == null || !m_storageDevice.IsConnected)
                    {
                        // デバイスを獲得出来ていない場合、もしくは接続されていない場合は、選択に戻る
                        m_state = States.ReadyToSelectStorageDevice;
                    }
                    else
                    {
                        // 指定されたゲームのコンテナを開く
                        m_asyncResult = m_storageDevice.BeginOpenContainer(m_diplayName, null, null);
                        m_state = States.OpeningStorageContainer;
                    }
                    break;

                case States.OpeningStorageContainer:
                    
                    if (m_asyncResult.IsCompleted)
                    {
                        // コンテナを開いたので、処理を終了させる
                        m_storageContainer = m_storageDevice.EndOpenContainer(m_asyncResult);
                        m_state = States.HandlingFile;
                    }
                    break;

                case States.HandlingFile:

                    if (m_storageContainer == null)
                    {
                        // コンテナを獲得できていない場合、戻る
                        m_state = States.ReadyToOpenStorageContainer;
                    }
                    else
                    {
                        try
                        {
                            // 継承されたクラスで行われる、ファイル処理
                            Process(m_data);
                        }
                        catch (IOException e)
                        {
                            // 問題があった場合
                            OnError(e.Message);
                        }
                        finally
                        {
                            // コンテナを破棄し、他も初期化する
                            m_storageContainer.Dispose();
                            m_storageContainer = null;
                            m_state = States.None;
                            m_filename = "";
                            m_diplayName = "";
                        }
                    }
                    break;
            }
        }

        protected virtual bool Process(SaveGameData data)
        {
            return false;
        }

        protected virtual void OnError(string exceptionMessage)
        {
            Debug.WriteLine(exceptionMessage);
        }

        public void Start(string fileName, string displayName, SaveGameData data, PlayerIndex controller)
        {
            // 処理を開始 ＋ 初期化
            if (m_state == States.None)
            {
                m_data = data;
                m_filename = fileName;
                m_diplayName = displayName;
                m_state = States.ReadyToOpenStorageContainer; 
                m_controller = controller;
            }
        }

        protected void DeleteExistingData(string fileName)
        {
            // 指定されたファイルが存在した場合、消去する
            if (m_storageContainer.FileExists(fileName))
            {
                m_storageContainer.DeleteFile(fileName);
            }
        }
    }
}
