var colors : Color[];
var fireWorks : Explosion[];
var smokeEffects : ParticleEmitter[];
var glowEffects : GameObject[];
var rocket : Rocket;
var smokeSettings : SmokeSettingsClass;
var glowGrow : float;
function ChangeColors () {
	for(i = 0; i < fireWorks.length; i ++){
		for(x = 0; x < colors.length; x ++){
			fireWorks[i].explosionColors[x] = colors[x];
		}
	}
	
	for(x = 0; x < colors.length; x ++){
			rocket.colors[x] = colors[x];
	}
}

function SmokeSettings () {
	for(i = 0; i < smokeEffects.length; i ++){
		smokeEffects[i].rndVelocity = smokeSettings.randomVelocity;
	}
	for(i = 0; i < glowEffects.length; i ++){
		glowEffects[i].GetComponent(ParticleAnimator).sizeGrow = glowGrow;
	}
}




class SmokeSettingsClass {
	var randomVelocity : Vector3;
}