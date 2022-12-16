using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private InputDevice targetDeviceLeft;
    private InputDevice targetDeviceRight;

    public Transform leftController;
    public Transform rightController;

    public int uiCount = 0;
    public GameObject ControllerUI;
    public GameObject HandUI;
    void Start()
    {
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

    private void Update()
    {
        if (uiCount == 0)
        {
            ControllerUI.gameObject.SetActive(true);
        }

        if (uiCount == 1)
        {
            ControllerUI.gameObject.SetActive(false);
            HandUI.gameObject.SetActive(true);
        }
    }
    void FixedUpdate()
    {
        // Bit shift the index of the layer (7) to get a bit mask
        int uiButton = 1 << 7;

        RaycastHit hit;
        if (Physics.Raycast(rightController.position, rightController.forward, out hit, Mathf.Infinity, uiButton))
        {
            Debug.DrawRay(rightController.position, rightController.forward * 500, Color.yellow);
            if (Input.GetMouseButtonDown(0))
            {
                uiCount += 1;
            }

            targetDeviceRight.TryGetFeatureValue(CommonUsages.trigger, out float triggerButtonValue);
            if (triggerButtonValue == 1.0f)
            {
                uiCount += 1;
            }
        }

        if (Physics.Raycast(leftController.position, leftController.forward, out hit, Mathf.Infinity, uiButton))
        {

            targetDeviceLeft.TryGetFeatureValue(CommonUsages.trigger, out float triggerButtonValue);
            if (triggerButtonValue == 1.0f)
            {
                Debug.Log("pressing primary button");
                uiCount += 1;
            }
        }
    }
}