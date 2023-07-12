using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class ShootingLaser : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    bool isShooting = false;
    Vector3 laserDirection;
    Quaternion laserRotation;

    // Start is called before the first frame update
    void Start()
    {
        laserPrefab.transform.position = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && !isShooting && (transform.rotation.eulerAngles.y ==  90 || transform.rotation.eulerAngles.y == 270))
        {
            Debug.Log("Space is pressed");
            laserPrefab.transform.position = transform.position;
            SetRotationOfLaser();
            laserPrefab.SetActive(true);
            laserPrefab.GetComponent<VisualEffect>().Play();
            isShooting = true;
        }

        if (isShooting)
        {
            laserPrefab.transform.position += laserDirection * Time.deltaTime * 25;
        }

        if(Vector3.Distance(laserPrefab.transform.position, transform.position) > 10)
        {
            laserPrefab.SetActive(false);
            laserPrefab.GetComponent<VisualEffect>().Stop();
            laserPrefab.transform.position = laserPrefab.transform.parent.position;
            isShooting = false;
        }
    }

    void SetRotationOfLaser()
    {       
        if(transform.rotation.eulerAngles.y == 90)
        {
            laserRotation.eulerAngles = new Vector3(0, 180, 0);
            laserDirection = Vector3.left;
        }
        if(transform.rotation.eulerAngles.y == 270)
        {
            laserRotation.eulerAngles = new Vector3(0, 0, 0);
            laserDirection = Vector3.right;
        }
        laserPrefab.transform.rotation = laserRotation;
    }
}
