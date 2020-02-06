using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seperation
{
    public Kinematic character;
    public Kinematic[] targets;

    private float maxAcceleration = 40f;
    private float threshold = 2f;
    private float decayCoefficient = 2f;
    private Vector3 direction;
    private float distance;
    private float strength;

    public SteeringOutput GetSteering()
    {
        SteeringOutput result = new SteeringOutput();

        for (int i = 0; i < targets.Length; i++)
        {
            //direction = targets[i].kPosition - character.kPosition;
            direction = character.kPosition - targets[i].kPosition;
            distance = direction.magnitude;

            if (distance < threshold)
            {
                //Get strength of repulsion using inverse square law\
                strength = Mathf.Min(decayCoefficient / (distance * distance), maxAcceleration);

                direction = direction.normalized;
                result.linear += strength * direction;
            }
        }
        return result;
    }
}

