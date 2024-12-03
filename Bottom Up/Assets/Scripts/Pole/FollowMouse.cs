using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Transform mouseTransform;
    public Transform poleTransform;
    // Adjust this to control the speed of rotation
    public float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        mouseTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
    }

    void LookAtMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // this is angle that the pole must rotate around to face cursor
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward); // .forward is the z axis
        poleTransform.rotation = rotation;
    }

    void Rotate()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert mouse position to world space
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Find the direction to the hit point
            Vector3 direction = hit.point - transform.position;

            // Calculate the rotation to look in that direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly interpolate to the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

}

