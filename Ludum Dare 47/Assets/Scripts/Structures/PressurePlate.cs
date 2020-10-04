using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : PowerCreator
{

    public float sustain;
    private float currentSustain = 0;

    public override void Start() 
    {
        displayName = "Pressure Plate";

        if (paramaters == null)
        {

            paramaters = new Paramater[1];

            paramaters[0] = new DecimalParamater("Sustain", 0.5f);

            thisType = StructureType.PressurePlate;
        }
        
        base.Start();
    }

    void Update()
    {

        if (currentSustain > 0)
            currentSustain -= Time.deltaTime;
        else
            UnPower();

        Collider2D occupantCheck = Physics2D.OverlapBox(transform.position, new Vector2 (transform.localScale.x, transform.localScale.y), 0);
        
        if (occupantCheck != null)
            if (occupantCheck.tag == "Player")
            {
                Power();
                currentSustain = sustain;
            }
    }
    
    public virtual void UpdateWithParams (Paramater[] paramaters)
    {
        sustain = (paramaters[0] as DecimalParamater).value;
    }
}
