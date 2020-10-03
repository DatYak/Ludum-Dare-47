using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCreator : MonoBehaviour
{

    //the list of power users that rely on this creator for power
    public List<PowerUser> dependants;

    //switch all dependants on
    public void Power ()
    {
        foreach (PowerUser p in dependants)
        {
            p.hasPower=true;
        }
    }

    //switch all dependents off
    public void UnPower ()
    {
        foreach (PowerUser p in dependants)
        {
            p.hasPower=false;
        }
    }

}