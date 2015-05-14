using System;
using System.Collections.Generic;
using System.Xml.Serialization;

public class XmlTable<T> where T : class, new()
{
    /// <summary>
    /// XMLファイルの拡張子
    /// </summary>
    public const string _FILE_EXTENSION = ".xml";

    protected T[] m_data;

    public XmlTable()
    {
        m_data = null;
    }

    /// <summary>
    /// データの数
    /// </summary>
    public int Size
    {
        get { return m_data.Length; }
    }

    /// <summary>
    /// xmlファイルの読み込み
    /// </summary>
    /// <param name="filepath">xmlファイルへのパス</param>
    /// <param name="extention">ファイルの拡張子</param>
    public virtual void Load(string filepath, string extention = _FILE_EXTENSION)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T[]));
        System.IO.FileStream fileStream = System.IO.File.OpenRead(filepath + extention);
        m_data = (serializer.Deserialize(fileStream) as T[]);
        fileStream.Close();
    }

    /// <summary>
    /// 読み込んだデータを獲得する
    /// </summary>
    /// <param name="index">何個目のデータかをしていする</param>
    /// <returns></returns>
    public T GetData(int index)
    {
        if (m_data == null)
        {
            return null;
        }

        int length = m_data.Length;
        if (index < 0 || index >= length)
        {
            return null;
        }

        return m_data[index];
    }

    /// <summary>
    /// 保持しているデータを破棄する
    /// </summary>
    public void Clear()
    {
        int size = m_data.Length;
        for (int i = 0; i < size; ++i)
        {
            m_data[i] = null;
        }

        m_data = null;
    }
}