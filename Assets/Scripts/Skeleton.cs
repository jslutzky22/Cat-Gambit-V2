using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : EnemyHealth
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
    [SerializeField] private float skeletonSpeed;
    [SerializeField] private AudioClip attackNoise;
    [SerializeField] private GameObject hurtParticle;
    [SerializeField] private Color hitColor = Color.red;
    private Color originalColor;

    //[SerializeField] private ParticleSystem particlesHurt;
    //private ParticleSystem ps;

    private bool isAttacking;
    Animator m_Animator;
  
    void Start()
    {
        //ps = GetComponent<ParticleSystem>();
        //ps.Stop();
        //agent.speed = skeletonSpeed;
        m_Animator = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;



        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }

       
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    
    private void Update()
    {
        if (target != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer > engagementDistance)
            {
                agent.ResetPath();
                agent.speed = 0f;
            }
            else if (isAttacking == false)
            {
                agent.SetDestination(target.position);
                agent.speed = skeletonSpeed;
                m_Animator.SetBool("Run", true);
            }

            if (distanceToPlayer <= attackRange)
            {
                agent.speed = 0;
                agent.ResetPath();
                if (!isAttacking)
                {
                    m_Animator.SetTrigger("Attack");
                   
                }
            }
            else if (isAttacking == false)
            {
                agent.SetDestination(target.position);
                agent.speed = skeletonSpeed;
            }
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

        AudioSource.PlayClipAtPoint(attackNoise, transform.position);

        StartCoroutine(AttackCooldown());

    }
    private IEnumerator AttackCooldown()
    {
       
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        agent.speed = skeletonSpeed;
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
        //ps.Play();
        m_Animator.SetTrigger("hurt");
        CinemaMachineShake.Instance.ShakeCamera(10, .1f);
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
        agent.speed = 0; spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(hitstunTime);
        //ps.Stop();
        agent.speed = skeletonSpeed;
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