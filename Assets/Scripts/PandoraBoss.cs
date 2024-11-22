using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PandoraBoss : EnemyHealth
{
    NavMeshAgent agent;
    [SerializeField] private GameObject[] flySpots;
    private GameObject lastSpot;
    [SerializeField] private GameObject[] snakePortals;
    [SerializeField] private GameObject[] firePillars1;
    [SerializeField] private GameObject[] firePillars2;
    [SerializeField] private GameObject mageBall;
    Animator m_Animator;
    private GameObject player;
    [SerializeField] private Color hitColor = Color.red;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private bool facingRight = false;
    [SerializeField] private float timer;
    [SerializeField] private float attackDelay;
    [SerializeField] private bool canAttack;
    [SerializeField] private GameObject sorrowCard;
    [SerializeField] private GameObject cardSpawnPos;
    [SerializeField] private GameObject arenaWalls;
    [SerializeField] private AudioClip magicProjectileNoise;
    [SerializeField] private AudioClip magicSpellNoise;
    [SerializeField] private AudioClip snakePortalNoise;
    [SerializeField] private GameObject hurtParticle;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;


    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        FlipBasedOnMovement();
        if (canAttack == true)
        {
            timer += Time.deltaTime;
            if (timer > attackDelay)
            {
                timer = 0;
                //m_Animator.SetTrigger("attack");
                canAttack = false;
                //StartCoroutine(knightCooldown());
                Shoot();
            }
        }
        Vector3 fixedPosition = transform.position;
        fixedPosition.z = 2;
        transform.position = fixedPosition;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arena")
        {
            canAttack = true;
            m_Animator.SetBool("Fighting", true);
        }
    }
    private void Shoot()
    {
        int attackType = Random.Range(1, 5);
        if (attackType == 1)
        {
            //Debug.Log("CardThrow");
            //StartCoroutine(cardAttack());
            m_Animator.SetTrigger("card");
        }
        if (attackType == 2)
        {
            //Debug.Log("MageBallSpawn");
            m_Animator.SetTrigger("mageBall");
            //StartCoroutine(mageBall());
        }
        if (attackType == 3)
        {
            //Debug.Log("FirePillarSpawn");
            m_Animator.SetTrigger("firePillar");
            //StartCoroutine(enemySummon());
        }
        if (attackType == 4)
        {
            //Debug.Log("SnakePortalSPawn");
            m_Animator.SetTrigger("snakePortal");
            //StartCoroutine(enemySummon());
        }
    }

    /*private IEnumerator cardAttack()
    {
        //m_Animator.SetTrigger("card");
        //yield return new WaitForSeconds(1f);

    }*/
    private void cardSpawn()
    {
        StartCoroutine(cardShooting());
        //Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        //canAttack = true;
    }
    private IEnumerator cardShooting()
    {
        Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(magicSpellNoise, transform.position);
        yield return new WaitForSeconds(0.2f);
        Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(magicSpellNoise, transform.position);
        yield return new WaitForSeconds(0.2f);
        Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(magicSpellNoise, transform.position);
        yield return new WaitForSeconds(1f);
        StartCoroutine(moveTo());
    }
    /*private IEnumerator cardAttack()
    {
        m_Animator.SetTrigger("card");
        //yield return new WaitForSeconds(1f);

    }*/
    private void mageBallSpawn()
    {
        StartCoroutine(mageBallShooting());
        //Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        //canAttack = true;
    }
    private IEnumerator mageBallShooting()
    {
        Instantiate(mageBall, player.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(magicSpellNoise, transform.position);
        yield return new WaitForSeconds(0.5f);
        Instantiate(mageBall, player.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(magicSpellNoise, transform.position);
        yield return new WaitForSeconds(0.5f);
        Instantiate(mageBall, player.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(magicSpellNoise, transform.position);
        yield return new WaitForSeconds(1f);
        StartCoroutine(moveTo());
    }

    private void firePillarSpawn()
    {
        StartCoroutine(firePillarShooting());
        //Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        //canAttack = true;
    }
    private IEnumerator firePillarShooting()
    {
        foreach (GameObject firePillars in firePillars1)
        {
            firePillars.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        foreach (GameObject firePillars in firePillars2)
        {
            firePillars.SetActive(true);
        }
        StartCoroutine(moveTo());
    }
    private void snakePortalSpawn()
    {
        StartCoroutine(snakePortalShooting());
        //Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        //canAttack = true;
    }
    private IEnumerator snakePortalShooting()
    {
        // Choose the first random portal index
        int portalSpot1 = Random.Range(0, snakePortals.Length);

        // Choose a second portal that isn't the same as the first
        int portalSpot2;
        do
        {
            portalSpot2 = Random.Range(0, snakePortals.Length);
        } while (portalSpot2 == portalSpot1);

        // Activate the selected portals
        snakePortals[portalSpot1].SetActive(true);
        snakePortals[portalSpot2].SetActive(true);

        AudioSource.PlayClipAtPoint(snakePortalNoise, transform.position);

        yield return new WaitForSeconds(1f);

        // Continue with the next action
        StartCoroutine(moveTo());
        yield return new WaitForSeconds(3f);
        snakePortals[portalSpot1].SetActive(false);
        snakePortals[portalSpot2].SetActive(false);
    }


    private IEnumerator moveTo()
    {
        if (agent == null)
        {
            yield break; // Exit if agent is not initialized
        }

        // Choose a random fly spot that isn't the last one visited
        int randomIndex = Random.Range(0, flySpots.Length);
        if (flySpots[randomIndex] == lastSpot)
        {
            randomIndex = (randomIndex + 1) % flySpots.Length;
        }

        // Set the chosen fly spot as the destination, preserving the current Z position
        GameObject targetSpot = flySpots[randomIndex];
        Vector3 targetPosition = new Vector3(targetSpot.transform.position.x, targetSpot.transform.position.y, transform.position.z);
        agent.SetDestination(targetPosition);

        // Update lastSpot to the current destination
        lastSpot = targetSpot;

        // Wait until the agent has reached the destination
        yield return new WaitForSeconds(1f);

        canAttack = true;
    }


    private void newSpot()
    {
        //CODE FOR PANDORA MOVING
        int randomIndex = Random.Range(0, flySpots.Length);
        if (flySpots[randomIndex] == lastSpot)
        {
            randomIndex = (randomIndex + 1) % flySpots.Length;
        }
        GameObject randomSpawn = flySpots[randomIndex];
        gameObject.transform.position = randomSpawn.transform.position;
        lastSpot = randomSpawn;
        canAttack = true;
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
        StartCoroutine(gotHit());
        Instantiate(hurtParticle, gameObject.transform.position, Quaternion.identity);
        //CinemaMachineShake.Instance.ShakeCamera(10, .1f);
        //m_Animator.SetTrigger("hurt");
        if (health <= 0)
        {
            Die();
        }
        //Play animation
    }
    private IEnumerator gotHit()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    public override void Die()
    {
        base.Die();
        m_Animator.SetBool("Dead", true);
        arenaWalls.SetActive(false);
    }

    public void destroy()
    {
        SceneManager.LoadScene("winScreen");
        Destroy(gameObject);
    }
}
