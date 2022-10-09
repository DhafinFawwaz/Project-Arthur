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
        // LoadData();
    }


    










    [SerializeField] Encryption _encryption;
    public void SaveData()
    {
        _encryption.SaveData(Data);
    }
    public SaveData LoadData()
    {
        Data = _encryption.LoadData();
        OnDataLoaded();
        return Data;
    }
}
