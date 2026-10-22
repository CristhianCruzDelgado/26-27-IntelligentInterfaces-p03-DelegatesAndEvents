using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanoid_notifier : MonoBehaviour
{
    public delegate void CollisionAction();
    public static event CollisionAction OnCubeCollidedHumanoidRed;
    public static event CollisionAction OnCubeCollidedHumanoidGreen;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cube"))
        {
            if (gameObject.CompareTag("humanoid-red"))
            {
                OnCubeCollidedHumanoidRed?.Invoke();
            }
            else if (gameObject.CompareTag("humanoid-green"))
            {
                OnCubeCollidedHumanoidGreen?.Invoke();
            }
        }
    }
}
