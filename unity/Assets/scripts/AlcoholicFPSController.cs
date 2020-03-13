using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholicFPSController : MonoBehaviour
{
    CharacterController characterController;
    public Camera characterCamera;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float shiftAdditionalSpeed = 6.0f;
    public float gravity = 20.0f;
    public float lookSensivity = 75.0f;

    public float health = 1.0f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 alcoLookVector = Vector3.zero;
    private Vector3 alcoMoveVector = Vector3.zero;
    public float alcoMoveImpact = 1f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        StartCoroutine(alcoLookVectorChangeRoutine());
        StartCoroutine(alcoMoveVectorChangeRoutine());
    }

    IEnumerator alcoLookVectorChangeRoutine()
    {
        while (true)
        {
            // рандомизируем вектор взгляда
            Vector3 toVector = Random.onUnitSphere;
            Vector3 fromVector = alcoLookVector;
            float duration = Random.Range(1f, 10f);
            float startTime = 0;
            while(startTime < duration)
            {
                alcoLookVector = Vector3.Slerp(fromVector, toVector, startTime / duration);
                startTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator alcoMoveVectorChangeRoutine()
    {
        while (true)
        {
            // рандомизируем вектор передвижения
            Vector3 toVector = Random.onUnitSphere;
            Vector3 fromVector = alcoMoveVector;
            float duration = Random.Range(3f, 10f);
            float startTime = 0;
            while (startTime < duration)
            {
                alcoMoveVector = Vector3.Slerp(fromVector, toVector, startTime / duration);
                alcoMoveVector.y = 0;
                startTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float characterVelocity = Mathf.Min(Mathf.Max(characterController.velocity.magnitude, 1), 2);
        // поворот по горизонтали
        transform.Rotate( 0, Input.GetAxis("Mouse X") * Time.deltaTime * lookSensivity + alcoLookVector.y * health * characterVelocity, 0);
        // поворот по вертикали
        characterCamera.transform.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * lookSensivity + alcoLookVector.x * health * characterVelocity, 0, 0);

        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate

            moveDirection = transform.forward;
            moveDirection *= Input.GetAxis("Vertical");
            moveDirection += Input.GetAxis("Horizontal") * transform.right;
            moveDirection *= speed + Input.GetAxis("Shift") * shiftAdditionalSpeed;

            moveDirection += health * alcoMoveVector * characterVelocity * alcoMoveImpact;

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
