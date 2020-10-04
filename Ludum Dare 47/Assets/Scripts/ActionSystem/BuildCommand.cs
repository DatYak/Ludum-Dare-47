using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCommand : IAction
{

    private GameObject structureToBuild;
    private Vector2 location;

    private GameObject instantiatedObject;

    public BuildCommand (GameObject structureToBuild, Vector2 location)
    {
        this.structureToBuild = structureToBuild;
        this.location = location;
    }

    public void ExecuteCommand ()
    {
        instantiatedObject = GameObject.Instantiate(structureToBuild, location, Quaternion.identity);
    }

    public void UndoCommand ()
    {
        GameObject.Destroy (instantiatedObject);   
    }
}
