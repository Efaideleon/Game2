using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f; // The speed at which the player moves
    public float x;
    public float y;
    enum MoveDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    private MoveDirection latestDirection = MoveDirection.None;
    private bool upKeyPressed, downKeyPressed, leftKeyPressed, rightKeyPressed;


    void Start() { }

    void Update()
    {
        MoveOneUnit();
    }

    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.CompareTag("Cotton");
        Destroy(collision.gameObject);
        Debug.Log("Collision!");
    }

    void MoveOneUnit()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upKeyPressed = true;
            latestDirection = MoveDirection.Up;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            upKeyPressed = false;
            if (latestDirection == MoveDirection.Up)
                ResetLatestDirection();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downKeyPressed = true;
            latestDirection = MoveDirection.Down;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            downKeyPressed = false;
            if (latestDirection == MoveDirection.Down)
                ResetLatestDirection();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftKeyPressed = true;
            latestDirection = MoveDirection.Left;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftKeyPressed = false;
            if (latestDirection == MoveDirection.Left)
                ResetLatestDirection();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightKeyPressed = true;
            latestDirection = MoveDirection.Right;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightKeyPressed = false;
            if (latestDirection == MoveDirection.Right)
                ResetLatestDirection();
        }

        switch (latestDirection)
        {
            case MoveDirection.Up:
                MoveVertical(1);
                break;
            case MoveDirection.Down:
                MoveVertical(-1);
                break;
            case MoveDirection.Left:
                MoveHorizontal(-1);
                break;
            case MoveDirection.Right:
                MoveHorizontal(1);
                break;
            default:
                break;
        }
    }

    void ResetLatestDirection()
    {
        if (upKeyPressed)
            latestDirection = MoveDirection.Up;
        else if (downKeyPressed)
            latestDirection = MoveDirection.Down;
        else if (leftKeyPressed)
            latestDirection = MoveDirection.Left;
        else if (rightKeyPressed)
            latestDirection = MoveDirection.Right;
        else
            latestDirection = MoveDirection.None;
    }

    void MoveHorizontal(float x_)
    {
        int moveX = Mathf.RoundToInt(x_);
        Vector3 move = new Vector3(-moveX, 0, 0);
        Move(move);
    }

    void MoveVertical(float y_)
    {
        int moveY = Mathf.RoundToInt(y_);
        Vector3 move = new Vector3(0, moveY, 0);
        Move(move);
    }

    void Move(Vector3 move)
    {
        Vector3 direction = move * speed * Time.fixedDeltaTime;
        transform.position += direction; // Move the player
    }
}
