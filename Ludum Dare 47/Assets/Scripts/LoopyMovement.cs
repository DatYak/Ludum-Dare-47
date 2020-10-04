using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopyMovement : MonoBehaviour
{

    public float moveSpeed = 1.5f;
    //How close a unit needs to be to snap to the position
    public float snap = 0.05f;

    public float radius {get {return transform.localScale.x/2;}}

    public Vector2 lookDir = Vector2.right;

    public Vector2 turnTo = Vector2.zero;
    
    //at what location to turn
    public Vector2 turnLoc;

    void Update()
    {

        if (turnTo != Vector2.zero)
        {
            if ((new Vector2 (transform.position.x, transform.position.y) - turnLoc).magnitude <= snap)
            {
                transform.position = turnLoc;
                lookDir = turnTo;
                turnTo = turnLoc = Vector2.zero;
            }
        }

        transform.Translate(lookDir * moveSpeed * Time.deltaTime * GameManager.gm.movespeed);
    }
}
