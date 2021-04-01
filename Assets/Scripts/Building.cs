using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// It's better to each class have its own file
// but here we placed three classes into one file to easier manage them.

[System.Serializable] // Shows this class in the Inspector
public class PriceTag
{
    public float price_wood;
    public float price_stones;
    public float price_food;
}

[System.Serializable] // Shows this class in the Inspector
public class BuildingInfo
{
    public int id;
    public int connectedGridId;
    public float level = 0;
    public float yRotation = 0;
}

public class Building : MonoBehaviour
{
    public BuildingInfo info;
    public PriceTag price;

    public string objName;
    public bool isPositioned;
    public int baseResourceGain = 1;

    Resources resources;

    void Awake()
    {
        resources = FindObjectOfType<Resources>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPositioned)
            return;

        switch (info.id)
        {
            // Lumberjack
            case 1:
                resources.wood += (baseResourceGain * info.level) * Time.deltaTime;
                return;
            // Stone Mason
            case 2:
                resources.stones += (baseResourceGain * info.level) * Time.deltaTime;
                return;
            // Wind Mill
            case 3:
                resources.food += (baseResourceGain * info.level) * Time.deltaTime;
                return;
        }
    }
}
