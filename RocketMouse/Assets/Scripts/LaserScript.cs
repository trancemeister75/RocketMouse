using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	public Sprite laserOnSprite;
	public Sprite laserOffSprite;

	public float interval = 0.5f;
	public float rotationSpeed = 0.0f;

	public bool isLaserOn = true;
	private float timeUntilNextToogle;

	Renderer renderer;

	BoxCollider2D collider;

	// Use this for initialization
	void Start () {
		timeUntilNextToogle = interval;
		collider = GetComponent<BoxCollider2D>();
		renderer = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timeUntilNextToogle -= Time.deltaTime;
		if(timeUntilNextToogle <= 0)
		{
			isLaserOn = !isLaserOn;

			collider.enabled = isLaserOn;

			SpriteRenderer spriteRenderer = ((SpriteRenderer)renderer);
			if(isLaserOn){
				spriteRenderer.sprite = laserOnSprite;
			}
			else{
				spriteRenderer.sprite = laserOffSprite;
			}
			timeUntilNextToogle = interval;
		}
		transform.RotateAround (transform.position, Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
	}
}
