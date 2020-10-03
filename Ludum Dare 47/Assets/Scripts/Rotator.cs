using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : PowerUser
{
    public Vector2 turnToDir;

    void Update()
    {

        UpdatePower();

        if (hasPower)
        {
            Collider2D occupantCheck = Physics2D.OverlapBox(transform.position, new Vector2 (transform.localScale.x, transform.localScale.y), 0);
            if (occupantCheck != null)
                if (occupantCheck.tag == "Player")
                {
                    LoopyMovement occupant = occupantCheck.gameObject.GetComponent<LoopyMovement>();
                    if (occupant.lookDir != turnToDir)
                    {
                        occupant.turnTo = turnToDir;
                        occupant.turnLoc = transform.position;
                    }
                }
        }
    }
}

