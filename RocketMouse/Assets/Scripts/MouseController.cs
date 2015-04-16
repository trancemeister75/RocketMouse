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
	public Texture2D coinIconTexture;
	public AudioClip coinCollectSound;
	public AudioSource jetpackAudio;
	public AudioSource footstepsAudio;
	public ParallaxScroll parallax;
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
		AdjustFootstepsAndJetpackSound(jetpackactive);
		parallax.offset = transform.position.x;
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
		if (!dead) {
			collider.gameObject.GetComponent<AudioSource>().Play();
		}
		dead = true;
		animator.SetBool ("dead",true);

	}

	void CollectCoin(Collider2D collider)
	{
		coins++;
		Destroy (collider.gameObject);
		AudioSource.PlayClipAtPoint (coinCollectSound, transform.position);
	}

	void DisplayCoinsCount()
	{
		Rect coinIconRect = new Rect (10,10,32,32);
		GUI.DrawTexture (coinIconRect, coinIconTexture);

		GUIStyle style = new GUIStyle ();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.yellow;

		Rect labelRect = new Rect (coinIconRect.xMax,coinIconRect.y,60,32);
		GUI.Label (labelRect, coins.ToString(),style);
	}

	void OnGUI()
	{
		DisplayCoinsCount ();
		DisplayRestartButton ();
	}

	void DisplayRestartButton()
	{
		if (dead && grounded) {
			Rect buttonRect = new Rect(Screen.width * 0.35f,Screen.height * 0.45f,Screen.width * 0.30f,Screen.height * 0.1f);
			if(GUI.Button(buttonRect,"Tap to Restart!!"))
			{
				Application.LoadLevel(Application.loadedLevelName);
			}
		}
	}

	void AdjustFootstepsAndJetpackSound(bool jetpackActive)    
	{
		footstepsAudio.enabled = !dead && grounded;
		
		jetpackAudio.enabled =  !dead && !grounded;
		jetpackAudio.volume = jetpackActive ? 1.0f : 0.5f;        
	}
}
