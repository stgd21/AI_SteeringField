using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SteeringType
{
    Seek,
    Flee,
    Pursue,
    Evade,
    FollowPath,
    Seperation,
    Arrive
}

public enum LookType
{
    None,
    Align,
    Face,
    LookWhereGoing
}

public class AppliedSteering : MonoBehaviour
{
    public Vector3 linearVelocity;
    public float angularVelocity;
    public Kinematic target;
    public SteeringType moveType;
    public LookType lookType;

    //Kinematic holds data about our agent
    private Kinematic kinematic;

    //For Pathfollow
    public Kinematic[] pathOfObjects;

    //For Seperation
    public Kinematic[] seperateObstacles;

    private PathFollow followAI;
    private Pursue pursueAI;
    private Seek seekAI;
    private Flee fleeAI;
    private Evade evadeAI;
    private Seperation seperationAI;
    private Arrive arriveAI;

    private Align alignAI;

    private SteeringOutput lookSteering;

    private void Start()
    {
        kinematic = GetComponent<Kinematic>();

        switch (moveType)
        {
            case SteeringType.Pursue:
                pursueAI = new Pursue();
                pursueAI.character = kinematic;
                pursueAI.target = target;
                break;
            case SteeringType.Evade:
                evadeAI = new Evade();
                evadeAI.character = kinematic;
                evadeAI.target = target;
                break;
            case SteeringType.FollowPath:
                followAI = new PathFollow();
                followAI.character = kinematic;
                followAI.path = pathOfObjects;
                break;
            case SteeringType.Seek:
                seekAI = new Seek();
                seekAI.character = kinematic;
                seekAI.target = target;
                break;
            case SteeringType.Flee:
                fleeAI = new Flee();
                fleeAI.character = kinematic;
                fleeAI.target = target;
                break;
            case SteeringType.Seperation:
                seperationAI = new Seperation();
                seperationAI.character = kinematic;
                seperationAI.targets = seperateObstacles;
                break;
            case SteeringType.Arrive:
                arriveAI = new Arrive();
                arriveAI.character = kinematic;
                arriveAI.target = target;
                break;
        }

        switch (lookType)
        {
            case LookType.Align:
                alignAI = new Align();
                alignAI.character = kinematic;
                alignAI.target = target;
                break;
        }
    }
    void Update()
    {
        SteeringOutput movementSteering;
        //SteeringOutput lookSteering;
        //Update position and rotation
        transform.position += linearVelocity * Time.deltaTime;
        Vector3 angularIncrement = new Vector3(0, angularVelocity * Time.deltaTime, 0);
        transform.eulerAngles += angularIncrement;

        switch (moveType)
        {
            case SteeringType.Pursue:
                movementSteering = pursueAI.GetSteering();
                break;
            case SteeringType.Evade:
                movementSteering = evadeAI.GetSteering();
                break;
            case SteeringType.FollowPath:
                movementSteering = followAI.GetSteering();
                break;
            case SteeringType.Seek:
                movementSteering = seekAI.GetSteering();
                break;
            case SteeringType.Flee:
                movementSteering = fleeAI.GetSteering();
                break;
            case SteeringType.Seperation:
                movementSteering = seperationAI.GetSteering();
                break;
            case SteeringType.Arrive:
                movementSteering = arriveAI.GetSteering();
                break;
            default:
                movementSteering = seekAI.GetSteering();
                break;
        }
  
        if (movementSteering != null)
        {
            linearVelocity += movementSteering.linear * Time.deltaTime;
            //angularVelocity += movementSteering.angular * Time.deltaTime;
        }
        else
        {
            linearVelocity = Vector3.zero;
            //angularVelocity = 0;
        }

        switch (lookType)
        {
            case LookType.None:
                break;
            case LookType.Align:
                lookSteering = alignAI.GetSteering();
                break;
            default:
                lookSteering = alignAI.GetSteering();
                break;
        }

        if (lookSteering != null)
        {
            angularVelocity += lookSteering.angular * Time.deltaTime;
        }
        else
        {
            angularVelocity = 0;
        }
        //Update kinematic reference with complex data it can't get by itself
        kinematic.GetData(movementSteering);
        kinematic.GetData(lookSteering);
    }
}

public class SteeringOutput
{
    public Vector3 linear;
    public float angular;
}
