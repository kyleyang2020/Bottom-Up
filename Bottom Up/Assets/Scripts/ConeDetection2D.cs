using UnityEngine;

public class ConeDetection2D : MonoBehaviour
{
    public float detectionRadius = 10f;  // How far the enemy can see
    public float detectionAngle = 60f;  // Cone angle in degrees
    public Transform target;            // The player or object to detect
    public LayerMask obstructionMask;   // Mask for obstacles that block vision

    private Mesh coneMesh;              // Mesh for visualizing the detection cone
    public Color coneColor = new Color(1, 1, 0, 0.3f); // Transparent yellow color

    public float moveSpeed = 3f;             // Speed of the enemy
    public float rotationSpeed = 200f;       // Speed at which the enemy rotates
    private bool isChasing = false;

    void Start()
    {
        // Create a new mesh for the cone
        coneMesh = new Mesh();
    }

    void Update()
    {
        DrawCone();
        if (IsTargetInCone())
        {
            Debug.Log("Player detected!");
            // Add behavior for when the player is detected
        }

        // Check if the player is detected
        if (IsTargetInCone())
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // Handle chasing behavior
        if (isChasing)
        {
            ChaseTarget();
        }
    }

    public bool IsTargetInCone()
    {
        // Calculate direction to the target
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        // Check if target is within radius
        if (Vector2.Distance(transform.position, target.position) > detectionRadius)
            return false;

        // Check if target is within angle
        float angleToTarget = Vector2.Angle(transform.right, directionToTarget);
        if (angleToTarget > detectionAngle / 2)
            return false;

        // Check if there's a clear line of sight
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, detectionRadius, obstructionMask);
        if (hit.collider != null && hit.collider.transform != target)
            return false; // Something is obstructing the view

        return true; // Target is detected
    }

    void ChaseTarget()
    {
        // Calculate the direction to the target
        Vector2 direction = (target.position - transform.position).normalized;

        // Rotate smoothly toward the target
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);

        // Move toward the target
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void DrawCone()
    {
        // Generate the vertices for the cone mesh
        int segmentCount = 30; // More segments for smoother cone
        Vector3[] vertices = new Vector3[segmentCount + 2];
        int[] triangles = new int[segmentCount * 3];

        // Center point of the cone
        vertices[0] = Vector3.zero;

        // Calculate vertices around the cone
        float angleStep = detectionAngle / segmentCount;
        for (int i = 0; i <= segmentCount; i++)
        {
            float currentAngle = -detectionAngle / 2 + angleStep * i;
            float radian = currentAngle * Mathf.Deg2Rad;
            Vector3 vertex = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * detectionRadius;
            vertices[i + 1] = transform.InverseTransformPoint(transform.position + vertex);
        }

        // Define the triangles for the mesh
        for (int i = 0; i < segmentCount; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        // Update the mesh
        coneMesh.Clear();
        coneMesh.vertices = vertices;
        coneMesh.triangles = triangles;
        coneMesh.RecalculateNormals();

        // Draw the cone using the MeshRenderer
        Graphics.DrawMesh(coneMesh, transform.position, transform.rotation, GetConeMaterial(), 0);
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the detection cone in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Vector3 coneStart = Quaternion.Euler(0, 0, -detectionAngle / 2) * transform.right * detectionRadius;
        Vector3 coneEnd = Quaternion.Euler(0, 0, detectionAngle / 2) * transform.right * detectionRadius;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + coneStart);
        Gizmos.DrawLine(transform.position, transform.position + coneEnd);
    }

    Material GetConeMaterial()
    {
        Material mat = new Material(Shader.Find("Sprites/Default"));
        mat.color = coneColor;
        return mat;
    }
}
