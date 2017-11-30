using System.Collections;
using UnityEngine;

public class Rotater : MonoBehaviour {
	
	public float tumble;

	void Start ()
	{
		GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitSphere * tumble;
	}
}
