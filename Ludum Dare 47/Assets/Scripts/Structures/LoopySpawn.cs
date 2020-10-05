using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopySpawn : PowerCreator
{

    public LoopyMovement normalLoopyPrefab;
    public LoopyMovement speedyLoopyPrefab;
    public LoopyMovement lazyLoopyPrefab;
    public Vector2 spawnDirection;

    public float spawnInterval = 1;
    private float timeToNextSpawn;

    public LoopyType[] loopySpawnOrder;
    public int loopySpawnIndex = 0;

    public override void Start()
    {
        
        displayName = "Unit Spawn";

        paramaters = new Paramater[2];

        paramaters[0] = new DecimalParamater("Spawn Inerval (seconds)", 1f);

        List<string> faceOptions =  new List<string>
        {
            "Up",
            "Down",
            "Left",
            "Right"
        };

        paramaters[1] = new DropdownParamater("Starting direction", faceOptions);

        timeToNextSpawn = spawnInterval;
        base.Start();
    }

    private void Update() 
    {

        timeToNextSpawn -= Time.deltaTime * GameManager.gm.movespeed;

        if (timeToNextSpawn <= 0)
        {
            SpawnLoopy();
        }

        //funtion as a rototator
        Collider2D occupantCheck = Physics2D.OverlapBox(transform.position, new Vector2 (transform.localScale.x, transform.localScale.y), 0);
        if (occupantCheck != null)
            if (occupantCheck.tag == "Player")
            {
                LoopyMovement occupant = occupantCheck.gameObject.GetComponent<LoopyMovement>();
                if (occupant.lookDir != spawnDirection)
                {
                    occupant.turnTo = spawnDirection;
                    occupant.turnLoc = transform.position;
                }
            }
    }

    public void SpawnLoopy ()
    {

        if (loopySpawnIndex >= loopySpawnOrder.Length)
            return;
        
        LoopyMovement toSpawn = normalLoopyPrefab;

        switch (loopySpawnOrder[loopySpawnIndex])
        {
            case LoopyType.normal:
                toSpawn = normalLoopyPrefab;
                break;
            case LoopyType.speedy:
                toSpawn = speedyLoopyPrefab;
                break;
            case LoopyType.lazy:
                toSpawn = lazyLoopyPrefab;
                break;
        }
        LoopyMovement newLoopyMovement = Instantiate(toSpawn, transform.position, Quaternion.identity);
        newLoopyMovement.lookDir = spawnDirection;
        timeToNextSpawn = spawnInterval;

        loopySpawnIndex++;
    }

    public override void UpdateWithParams(Paramater[] paramaters)
    {
        spawnInterval = (paramaters[0] as DecimalParamater).value;
        timeToNextSpawn = spawnInterval;
        
        int dirIndex;

        dirIndex = (paramaters[1] as DropdownParamater).selected;

        switch (dirIndex)
        {
            case 0:
                spawnDirection = Vector2.up;
                break;

            case 1:
                spawnDirection = Vector2.down;
                break;

            case 2:
                spawnDirection = Vector2.left;
                break;

            case 3:
                spawnDirection = Vector2.right;
                break;
            
        }
    }
    public override void Destroy()
    {
        Debug.Log("Can't destroy Spawn!");
    }
}

public enum LoopyType
{
    normal,
    speedy,
    lazy
}
