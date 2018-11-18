using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	public GameObject lifeTextObj;
	private Text lifeText;
	private string baseLifeText;

	void Start ()
	{
		lifeText = lifeTextObj.GetComponent<Text> ();

		baseLifeText = lifeText.text;
	}

	public void UpdateLife(int life) {
		lifeText.text = baseLifeText.Replace (",", life.ToString());
	}
}
