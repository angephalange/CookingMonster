
var scrollSpeed_X = 0.1;
var scrollSpeed_Y = 0.1;
function Update() {
var offsetX = Time.time * scrollSpeed_X;
var offsetY = Time.time * scrollSpeed_Y;
GetComponent.<Renderer>().material.mainTextureOffset = Vector2 (offsetX,offsetY);
}