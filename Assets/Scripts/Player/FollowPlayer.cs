using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //Variables

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(5, 0, 0);
    [SerializeField] float rotationSpeed = 15f;

    float xRotation = 0f;

    public static Quaternion weaponRotation;

    //Input manager
    PlayerActionControls playerActionControls;
    // Start is called before the first frame update
    void Awake()
    {
        playerActionControls = new PlayerActionControls();
    }

    void Start(){
        //Lock the cursor (make the cursor disappear)
        Cursor.lockState = CursorLockMode.Locked;
    }


    //--------------------------------------------
    //Input functions
    private void OnEnable(){
        playerActionControls.Enable();
    }

    private void OnDisable(){
        playerActionControls.Disable();
    }
    //--------------------------------------------

    // Update is called once per frame
    void Update()
    {
        //Mouse input
        Vector2 look = playerActionControls.Player.Look.ReadValue<Vector2>();

        //x rotation for the camera
        xRotation -= look.y * rotationSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //apply x rotation for camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //y rotation for player
        player.transform.Rotate(Vector3.up * look.x * rotationSpeed * Time.deltaTime);

        //weapon variable
        weaponRotation = transform.localRotation;
    }
    
}
