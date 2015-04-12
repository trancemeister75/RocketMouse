using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {
	public float jetpackforce = 75.0f;
	public float forwardMovementSpeed = 3.0f;
	public Transform groundCheckTransform;
	private bool grounded;
	public LayerMask groundCheckLayerMask;
	Animator animator;
	Rigidbody2D body;
	public ParticleSystem jetpack;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
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
		Vector2 newVelocity = body.velocity;
		newVelocity.x = forwardMovementSpeed;
		body.velocity = newVelocity;
		updateGroundedStatus ();
		AdjustJetpack (jetpackactive);
	}

	void updateGroundedStatus()
	{
		grounded = Physics2D.OverlapCircle (groundCheckTransform.position, 0.1f, groundCheckLayerMask);
		animator.SetBool ("grounded", grounded);
	}

	void AdjustJetpack(bool jetpackActive)
	{
		jetpack.enableEmission = !grounded;
		jetpack.emissionRate = jetpackActive ? 300.0f : 75.0f;
	}
}
