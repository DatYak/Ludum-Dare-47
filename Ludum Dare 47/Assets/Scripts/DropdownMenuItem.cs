using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownMenuItem : ParamaterSetting
{

    public TextMeshProUGUI paramaterName;
    public TMP_Dropdown selection;

    public void Setup (DropdownParamater paramaterInfo) 
    {
        editing = paramaterInfo;
        
        paramaterName.text= paramaterInfo.name;

        selection.ClearOptions();
        selection.AddOptions(paramaterInfo.options);
        selection.value = paramaterInfo.selected;
    }

    private void Update() {
        (editing as DropdownParamater).selected = selection.value;
    }
}
