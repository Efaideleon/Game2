using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f; // The speed at which the player moves
    public float x;
    public float y;
    public enum Direction
    {
        Horizontal,
        Vertical,
        None
    }

    private float horizontal = 0f;
    private float vertical = 0f;

    private Direction latestDirection = Direction.None;

    void Start() { }

    void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal"); // Get the horizontal input axis
        y = Input.GetAxisRaw("Vertical"); // Get the vertical input axis
        MoveOneUnit(x, y);
    }

    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.CompareTag("Cotton");
        Destroy(collision.gameObject);
        Debug.Log("Collision!");
    }

    void MoveOneUnit(float newHorizontal, float newVertical)
    {
        // Update horizontal if changed from not pressing to pressing
        if ((newHorizontal > 0 && horizontal >= 0) || (newHorizontal < 0 && horizontal <= 0) || horizontal == 0)
        {
            if (newHorizontal != 0 && horizontal == 0)
            {
                latestDirection = Direction.Horizontal;
            }
            horizontal = newHorizontal;
        }

        // Update vertical if changed from not pressing to pressing
        if ((newVertical > 0 && vertical >= 0) || (newVertical < 0 && vertical <= 0) || vertical == 0)
        {
            if (newVertical != 0 && vertical == 0)
            {
                latestDirection = Direction.Vertical;
            }
            vertical = newVertical;
        }

        // Reset horizontal if not pressing
        if (newHorizontal == 0)
        {
            horizontal = 0f;
        }

        // Reset vertical if not pressing
        if (newVertical == 0)
        {
            vertical = 0f;
        }

        // Decide which direction to move based on the latest input
        if (latestDirection == Direction.Horizontal && horizontal != 0)
        {
            MoveHorizontal(horizontal);
        }
        else if (vertical != 0)
        {
            MoveVertical(vertical);
        }
        else if (horizontal != 0)
        {
            MoveHorizontal(horizontal);
        }
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
