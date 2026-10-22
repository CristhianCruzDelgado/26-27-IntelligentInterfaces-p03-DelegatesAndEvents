using UnityEngine;

public class cube_player : MonoBehaviour
{
    private Rigidbody rigibody;
    private Vector3 direction;
    private float speed = 1f;

    void Start()
    {
        rigibody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateDirection();
    }

    private void FixedUpdate()
    {
        rigibody.linearVelocity = direction * speed;
    }

    private void UpdateDirection()
    {
        float xAxisVelocity = Input.GetAxisRaw("Horizontal");
        float zAxisVelocity = Input.GetAxisRaw("Vertical");
        direction = new Vector3(xAxisVelocity, 0f, zAxisVelocity).normalized;
    }
}
