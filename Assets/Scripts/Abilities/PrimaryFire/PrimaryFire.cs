using System;
using System.Timers;
using UnityEngine;

public class PrimaryFire : Ability {

	private GameObject primaryFirePrefab;

	public PrimaryFire(Spaceship user) : base(user) {
		_cooldown = 0.5f;
		_cooldownTimer.SetResetTime (_cooldown);
		_cooldownTimer.AddTime (_cooldown);
		primaryFirePrefab = Resources.Load ("Prefab/PrimaryFirePrefab") as GameObject;
	}

	override public void Exec () {
		if (_onGoing) {
			_onGoing = false;

			Vector2 directionVec2 = Tools.getLookingDirection ();
			directionVec2.Normalize ();
			Vector3 direction = new Vector3 (directionVec2.x, 0, directionVec2.y);

			GameObject projectileObj = UnityEngine.Object.Instantiate (primaryFirePrefab, 
				_user.transform.position + direction * (_user.transform.localScale.x + primaryFirePrefab.transform.localScale.x) / 1.5f, 
				Quaternion.LookRotation(direction));
			projectileObj.GetComponentInChildren<LinearProjectile> ().user = _user;

			_onGoing = false;
		}
	}

	override protected void InternalUse () {
		_onGoing = true;
	}
}