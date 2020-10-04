using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablingAction : IAction
{

    private Cable currentCable;
    private Structure point0;
    private Structure point1;

    public CablingAction(Structure point0, Structure point1)
    {
        this.point0 = point0;
        this.point1 = point1;
    }

    public void ExecuteCommand ()
    {
        currentCable = GameObject.Instantiate(GameManager.gm.cablePrefab).GetComponent<Cable>();
        point0.connectedCables.Add(currentCable);
        point1.connectedCables.Add(currentCable);
        
        if (point0 is PowerCreator)
        {
            (point0 as PowerCreator).dependants.Add(point1 as PowerUser);
        }
        
        if (point1 is PowerCreator)
        {
            (point1 as PowerCreator).dependants.Add(point0 as PowerUser);
        }

        currentCable.Setup(point0.transform.position, point1.transform.position);
    }

    public void UndoCommand()
    {
        
        if (point0 is PowerCreator)
        {
            (point0 as PowerCreator).dependants.Remove(point1 as PowerUser);
        }
        
        if (point1 is PowerCreator)
        {
            (point1 as PowerCreator).dependants.Remove(point0 as PowerUser);
        }

        point0.connectedCables.Remove(currentCable);
        point1.connectedCables.Remove(currentCable);
        GameObject.Destroy(currentCable.gameObject);
    }

}
