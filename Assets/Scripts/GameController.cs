using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;
using UnityEngine;

public class GameController : MonoBehaviour {

	static public GameObject lifeUICanvas;

	public GameObject playerObj;
	public GameObject _cameraObj;
	public GameObject gameUIObj;

	private CameraController _camera;
	private Spaceship player;

	private GameUI gameUI;

	void Awake() {
		lifeUICanvas = GameObject.Find ("LifeUI");
		player = playerObj.GetComponent<Spaceship> ();
		_camera = _cameraObj.GetComponent<CameraController> ();
	}

	// Use this for initialization
	void Start () {
		_camera.focus = player;
		/*gameUI = gameUIObj.GetComponent<GameUI> ();

		gameUI.UpdateLife (player.life);*/
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
