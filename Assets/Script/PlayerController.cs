using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using DoubleL;
using UnityEngine.UIElements.Experimental;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 movingVelocity;  // 実際に適用する移動ベクトル
    [SerializeField] private float moveSpeed = 7.5f;  // 移動の速さ
    [SerializeField] private float applySpeed = 0.2f; // 回転の補間係数

    [SerializeField] Rigidbody rb;

    Vector3 moveDirection;      // カメラ基準での移動単位ベクトル
    public Transform Camera;    // 追従カメラ
    public float RotationSpeed; // カメラの回転スピード


    public Animator PlayerAnimator; // アニメーター(walkフラグを渡す)
    bool isWalk;                    // 歩き中フラグ(Animatorに渡す)

    private void Update()
    {
        Move();
        CameraRotation();
        Camera.transform.position = transform.position;
    }

    public void GetVelocity()
    {
        float x = 0f;
        float z = 0f;

        if (Keyboard.current.dKey.isPressed) x = 1f;
        if (Keyboard.current.aKey.isPressed) x = -1f;

        if (Keyboard.current.wKey.isPressed) z = 1f;
        if (Keyboard.current.sKey.isPressed) z = -1f;

        Vector3 camForward = Camera.transform.forward;
        Vector3 camRight = Camera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        moveDirection = (camForward * z + camRight * x).normalized;
        movingVelocity = moveDirection * moveSpeed;
    }

    void Move()
    {
        GetVelocity();

        Vector3 horizontalVelocity = new Vector3(movingVelocity.x, 0, movingVelocity.z);
        rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);

        if (horizontalVelocity.magnitude > 0.1f)
        {
            isWalk = true;

            Quaternion targetRot = Quaternion.LookRotation(moveDirection, Vector3.up);

            float smooth = applySpeed * Time.deltaTime * 20f;

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, smooth);
        }
        else
        {
            isWalk = false;
        }

        PlayerAnimator.SetBool("walk", isWalk);
    }

    void CameraRotation()
    {
        var speed = Vector3.zero;

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            speed.y = RotationSpeed;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            speed.y = -RotationSpeed;
        }

        Camera.transform.eulerAngles += speed;
    }
}