using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : EnemyHealth
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private Transform enemySpawnPos;

    //[SerializeField] private float health;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float timer;
    [SerializeField] private float distanceToSummon;
    [SerializeField] private AudioClip summonNoise;
    private GameObject player;
    Animator m_Animator;
    private bool facingRight = false;
    [SerializeField] private GameObject hurtParticle;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FlipBasedOnMovement();
        //timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < distanceToSummon)
        {
            timer += Time.deltaTime;
            if (timer > spawnDelay)
            {
                timer = 0;
                //shoot();
                m_Animator.SetTrigger("attack");
                //Replace with animation later
            }
        }
        Vector3 fixedPosition = transform.position;
        fixedPosition.z = 0;
        transform.position = fixedPosition;
    }

    void shoot()
    {
        Instantiate(enemyToSpawn, enemySpawnPos.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(summonNoise, transform.position);
    }
    private void FlipBasedOnMovement()
    {

        Vector3 direction = player.transform.position - transform.position;
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
        m_Animator.SetTrigger("hurt");
        Instantiate(hurtParticle, gameObject.transform.position, Quaternion.identity);
        CinemaMachineShake.Instance.ShakeCamera(10, .1f);
        if (health <= 0)
        {
            Die();
        }
        //Play animation
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
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EDamage")
        {
            health--;
        }
    }*/


}


