using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUser : MonoBehaviour
{
    public bool hasPower;

    public SpriteRenderer renderer;
    //TODO: Change color shift to a sprite change 
    public Color unpoweredColor;
    public Color poweredColor;

    public virtual void Start() 
    {
        UpdatePower();
    }

    public void UpdatePower () 
    {
        renderer.color = hasPower ? poweredColor : unpoweredColor;    
    }

}
