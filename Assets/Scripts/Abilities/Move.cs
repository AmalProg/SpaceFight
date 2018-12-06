using System;
using UnityEngine;

public class Move : Ability {

	public Move(Spaceship user) : base(user) {
		_cooldown = 0.0f;
		_cooldownTimer.SetResetTime (_cooldown);
	}

	override public void Exec () {
		float elapsedTime = Time.fixedDeltaTime;
		float verTranslation = Input.GetAxis ("Vertical") * _user._speed * elapsedTime;
		float horTranslation = Input.GetAxis("Horizontal") * _user._speed * elapsedTime;

		_userRB.AddForce (horTranslation, 0, verTranslation, ForceMode.VelocityChange);
	}

	override protected void InternalUse () {
		_onGoing = false;
	}
}