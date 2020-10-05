using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyLoopyMovement : LoopyMovement
{

    public float auraRadius;

    // Update is called once per frame
    public override void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, auraRadius);
        if (col!= GetComponent<Collider2D>() && col.tag == "Player")
        {
            base.Update();
        }
        else
        {
            anim.isSpecial = true;
        }
    }
}
