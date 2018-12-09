using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;
using UnityEngine;

public class GameController : MonoBehaviour {

	static public GameObject lifeUICanvas;

	public GameObject playerObj;
	public GameObject abilitiesUIObj;

	private CameraController _camera;
	private Spaceship player;

	private AbilitiesUI abilitiesUI;

	void Awake() {
		lifeUICanvas = GameObject.Find ("LifeUI");
		abilitiesUIObj = GameObject.Find ("AbilitiesUI");
		_camera = GameObject.Find ("Main Camera").GetComponent<CameraController>();
	}

	// Use this for initialization
	void Start () {
		player = playerObj.GetComponent<Spaceship> ();
		_camera.focus = player;

		abilitiesUI = abilitiesUIObj.GetComponent<AbilitiesUI> ();
		abilitiesUI.abilities = player.abilities;
		abilitiesUI.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		float elaspedTime = Time.deltaTime;

		if (player.isDead)
			EndGame ();
	}

	void FixedUpdate() {
		Timer.FixedUpdate (Time.fixedDeltaTime);
	}

	public void EndGame() {
	}
}
