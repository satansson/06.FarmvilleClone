using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GridElement curSelectedGridElement;
    public GridElement curHoveredGridElement;
    public GridElement[] grid;
    public Buildings buildings;

    [Header("Colors")]
    public Material onHoverMat;
    public Material onOccupiedMat;

    GridElement g;
    Material normMat;
    RaycastHit mouseHit;
    GameObject curCreatedBuildable;

    bool buildInProgress;

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
            // Tries to get from it a 'GridElement' component.
            g = mouseHit.transform.GetComponent<GridElement>();

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

            // Defines the mouse clicked 'selectedGridElement' with the 'g' value
            if (Input.GetMouseButtonDown(0))
            {
                curSelectedGridElement = g;
            }
        }
        else
        {
            // else sets previously hovered element (if there is one) material to normal
            if (curHoveredGridElement)
            {
                curHoveredGridElement.GetComponent<MeshRenderer>().material = normMat;
            }
        }

        MoveBuilding();
        PlaceBuilding();
    }

    public void OnBtnCreateBuilding(int id)
    {
        if (buildInProgress)
        {
            return;
        }

        GameObject g = null;

        foreach (GameObject gO in buildings.buildables)
        {
            Building b = gO.GetComponent<Building>();
            if (b.info.id == id)
            {
                g = b.gameObject;
            }
        }

        curCreatedBuildable = Instantiate(g);
        curCreatedBuildable.transform.rotation = Quaternion.Euler(0, -225, 0);
        buildInProgress = true;
    }

    // The chosen building follows the mouse
    public void MoveBuilding()
    {
        if (!curCreatedBuildable)
        {
            return;
        }

        curCreatedBuildable.gameObject.layer = 2;

        if (curHoveredGridElement)
        {
            curCreatedBuildable.transform.position = curHoveredGridElement.transform.position;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(curCreatedBuildable);
            curCreatedBuildable = null;
            buildInProgress = false;
        }

        if (Input.GetMouseButton(2))
        {
            curCreatedBuildable.transform.Rotate(transform.up * 5);
        }
    }

    // The chosen building is placed by mouse left click
    public void PlaceBuilding()
    {
        if (!curCreatedBuildable || curHoveredGridElement.occupied)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            buildings.built.Add(curCreatedBuildable);
            curHoveredGridElement.occupied = true;

            Building b = curCreatedBuildable.GetComponent<Building>();

            curHoveredGridElement.connectedBuilding = b;
            b.isPlaced = true;

            b.info.connectedGridId = curHoveredGridElement.gridId;
            b.info.yRotation = b.transform.localEulerAngles.y;

            b.UpgradeBuilding();

            curCreatedBuildable = null;
            buildInProgress = false;
        }
    }
}
