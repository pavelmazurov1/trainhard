using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonUnitController : MonoBehaviour
{
    public CharacterController characterController;
    public Animator characterAnimator;

    public int moveState = 0;

    public float rotationSpeed = 1.0f;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float shiftAdditionalSpeed = 6.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 charLastPosition = Vector3.zero;

    enum MoveStateEnum
    {
        Idle = 0,
        Walk,
        Run,
        Sit,
        Crowl,
        Jump
    }

    // Start is called before the first frame update
    void Start()
    {
        var sceneDataObject = GameObject.FindGameObjectWithTag("SceneData");
        if (sceneDataObject)
        {
            sceneDataObject.GetComponent<PlayerData>().PlayerUnit = gameObject;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        charLastPosition = characterAnimator.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate

            moveDirection = transform.forward;
            moveDirection *= Input.GetAxis("Vertical");
            moveDirection += Input.GetAxis("Horizontal") * transform.right;
            moveDirection *= speed + Input.GetAxis("Shift") * shiftAdditionalSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        var newForwardVector = characterAnimator.gameObject.transform.position - charLastPosition ;
        newForwardVector.y = 0;
        if (newForwardVector.sqrMagnitude > 0.001)
            characterAnimator.gameObject.transform.forward = newForwardVector;

        var charVel = characterController.velocity.sqrMagnitude;

        var newMoveState = MoveStateEnum.Idle;
        if (!characterController.isGrounded)
        {
            newMoveState = MoveStateEnum.Jump;
        } 
        else if (Input.GetButton("Sit"))
        {
            newMoveState = MoveStateEnum.Sit;
            if (charVel > 0.001)
            {
                newMoveState = MoveStateEnum.Crowl;
            }
        } 
        else
        {
            if (charVel > 0.001)
            {
                if (Input.GetButton("Shift"))
                {
                    newMoveState = MoveStateEnum.Run;
                }
                else
                {
                    newMoveState = MoveStateEnum.Walk;
                }
            }
        }
        
        setMoveState((int)newMoveState);
        
        charLastPosition = characterAnimator.gameObject.transform.position;
    }

    void setMoveState(int newMoveState)
    {
        if(moveState != newMoveState)
        {
            moveState = newMoveState;
            Debug.Log("newMoveState " + newMoveState);
            characterAnimator.SetInteger("moveState", moveState);
        }
    }
}
