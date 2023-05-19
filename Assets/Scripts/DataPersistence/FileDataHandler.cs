using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";

    private bool useEncryption = false;

    private readonly string encryptionCodeWord = "zlqpam";

    public FileDataHandler(string dataDirPath,string dataFileName,bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public UserData Load()
    {
        string fullpath = Path.Combine(dataDirPath,dataFileName);
        UserData loadedData = null;
        if(File.Exists(fullpath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullpath,FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if(useEncryption){
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<UserData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.Log("Error occured when trying to load data from file " + fullpath + "\n"+e);
            }
        }
        return loadedData;
    }

    public void Save(UserData data)
    {
        string fullpath = Path.Combine(dataDirPath,dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
            
            string dataToStore = JsonUtility.ToJson(data,true);
            if(useEncryption){
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullpath,FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occurend when trying to save data to file "+ fullpath + "\n"+e);
        }
    }

    private string EncryptDecrypt(string data){
        string modifiedData = "";
        for(int i = 0; i < data.Length; i++){
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i%encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
