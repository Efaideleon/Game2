using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f; // The speed at which the player moves
    public float x;
    public float y;
    public enum CurrentInput
    {
        Horizontal,
        Vertical,
        None
    }

    public CurrentInput currInput;

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

    void MoveOneUnit(float x_, float y_)
    {
        if (x_ != 0)
        {
            if (currInput == CurrentInput.Horizontal && y_ != 0) {
                currInput = CurrentInput.Vertical;
            }
            else if(currInput != CurrentInput.Vertical) 
            {
                currInput = CurrentInput.Horizontal;
            }
        }
        else if (y_ != 0)
        {
            if (currInput == CurrentInput.Vertical && x_ != 0){
                currInput = CurrentInput.Horizontal;
            }
            else if (currInput != CurrentInput.Horizontal)
            {
                currInput = CurrentInput.Vertical;
            }
        }

        if (currInput == CurrentInput.Horizontal)
        {
            MoveHorizontal(x_);
        }
        else if (currInput == CurrentInput.Vertical)
        {
            MoveVertical(y_);
        }

        if (x_ == 0 && y_ ==0){
            currInput = CurrentInput.None;
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
