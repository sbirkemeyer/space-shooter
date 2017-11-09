using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour 
{
	public float speed;

	void Start () 
	{
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}

	public void moreSpeed()
	{
		speed --;
	}
}
