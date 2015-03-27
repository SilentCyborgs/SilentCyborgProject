using UnityEngine;
using System.Collections;

[AddComponentMenu ("Environment/Sun")]

public class Sun : MonoBehaviour {

	public float MaxLightBrightness;
	public float MinLightBrightness;

	public float MaxFlareBrightness;
	public float MinFlareBrightness;

	public bool GiveLight = false;

	void Start() {
		if (GetComponent<Light> () != null)
						GiveLight = true;
	}

}
