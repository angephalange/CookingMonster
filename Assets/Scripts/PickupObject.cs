using UnityEngine;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.UI;
using System.Collections.Generic;

public class PickupObject : MonoBehaviour
{
    Camera mainCamera;
    public bool carrying;
    GameObject carriedObject;
    public float distance;
    public float smooth;
    public bool zoom = false;
    public bool letGo = false;
    public bool destroyMe = false;
    public float turningRate = 30f; //rotation rate in degrees per second
    private Quaternion _targetRotation = Quaternion.identity;  //rotation we should blend towards
    private float scrollDirection;
    [Tooltip("Drag in the 'HUDUpdtate' from the HUD to this box")]
    public Text interactable;
    [Tooltip("Drag in the 'HUDNote' from the HUD to this box")]

    public static PickupObject pickUp;

    private float rotateHorizontal = 2.0f;
    private float rotateVertical = 2.0f;
    public float throwPower;
    public float throwChargeRate;
    public float maxThrowPower;
    //[HideInInspector]
    public bool lookingAtButton = false;
    public GameObject[] sprayTex;



    void Start()
    {


        pickUp = this;
        throwPower = 15f;
        throwChargeRate = 75f;

        // player = GameObject.Find("FPSController");
        mainCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {

        // Turn towards our target rotation.
        if (!zoom)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.p_Player.p_Zoom = false;
        }
        else
        {
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.p_Player.p_Zoom = true;
            //transform.rotation = Quaternion.identity;
        }
        //check Drop + Drop/Pickup

        if (letGo)
        {
            dropObject();
            letGo = false;
        }

        if (destroyMe)
        {

            DestroyMe();


        }
        if (carrying)
        {
            carry(carriedObject);
            checkDrop();
        }
        else
        {
            pickup();
        }
        //Interaction's update Raycast
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2))
        {
            Pickupable p = hit.collider.GetComponent<Pickupable>();

          
            Interactable c = hit.collider.GetComponent<Interactable>();

            if (Input.GetKeyDown(KeyCode.C) && !hit.collider.isTrigger) //SUPER SECRET SPRAY CODE
            {

                Instantiate(sprayTex[Random.Range(0, sprayTex.Length)], hit.point, Quaternion.LookRotation(hit.normal * -1, Vector3.up));
                //Debug.Log("Sprayed");
            }

           

            if (c != null)
            {
                interactable.text = c.hudUpdateMessage.ToString();
                //Debug.Log("Send ID to hud");

                if (carrying)
                {
                    interactable.text = c.hudUpdateMessage.ToString();

                    {
                        if (interactable.text != null)
                        {
                            interactable.text = p.myState.ToString();
                        }
                    }
                }

            }
        }
        else
        {

            interactable.text = "";
        }
    }



    //functions while carrying
    void carry(GameObject o)
    {

        scrollDirection = Input.GetAxis("ScrollWheel");

        if (Input.GetButtonDown("Fire2") && !zoom) //throw lel
        {
            if (throwPower < maxThrowPower)
            {
                throwPower += throwChargeRate * 3;
            }

        }

        if (Input.GetButtonUp("Fire2") && !zoom)
        {
            throwObject();
            throwPower = 15f;
        }
        if (Input.GetKeyDown(KeyCode.Q)) //inspect
        {
            carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            zoom = true;
            o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
            if (zoom) //rotate carried object when Q is pressed
            {

                float h = rotateHorizontal * Input.GetAxis("Mouse X");
                float v = rotateVertical * Input.GetAxis("Mouse Y");
                carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                o.transform.Rotate(h, v, 0);
            }
        }
        else
        {

            zoom = false;
            o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
            //o.transform.rotation = Quaternion.identity;
            if (scrollDirection < 0f)
            {
                if (distance > 1)
                {
                    distance -= .1f;
                }
            }
            else if (scrollDirection > 0f)
            {
                if (distance < 2.45)
                {
                    distance += .1f;
                }
            }

        }
    }



    void pickup()
    {
        if (Input.GetButtonDown("Fire1"))
        //if (Input.GetKeyDown(KeyCode.E))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2))
            {

                Pickupable p = hit.collider.GetComponent<Pickupable>();
                if (p != null)
                {

                    carrying = true;
                    carriedObject = p.gameObject;
                    //p.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    //p.gameObject.GetComponent<Rigidbody>().
                    p.p_Carried = true;



                }
                else if (Physics.Raycast(ray, out hit, 2))
                {
                    /*New_Reciever r = hit.collider.GetComponent<New_Reciever>();
                    if (r != null)
                    {
                        r.PrintObjectID();
                        //do the thing
                    }  */

                }

              

            }
        }

    }




    void checkDrop()
    {
        if (Input.GetButtonDown("Fire1") && !zoom)

        {
            dropObject();
        }
    }

    void DestroyMe()
    {
        Debug.Log("I am dead");
        carrying = false;
        Destroy(carriedObject);
        carriedObject = null;
        destroyMe = false;
    }

    void throwObject()
    {
        carrying = false;
        //carriedObject.gameObject.GetComponent<MeshRenderer>().enabled = true;

        //carriedObject.gameObject.GetComponent<Collider>().enabled = true;


        //carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwPower);

        carriedObject = null;

    }

    void dropObject()
    {


        //carriedObject.gameObject.GetComponent<MeshRenderer>().enabled = true;
        //carriedObject.gameObject.GetComponent<Collider>().enabled = true;

        carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.GetComponent<Pickupable>().p_Carried = false;
        carrying = false;
        carriedObject = null;

    }
}
