using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI topButtonText;

    public Transform itemDock;
    public Transform powerUserDock;
    public Transform PowerCreatorDock;

    public GameObject settingsUI; 
    public TextMeshProUGUI settingsHeadderLabel;

    public Transform settingsView;
    public List<ParamaterSetting> currentSettings;
    public Structure currentFocus;

    public GameObject dropdownParamaterUIPrefab;
    public GameObject decimalParamaterUIPrefab;

    private void Start() 
    {
        SeePowerCreators();    
        HideStructureSettingsUI();
        GameManager.gm.RegisterUIManager(this);
    }

    public void CollapseDock()
    {
        itemDock.gameObject.SetActive (!itemDock.gameObject.activeInHierarchy);
    }

    //change what the player is spawning with each click
    public void SeePowerUsers()
    {
        PowerCreatorDock.gameObject.SetActive(false);
        powerUserDock.gameObject.SetActive(true);
    }

    public void SeePowerCreators()
    {
        PowerCreatorDock.gameObject.SetActive(true);
        powerUserDock.gameObject.SetActive(false);
    }
    
    public void SpawnTestBuilding (string buildingName)
    {
        GameManager.gm.StartTestBuilding(buildingName);
    }

    public void UpdateFocus ()
    {
        Paramater[] currentParamaters = new Paramater[currentSettings.Count];

        for (int i = 0; i < currentParamaters.Length; i++)
        {
            currentParamaters[i] = currentSettings[i].editing;
        }

        currentFocus.UpdateWithParams(currentParamaters);
    }

    public void StartButton ()
    {
        if (GameManager.gm.movespeed == 0)
        {
            topButtonText.text = "RESET";
        }
        else
        {
            topButtonText.text = "PLAY";
        }

        GameManager.gm.StartGame();
    }

    public void ShowStructureSettingsUI(Structure focus)
    {

        settingsUI.SetActive(true);

        currentFocus = focus;

        settingsHeadderLabel.text = focus.displayName;

        foreach(Paramater p in focus.paramaters)
        {
            if (p is DropdownParamater)
            {
                DropdownMenuItem item = Instantiate(dropdownParamaterUIPrefab, settingsView, false).GetComponent<DropdownMenuItem>();
                item.Setup(p as DropdownParamater);
                currentSettings.Add(item);
            }
            if (p is DecimalParamater)
            {
                DecimalMenuItem item = Instantiate(decimalParamaterUIPrefab, settingsView, false).GetComponent<DecimalMenuItem>();
                item.Setup(p as DecimalParamater);
                currentSettings.Add(item);
            }
        }
    }

    public void HideStructureSettingsUI()
    {
        settingsUI.SetActive(false);

        foreach (ParamaterSetting ps in currentSettings)
        {
            ps.Destroy();
        }

        currentSettings.Clear();
    }

}
