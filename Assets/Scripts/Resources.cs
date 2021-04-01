using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resources : MonoBehaviour
{
    public float wood;
    public float stone;
    public float food;

    [Header("UI Reference")]
    public TextMeshProUGUI resourcesText;

    void FixedUpdate()
    {
        resourcesText.text = "Wood: " + wood.ToString("F0") + " | Stones: "
            + stone.ToString("F0") + " | Food: " + food.ToString("F0");
    }
}
