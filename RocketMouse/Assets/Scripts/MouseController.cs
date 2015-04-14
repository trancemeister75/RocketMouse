using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {
	public float jetpackforce = 75.0f;
	public float forwardMovementSpeed = 3.0f;
	public Transform groundCheckTransform;
	private bool grounded;
	private bool dead = false;
	public LayerMask groundCheckLayerMask;
	Animator animator;
	Rigidbody2D body;
	public ParticleSystem jetpack;
	private uint coins = 0;
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
		jetpackactive = jetpackactive && !dead;
		if(jetpackactive)
		{
			body.AddForce(new Vector2(0,jetpackforce));
		}
		if (!dead) {
			Vector2 newVelocity = body.velocity;
			newVelocity.x = forwardMovementSpeed;
			body.velocity = newVelocity;
		}
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

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.CompareTag("Coins"))
		{
			CollectCoin(collider);
		}
		else{
		HitByLaser (collider);
		}
	}

	void HitByLaser(Collider2D collider)
	{
		dead = true;
		animator.SetBool ("dead",true);
	}

	void CollectCoin(Collider2D collider)
	{
		coins++;
		Destroy (collider.gameObject);
	}
}
