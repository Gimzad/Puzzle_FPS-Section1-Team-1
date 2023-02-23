using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] float smooth;
    [SerializeField] float swayMultiplier;

    Quaternion rotationX, rotationY, targetRotation;

    private void Update()
    {
        if (GameManager.Instance.PlayerScript().weaponList.Count > 0)
        {
            float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;
            float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
            if (GameManager.Instance.PlayerScript().zooming)
            {
                rotationX = Quaternion.AngleAxis(-mouseY, Vector3.left);
                rotationY = Quaternion.AngleAxis(mouseX, Vector3.down);
            }
            else
            {
                rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
                rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
            }

            targetRotation = rotationX * rotationY;

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
    }
}
