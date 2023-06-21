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
    private bool upKeyPressed,
        downKeyPressed,
        leftKeyPressed,
        rightKeyPressed;

    void Start() { }

    void Update()
    {
        GetKeyboardInput();
    }

    void FixedUpdate()
    {
        MoveToDirection();
    }

    void MoveToDirection()
    {
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

    void GetKeyboardInput()
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
        Vector3 newPosition = transform.position + new Vector3(-x_, 0, 0);
        newPosition.x = Mathf.Round(newPosition.x);
        transform.position = Vector3.MoveTowards(
            transform.position,
            newPosition,
            speed * Time.fixedDeltaTime
        );
    }

    void MoveVertical(float y_)
    {
        Vector3 newPosition = transform.position + new Vector3(0, y_, 0);
        newPosition.y = Mathf.Round(newPosition.y);
        transform.position = Vector3.MoveTowards(
            transform.position,
            newPosition,
            speed * Time.fixedDeltaTime
        );
    }
}
