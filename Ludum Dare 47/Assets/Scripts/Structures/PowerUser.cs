using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUser : Structure
{
    public bool hasPower;

    public virtual void Start() 
    {
        SetupStructure();
        UpdatePower();
        UpdateWithParams(paramaters);
    }
    
    public override void UpdatePower()
    {
        sprite.color = hasPower ? poweredColor : unpoweredColor;
    }
}
