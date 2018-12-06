using System;
using UnityEngine;

static class Tools {
	public static Vector2 getLookingDirection () {
		Vector3 mousePos = Input.mousePosition;
		Vector2 centerPos = new Vector2 (Screen.width / 2, Screen.height / 2);
		return new Vector2(mousePos.x - centerPos.x, mousePos.y - centerPos.y);
	}
}