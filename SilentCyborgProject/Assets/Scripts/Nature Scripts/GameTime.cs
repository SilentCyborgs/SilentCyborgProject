using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	public enum TimeOfDay {
		Idle,
		Sunrise,
		Sunset
		}
	public Transform[] sun;

	private Sun[] _Sunscript;

	public float Sunrise;					//The time of day we have sunrise.
	public float Sunset;					//The time of day we start the sunset.
	public float SkyBoxBlendModifier; 		//Speed at which the textures blend.

	public float dayCycleInMinutes = 1;
	private float dayCycleInSeconds;

	private const float Second = 1;
	private const float Minute = 60 * Second;
	private const float Hour = 60 * Minute;
	private const float Day = 24 * Hour;

	private const float Degree_Per_Second = 360 / Day;

	private float _degreeRotation;

	private float _timeOfDay;
	private TimeOfDay _TOD;
	private float _NoonTime; 		//This is the time of noon

	private float _MorningLength;
	private float _EveningLength;


	// Use this for initialization
	void Start () {
		_TOD = TimeOfDay.Idle;

		dayCycleInSeconds = dayCycleInMinutes * Minute;

		RenderSettings.skybox.SetFloat ("_Blend", 0);

		_Sunscript = new Sun[sun.Length];

		for (int cnt = 0; cnt < sun.Length; cnt++){
			Sun temp = sun[cnt].GetComponent<Sun>();

			if(temp == null){
				sun[cnt].gameObject.AddComponent<Sun>();
				temp = sun[cnt].GetComponent<Sun>();
			}
			_Sunscript[cnt] = temp;
		}

		_timeOfDay = 0;
		_degreeRotation = Degree_Per_Second * Day / (dayCycleInSeconds);
	
		Sunrise *= dayCycleInSeconds;
		Sunset *= dayCycleInSeconds;
		_NoonTime = dayCycleInSeconds / 2;

		_MorningLength = _NoonTime - Sunrise;
		//_EveningLength = (Sunset - _NoonTime);

		SetupLighting (); 
	}
	
	// Update is called once per frame
	void Update () {

		for(int cnt = 0; cnt < sun.Length; cnt++)
			sun[cnt].Rotate(new Vector3(_degreeRotation,0,0) * Time.deltaTime);
		if (_timeOfDay > Sunrise && _timeOfDay < _NoonTime) {
			AdjustLighting (true);
				} else if (_timeOfDay > _NoonTime && _timeOfDay < Sunset) {
			AdjustLighting (false);
				}
		_timeOfDay += Time.deltaTime;

		if (_timeOfDay > dayCycleInSeconds)
						_timeOfDay -= dayCycleInSeconds;

//		Debug.Log (_timeOfDay); 

		if (_timeOfDay > Sunrise && _timeOfDay < Sunset && RenderSettings.skybox.GetFloat ("_Blend") < 1) {
						_TOD = GameTime.TimeOfDay.Sunrise;
						BlendSkyBox ();
				} else if (_timeOfDay > Sunset && RenderSettings.skybox.GetFloat ("_Blend") > 0) {
						_TOD = GameTime.TimeOfDay.Sunset;
						BlendSkyBox ();
				} else {
			_TOD = GameTime.TimeOfDay.Idle;	
		}
	}

	 private void BlendSkyBox() {
			float temp = 0;

			switch(_TOD){
			case TimeOfDay.Sunrise:
				temp = (_timeOfDay - Sunrise) / dayCycleInSeconds * SkyBoxBlendModifier ;
				break;
			case TimeOfDay.Sunset:
				temp = (_timeOfDay - Sunset) / dayCycleInSeconds * SkyBoxBlendModifier ;
				temp = 1-temp;
				break;
			}

		RenderSettings.skybox.SetFloat ("_Blend", temp);

//		Debug.Log (temp);
		}

	private void SetupLighting(){
		for (int cnt = 0; cnt < _Sunscript.Length; cnt++) {
			if(_Sunscript[cnt].GiveLight){
				sun[cnt].GetComponent<Light>().intensity = _Sunscript[cnt].MinLightBrightness; 
				}	
			}
		}
	private void AdjustLighting(bool brighten){
		if (brighten) {
			float pos = (_timeOfDay - Sunrise) / _MorningLength;
			for(int cnt = 0; cnt < _Sunscript.Length; cnt++){
				if(_Sunscript[cnt].GiveLight){
					Debug.Log (pos);
					_Sunscript[cnt].GetComponent<Light>().intensity = _Sunscript[cnt].MaxLightBrightness * pos;
				}
				}
		} //else { 
			//float pos = (Sunset - _timeOfDay) / _EveningLength;
			//for(int cnt = 0; cnt < _Sunscript.Length; cnt++){
				//if(_Sunscript[cnt].GiveLight){
				//	Debug.Log (pos);
					//_Sunscript[cnt].GetComponent<Light>().intensity = _Sunscript[cnt].MaxLightBrightness * pos;
				//}
			//}
				//}
						
		}

	}
