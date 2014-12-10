using UnityEngine;
using System.Collections;

public class RandomSpawnScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (Random.Range (-5.0f, 5.0f), 1.05f, Random.Range (-5.0f, 5.0f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
