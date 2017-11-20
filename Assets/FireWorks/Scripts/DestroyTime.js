var time : float;
function Start () {
	yield WaitForSeconds(time);
	GetComponent.<ParticleEmitter>().emit = false;
}