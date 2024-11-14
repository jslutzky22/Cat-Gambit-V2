using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootSkull : EnemyHealth
{
    NavMeshAgent agent;
    private Transform target;
    private bool facingRight = false;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackDelay;
    [SerializeField] private float engagementDistance;
    [SerializeField] private float hitstunTime;
    [SerializeField] private GameObject hurtParticle;
    private bool spawning;


    private bool isAttacking;
    Animator m_Animator;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        //rb.constraints = RigidbodyConstraints2D.Fr;


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }


        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //Debug.Log(transform.position);
    }

    public void spawnDelay()
    {
        agent.ResetPath();
        spawning = true;
    }
    public void startHunt()
    {
        agent.SetDestination(target.position);
        spawning = false;
    }


    private void Update()
    {
        
  
            
        if (target != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer > engagementDistance)
            {
                agent.ResetPath();
                m_Animator.SetBool("Run", false);
            }
            if (distanceToPlayer <= attackRange && spawning == false)
            {
                //m_Animator.SetTrigger("Attack");
                agent.ResetPath();
                m_Animator.SetBool("Run", false);
                //Debug.Log("INRange");
                if (!isAttacking)
                {
                    m_Animator.SetTrigger("Attack");

                }
            }
            if (spawning == false && distanceToPlayer >= attackRange && distanceToPlayer < engagementDistance)
            {
                agent.SetDestination(target.position);
                m_Animator.SetBool("Run", true);
            }


            /*else
            {
                m_Animator.SetBool("Run", true);
                agent.SetDestination(target.position);
            }*/
            Vector3 fixedPosition = transform.position;
            fixedPosition.z = 0;
            transform.position = fixedPosition;

            FlipBasedOnMovement();
        }
    }

    private void Attack()
    {
        if (isAttacking == false)
        {
            isAttacking = true;
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<Player>().takeDamage(attackDamage);
            }
        }


        StartCoroutine(AttackCooldown());

    }
    private IEnumerator AttackCooldown()
    {
         
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }





    private void FlipBasedOnMovement()
    {

        Vector3 direction = agent.desiredVelocity;


        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }

        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }


    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public override void takeDamage(int damage)
    {
        base.takeDamage(damage);
        //agent.ResetPath();
        //m_Animator.SetTrigger("hurt");
        Instantiate(hurtParticle, gameObject.transform.position, Quaternion.identity);
        StartCoroutine(hitStun());
        if (health <= 0)
        {
            Die();
        }
        // Play animation
    }

    private IEnumerator hitStun()
    {
        agent.ResetPath();
        yield return new WaitForSeconds(hitstunTime);
        agent.SetDestination(target.position);
    }



    public override void Die()
    {
        base.Die();
        m_Animator.SetBool("Dead", true);
        //Destroy(gameObject);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

}
