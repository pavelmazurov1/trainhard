using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFirstPersionUnit : MonoBehaviour
{
    CharacterController characterController;
    public Camera characterCamera;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float shiftAdditionalSpeed = 6.0f;
    public float gravity = 20.0f;
    public float lookSensivity = 1.0f;

    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        var sceneDataObject = GameObject.FindGameObjectWithTag("SceneData");
        if(sceneDataObject)
        {
            sceneDataObject.GetComponent<PlayerData>().PlayerUnit = gameObject;
        }

        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //
        transform.Rotate( 0, Input.GetAxis("Mouse X") * Time.deltaTime * lookSensivity, 0);
        characterCamera.transform.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * lookSensivity, 0, 0);

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
    }
}
