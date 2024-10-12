using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Saver<T>
{
    
    public static void TryLoad(string fileName, ref T completionData)
    {
        string path = FileHandler.Path(fileName);
        if (File.Exists(path))
        {
            var dataString = File.ReadAllText(path);
            var saver = JsonUtility.FromJson<Saver<T>>(dataString);
            completionData = saver.data;
        }
    }

    internal static void Save(string fileName, T completionData)
    {

        var wrapper = new Saver<T> { data = completionData };
        var dataString = JsonUtility.ToJson(wrapper);
        File.WriteAllText(FileHandler.Path(fileName), dataString);
    }

  

    public T data;

}

public static class FileHandler
{
    public static string Path(string filename)
    {
        return $"{Application.persistentDataPath}/{filename}";
    }

    public static void Reset(string filename)
    {
        if (File.Exists(Path(filename)))
        {
            File.Delete(Path(filename));
        }
    }

    public static bool HasFile(string fileName)
    {
        return File.Exists(Path(fileName));
    }
}