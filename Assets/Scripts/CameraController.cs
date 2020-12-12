using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 150f;
    public Transform body;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (transform.parent.GetComponent<PlayerController>().settingsMenu.activeSelf == false)
        {
            xRotation -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            body.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime);
        }
    }

    public void SetSensitivity(string value)
    {
        sensitivity = float.Parse(value);
    }
}
