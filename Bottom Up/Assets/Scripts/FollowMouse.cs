using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Transform mouseTransform;
    public Transform poleTransform;

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
}
