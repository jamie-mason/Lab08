using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class FileDataHandler
{
    //directory path for save data
    private string dataDirPath = "";
   //fids the fine name for the saved data
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        //check to see if the save file actually exists.
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad); 
            }
            catch(Exception e)
            {
                Debug.LogError("Error occurred when trying to load the file: " + fullPath + "\n" + e);

            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        //Path.Combine accounts for different opperating systems having different path separators. 
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //create the directory the file will be written to if it does not already exist.
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error occurred when trying to save the file: " + fullPath + "\n" + e);
        }

    }
}
