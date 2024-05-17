using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] LayerMask groundLayer;

    public float mouseSenstitivity = 100f;
    public float xRotation = 0f;
    public Vector3 jumpVelocity=Vector3.zero;
    public bool isWalking = false;
    public bool isjumping = false;
    public bool isGrounded = false;
    public bool isCrouching=false;
    public PlayerScriptable player;

    private CharacterController cc;
    public Vector3 move = Vector3.zero;
    public Vector3 inputVector=Vector3.zero;
    void Start()
    {
        cc=GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, 0.4f, groundLayer);
        HandileJumpInput();
        HandileMovement();
        HandleMouseLOOK();

    }
    void HandileJumpInput()
    {
        bool isTringToJump=Input.GetKeyDown(KeyCode.Space);

        if (isTringToJump&&isGrounded)
        {
            isjumping = true;
        }
        else
        {
            isjumping = false;
        }

        if (isGrounded && jumpVelocity.y < 0f)
        {
            jumpVelocity.y = -2f;
        }

        if (isjumping)
        {
            jumpVelocity.y = Mathf.Sqrt(player.jumpHight * -2f * player.gravity);
        }jumpVelocity.y += player.gravity * Time.deltaTime;

        cc.Move(jumpVelocity * Time.deltaTime);
    }
    void HandileMovement() 
    {
      inputVector.z = Input.GetAxis("Vertical");
        inputVector.x = Input.GetAxis("Horizontal");
        isWalking = Input.GetKey(KeyCode.LeftShift);
         isCrouching=Input.GetKey(KeyCode.LeftControl);


        if (isCrouching)
        {
            HandileCrouch();
        }
        else
        {
            HandileStand();
        }
         move= Vector3.ClampMagnitude(transform.right*inputVector.x+transform.forward*inputVector.z,1.0f);


        if (isCrouching)
        {

            cc.Move(move * player.crouchingMovementSpeed * Time.deltaTime);
        }
        else if(isWalking){


            cc.Move(move * player.walkindMovementSpeed * Time.deltaTime);
        }else
        {

            cc.Move(move * player.runningMovementSpeed * Time.deltaTime);
        }
        
    }

    void HandleMouseLOOK()
    {
        float mouseX = Input.GetAxis("Mouse X")*mouseSenstitivity*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")*mouseSenstitivity*Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation=Quaternion.Euler(xRotation,0,0);
        transform.Rotate(Vector3.up*mouseX);
    }

    void HandileCrouch()
    {
        if (cc.height > player.crouchHeightY)
        {
           updateCharactorHeght(player.crouchHeightY);
            if (cc.height - 0.05f <= player.crouchHeightY)
            {
                cc.height = player.crouchHeightY;
            }
        }
       
    }
    void HandileStand()
    {
        if(cc.height<player.standingHeightY)
        {
            float lastHeight = cc.height;


            RaycastHit hit;
            if(Physics.Raycast(transform.position,transform.up,out hit, player.standingHeightY))
            {
                if (hit.distance < player.standingHeightY - player.crouchHeightY)
                {
                    updateCharactorHeght(player.crouchHeightY+hit.distance);
                    return;
                }
                else
                {
                    updateCharactorHeght(player.standingHeightY);
                }
            }
            else
            {
                updateCharactorHeght(player.standingHeightY);
            }
     
            if (cc.height + 0.05f >= player.standingHeightY)
            {
                cc.height = player.standingHeightY;
            }
            transform.position += new Vector3(0, (cc.height - lastHeight) / 2, 0);
        }
    }
    void updateCharactorHeght(float newHeight)
    {
        cc.height = Mathf.Lerp(cc.height, newHeight, player.crouchSpeed * Time.deltaTime);

    }
}
