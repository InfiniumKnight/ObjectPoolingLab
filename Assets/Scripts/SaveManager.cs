using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[System.Serializable]
public class SavedData
{
    public List<GameObject> Target;
    public List<Vector3> targetPosition;
}

[System.Serializable]
public static class SaveManager
{
    public static int currentScore;
    public static string binaryFile = "ScoreBianary.bin";

    public static void SaveData()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Target");

        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        currentScore = gameManager.CurrentScore;

        SavedData savedData = new SavedData();

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream binarySaveFile = File.Create(Application.persistentDataPath + "/" + binaryFile);

        for(int i = 0; i <= gameObjects.Length - 1; i++)
        {
            SavedData save = new SavedData();
            save.Target.Add(gameObjects[i]); 
            save.targetPosition.Add(gameObjects[i].transform.position);
        }


        string jsonData = JsonUtility.ToJson(savedData);
        string filePath = Application.persistentDataPath + "/SavedData.json";
        formatter.Serialize(binarySaveFile, currentScore);

        binarySaveFile.Close();

        Debug.Log(filePath);
        File.WriteAllText(filePath, jsonData);

        
    }

    public static void LoadData()
    {
        string filepath = Application.persistentDataPath + "/SavedData.json";
        string jsonData = File.ReadAllText(filepath);
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream binarySaveFile = File.Open(Application.persistentDataPath + "/" + binaryFile, FileMode.Open);

        currentScore = (int) formatter.Deserialize(binarySaveFile);
        gameManager.CurrentScore = currentScore;

        binarySaveFile.Close();

        SavedData savedData = JsonUtility.FromJson<SavedData>(jsonData);

        for (var i = 0; i < savedData.Target.Count; i++)
        {
            savedData.Target[i].transform.position = savedData.targetPosition[i];
        }
    }
}
