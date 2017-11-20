var speed : float;
private var randomx : float;
private var randomy : float;
private var randomz : float;
var useYrandom = true;
function Start () {
	InvokeRepeating("NewDir", .3,.3);
}

function NewDir () {
	randomx = Random.Range(-speed, speed);
	if(useYrandom){
		randomy = Random.Range(-speed, speed);
	}
	randomz = Random.Range(-speed, speed);
}

function Update () {
	transform.Translate(Vector3(randomx, randomy, randomz)*Time.deltaTime);
}