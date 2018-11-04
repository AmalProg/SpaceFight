using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Spaceship focus { get { return _focus; } set { _focus = value; } }

	public float _height;
	private Spaceship _focus;

	// Use this for initialization
	void Start () {
		_height = 15;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3 (_focus.transform.position.x, _height, _focus.transform.position.z);
	}
}
