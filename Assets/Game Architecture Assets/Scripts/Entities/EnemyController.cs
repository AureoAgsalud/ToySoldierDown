using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.AI; //required for NavMeshAgent

public class EnemyController : MonoBehaviour
{
    // - Enemy Properties
    public float moveSpeed = 3f;
    public float attackRange = 3f;
    public float detectionRange = 10f;
    public bool canAttack = true;

    [Header("Combat Settings")]
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float projectileSpeed = 50f;
    [SerializeField] private float attackCooldown = 2f;

    public GameObject target = null;
    public GameObject projectilePrefab = null;
    public GameObject projectileSpawnReference = null;

    //NavMesh
    [HideInInspector] public NavMeshAgent agent;

    // - Ememy States
    EnemyState currentState = null;

    public HealthScript healthScript = null;

    private void Awake()
    {
        healthScript.OnDeath += OnDeathResponse;

        //Configure NavMesh
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = moveSpeed;
            //Stop the agent from fighting your manual rotation calculations
            agent.updateRotation = false;
        }
    }

    private void Start()
    {
        ChangeState(new EnemyIdleState(this));
    }
    private void Update()
    {
        if (currentState != null)
            currentState.OnStateUpdate();
    }
    public void ChangeState(EnemyState newState)
    {
        // - Call the current state's OnStateExit method before changing state
        if(currentState != null)
        currentState.OnStateExit();

        // - Set the new state
        currentState = newState;

        // - Call the new state's OnStateEntered method
        currentState.OnStateEntered();  
    }

    public void OnDeathResponse()
    {
        Destroy(this.gameObject);
    }
    public float GetAttackDamage() => attackDamage;
    public float GetProjectileSpeed() => projectileSpeed;
    public float GetAttackCooldown() => attackCooldown; 
}
