using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cylinder_script : MonoBehaviour
{
    public delegate void CollisionAction();
    public static event CollisionAction OnCubeCollided;

    private void OnCollisionEnter(Collision collision)
    {
        NotifyEvent();
    }

    private void NotifyEvent()
    {
        Debug.Log("Cube collided. Broadcaster emitting event...");
        OnCubeCollided?.Invoke();
    }
}
