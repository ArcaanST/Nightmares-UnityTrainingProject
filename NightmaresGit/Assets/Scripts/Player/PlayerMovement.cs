using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	InputMaster controls;
	
	public float speed = 6f;
	Vector2 rotate;
	
	Vector3 movement;

	Animator anim;
	
	Rigidbody playerRigidbody;
	
	int floorMask;
	
	float camRayLength = 100f;

	void Awake()
	{
		controls = new InputMaster();
		floorMask = LayerMask.GetMask("Floor");

		controls.PlayerControls.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
		controls.PlayerControls.Move.canceled += ctx => rotate = Vector2.zero;
		
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

	
		Move(h, v);

		Vector2 r = new Vector2(-rotate.y, -rotate.x) * 100 * Time.deltaTime;
		this.transform.Rotate(r, Space.World);
		Turning();


		Animating(h, v);
	}

	void Move(float h, float v)
	{
		
		movement.Set(h, 0f, v);

		
		movement = movement.normalized * speed * Time.deltaTime;

		
		playerRigidbody.MovePosition(transform.position + movement);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		
		RaycastHit floorHit;

		
		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
		{

			Vector3 playerToMouse = floorHit.point - transform.position;

			
			playerToMouse.y = 0f;

		
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

		
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	void Animating(float h, float v)
	{
		
		bool walking = h != 0f || v != 0f;

		
		anim.SetBool("IsWalking", walking);
	}
}
