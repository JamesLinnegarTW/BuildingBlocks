using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {
	private Light light;
	public GameObject spotLight;
	public GameObject lamp;
	// Use this for initialization
	void Start () {

		light = spotLight.GetComponent<Light>();
		light.intensity = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.value > 0.7) {
			float lighting = Random.value;

			if (light) {
				light.intensity = lighting;
			}
			if (lamp) {
				Renderer renderer = lamp.GetComponent<Renderer> ();
				Material mat = renderer.material;

				Color baseColor = Color.yellow; //Replace this with whatever you want for your base color at emission level '1'

				Color finalColor = baseColor * Mathf.LinearToGammaSpace (lighting);

				mat.SetColor ("_EmissionColor", finalColor);
			}
		}
	}
}
