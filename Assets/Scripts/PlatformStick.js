#pragma strict

function Start () {

}

function Update () {    

}function OnTriggerStay(other : Collider){
             
    if(other.gameObject.tag == "Platform"){
        transform.parent = other.transform;
 
    }
}
 
    function OnTriggerExit(other : Collider){
        if(other.gameObject.tag == "Platform"){
            transform.parent = null;
             
        }
    } 
