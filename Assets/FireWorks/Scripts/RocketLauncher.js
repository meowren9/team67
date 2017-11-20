var rocket : Transform;
var timeMin : float = .2;
var timeMax : float = 1;
var randomxz : float;
var speed : float;
var speedRandom : float;
var stop = false;
private var time : float;
private var nextfire : float;
function Update () {
	InvokeRepeating("Launch", time, time);
}

function Launch () {
	if(stop == true){
		return;
	}
	time = Random.Range(timeMin, timeMax);
	
	if(Time.time>nextfire){
		nextfire = Time.time+Random.Range(timeMin,timeMax);
	
		var copy = Instantiate(rocket, transform.position,transform.rotation);
		copy.transform.parent = transform;
		var randomx : float = Random.Range(-randomxz,randomxz);
		var randomz : float = Random.Range(-randomxz,randomxz);

		copy.GetComponent.<Rigidbody>().velocity = transform.TransformDirection(randomx,speed+Random.Range(-speedRandom,speedRandom),randomz);
	}

}