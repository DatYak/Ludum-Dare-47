using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : PowerUser
{
    public Vector2 turnToDir;

    public override void Start() 
    {
        displayName = "Rotator";

        if (paramaters == null)
        {
            paramaters = new Paramater[1];

            List<string> faceOptions =  new List<string>
            {
                "Up",
                "Down",
                "Left",
                "Right"
            };
             paramaters[0] = new DropdownParamater ("Turn to face:", faceOptions);
        }
        
        thisType = StructureType.Rotator;

        base.Start();
    }

    public override void UpdateWithParams (Paramater[] paramaters)
    {
        
        int dirIndex;

        dirIndex = (paramaters[0] as DropdownParamater).selected;

        switch (dirIndex)
        {
            case 0:
                turnToDir = Vector2.up;
                sprite.transform.up = Vector2.down;
                break;

            case 1:
                turnToDir = Vector2.down;
                sprite.transform.up = Vector2.up;
                break;

            case 2:
                turnToDir = Vector2.left;
                sprite.transform.up = Vector2.right;
                break;

            case 3:
                turnToDir = Vector2.right;
                sprite.transform.up = Vector2.left;
                break;
            
        }
    }

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

