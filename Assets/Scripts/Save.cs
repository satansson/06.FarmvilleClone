using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class SavedProfile
{
    public float savedWood;
    public float savedStones;
    public float savedFood;

    public List<BuildingInfo> savedBiuldings = new List<BuildingInfo>();
}

public class Save : MonoBehaviour
{
    public SavedProfile savedProfile;

    Resources resources;
    Buildings buildings;
    Build build;


    void Awake()
    {
        resources = FindObjectOfType<Resources>();
        buildings = FindObjectOfType<Buildings>();
        build = FindObjectOfType<Build>();

        // TODO: Load savedProfile.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
            print("Game was saved.");
        }
    }

    void SaveGame()
    {
        if (savedProfile == null)
        {
            savedProfile = new SavedProfile();
        }

        savedProfile.savedWood = resources.wood;
        savedProfile.savedStones = resources.stones;
        savedProfile.savedFood = resources.food;

        foreach (GameObject gO in buildings.built)
        {
            BuildingInfo bI = gO.GetComponent<Building>().info;
            savedProfile.savedBiuldings.Add(bI);
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.dataPath + "/save.dat";

        if (File.Exists(path))
            File.Delete(path);

        FileStream fileStream = File.Open(path, FileMode.OpenOrCreate);
        binaryFormatter.Serialize(fileStream, savedProfile);

        fileStream.Close();
    }
}
