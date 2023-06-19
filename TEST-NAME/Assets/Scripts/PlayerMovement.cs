using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f; // The speed at which the player moves

    void Start()
    {
    } 
    
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal"); // Get the horizontal input axis
        float y = Input.GetAxis("Vertical"); // Get the vertical input axis
        MoveOneUnit(x, y);
    }

    void MoveOneUnit(float x, float y) {
        Vector3 move = transform.right * -x + transform.up * y; // Calculate the movement vector

        if (move.x < 0) {
            move.x = Mathf.Floor(move.x);
            move.y = Mathf.Floor(move.y);
        }
        else{
            move.x = (float) Mathf.Ceil(move.x);
            move.y = (float) Mathf.Ceil(move.y);
        }

        transform.position += move * speed * Time.fixedDeltaTime; // Move the player
    }

}