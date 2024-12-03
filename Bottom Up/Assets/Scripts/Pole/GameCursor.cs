using UnityEngine;

public static class GameCursor
{
    public static Vector2 WorldPosition
    {
        get
        {
            float distance;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_gamePlane.Raycast(ray, out distance))
                return ray.GetPoint(distance);
            // Ray did not hit plane
            return Vector2.zero;
        }
    }
    private static Plane _gamePlane = new Plane(Vector3.forward, 0);
}
