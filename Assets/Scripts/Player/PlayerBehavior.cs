using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
	//Variables


	//player controller
	[SerializeField] private CharacterController controller;
	[SerializeField] private GameObject weapon;

	//Movement
	[SerializeField] private float normalSpeed = 8f;
    private float speed;
	
	//Jump variables
	[SerializeField] private float gravity = -9.81f;
	[SerializeField] private Vector3 velocity;
	[SerializeField] private float jumpHeight = 3f;

	//ground check variables
	[SerializeField] private Transform groundCheck;
	private float groundDistance = 0.4f; //radius of groundCheck
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private LayerMask environmentMask;
	bool isGrounded;
    
    //Inputs
	private PlayerActionControls playerActionControls;

	//-----------------------------------------------------------------------------
	//Inputs

	// create the input manager before starting
	private void Awake() {
		playerActionControls = new PlayerActionControls();
	}

	//Enable the input manager
	private void OnEnable() {
		playerActionControls.Enable();
	}

	//Disable the input manager
	private void OnDisable() {
		playerActionControls.Disable();
	}

	void Start() {
		playerActionControls.Player.Jump.performed += _ => Jump();
		speed = normalSpeed;
	}

	//------------------------------------------------------------------------------------------
	//Movements

	//Make the player jump
	private void Jump() {
		if (isGrounded) {
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}
	}

    // Update is called once per frame
  	void Update()
  	{
		//check if the player touch the ground
		isGrounded = (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || Physics.CheckSphere(groundCheck.position, groundDistance, environmentMask));

		//change the velocity if the player is grounded and if his velocity is < 0
		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}

		if (playerActionControls.Player.Sprint.ReadValue<float>() > 0){
			speed = 15;
		}
		else if (speed > 10){
			speed = normalSpeed;
		}

		// Read the movement values
        Vector2 movement = playerActionControls.Player.Move.ReadValue<Vector2>();

        //create a vector3 to move the player
		Vector3 move = transform.right * movement.x + transform.forward * movement.y;
		Vector3 moveWeapon = weapon.transform.right * movement.x + weapon.transform.forward * movement.y;

		//move the player using the Vector3 move
		controller.Move(move * Time.deltaTime * speed);

		//Change velocity (for gravity)
		velocity.y += gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);
		weapon.transform.position = moveWeapon * Time.deltaTime * speed * Time.deltaTime;

		//---------------------------------------------------------------------------------------------
  	}
}
