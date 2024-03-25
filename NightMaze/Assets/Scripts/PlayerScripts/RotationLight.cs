using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLight : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    private void Update()
    {
        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            float angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, (float)angle - 90);
        }
    }
}
