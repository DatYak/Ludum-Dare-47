using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecimalMenuItem : ParamaterSetting
{

    public TextMeshProUGUI paramaterName;
    public TMP_InputField value;

    public void Setup (DecimalParamater paramaterInfo) 
    {
        editing = paramaterInfo;
        paramaterName.text= paramaterInfo.name;
        value.text = paramaterInfo.value.ToString();
    }

    public void Update ()
    {
        (editing as DecimalParamater).value = float.Parse(value.text);
    }
}

