var force : float;
var smokeTrail : Transform;
var timeMin : float;
var timeMax : float;
var explosions : Transform[];
var colors : Color[];
var glow : ParticleEmitter;

function Update () {
	GetComponent.<Rigidbody>().drag += .3*Time.deltaTime;
	GetComponent.<Rigidbody>().AddForce(0,force,0);
}

function Start () {
	GetComponent.<Renderer>().material.SetColor("_TintColor", colors[Random.value*colors.length]);
	GetComponent.<AudioSource>().time = Random.Range(.5, 3);
	yield WaitForSeconds(Random.Range(timeMin, timeMax));
	var copy = Instantiate(explosions[Random.value*explosions.length], transform.position, transform.rotation);
	copy.transform.parent = gameObject.Find("RocketLauncher").transform;
	if(glow){
		Destroy(glow.gameObject);
	}
	GetComponent.<ParticleEmitter>().emit = false;
	smokeTrail.GetComponent.<ParticleEmitter>().emit = false;
	smokeTrail.parent = null;
}

