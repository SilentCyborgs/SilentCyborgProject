using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
	public float Speed = 5.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Speed*Time.deltaTime,0,0);
	}
}
