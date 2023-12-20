using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    private float horizontalMove;
    private float verticalMove;
    private Vector3 playerInput;
    public CharacterController player;

    public float playerSpeed = 5;   
    public float gravity = 9.8f;    
    public float jumpForce = 5;
    public float slideVelocity = 7;
    public float slopeForceDown = -10;

    private float fallVelocity;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    

    // Start is called before the first frame update
    void Start(){
        player = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update(){
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        CamDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);

    }

    private void FixedUpdate() {
        

    }

    void CamDirection() {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;
        
        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized; 
    }

    void PlayerSkills() {
        if (player.isGrounded && Input.GetButtonDown("Jump")){
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }
    
    void SetGravity() {      
        if (player.isGrounded) {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        SlideDown();
    }

    public void SlideDown() {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;

        if (isOnSlope ) {
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;

            movePlayer.y += slopeForceDown;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        hitNormal = hit.normal;
    }
}
