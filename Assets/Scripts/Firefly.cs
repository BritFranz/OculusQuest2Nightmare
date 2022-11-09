using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Firefly : MonoBehaviour
{
    public PathCreator pathCreator;
    public float flySpeed = 1;
    float distanceTravelled;

    private void Update()
    {
        distanceTravelled += flySpeed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
    }
}
