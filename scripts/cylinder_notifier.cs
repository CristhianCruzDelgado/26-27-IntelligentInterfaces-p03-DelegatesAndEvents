using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cylinder_notifier : MonoBehaviour
{
    public delegate void CollisionAction();
    public static event CollisionAction OnCubeApproachedReference;
    private Rigidbody cubeRigidbody;
    private Rigidbody referenceRigidbody;
    private float proximityThreshold = 2f;
    private bool eventEmitted = false;

    void Awake()
    {
        referenceRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        GameObject cubeObject = GameObject.FindGameObjectWithTag("cube");
        cubeRigidbody = cubeObject.GetComponent<Rigidbody>();
        if (!eventEmitted && cubeRigidbody != null && referenceRigidbody != null)
        {
            float distance = Vector3.Distance(referenceRigidbody.position,
                                              cubeRigidbody.position);

            if (distance <= proximityThreshold)
            {
                eventEmitted = true;
                OnCubeApproachedReference?.Invoke();
            }
        }
    }
}
