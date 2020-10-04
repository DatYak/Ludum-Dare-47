using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
 
    private GameObject activlyPlacing;
    public bool editing;

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
        if (!editing)
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

        if (connecting)
        {
            currentCable.Setup(firstPoint.transform.position, transform.position);
        }

        if (!eventSys.IsPointerOverGameObject())
        {
           Structure newHover = GameManager.gm.GetNode(transform.position);
        
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

        if (Input.GetMouseButtonDown(0) && !eventSys.IsPointerOverGameObject())
        {
            if (activlyPlacing != null)
            {
                if (GameManager.gm.GetNode(transform.position) != null)
                    GameManager.gm.GetNode(transform.position).Destroy();
                Instantiate (activlyPlacing, transform.position, Quaternion.identity);
                activlyPlacing = null;
            }
            else
            {
                if (!editing)
                {
                    GameManager.gm.EditNode(transform.position);
                    editing = true;
                }
                else
                {
                    GameManager.gm.uimanager.HideStructureSettingsUI();
                    editing = false;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Connect();
        }
    }

    private void Connect ()
    {
    
        Structure target = GameManager.gm.GetNode(transform.position);
        
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
