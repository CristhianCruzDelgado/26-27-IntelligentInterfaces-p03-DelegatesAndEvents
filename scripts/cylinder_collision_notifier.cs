using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cylinder_collision_notifier : MonoBehaviour
{
    public delegate void CollisionAction();
    public static event CollisionAction OnCubeCollided;

    void OnCollisionEnter(Collision collision)
    {
        NotifyEvent();
    }

    private void NotifyEvent()
    {
        OnCubeCollided?.Invoke();
    }
}
