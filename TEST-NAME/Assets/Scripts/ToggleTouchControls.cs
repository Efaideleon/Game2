using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleTouchControls : MonoBehaviour
{
    [SerializeField] GameObject joyStick;
    [SerializeField] GameObject buttonA;
    [SerializeField] Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ToggleControls();
    }

    void ToggleControls()
    {
        if (toggle.isOn)
        {
            joyStick.SetActive(true);
            buttonA.SetActive(true);
        }
        else
        {
            joyStick.SetActive(false);
            buttonA.SetActive(false);
        }
    }
}
