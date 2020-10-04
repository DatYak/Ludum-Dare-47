using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Structure : MonoBehaviour
{

    public string displayName;


    public StructureType thisType;

    public SpriteRenderer sprite;
    //TODO: Change color shift to a sprite change 
    public Color unpoweredColor;
    public Color poweredColor;

    [HideInInspector]
    public List<Cable> connectedCables;

    int cost;

    public Paramater[] paramaters;

    public void SetupStructure() 
    {
        GameManager.gm.AddBuilding(this, transform.position);
        connectedCables =  new List<Cable>();
    }

    public virtual void Destroy()
    {
        foreach (Cable c in connectedCables)
        {
            Structure partener = null;
            if (GameManager.gm.GetNodeOccupant(c.pos0) == this)
                partener = GameManager.gm.GetNodeOccupant(c.pos1);
            else if (GameManager.gm.GetNodeOccupant(c.pos1) == this)
                partener = GameManager.gm.GetNodeOccupant(c.pos0);

            if (partener != null)
                partener.connectedCables.Remove(c);
            Destroy (c.gameObject);
        }

        GameManager.gm.DestroyNode(transform.position);

        Destroy (this.gameObject);
    }

    public virtual void UpdateWithParams (Paramater[] paramaters)
    {

    }
    
    public virtual void UpdatePower () 
    {

    }
}

public class Paramater
{
    public string name;
    
}

public class DecimalParamater : Paramater
{
    public DecimalParamater(string name, float intitial)
    {
        value = intitial;
        this.name = name;
    }
    public float value;
}

public class DropdownParamater : Paramater
{
    public DropdownParamater(string name, List<string> initial)
    {
        options = initial;
        this.name = name;
        selected = 0;
    }
    public List<string> options;
    public int selected;
}



public enum StructureType
{
    PressurePlate,
    Rotator,
    Light,
}
