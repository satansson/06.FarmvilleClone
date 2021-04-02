using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Info : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Button upgradeBtn;
    public Button destroyBtn;

    Build build;
    Building selectedBuilding;
    Resources resources;

    // Start is called before the first frame update
    void Awake()
    {
        build = FindObjectOfType<Build>();
        resources = FindObjectOfType<Resources>();
    }

    // Update is called once per frame
    void Update()
    {
        if (build.curSelectedGridElement != null && build.curSelectedGridElement.connectedBuilding != null)
        {
            selectedBuilding = build.curSelectedGridElement.connectedBuilding;
            nameText.text = build.curSelectedGridElement.connectedBuilding.objName + "\nLevel: " + selectedBuilding.info.level;
        }
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

    }
}
