using System;
using UnityEngine;

public class AbilityMonoBehaviour : MonoBehaviour
{
	public Spaceship user { get { return _user; } set { _user = value; } }

	protected Spaceship _user;
}

