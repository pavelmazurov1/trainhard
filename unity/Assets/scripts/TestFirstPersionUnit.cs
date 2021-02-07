using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFirstPersionUnit : MonoBehaviour
{
    CharacterController characterController;
    public Camera characterCamera;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float shiftSpeedMultiplyer = 2.0f;
    public float gravity = 20.0f;
    public float lookSensivity = 1.0f;

    public Vector3 moveDirection = Vector3.zero;

    private Coroutine currentMoveCoroutine;
    private UnitState unitState;

    // Start is called before the first frame update
    void Start()
    {
        var sceneDataObject = GameObject.FindGameObjectWithTag("SceneData");
        if(sceneDataObject)
        {
            sceneDataObject.GetComponent<PlayerData>().PlayerUnit = gameObject;
        }

        characterController = GetComponent<CharacterController>();

        if(unitState == null)
            unitState = GetComponent<UnitState>();

        currentMoveCoroutine = StartCoroutine(basicMoveState());

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    bool anyMoveInput(float verticalAxis, float horizontalAxis)
    {
        return !(Mathf.Approximately(verticalAxis, Mathf.Epsilon) && Mathf.Approximately(horizontalAxis, Mathf.Epsilon));
    }
    
    IEnumerator basicMoveState()
    {
        float fallTime = 0;

        while (true)
        {
            if (characterController.isGrounded)
            {
                fallTime = 0;
                var verticalAxis = Input.GetAxis("Vertical");
                var horizontalAxis = Input.GetAxis("Horizontal");
                var jumpAction = Input.GetButton("Jump");
                if(!anyMoveInput(verticalAxis, horizontalAxis) && !jumpAction)
                {
                    unitState.moveState = (int)UnitState.MoveStateEnum.Idle;
                    moveDirection = Vector3.zero;
                } 
                else
                {
                    moveDirection = transform.forward;
                    moveDirection *= verticalAxis;
                    moveDirection += horizontalAxis * transform.right;
                    moveDirection *= speed;

                    if (jumpAction)
                    {
                        unitState.moveState = (int)UnitState.MoveStateEnum.Jump;
                        if (Input.GetButton("Shift")) moveDirection *= shiftSpeedMultiplyer;
                        moveDirection.y = jumpSpeed;
                    } 
                    else if (Input.GetButton("Shift"))
                    {
                        unitState.moveState = (int)UnitState.MoveStateEnum.Run;
                        moveDirection *= shiftSpeedMultiplyer;
                    }
                    else
                    {
                        unitState.moveState = (int)UnitState.MoveStateEnum.Walk;
                    }
                }
            }
            else if (fallTime >= 1f)
            {
                unitState.moveState = (int)UnitState.MoveStateEnum.Fall;
            }
            else
            {
                fallTime += Time.deltaTime;
            }
            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator climbMoveState(Ladder ladder)
    {
        unitState.moveState = (int)UnitState.MoveStateEnum.Climb;

        var topPos = ladder.Top.transform.position;
        var bottomPos = ladder.Bottom.transform.position;
        
        Vector3 smallestLimit;
        Vector3 biggestLimit;

        smallestLimit.x = Mathf.Min(topPos.x, bottomPos.x);
        smallestLimit.y = Mathf.Min(topPos.y, bottomPos.y);
        smallestLimit.z = Mathf.Min(topPos.z, bottomPos.z);
        biggestLimit.x = Mathf.Max(topPos.x, bottomPos.x);
        biggestLimit.y = Mathf.Max(topPos.y, bottomPos.y);
        biggestLimit.z = Mathf.Max(topPos.z, bottomPos.z);

        while (true)
        {
            var newPos = transform.position + (topPos - bottomPos).normalized * speed * Input.GetAxis("Vertical") * Time.deltaTime;
            newPos.x = Mathf.Clamp(newPos.x, smallestLimit.x, biggestLimit.x);
            newPos.y = Mathf.Clamp(newPos.y, smallestLimit.y, biggestLimit.y);
            newPos.z = Mathf.Clamp(newPos.z, smallestLimit.z, biggestLimit.z);
            transform.position = newPos;

            if (Input.GetButton("Jump"))
            {
                var verticalAxis = Input.GetAxis("Vertical");
                var horizontalAxis = Input.GetAxis("Horizontal");
                moveDirection = transform.forward;
                moveDirection *= verticalAxis;
                moveDirection += horizontalAxis * transform.right;
                moveDirection *= speed;
                moveDirection.y = jumpSpeed;
                unitState.moveState = (int)UnitState.MoveStateEnum.Jump;

                currentMoveCoroutine = StartCoroutine(basicMoveState());
                break;
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //
        transform.Rotate( 0, Input.GetAxis("Mouse X") * Time.deltaTime * lookSensivity, 0);
        characterCamera.transform.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * lookSensivity, 0, 0);

        if (Input.GetButton("Sit"))
        {
            characterController.height = 0.5f;
        } 
        else
        {
            characterController.height = 1.7f;
        }

        if (Input.GetButton("Use"))
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(characterCamera.transform.position, characterCamera.transform.forward, out hitInfo, 1))
            {
                var ladder = hitInfo.collider.GetComponent<Ladder>();
                if(ladder != null)
                {
                    StopCoroutine(currentMoveCoroutine);
                    currentMoveCoroutine = StartCoroutine(climbMoveState(ladder));
                }
            }
        }
    }
}
