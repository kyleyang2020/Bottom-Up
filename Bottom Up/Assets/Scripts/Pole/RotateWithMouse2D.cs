using UnityEngine;

public class RotateWithMouse2D : MonoBehaviour
{
    void Update()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert mouse position to world space
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = transform.position.z; // Keep the Z-coordinate constant

        // Calculate the direction from the pole's position to the mouse position
        Vector3 direction = worldMousePosition - transform.position;

        // Calculate the angle in degrees for rotation around the Z-axis
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the pole
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}