using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public int orbsCollected = 0;
    public Light myLight;

    private void Update()
    {
        if(orbsCollected == 6)
        {
            myLight.intensity = 0;
        }
    }
    private void FixedUpdate()
    {
        int layerMask = 1 << 6;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (Input.GetMouseButtonDown(0))
            {
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
