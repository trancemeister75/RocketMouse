using UnityEngine;
using System.Collections;

public class ParallaxScroll : MonoBehaviour {

	public Renderer background;
	public Renderer foreground;

	public float backgroundSpeed = 0.02f;
	public float foregorundSpeed = 0.06f;

	public float offset = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float backgroudOffset = offset * backgroundSpeed;
		float foregroundOffset = offset * foregorundSpeed;

		background.material.mainTextureOffset = new Vector2 (backgroudOffset,0);
		foreground.material.mainTextureOffset = new Vector2 (foregroundOffset,0);
	}
}
