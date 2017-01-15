using UnityEngine;
using System.Collections;

public class Stay : MonoBehaviour {


    public GameObject movePlatform;

    private void OnTriggerStay()
    {
        movePlatform.transform.position += movePlatform.transform.forward * Time.deltaTime;
    }

}
