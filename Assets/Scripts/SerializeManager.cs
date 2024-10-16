using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializeManager
{
    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter binaryFormatter = GetBinaryFormatter();

        string savesFolderPath = Application.persistentDataPath + "/saves";

        if (!Directory.Exists(savesFolderPath))
        {
            Directory.CreateDirectory(savesFolderPath);
        }

        FileStream fileStream = File.Create($"{savesFolderPath}/{saveName}.save");

        binaryFormatter.Serialize(fileStream, saveData);
        fileStream.Close();

        return true;
    }

    public static object Load(string saveName)
    {
        BinaryFormatter binaryFormatter = GetBinaryFormatter();
        string savesFolderPath = Application.persistentDataPath + "/saves";

        if (!File.Exists($"{savesFolderPath}/{saveName}.save"))
        {
            return null;
        }

        FileStream fileStream = File.Open($"{savesFolderPath}/{saveName}.save", FileMode.Open);

        try
        {
            object saveData = binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return saveData;
        }
        catch
        {
            Debug.LogError($"Failed to load save file {saveName}");
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter binaryFormatter = new();
        SurrogateSelector surrogateSelector = new();

        Vector2Serialization vector2serialize = new ();

        surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2serialize);

        binaryFormatter.SurrogateSelector = surrogateSelector;
        return binaryFormatter;
    }
}
