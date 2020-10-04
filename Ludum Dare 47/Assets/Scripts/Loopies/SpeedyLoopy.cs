using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyLoopy : LoopyMovement
{

    public float movespeedMax;
    public float movespeedMin;
    public float acceleration;

    public Vector2 lastDirection;

    public override void Update()
    {
        if (lastDirection == lookDir)
        {
            moveSpeed += acceleration * Time.deltaTime * GameManager.gm.movespeed;
            moveSpeed = Mathf.Clamp(moveSpeed, movespeedMin, movespeedMax);
        }
        else
        {
            moveSpeed = movespeedMin;
        }

        lastDirection = lookDir;
        base.Update();
    }

}
