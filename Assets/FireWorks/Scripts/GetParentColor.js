
function Update () {
	if(transform.parent){
		GetComponent.<Renderer>().material.SetColor("_TintColor", transform.parent.GetComponent.<Renderer>().material.GetColor("_TintColor"));
	}
}