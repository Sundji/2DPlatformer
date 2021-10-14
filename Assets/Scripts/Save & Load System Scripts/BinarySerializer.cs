using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class BinarySerializer
{

    private static readonly string path = Application.persistentDataPath + "/BestTime.dat";

    public static float LoadTime()
    {

        if (File.Exists(path))
        {

            FileStream file = File.Open(path, FileMode.Open);

            BinaryFormatter formatter = new BinaryFormatter();
            float time = (float)formatter.Deserialize(file);

            file.Close();

            return time;

        }

        return 0.0f;

    }

    public static void SaveTime(float time)
    {

        FileStream file = File.Create(path);

        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, time);

        file.Close();

    }

}
