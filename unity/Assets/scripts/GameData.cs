using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public string Scene;
    public float LookSensivity;

    public AudioSource FlatAudioSource;

    private void Awake()
    {
        if (GameData.Instance != null)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
        Load();
        Instance = this;
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savedgamedata.bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        GameDataPersistent data = new GameDataPersistent();
        data.Scene = Scene;
        data.LookSensivity = LookSensivity;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savedgamedata.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameDataPersistent data = formatter.Deserialize(stream) as GameDataPersistent;
            stream.Close();

            Scene = data.Scene;
            LookSensivity = data.LookSensivity;
        }
    }
    
}

[System.Serializable]
class GameDataPersistent
{
    public string Scene;
    public float LookSensivity;
}
