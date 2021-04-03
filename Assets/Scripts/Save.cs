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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
            print("Game has been saved.");
        }else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
            print("Game has been loaded.");
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

    void LoadGame()
    {
        string pathToLoad = Application.dataPath + "/save.dat";

        if (!File.Exists(pathToLoad))
        {
            Debug.Log("No saved files found.");
            return;
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(pathToLoad, FileMode.Open);
        SavedProfile loadedProfile = binaryFormatter.Deserialize(fileStream) as SavedProfile;
        fileStream.Close();

        resources.wood = loadedProfile.savedWood;
        resources.stones = loadedProfile.savedStones;
        resources.food = loadedProfile.savedFood;

        for (int i = 0; i < loadedProfile.savedBiuldings.Count; i++)
        {
            BuildingInfo buildingFromSave = loadedProfile.savedBiuldings[i];

            build.RebuildBuilding(buildingFromSave.id, buildingFromSave.connectedGridId,
                buildingFromSave.level, buildingFromSave.yRotation);
            print("Building with ID of " + buildingFromSave.id + " was loaded.");
        }
    }
}
