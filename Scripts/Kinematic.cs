using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This purely holds data that needs to be referenced by other scripts
public class Kinematic : MonoBehaviour
{
    [Header("Setting values here will only affect how other scripts view this gameObject")]
    public Vector3 kPosition;
    public float kOrientation;
    public Vector3 kVelocity = Vector3.zero;
    public float kRotation = 0f;
    
    public void GetData(SteeringOutput currentSteering)
    {
        if (currentSteering != null)
        {
            kVelocity += currentSteering.linear * Time.deltaTime;
            kRotation += currentSteering.angular * Time.deltaTime;
        }     
    }

    private void Update()
    {
        kPosition = transform.position;
        kOrientation = transform.rotation.y;
    }
}
