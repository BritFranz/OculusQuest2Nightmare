using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CollectorScript : MonoBehaviour
{
    public float mouseSensitifity = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    public int orbsCollected = 0;

    public Light myLight;

    private InputDevice targetDeviceLeft;
    private InputDevice targetDeviceRight;

    public Transform leftController;
    public Transform rightController;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        List<InputDevice> devices = new List<InputDevice>();

        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if (devices.Count > 0)
        {
            targetDeviceRight = devices[0];
        }


        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if (devices.Count > 0)
        {
            targetDeviceLeft = devices[0];
        }


    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitifity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitifity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (orbsCollected == 6)
        {
            myLight.intensity = 0;
        }


    }

    private void FixedUpdate()
    {
        int Collectable = 1 << 6;

        RaycastHit hit;
        if (Physics.Raycast(rightController.position, rightController.forward, out hit, Mathf.Infinity, Collectable))
        {
            Debug.DrawRay(rightController.position, rightController.forward * 500, Color.yellow);
            if (Input.GetMouseButtonDown(0))
            {
                orbsCollected += 1;
                Destroy(hit.transform.gameObject);
                myLight.intensity += 1 / 3f;

            }

            targetDeviceRight.TryGetFeatureValue(CommonUsages.triggerButton, out bool primaryButtonValue);
            if (primaryButtonValue == true)
            {
                Debug.Log("pressing primary button");
                orbsCollected += 1;
                Destroy(hit.transform.gameObject);
                myLight.intensity += 1 / 3f;
            }
        }

        if (Physics.Raycast(leftController.position, leftController.forward, out hit, Mathf.Infinity, Collectable))
        {
            Debug.DrawRay(leftController.position, leftController.forward * 500, Color.yellow);
            if (Input.GetMouseButtonDown(0))
            {
                orbsCollected += 1;
                Destroy(hit.transform.gameObject);
                myLight.intensity += 1 / 3f;

            }

            targetDeviceLeft.TryGetFeatureValue(CommonUsages.triggerButton, out bool primaryButtonValue);
            if (primaryButtonValue == true)
            {
                Debug.Log("pressing primary button");
                orbsCollected += 1;
                Destroy(hit.transform.gameObject);
                myLight.intensity += 1 / 3f;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }
}
