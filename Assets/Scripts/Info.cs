using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Info : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] TextMeshProUGUI destroyText;
    [SerializeField] Button upgradeBtn;
    [SerializeField] Button destroyBtn;

    Color orange;
    Color red;
    Color darkOrange;
    Color darkRed;

    Build build;
    Building selectedBuilding;
    Resources resources;

    // Start is called before the first frame update
    void Awake()
    {
        build = FindObjectOfType<Build>();
        resources = FindObjectOfType<Resources>();

        orange = upgradeText.color;
        darkOrange = new Color(orange.r, orange.g, orange.b, orange.a * .5f);

        red = destroyText.color;
        darkRed = new Color(red.r, red.g, red.b, red.a * .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (build.curSelectedGridElement != null && build.curSelectedGridElement.connectedBuilding != null)
        {
            selectedBuilding = build.curSelectedGridElement.connectedBuilding;
            nameText.text = build.curSelectedGridElement.connectedBuilding.objName;
            levelText.text = "Level: " + selectedBuilding.info.level;
        }
        else
        {
            nameText.text = string.Empty;
            levelText.text = string.Empty;
            selectedBuilding = null;
        }

        destroyBtn.interactable = selectedBuilding;

        bool isEnoughResources = false;

        if (selectedBuilding)
        {
            if (resources.wood >= selectedBuilding.price.price_wood &&
            resources.stones >= selectedBuilding.price.price_stones &&
            resources.food >= selectedBuilding.price.price_food)
            {
                isEnoughResources = true;
            }

            upgradeBtn.interactable = isEnoughResources;
            destroyBtn.interactable = isEnoughResources;
        }
        else
        {
            upgradeBtn.interactable = false;
        }

        UpdateUpgradeTextColor();
        UpdateTearDownTextColor();
    }

    public void OnUpgradeBtn()
    {
        if (selectedBuilding)
        {
            selectedBuilding.UpgradeBuilding();
        }
    }

    public void OnDestroyBtn()
    {
        if (selectedBuilding)
        {
            build.buildings.built.Remove(selectedBuilding.gameObject);
            Destroy(selectedBuilding.gameObject);
            build.curSelectedGridElement.occupied = false;
        }
    }

    // Changes text color depending on corresponding button state
    void UpdateUpgradeTextColor()
    {
        if (upgradeBtn.interactable && upgradeText.color != orange)
            upgradeText.color = orange;
        else if (!upgradeBtn.interactable && upgradeText.color != darkOrange)
            upgradeText.color = darkOrange;
    }

    // Changes text color depending on corresponding button state
    void UpdateTearDownTextColor()
    {
        if (destroyBtn.interactable && destroyText.color != red)
            destroyText.color = red;
        else if (!destroyBtn.interactable && destroyText.color != darkRed)
            destroyText.color = darkRed;
    }
}
