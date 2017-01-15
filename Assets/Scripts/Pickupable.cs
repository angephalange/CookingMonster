using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Pickupable : MonoBehaviour
{
 
    private Collider p_collider;
    private MeshRenderer p_rend;
    private Rigidbody p_rigidBody;

   
    public bool p_Carried = false;
    public string myState;
   

    // Use this for initialization
    void Start()
    {

      
    }
    // Update is called once per frame
    void Update()
    {

    }
    
}
