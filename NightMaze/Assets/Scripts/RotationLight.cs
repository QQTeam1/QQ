using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLight : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    private void Update()
    {
        if (_joystick.Vertical != 0 || _joystick.Horizontal != 0)
        {
            float angle = Mathf.Atan2(_joystick.Vertical, _joystick.Horizontal) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, (float)angle - 90);
            //((Component)_lightObject).transform.Rotate(0, 0, );
        }
    }
}
