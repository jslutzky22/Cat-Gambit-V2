using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HybridSkeleton : EnemyHealth
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
    //[SerializeField] private float distanceToShoot;
    [SerializeField] private float rangedDistance;
    [SerializeField] private float hitstunTime;
    [SerializeField] private float timer;
    [SerializeField] private float shotDelay;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private AudioClip meleeNoise;
    [SerializeField] private AudioClip shootNoise;
    [SerializeField] private GameObject hurtParticle;
    private bool isAttacking;
    Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();

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


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer > engagementDistance)
            {
                agent.ResetPath();
            }
            if (distanceToPlayer <= engagementDistance)
            {
                agent.SetDestination(target.position);
            }

            if (distanceToPlayer <= attackRange)
            {

                agent.ResetPath();
                if (!isAttacking)
                {
                    m_Animator.SetTrigger("attackMelee");

                }
            }
            if (engagementDistance <= distanceToPlayer && distanceToPlayer <= rangedDistance)
            {
                timer += Time.deltaTime;
                if (timer > shotDelay)
                {
                    timer = 0;
                    m_Animator.SetTrigger("attackRanged");
                    //shoot();
                }
            }
            /*else
            {

                agent.SetDestination(target.position);
            }*/
            Vector3 fixedPosition = transform.position;
            fixedPosition.z = 0;
            transform.position = fixedPosition;


            FlipBasedOnMovement();
        }
    }
    private void AttackMelee()
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

        AudioSource.PlayClipAtPoint(meleeNoise, transform.position);

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

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(shootNoise, transform.position);
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
