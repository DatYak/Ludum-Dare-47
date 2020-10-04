using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCreator : Structure
{

    //the list of power users that rely on this creator for power
    public List<PowerUser> dependants;

    public virtual void Start()
    {
        SetupStructure();
        sprite.color = unpoweredColor;
        UpdateWithParams(paramaters);
    }

    //switch all dependants on
    public void Power ()
    {
        sprite.color = poweredColor;
        foreach (PowerUser p in dependants)
        {
            p.hasPower=true;
        }
    }

    //switch all dependents off
    public void UnPower ()
    {
        
        sprite.color = unpoweredColor;
        if (dependants != null)
            foreach (PowerUser p in dependants)
            {
                p.hasPower=false;
            }
    }
}