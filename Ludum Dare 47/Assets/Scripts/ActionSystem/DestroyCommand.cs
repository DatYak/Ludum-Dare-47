using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCommand : IAction
{
    private Vector2 location;
    private Structure destroyedStructure;
    private StructureType typeDestroyed;
    private Paramater[] paramatersDestroyed;

    public DestroyCommand (Vector2 location)
    {
        this.location = location;
    }

    public void ExecuteCommand ()
    {
        destroyedStructure = GameManager.gm.GetNodeOccupant(location);
        typeDestroyed = destroyedStructure.thisType;
        paramatersDestroyed = destroyedStructure.paramaters;
        destroyedStructure.Destroy();
    }

    public void UndoCommand ()
    {
        Structure newStruct = null;
        switch (typeDestroyed)
        {
            case StructureType.PressurePlate:
                newStruct = GameObject.Instantiate(GameManager.gm.pressurePlate, location, Quaternion.identity).GetComponent<Structure>();
                break;
            case StructureType.Light:
                newStruct = GameObject.Instantiate(GameManager.gm.influenceLight, location, Quaternion.identity).GetComponent<Structure>();
                break;
            case StructureType.Rotator:
                newStruct = GameObject.Instantiate(GameManager.gm.rotator, location, Quaternion.identity).GetComponent<Structure>();
                break;
        }  
        if (newStruct != null)
        {
            newStruct.paramaters = paramatersDestroyed;
        }
    }
}
