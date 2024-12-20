using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStateManager : MonoBehaviour
{
    [Header("Enemy State Duration Variables")]
    public float aggroDistance;
    public float deaggroDistance;
    public float attackDistance;
    public float idleAtPlayerLastPositionDuration;
    public float deleteTimer;
    public Transform spawnpoint;

    [Header("Enemy Projectile Variables")]
    public float turnSpeed; // speed of the enemy turn to attack player
    public PlayerInRange attackRange;

    [Header("References")]
    [HideInInspector] public Renderer render;
    private NavMeshAgent agent;
    public Timer timer = new Timer();
    private Transform playerTransform;
    private Rigidbody2D rb;


    // all the states of the enemies
    public BaseStateEnemy currentState;
    public IdleStateEnemy idleState;
    public AggroStateEnemy aggroState;
    public AttackStateEnemy attackState;
    public DamagedStateEnemy damagedState;
    public DeathStateEnemy deathState;


    void Start()
    {
        idleState = new IdleStateEnemy(aggroDistance);
        aggroState = new AggroStateEnemy(deaggroDistance, attackDistance);
        attackState = new AttackStateEnemy(attackDistance);
        damagedState = new DamagedStateEnemy(rb, 0.5f, Vector2.zero);
        deathState = new DeathStateEnemy();

        rb = GetComponent<Rigidbody2D>();

        if (spawnpoint == null)
        {
            spawnpoint = transform;
        }

        // setting references
        render = GetComponentInChildren<Renderer>();
        playerTransform = FindObjectOfType<Movmement2D>().transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // starting state for the state machine, aka idle
        currentState = idleState;
        // "this" is a ref to the context (this EXACT monobehavior script)
        // will call logic from EnterState
        currentState.EnterState(this);

    }

    void Update()
    {
        // will call any logic in UpdateState from the current state every frame
        timer.UpdateTimer();
        currentState.UpdateState(this);
    }

    // checks if ray is hitting at a given distance and returns a bool because of it with color
    public bool RayCastCheckGizmo(float distance)
    {
        if (Physics.Raycast(transform.position, (playerTransform.transform.position - transform.position), out RaycastHit hitInfo, distance))
        {
            // draws the ray in scene when hit, RED
            Debug.DrawRay(transform.position, (playerTransform.transform.position - transform.position) * hitInfo.distance, Color.red);
            return true;
        }
        else
        {
            // draws the ray in scene when NOT hit, GREEN
            Debug.DrawRay(transform.position, (playerTransform.transform.position - transform.position) * distance, Color.green);
            return false;
        }
    }

    
    // checks if ray is hitting at a given distance and returns a bool because of it
    public bool RayCastCheck(float distance)
    {
        return (playerTransform.position - transform.position).magnitude < distance;
    }
    

    #region Enemy Movement 

    // move towards the player
    public void MoveTowardsPlayer()
    {
        Debug.Log("moving towards player");
        agent.SetDestination(playerTransform.position);
    }

    // move to the original position of enemy
    public void MoveOriginalPosition()
    {
        Debug.Log("moving to original position");
        agent.SetDestination(spawnpoint.position);
    }

    // stops the enemy/nav mesh agent
    public void StopPosition()
    {
        agent.SetDestination(transform.position);
    }

    #endregion

    #region State Switching

    // changes the state of the enemy
    public void SwitchState(BaseStateEnemy state)
    {
        // change the state then call the EnterState from the new state
        currentState = state;
        currentState.EnterState(this);
    }

    // switches state to idle
    public void Idle()
    {
        SwitchState(idleState);
    }

    // after a certain amount of time switch to idle, mimics the deaggro time where they are standing still
    public void SwitchToIdle()
    {
        if (!timer.IsActive())
            timer.StartTimer(idleAtPlayerLastPositionDuration, Idle);
    }

    // switch state to deathstate
    public void Death()
    {
        SwitchState(deathState);
    }
    #endregion

    public void DeleteOnDeath()
    {
        Destroy(gameObject);
    }

    public void ApplyKnockback(Vector2 direction, float force, float duration)
    {
        SwitchState(damagedState);
        Vector2 knockbackForce = direction.normalized * force;
        damagedState = new DamagedStateEnemy(rb, duration, knockbackForce); // Reinitialize with new parameters
    }

    private void OnDrawGizmos()
    {
        Color holder = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, deaggroDistance);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = holder;
    }

    public void LookAtPlayer()
    {
        // set speed, will aimbot the player
        //this.transform.LookAt(playerTransform);

        Vector3 targetPlayer = playerTransform.position - transform.position;
        targetPlayer.y = 0;
        float step = turnSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetPlayer, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public bool DetectPlayer()
    {
        if (attackRange.playerHit)
        {
            Debug.Log("player hit");
            return true;
        }
        else
        {
            Debug.Log("no player");
            return false;
        }
    }
}