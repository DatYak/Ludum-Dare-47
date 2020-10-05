using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopyAnimator : MonoBehaviour
{

    static float frameDelay = 0.4f;

    public bool rightFacing;

    public int walkframes;
    public int specialframes;

    public Sprite[] spritesRight;

    public Sprite[] spritesLeft;

    public Sprite[] specialLeft;

    public Sprite[] specialRight;

    private SpriteRenderer  spriteRenderer;

    private float frameTimer;

    private int index;

    public bool isSpecial = true;

    private void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            frameTimer = 100;
    }

    private void Update ()
    {   
        frameTimer += Time.deltaTime;
        if (frameTimer >= frameDelay)
        {
            index += 1;
            if (isSpecial)
            {
                if (index >= specialframes)
                    index = 0;
                if (rightFacing)
                    spriteRenderer.sprite = specialRight[index];
                else
                    spriteRenderer.sprite = specialLeft[index];
            }
            else
            {
                if (index >= walkframes)
                    index = 0;
                if (rightFacing)
                    spriteRenderer.sprite = spritesRight[index];
                else
                    spriteRenderer.sprite = spritesLeft[index];
            }
            frameTimer = 0;
        }
    }
}
