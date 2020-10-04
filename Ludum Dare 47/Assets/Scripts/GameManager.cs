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
    public ActionHistoryManager actionRegistrar;

    public List<GridNode> nodes;

    public GameObject pressurePlate;

    public GameObject rotator;
    public GameObject influenceLight;
    
    public Cable cablePrefab;

    private void Awake() 
    {
        if (gm == null)
        {
            gm = this;
            nodes = new List<GridNode>();
            actionRegistrar = GetComponent<ActionHistoryManager>();
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

    public Structure GetNodeOccupant (Vector2 location)
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
    public GridNode GetNode (Vector2 location)
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
                    return nodes[i];
                }
            }
        }
        return new GridNode(false);
    }
    
    public void DestroyNode (Vector2 location)
    {
        GridNode toDestroy = GetNode(location);
        if (toDestroy.isReal == true)
        {
            nodes.Remove (toDestroy);
        }

    }

    public void EditNode (Vector2 location)
    {
        Structure toEdit = GetNodeOccupant(location);
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

    public bool isReal;

    public Vector2 location;

    public Structure occupant;

    public bool isEmpty {get {return occupant == null;}}

    public GridNode (Vector2 loc, Structure buildToAdd, bool exists = true)
    {
        isReal = true;
        location = loc;
        occupant = buildToAdd;
    }
    public GridNode (bool exists)
    {
        isReal = false;
        location = Vector2.zero;
        occupant = new Structure();

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