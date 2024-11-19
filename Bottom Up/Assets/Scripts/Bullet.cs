using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 moveForce;
    public Rigidbody rigidBody;
    public float damage;
    [SerializeField] float maxDistanceTravel;

    Vector3 spawnPos;
    private void Start()
    {
        spawnPos = transform.position;
    }
    private void FixedUpdate()
    {
        rigidBody.velocity = moveForce;
    }
    private void Update()
    {
        // Destorying itself when it travels far enough
        if ((spawnPos - transform.position).magnitude > maxDistanceTravel)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }
}