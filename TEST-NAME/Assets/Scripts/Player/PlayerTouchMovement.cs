using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchMovement : MonoBehaviour
{
    [SerializeField] private Vector2 JoystickSize = new Vector2(300, 300);
    [SerializeField] private FloatingJoyStick Joystick;
    [SerializeField] private TouchButton FireButton;
    private Finger MovementFinger;
    private Finger ShootFinger;
    private Vector2 MovementAmount;
    [SerializeField] PlayerMovement Player;
    [SerializeField] ShootEraser ShootEraser;
    float JoystickRadius = 105; //make jostick constant size to calculate its radius
    float FireButtonRadius = 140;
    
    void Start()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }
    // Update is called once per frame
    void Update()
    {
        Player.MoveVector(MovementAmount);
    }
    
    void OnDestroy()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;   
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = 35; 
            ETouch.Touch currentTouch = MovedFinger.currentTouch;


            Vector2 touchPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Joystick.GetComponent<RectTransform>(), currentTouch.screenPosition, null, out touchPos);

            float maxKnobDistance = Joystick.GetComponent<RectTransform>().sizeDelta.x * 0.3f;
            knobPosition = Vector2.ClampMagnitude(touchPos, maxKnobDistance);
            Joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;
        }
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if (LostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;
            MovementAmount = Vector2.zero;
        }
        if (ShootFinger == LostFinger)
        {
            ShootFinger = null;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null && IsTouchWithinJoystickRadius(TouchedFinger))
        {
            ETouch.Touch currentTouch = TouchedFinger.currentTouch;
            MovementFinger  = TouchedFinger;
            MovementAmount = Vector2.zero;

            Vector2 touchPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Joystick.GetComponent<RectTransform>(), currentTouch.screenPosition, null, out touchPos);

            // Limit the distance the knob can move from the center of the joystick.
            float maxKnobDistance = Joystick.GetComponent<RectTransform>().sizeDelta.x * 0.3f;
            Vector2 knobPosition = Vector2.ClampMagnitude(touchPos, maxKnobDistance);

            Joystick.Knob.anchoredPosition = knobPosition;
        }

        if (ShootFinger == null && IsTouchWithinFireButton(TouchedFinger) && !ShootEraser.IsFlying())
        {
            ShootFinger = TouchedFinger;
            ShootEraser.Fire();
        }
    }
    private bool IsTouchWithinJoystickRadius(Finger TouchedFinger)
    {
        if (Vector2.Distance(TouchedFinger.screenPosition, Joystick.transform.position) < JoystickRadius)
        {
            Debug.Log("Within the joystick");
            //Debug.Log("touched finger screen position" + TouchedFinger.screenPosition);
            return true;
        }
        else{
            Debug.Log("Not in jostick range");
            //Debug.Log("touched finger screen position" + TouchedFinger.screenPosition);
            return false;
        }
    }

    private bool IsTouchWithinFireButton(Finger TouchedFinger)
    {
        if (Vector2.Distance(TouchedFinger.screenPosition, FireButton.transform.position) < FireButtonRadius)
        {
            Debug.Log("Within the fire button");
            //Debug.Log("touched finger screen position" + TouchedFinger.screenPosition);
            return true;
        }
        else{
            Debug.Log("Not in fire button range");
            //Debug.Log("touched finger screen position" + TouchedFinger.screenPosition);
            return false;
        }
    }
}
