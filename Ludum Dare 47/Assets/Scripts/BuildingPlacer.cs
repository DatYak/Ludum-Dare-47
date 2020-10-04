using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
 
    private GameObject activlyPlacing;
    public bool editingTarget;

    public Tool currentTool;

    public Color buildColor;
    public Color editColor;
    public Color cablesColor;
    public Color destroyColor;

    public enum Tool
    {
        Building, Editing, Moving, Cables, Destroying
    }

    public GameObject pressurePlate;

    public GameObject rotator;
    public GameObject influenceLight;
    
    public Cable cablePrefab;
    private Cable currentCable;

    Structure hovering;

    public bool connecting = false;
    Structure firstPoint;
    bool hasUser;
    bool hasCreator;

    private EventSystem eventSys;
    
    private void Start() {
        eventSys = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }


    public void Setup (string toBuildName)
    {
        if (!editingTarget)
            switch(toBuildName)
            {
                case "pressurePlate":
                    activlyPlacing = pressurePlate;
                    break;
                case "rotator":
                    activlyPlacing = rotator;
                    break;
                case "influenceLight":
                    activlyPlacing = influenceLight;
                    break;
            }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x),
            Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y), 0);

        if (!eventSys.IsPointerOverGameObject())
        {
            Hover();
        }

        if (Input.GetMouseButtonDown(0) && !eventSys.IsPointerOverGameObject())
        {
            ToolAction();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            currentTool = Tool.Building;
            this.GetComponent<SpriteRenderer>().color = buildColor;
            GameManager.gm.uimanager.itemDock.gameObject.SetActive(true);
        }
            
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTool = Tool.Editing;
            this.GetComponent<SpriteRenderer>().color = editColor;
            GameManager.gm.uimanager.itemDock.gameObject.SetActive(false);
        }
            
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentTool = Tool.Cables;
            this.GetComponent<SpriteRenderer>().color = cablesColor;
            GameManager.gm.uimanager.itemDock.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            currentTool = Tool.Destroying;
            this.GetComponent<SpriteRenderer>().color = destroyColor;
            GameManager.gm.uimanager.itemDock.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Z) && !Input.GetKey(KeyCode.LeftShift))
        {
            GameManager.gm.actionRegistrar.UndoCommand();
        }
        if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.LeftShift))
        {
            GameManager.gm.actionRegistrar.RedoCommand();
        }
    }

    private void Hover()
    {
        
        if (connecting)
        {
            currentCable.Setup(firstPoint.transform.position, transform.position);
        }

        Structure newHover = GameManager.gm.GetNodeOccupant(transform.position);
    
        if (newHover != null)
        {
            if (hovering != newHover)
            {
                if (hovering != null)
                    foreach (Cable c in hovering.connectedCables)
                    {
                        c.Hide();
                    }
                
                foreach (Cable c in newHover.connectedCables)
                {
                    c.Show();
                }
                
                hovering = newHover;
            }
        }
        else
        {
            if (hovering != null)
                foreach (Cable c in hovering.connectedCables)
                {
                    c.Hide();
                }

            hovering = null;
        }
    }

    private void ToolAction ()
    {
        switch(currentTool)
        {
            case Tool.Building:
                BuildStructure();
                break;
            case Tool.Editing:
                Edit();
                break;
            case Tool.Cables:
                Connect();
                break;
            case Tool.Destroying:
                DestroyStructure();
                break;
        }
    }


    private void Edit ()
    {
        if (!editingTarget)
        {
            GameManager.gm.EditNode(transform.position);
            editingTarget = true;
        }
        else
        {
            GameManager.gm.uimanager.HideStructureSettingsUI();
            editingTarget = false;
        }
    }

    public void DestroyStructure ()
    {
        if (GameManager.gm.GetNodeOccupant(transform.position) != null)
            GameManager.gm.GetNodeOccupant(transform.position).Destroy();
    }

    private void BuildStructure ()
    {
        if (activlyPlacing != null)
        {
            DestroyStructure();

            IAction action = new BuildCommand(activlyPlacing, transform.position);
            GameManager.gm.actionRegistrar.ExecuteCommand(action);
        }
    }

    private void Connect ()
    {
    
        Structure target = GameManager.gm.GetNodeOccupant(transform.position);
        
        bool selectedUser = false;
        bool selectedCreator = false;

        if (target is PowerUser) selectedUser = true;
        if (target is PowerCreator) selectedCreator = true;

        if (target != null)
        {
            if (connecting)
            {
                if (hasUser && selectedUser) return;
                if (hasCreator && selectedCreator) return;

                if (hasUser)
                {
                    PowerCreator creator = target as PowerCreator;
                    creator.dependants.Add(firstPoint as PowerUser);
                    connecting = false;
                    currentCable.Setup(target.transform.position, firstPoint.transform.position);
                    firstPoint.connectedCables.Add(currentCable);
                    target.connectedCables.Add(currentCable);
                    currentCable.Hide();
                    currentCable = null;
                    hasUser = false;
                    hasCreator = false;
                }
                
                if (hasCreator)
                {
                    PowerUser user = target as PowerUser;
                    (firstPoint as PowerCreator).dependants.Add(user);
                    connecting = false;
                    currentCable.Setup(target.transform.position, firstPoint.transform.position);
                    firstPoint.connectedCables.Add(currentCable);
                    target.connectedCables.Add(currentCable);
                    currentCable.Hide();
                    currentCable = null;
                    hasUser = false;
                    hasCreator = false;
                }
            }
            else if (!connecting)
            {
                firstPoint = target;
                if (selectedCreator) hasCreator = true;
                if (selectedUser) hasUser = true;
                connecting = true;
                currentCable = Instantiate(cablePrefab);
            }
        }
        else if (connecting)
        {
            Destroy(currentCable.gameObject);
            firstPoint = null;
            connecting = false;
            hasUser = false;
            hasCreator = false;
        }
    }
}
