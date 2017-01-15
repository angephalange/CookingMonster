var onhand : Transform;

function Update () { 


}

function OnMouseDown () { 
    GetComponent.<Rigidbody>().useGravity = false; 
    this.transform.position = onhand.position; 
    this.transform.parent = GameObject.Find("FPSController").transform;
    this.transform.parent = GameObject.Find("FirstPersonCharacter").transform; 
}


function OnMouseUp () {
    this.transform.parent = null;
    GetComponent.<Rigidbody>().useGravity = true; 
    }
