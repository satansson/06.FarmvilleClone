using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public int connectedBuildingId;

    [HideInInspector]
    public Building connectedBuilding;
    public TextMeshProUGUI resourcesText;

    Button btn;
    Resources resources;

    void Awake()
    {
        btn = GetComponent<Button>();
        resources = FindObjectOfType<Resources>();

        Buildings buildings = FindObjectOfType<Buildings>();

        // Assigns already existing building script to 'connectedBuilding' variable
        // to know what it exactly is
        foreach (GameObject gO in buildings.buildables)
        {
            Building b = gO.GetComponent<Building>();
            if (b.buildingInfo.id == connectedBuildingId)
            {
                connectedBuilding = b;
                break;
            }
        }

        resourcesText.text = connectedBuilding.price.price_wood + " Wo. | " +
            connectedBuilding.price.price_stones + " St. | " + connectedBuilding.price.price_food + " Fo.";
    }

    void Update()
    {
        bool isEnoughResources = false;

        if (resources.wood >= connectedBuilding.price.price_wood &&
            resources.stones >= connectedBuilding.price.price_stones &&
            resources.food >= connectedBuilding.price.price_food)
        {
            isEnoughResources = true;
        }

        btn.interactable = isEnoughResources;
    }
}
