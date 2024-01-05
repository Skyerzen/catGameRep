using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickCntr : MonoBehaviour
{
    public GameObject touchObject; // The object to be positioned and made visible on touch
    public GameObject gameUI;
    void Update()
    {
        if (gameUI.GetComponent<ScrnTgl>().pantalla == true)
        {
            // Check if there is any touch input
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Check if the touch phase is the beginning of a touch
                if (touch.phase == TouchPhase.Began)
                {
                    // Get the touch position in world space
                    Vector3 touchPosition = touch.position;
                    Debug.Log(touchPosition);
                    // If the object doesn't exist, instantiate it
                    if (touchObject == null)
                    {
                        touchObject = Instantiate(gameObject, touchPosition, Quaternion.identity);
                    }
                    else
                    {
                        // Move the object to the touch position
                        touchObject.transform.position = touchPosition;
                    }

                    // Make the object visible
                    touchObject.SetActive(true);
                }
                // Check if the touch phase is the end of a touch
                else if (touch.phase == TouchPhase.Ended)
                {
                    // Hide the object
                    if (touchObject != null)
                    {
                        touchObject.SetActive(false);
                    }
                }
            }
        }
            
    }
}
