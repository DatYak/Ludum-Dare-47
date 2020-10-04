using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;
    public GameObject blueprint;
    BuildingPlacer placer;

    public float movespeed = 0;

    public UIManager uimanager;

    public List<GridNode> nodes;

    private void Awake() 
    {
        if (gm == null)
        {
            gm = this;
            nodes = new List<GridNode>();
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogError("Two game masters present in scene!");
        }
    }

    public void RegisterUIManager (UIManager toRegister)
    {
        uimanager = toRegister;
    }

    private void Start() 
    {
        placer = Instantiate(blueprint, Vector3.zero, Quaternion.identity).GetComponent<BuildingPlacer>();
    }

    public void StartTestBuilding (string buildingName)
    {
        placer.Setup(buildingName);
    }

    public void AddBuilding(Structure toAdd, Vector2 location)
    {
        bool nodeFound = false;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodeFound) break;
            if (nodes[i].location == location)
            {
                nodeFound = true;
                if (nodes[i].isEmpty)
                {
                    nodes[i].SetOccupant(toAdd);
                }
            }
        }
        
        if (!nodeFound)
        {
            nodes.Add(new GridNode(location, toAdd));
        }
    }

    public Structure GetNode (Vector2 location)
    {
        bool nodeFound = false;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodeFound) break;
            if (nodes[i].location == location)
            {
                nodeFound = true;
                if (!nodes[i].isEmpty)
                {
                    return nodes[i].occupant;
                }
            }
        }
        return null;
    }

    public void EditNode (Vector2 location)
    {
        Structure toEdit = GetNode(location);
        if (toEdit != null)
        {
            uimanager.ShowStructureSettingsUI(toEdit);
        }
    }

    public void StartGame ()
    {
        if (movespeed == 0) movespeed = 1;
        else
        {
            movespeed = 0;

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(g.gameObject);
            }
        }
    }
}

public struct GridNode
{

    public Vector2 location;

    public Structure occupant;

    public bool isEmpty {get {return occupant == null;}}

    public GridNode (Vector2 loc, Structure buildToAdd)
    {
        location = loc;
        occupant = buildToAdd;
    }

    public void SetOccupant(Structure newOcc)
    {
        occupant = newOcc;
    }

    public void DestroyAllContents ()
    {
        occupant.Destroy();
    }
}
