using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GridElement curSelectedGridElement;
    public GridElement curHoveredGridElement;
    public GridElement[] grid;

    [Header("Colors")]
    public Material onHoverMat;
    public Material onOccupiedMat;

    Material normMat;
    RaycastHit mouseHit;

    void Awake()
    {
        normMat = grid[0].GetComponentInChildren<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // If our mouse ray hits something
        if (Physics.Raycast(ray, out mouseHit))
        {
            // Checks if the hovered element has a 'GridElement' component.
            GridElement g = mouseHit.transform.GetComponent<GridElement>();

            // A) If it doesn't...
            if (!g)
            {
                // and if there was already a 'hoveredGridElement...
                if (curHoveredGridElement)
                {
                    // sets its material to Normal one
                    curHoveredGridElement.GetComponent<MeshRenderer>().material = normMat;
                    return;
                }                
            }

            // Defines the mouse clicked 'selectedGridElement' with the 'g' value
            if (Input.GetMouseButtonDown(0))
            {
                curSelectedGridElement = g;
            }

            // Checks if the 'g' is one we had already before...
            // (e.g. our mouse didn't hit any other element yet)
            if (g != curHoveredGridElement)
            {
                // and if it's not occupied
                if (!g.occupied)
                {
                    // sets its material to Hovered one
                    mouseHit.transform.GetComponent<MeshRenderer>().material = onHoverMat;
                }
                else
                {
                    // else sets it to Occupied one
                    mouseHit.transform.GetComponent<MeshRenderer>().material = onOccupiedMat;
                }
            }

            // If we already have a hovered element and now are hovering above the new one...
            if (curHoveredGridElement && curHoveredGridElement != g)
            {
                // ...changes previously hovered element material to normal...
                curHoveredGridElement.GetComponent<MeshRenderer>().material = normMat;
            }

            // ...and sets the current 'g' element as hovered
            curHoveredGridElement = g;
        }
        else
        {
            // else sets previously hovered element (if there is one) material to normal
            if (curHoveredGridElement)
            {
                curHoveredGridElement.GetComponent<MeshRenderer>().material = normMat;
            }
        }
    }
}
