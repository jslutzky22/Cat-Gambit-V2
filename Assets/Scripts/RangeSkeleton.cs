using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RangeSkeleton : EnemyHealth
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    //[SerializeField] private float health;
    [SerializeField] private float shotDelay;
    [SerializeField] private float timer;
    [SerializeField] private float distanceToShoot;
    [SerializeField] private AudioClip shotNoise;
    [SerializeField] private GameObject hurtParticle;
    private GameObject player;
    Animator m_Animator;
    private bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FlipBasedOnMovement();
        //timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < distanceToShoot)
        {
            timer += Time.deltaTime;
            if (timer > shotDelay)
            {
                timer = 0;
                m_Animator.SetTrigger("attack");
                //shoot();
            }
        }
        Vector3 fixedPosition = transform.position;
        fixedPosition.z = 0;
        transform.position = fixedPosition;

    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(shotNoise, transform.position);
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

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EDamage")
        {
            health--;
        }
    }*/


}
