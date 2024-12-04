using UnityEngine;

public class RotateWithMouse2D : MonoBehaviour
{
    // Adjust this to control the rotation speed
    public float rotationSpeed = 5f;

    void Update()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert mouse position to world space
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = transform.position.z;

        // Calculate the direction from the object to the mouse position
        Vector3 direction = worldMousePosition - transform.position;

        // Calculate the angle in degrees for rotation around Z-axis
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Create a target rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);

        // Smoothly interpolate to the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ENEMY")
        {
            Debug.Log("enemy hit");
        }
    }
}
