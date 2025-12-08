using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform Camera;
    public float PlayerSpeed;
    public float RotationSpeed;

    Vector3 speed = Vector3.zero;
    Vector3 rot = Vector3.zero;

    private void Update()
    {
        Move();
        Rotation();
        Camera.transform.position = transform.position;
    }

    void Move()
    {
        speed = Vector3.zero;
        rot = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            rot.y = 0;
            MoveSet();
        }
        if (Keyboard.current.sKey.isPressed)
        {
            rot.y = 180;
            MoveSet();
        }
        if (Keyboard.current.dKey.isPressed)
        {
            rot.y = 90;
            MoveSet();
        }
        if (Keyboard.current.aKey.isPressed)
        {
            rot.y = -90;
            MoveSet();
        }

        transform.Translate(speed);
    }

    void Rotation()
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

    void MoveSet()
    {
        speed.z = PlayerSpeed;
        transform.eulerAngles = Camera.transform.eulerAngles + rot;
    }
}