using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Transform itemDock;
    public Transform powerUserDock;
    public Transform PowerCreatorDock;

    private void Start() 
    {
        SeePowerCreators();    
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
    

}
