using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class Encryption : MonoBehaviour
{
    string _path;
    const string Glyphs= "abcdefghijklmnopqrstuvwxyz0123456789";
                        //01234567890123456789012345678901

    static readonly string JSONEncryptedKey = "#kJ83DAlowjkf39(#($%0_+[]:#dDA'a";
                                               //01234567890123456789012345678901
    void Start()
    {
        _path = "data/data/" + Application.identifier.ToString() + "/files/game.dat";//Path for android
        _path = Application.persistentDataPath + "/game.dat";//Path for pc
        LoadData();
    }

    

    public void SaveData(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        Rijndael crypto = new Rijndael();
        byte[] soup = crypto.Encrypt(json, JSONEncryptedKey);

        if (File.Exists(_path))
        {
            File.Delete(_path);
        }

        File.WriteAllBytes(_path, soup);
    }
    public SaveData LoadData()
    {
		if(File.Exists(_path))
		{ 
            Rijndael crypto = new Rijndael();
            byte[] soupBackIn = File.ReadAllBytes(_path);
            string jsonFromFile = crypto.Decrypt(soupBackIn, JSONEncryptedKey);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonFromFile);
            return data;
		}
		else
		{
            Debug.Log("File not found");
            return new SaveData();
		}
    }
}

