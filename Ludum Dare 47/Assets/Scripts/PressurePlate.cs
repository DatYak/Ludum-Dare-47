using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : PowerCreator
{
    private float currentSustain = 0;

    void Update()
    {
        
        Collider2D occupantCheck = Physics2D.OverlapBox(transform.position, new Vector2 (transform.localScale.x, transform.localScale.y), 0);
        if (occupantCheck == null)
        {
            UnPower();
        }
        else if (occupantCheck.tag == "Player")
        {
            Power();
        }
    }
}
