using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SaveManager : MonoBehaviour
{
    public SaveData Data;
    public void OnDataLoaded()
    {

    }
    void Start()
    {
        LoadData();
    }


    










    [SerializeField] Encryption encryption;
    public void SaveData()
    {
        encryption.SaveData(Data);
    }
    public void LoadData()
    {
        Data = encryption.LoadData();
        OnDataLoaded();
    }
}
