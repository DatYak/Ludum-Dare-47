using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceLight : PowerUser
{
    public float radius;
    public SpriteRenderer radiusVisual;

    private Collider2D[] inrange;

    public override void Start() 
    {
        UpdatePower();
        UpdateRadius();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePower();

        if (hasPower)
        {

            //Check all 4 sides
            RaycastHit2D[] hitUp = Physics2D.RaycastAll(transform.position, Vector2.up, radius);
            RaycastHit2D[] hitDown = Physics2D.RaycastAll(transform.position, Vector2.down, radius);
            RaycastHit2D[] hitLeft = Physics2D.RaycastAll(transform.position, Vector2.left, radius);
            RaycastHit2D[] hitRight = Physics2D.RaycastAll(transform.position, Vector2.right, radius);

            foreach (RaycastHit2D rayhit in hitUp)
            {
                if (rayhit.collider.gameObject.tag == "Player")
                    PullIn(rayhit.collider.GetComponent<LoopyMovement>(), Vector2.down, rayhit.distance);
            }
            foreach (RaycastHit2D rayhit in hitDown)
            {
                if (rayhit.collider.gameObject.tag == "Player")
                    PullIn(rayhit.collider.GetComponent<LoopyMovement>(), Vector2.up, rayhit.distance);
            }
            foreach (RaycastHit2D rayhit in hitLeft)
            {
                if (rayhit.collider.gameObject.tag == "Player")
                    PullIn(rayhit.collider.GetComponent<LoopyMovement>(), Vector2.right, rayhit.distance);
            }
            foreach (RaycastHit2D rayhit in hitRight)
            {
                if (rayhit.collider.gameObject.tag == "Player")
                    PullIn(rayhit.collider.GetComponent<LoopyMovement>(), Vector2.left, rayhit.distance);
            }

        }
    }

    private void PullIn (LoopyMovement target, Vector2 dir, float dist)
    {
        if (target.lookDir != dir)
        {
            target.turnTo = dir;
            target.turnLoc = transform.position - (new Vector3 (dir.x, dir.y, 0) * (dist + target.radius));
        }
    }

    public void UpdateRadius ()
    {
        radiusVisual.transform.localScale =  new Vector3(radius*2, radius*2, 1);
    }
}
