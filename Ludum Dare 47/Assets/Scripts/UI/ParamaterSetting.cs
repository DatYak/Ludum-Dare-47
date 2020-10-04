using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamaterSetting : MonoBehaviour
{

    public Paramater editing;

    public void Destroy ()
    {
        Destroy(this.gameObject);
    }

}
