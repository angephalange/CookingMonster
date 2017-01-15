using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System.Collections.Generic;

public class Interactable : MonoBehaviour

{
    [Tooltip("Write here to set hover text")]
    public string hudUpdateMessage;
    [Tooltip("Drag in the 'HUDUpdtate' from the HUD to this box")]
    private Text objectInteract;

    [HideInInspector]








    private PickupObject player;

    // Use this for initialization
    void Start()
    {

        /*objectInteract = GameObject.Find("HUDUpdate").GetComponent<Text>();

        if (hudUpdateMessage == (""))
        {
            hudUpdateMessage = "left click to interact";
        }
        */


    }

    // Update is called once per frame
    void Update()
    {



    }


}



