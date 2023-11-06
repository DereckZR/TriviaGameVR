using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
public static class FileHandler{
    public static List<T> ReadFromJSON<T>(string filename)
    {
        string content = ReadFile(GetPath(filename));
        //Debug.Log(content);
        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<T>();
        }
        //Debug.Log(JsonHelper.FromJson<T>(content));
        List<T> res = JsonHelper.FromJson<T>(content).ToList();
        
        return res;
    }
    private static string GetPath(string filename)
    {
        return Application.dataPath + "/" + filename;
    }
    private static string ReadFile(string path)
    {
        if(File.Exists(path))
        {
            //Debug.Log("exist");
            using(StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        //Debug.Log("not exist");
        return "";
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.questions;
    }
    
    [Serializable]
    private class Wrapper<T>
    {
        public T[] questions;
    }
}
