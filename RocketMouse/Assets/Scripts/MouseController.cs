using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {
	public float jetpackforce = 75.0f;
	Rigidbody2D body;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		bool jetpackactive = Input.GetButton ("Fire1");
		if(jetpackactive)
		{
			body.AddForce(new Vector2(0,jetpackforce));
		}
	}
}
