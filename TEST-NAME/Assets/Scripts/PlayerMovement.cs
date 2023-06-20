using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f; // The speed at which the player moves

    void Start() { }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal"); // Get the horizontal input axis
        float y = Input.GetAxisRaw("Vertical"); // Get the vertical input axis
        MoveOneUnit(x, y);
    }

    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.CompareTag("Cotton");
        Destroy(collision.gameObject);
        Debug.Log("Collision!");
    }

    void MoveOneUnit(float x, float y)
    {
        if (x != 0)
        {
            y = 0;
        }
        if (y != 0)
        {
            x = 0;
        }
        Vector3 move = new Vector3(-x, y, 0);
        Vector3 direction = move * speed * Time.fixedDeltaTime;
        transform.position += direction; // Move the player
    }
}
