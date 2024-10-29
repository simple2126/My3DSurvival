using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigidbody;

    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;
    private bool isDoubleJump;
    private int jumpCount;
    private bool isInvincibility;

    [Header("Look")]
    public Transform cameraContainer;
    private Vector2 mouseLook;
    private float curXLook;
    public float lookSensitivity;
    public float maxXLook;
    public float minXLook;

    [HideInInspector]
    public bool canLook = true;

    public Action inventory;
    private Coroutine coroutine;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            Look();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            if(!IsGround() && isDoubleJump && jumpCount < 2)
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount++;
            }
            else if(IsGround() && !isDoubleJump)
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        } 
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }

    private bool IsGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    private void Look()
    {
        curXLook += mouseLook.y * lookSensitivity;
        curXLook = Mathf.Clamp(curXLook, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-curXLook, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseLook.x * lookSensitivity, 0);
    }

    public void OnInventory(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void SpeedBoost(float durationTime, float upPercent)
    {
        float initialSpeed = moveSpeed;
        moveSpeed *= upPercent;
        StartCoroutine(SpeedUpCoroutine(durationTime, initialSpeed));
    }

    IEnumerator SpeedUpCoroutine(float time, float speed)
    {
        yield return new WaitForSeconds(time);
        moveSpeed = speed;
    }

    public void DoubleJump(float durationTime)
    {
        isDoubleJump = true;
        StartCoroutine(DoubleJumpCoroutine(durationTime));
    }

    IEnumerator DoubleJumpCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        isDoubleJump = false;
    }

    public void Invincibility(float durationTime)
    {
        isInvincibility = true;
        StartCoroutine(InvincibilityCoroutine(durationTime));
    }

    IEnumerator InvincibilityCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        isInvincibility = false;
    }
}