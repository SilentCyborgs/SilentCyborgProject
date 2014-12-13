using UnityEngine;
using System.Collections;

public class RandomSpawnScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
Public var XRangeMin = -5.0f
Public var XRangeMax = 5.0f
Public var ZRangeMin = -5.0f
Public var ZRangeMax = 5.0f
		transform.position = new Vector3 (Random.Range (XRangeMin, XRangeMax), 1.05f, Random.Range (ZRangeMin, ZRangeMax));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
