using UnityEngine;
using System.Collections;

public class RandomSpawnScript : MonoBehaviour {
	public float XRandomMin = -5.0f;
	public float XRandomMax = 5.0f;
	public float ZRandomMin = -5.0f;
	public float ZRandomMax = 5.0f;
	private bool PlayerSpawned = false; 
	// Use this for initialization
	void Start () {
		if (PlayerSpawned != true) {
						float XPosition = Random.Range (XRandomMin, XRandomMax);
						float ZPosition = Random.Range (ZRandomMin, ZRandomMax);
						Vector3 PlayerPosition = new Vector3 (XPosition, 0f, ZPosition);
						PlayerPosition.y = Terrain.activeTerrain.SampleHeight (PlayerPosition) + Terrain.activeTerrain.GetPosition ().y;
						PlayerPosition.y += 1.2f;
						GameObject Player = Instantiate (Resources.Load ("Prefabs/Player"), PlayerPosition, Quaternion.identity) as GameObject;
						PlayerSpawned = true;
				}
	}
	// Update is called once per frame
	void Update () {}
}