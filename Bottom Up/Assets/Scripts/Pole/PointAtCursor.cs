using UnityEngine;

public class PointAtCursor : MonoBehaviour
{
    private void Update()
    {
        transform.up = GameCursor.WorldPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(GameCursor.WorldPosition, 0.25f);
    }
}
