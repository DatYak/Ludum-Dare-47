using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopySpawn : PowerCreator
{

    public LoopyMovement normalLoopyPrefab;
    public LoopyMovement speedyLoopyPrefab;
    public Vector2 spawnDirection;

    public float spawnInterval = 1;
    private float timeToNextSpawn;

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
    }

    public void SpawnLoopy ()
    {
        LoopyMovement newLoopyMovement = Instantiate(speedyLoopyPrefab, transform.position, Quaternion.identity);
        newLoopyMovement.lookDir = spawnDirection;
        timeToNextSpawn = spawnInterval;
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
                sprite.transform.rotation = Quaternion.Euler(0,0,0);
                break;

            case 1:
                spawnDirection = Vector2.down;
                sprite.transform.rotation = Quaternion.Euler(0,0,180);
                break;

            case 2:
                spawnDirection = Vector2.left;
                sprite.transform.rotation = Quaternion.Euler(0,0,90);
                break;

            case 3:
                spawnDirection = Vector2.right;
                sprite.transform.rotation = Quaternion.Euler(0,0,270);
                break;
            
        }
    }
    public override void Destroy()
    {
        Debug.Log("Can't destroy Spawn!");
    }

}
