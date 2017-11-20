private var flyMode = false;
var speed : float;
var rocketLauncher : RocketLauncher;
var blackScreen : GUITexture;
var finaleOver = false;
private var pos : Vector3;
private var rot : Quaternion;
function OnGUI () {
	if(finaleOver == false){
		if(GUI.Button(Rect((1/Screen.width)+20, Screen.height-40, 100, 20), "Fly Mode")){
			flyMode = !flyMode;
		}
		
		if(GUI.Button(Rect((1/Screen.width)+260, Screen.height-40, 100, 20), "Quit App")){
			Application.Quit();
		}

		if(GUI.Button(Rect((1/Screen.width)+140, Screen.height-40, 100, 20), "Start Finale")){
			rocketLauncher.timeMin = .1;
			rocketLauncher.timeMax = .2;
			CountDown();
		}

		if(flyMode == true||Input.GetKeyDown("f")){
			GUI.Label(Rect(Screen.width/2,(1/Screen.height/2)+50, 100, 100), "Press F to exit/begin FlyMode!");
		}
	}
}

function CountDown () {
	yield WaitForSeconds(15);
	rocketLauncher.stop = true;
	yield WaitForSeconds(5);
	finaleOver = true;
}

function Update () {	
	if(blackScreen.color.a >= .5){
		Application.Quit();
	}
	if(finaleOver){
		blackScreen.color.a += 0.1*Time.deltaTime;
		GetComponent.<AudioSource>().volume -= 0.02*Time.deltaTime;
	}
	if(flyMode == true){
		transform.Translate(Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"))*Time.deltaTime*speed);
		gameObject.GetComponent("MouseLook").enabled = true;
		Screen.lockCursor = true;
	}
	else{
		Screen.lockCursor = false;
		gameObject.GetComponent("MouseLook").enabled = false;
		transform.position = pos;
		transform.rotation = rot;
	}
	if(Input.GetKeyDown("f")){
		flyMode = !flyMode;
	}
}

function Start () {
	pos = transform.position;
	rot = transform.rotation;
}