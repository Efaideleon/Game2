using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEraser : MonoBehaviour
{
    [SerializeField] GameObject eraser;
    private EraserMovement eraserMovement;
    private Vector3 eraserDirection;
    private Rigidbody eraserRb;
    private float speed = 20f;
    bool isFlying = false;
    Direction eraserDirectionEnum;
    enum Direction{
        Left,
        Right
    }
    // Start is called before the first frame update
    void Start()
    {
        eraserDirection = new Vector3(-1, 0, 0);
        eraserRb = eraser.GetComponent<Rigidbody>();
        eraserMovement = eraser.GetComponent<EraserMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isFlying)
        {
            SetEraserDirection();
            eraserMovement.LaunchEraser();
            isFlying = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFlying)
        {
            eraserRb.MovePosition(eraser.transform.position + eraserDirection * Time.fixedDeltaTime * speed);
        }

        if (IsEraserAtMaxDistance())
        {
            eraserMovement.KillEraser();
            isFlying = false;
        }
    }

    void SetEraserStartPositionWithOffset(int offset)
    {
        eraser.transform.position = transform.position + new Vector3(offset, 0, 0);
    }

    void SetEraserDirection()
    {
        if (transform.rotation.eulerAngles.y ==  90)
        {
            eraserDirection = Vector3.left;
            SetEraserStartPositionWithOffset(-1);
        }
        else if (transform.rotation.eulerAngles.y == 270)
        {
            eraserDirection = Vector3.right;
            SetEraserStartPositionWithOffset(1);
        }
    }


    bool IsEraserAtMaxDistance()
    {
        if (Vector3.Distance(eraser.transform.position, transform.position) > 10)
        {
            return true;
        }
        return false;
    }
}
