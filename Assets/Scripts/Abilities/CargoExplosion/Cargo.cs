using System;
using UnityEngine;

public class Cargo : Ability {

	private GameObject projectilePrefab;

	public Cargo(Spaceship user) : base(user) {
		_cooldown = 1.0f;
		_cooldownTimer.SetResetTime (_cooldown);
		projectilePrefab = Resources.Load ("Prefab/ProjectilePrefab") as GameObject;
	}

	override public void Exec () {
		if (_onGoing) {
			Vector2 directionVec2 = Tools.getLookingDirection ();
			directionVec2.Normalize ();
			Vector3 direction = new Vector3 (directionVec2.x, 0, directionVec2.y);

			GameObject cargoExploObj = UnityEngine.Object.Instantiate (projectilePrefab, 
				_user.transform.position + direction * (_user.transform.localScale.x + projectilePrefab.transform.localScale.x) / 1.5f, 
				Quaternion.LookRotation(direction));
			cargoExploObj.GetComponent<CargoExplosion> ().user = _user;

			_onGoing = false;
		}
	}

	override protected void InternalUse () {
		_onGoing = true;
	}
}

