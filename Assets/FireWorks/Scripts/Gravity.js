var force : float;
var time : float;
var useColors = true;
var addForceAmount : float = 10;
private var startDrag = false;
private var col : Color = Color.white;
private var colBackup : Color;
function Update () {
	if(GetComponent.<Rigidbody>()){
		GetComponent.<Rigidbody>().AddForce(0,force,0);
	}
	if(startDrag == true){
		GetComponent.<Renderer>().material.SetColor("_TintColor",Color.Lerp(GetComponent.<Renderer>().material.GetColor("_TintColor"), Color.black, Time.deltaTime*3.5));
		GetComponent.<Rigidbody>().drag += 0.15;
		force -= addForceAmount;
		if(GetComponent.<Renderer>().material.GetColor("_TintColor").r <= 0.01){
			Destroy(gameObject);
		}
	}
}
function Start () {
	if(useColors){
		colBackup = col;
		GetComponent.<Renderer>().material.SetColor("_TintColor",col);
	}
	yield WaitForSeconds(time);
	startDrag = true;
	var smoke = transform.Find("Smoke");
	smoke.parent = null;
	smoke.GetComponent.<ParticleEmitter>().emit = false;
}