using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonDataService : IDataService
{
    public bool SaveData<T>(string RelativePath, T data, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        if (File.Exists(path))
        {
            try
            {
                Debug.Log("Data exists. Deleting old file and writing a new one!");
                File.Delete(path);
                using FileStream stream = File.Create(path);
                stream.Close();
                File.WriteAllText(path, JsonConvert.SerializeObject(data));
                return true;
            }
            catch(Exception e) 
            {
                Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
                return false;
            }
        }
        else { }
    }
    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        throw new System.NotImplementedException();
    }

   
}
