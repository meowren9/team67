var glow : Renderer;
var explosionColors : Color[];
var trail : Transform;
var trails : int;
var trailVelocity : float;
var useTrailColor = true;
var isBasicExplosion = true;
var explosionDetail = false;
var crackling = false;
var cracklingEffect : Transform;
function Start () {
	if(GetComponent.<AudioSource>()){
		GetComponent.<AudioSource>().pitch = Random.Range(.8, 1.2);
	}
	var randomMaterial = explosionColors[Random.value*explosionColors.length];
	if(glow){
			glow.material.SetColor("_TintColor", Color(randomMaterial.r, randomMaterial.g, randomMaterial.b, 0.1));
	}
	if(explosionDetail){
		GetComponent.<Renderer>().material.SetColor("_TintColor", randomMaterial);
	}

	if(isBasicExplosion == true){
		GetComponent.<Renderer>().material.SetColor("_TintColor", randomMaterial);
	}
	else{
		for(i = 0; i < trails; i++){
			var copy = Instantiate(trail,transform.position, transform.rotation);
			copy.transform.parent = gameObject.Find("RocketLauncher").transform;
			if(useTrailColor){
				copy.gameObject.GetComponent("Gravity").col = randomMaterial;
			}
			var randomx = Random.Range(-trailVelocity, trailVelocity);
			var randomy = Random.Range(-trailVelocity, trailVelocity);
			var randomz = Random.Range(-trailVelocity, trailVelocity);
			copy.GetComponent.<Rigidbody>().velocity = Vector3(randomx, randomy ,randomz);
		}
	}
	if(crackling){
		yield WaitForSeconds(.65);
		var crackle = Instantiate(cracklingEffect,transform.position, transform.rotation);
		crackle.GetComponent.<Renderer>().material.SetColor("_TintColor", GetComponent.<Renderer>().material.GetColor("_TintColor"));
	}
	
}