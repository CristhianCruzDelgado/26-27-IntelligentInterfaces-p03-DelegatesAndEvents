using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cylinder_proximity_notifier : MonoBehaviour
{
    public delegate void CollisionAction();
    public static event CollisionAction OnCubeApproachedReference;
    private Rigidbody cubeRigidbody;
    private float proximityThreshold = 2f;
    private bool eventEmitted = false;

    void FixedUpdate()
    {
        GameObject cubeObject = GameObject.FindGameObjectWithTag("cube");
        if (cubeObject != null)
        {
            cubeRigidbody = cubeObject.GetComponent<Rigidbody>();
            RigidBody rigidBody = GetComponent<Rigidbody>();
            if (!eventEmitted && 
                cubeRigidbody != null && 
                rigidBody != null)
            {
                float distance = Vector3.Distance(cubeRigidbody.position,
                                                  rigidBody.position);

                if (distance < proximityThreshold)
                {
                    eventEmitted = true;
                    OnCubeApproachedReference?.Invoke();
                }
            }
        }
    }
}
