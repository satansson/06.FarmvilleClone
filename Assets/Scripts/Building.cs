using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// It's better to each class have its own file
// but here we placed three classes into one file to easier manage them.

[System.Serializable] // Shows this class in the Inspector and allows to serialize it
public class PriceTag
{
    public float price_wood;
    public float price_stones;
    public float price_food;
}

[System.Serializable] // Shows this class in the Inspector and allows to serialize it
public class BuildingInfo
{
    public int id;
    public int connectedGridId;
    public int level = 0;
    public float yRotation = 0;
}

public class Building : MonoBehaviour
{
    public BuildingInfo info;
    public PriceTag price;

    public string objName;
    public bool isPlaced;
    [SerializeField] int baseResourceGain = 1;
    [SerializeField] float gainRate;

    Resources resources;

    void Awake()
    {
        resources = FindObjectOfType<Resources>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaced)
            return;

        switch (info.id)
        {
            // Lumberjack
            case 1:
                resources.wood += (baseResourceGain * info.level) * Time.deltaTime * gainRate;
                return;
            // Stone Mason
            case 2:
                resources.stones += (baseResourceGain * info.level) * Time.deltaTime * gainRate;
                return;
            // Wind Mill
            case 3:
                resources.food += (baseResourceGain * info.level) * Time.deltaTime * gainRate;
                return;
        }
    }

    public void UpgradeBuilding()
    {
        info.level++;

        resources.wood -= price.price_wood;
        resources.stones -= price.price_stones;
        resources.food -= price.price_food;
    }
}
