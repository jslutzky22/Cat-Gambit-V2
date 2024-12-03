using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorrowBoss : EnemyHealth
{
    [SerializeField] private GameObject guitarRiff;
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject zoomCam;
    [SerializeField] private GameObject portalCam;
    [SerializeField] private GameObject[] spawnSpot1;
    [SerializeField] private GameObject[] portals;
    [SerializeField] private GameObject[] enemiesToSpawn;
    private GameObject lastSpawn;
    private List<GameObject> summonedEnemies = new List<GameObject>();
    Animator m_Animator;
    private bool facingRight = true;
    [SerializeField] private float timer;
    [SerializeField] private float attackDelay;
    [SerializeField] private bool canAttack;
    [SerializeField] private GameObject sorrowCard;
    [SerializeField] private GameObject cardSpawnPos;
    [SerializeField] private GameObject arenaWalls;
    [SerializeField] private AudioClip evilCardThrowNoise;
    [SerializeField] private AudioClip portalSummonNoise;
    [SerializeField] private AudioClip teleportNoise;
    [SerializeField] private GameObject hurtParticle;
    [SerializeField] private GameObject victoryObject;
    [SerializeField] private GameObject goRightArrow;
    //[SerializeField] private float delay;

    private GameObject player;
    [SerializeField] private Color hitColor = Color.red;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

    }

    // Update is called once per frame
    void Update()
    {
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
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arena")
        {
           canAttack = true;
            m_Animator.SetBool("Fighting", true);
        }
    }
    /*private void startFight()
    {
        canAttack = true;
    }*/
    private void Shoot()
    {
        int attackType = Random.Range(1, 3); 
        if (attackType == 1)
        {
            //Debug.Log("Attack 1");
            StartCoroutine(cardAttack());
        }
        else
        {
            //Debug.Log("Attack 2");
            StartCoroutine(enemySummon());
        }
    }

    private IEnumerator cardAttack()
    {
        m_Animator.SetTrigger("card");
        yield return new WaitForSeconds(1f);
        AudioSource.PlayClipAtPoint(evilCardThrowNoise, transform.position);
    }
    private IEnumerator enemySummon()
    {
        m_Animator.SetTrigger("summon");
        yield return new WaitForSeconds(1f);
        AudioSource.PlayClipAtPoint(portalSummonNoise, transform.position);
    }
    private void cardShoot()
    {
        StartCoroutine(cardShooting());
        //Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        //canAttack = true;
    }
    private IEnumerator cardShooting()
    {
        Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        StartCoroutine(teleport());
    }
    private void summonEnemy()
    {
        StartCoroutine(summoningEnemy());
    }
    /*private IEnumerator summoningEnemy()
    {
        GameObject randomEnemy = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
        GameObject randomPortal = portals[Random.Range(0, portals.Length)];
        randomPortal.SetActive(true);
        yield return new WaitForSeconds(1f);
        Instantiate(randomEnemy, randomPortal.transform.position, Quaternion.identity);
        summonedEnemies.Add(randomEnemy); // Track the summoned enemy
        Debug.Log($"{randomEnemy.name} summoned at {randomPortal.name}");
        yield return new WaitForSeconds(1f);
        randomPortal.SetActive(false);
        StartCoroutine(teleport());
    }*/
    private IEnumerator summoningEnemy()
    {
        GameObject randomEnemyPrefab = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
        GameObject randomPortal = portals[Random.Range(0, portals.Length)];
        randomPortal.SetActive(true);
        yield return new WaitForSeconds(1f);

       
        GameObject summonedEnemy = Instantiate(randomEnemyPrefab, randomPortal.transform.position, Quaternion.identity);
        summonedEnemies.Add(summonedEnemy);

        //Debug.Log($"{randomEnemyPrefab.name} summoned at {randomPortal.name}");
        yield return new WaitForSeconds(1f);

        randomPortal.SetActive(false);
        StartCoroutine(teleport());
    }

    private IEnumerator teleport()
    {
        yield return new WaitForSeconds(1f);
        GameObject randomSpawn = spawnSpot1[Random.Range(0, spawnSpot1.Length)];
        m_Animator.SetTrigger("teleport");
        AudioSource.PlayClipAtPoint(teleportNoise, transform.position);

        //canAttack = true;

    }
    private void newSpot()
    {
        int randomIndex = Random.Range(0, spawnSpot1.Length);
        if (spawnSpot1[randomIndex] == lastSpawn)
        {
            randomIndex = (randomIndex + 1) % spawnSpot1.Length;
        }
        GameObject randomSpawn = spawnSpot1[randomIndex];
        gameObject.transform.position = randomSpawn.transform.position;
        lastSpawn = randomSpawn;
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
        //CinemaMachineShake.Instance.ShakeCamera(10, .1f);
        Instantiate(hurtParticle, gameObject.transform.position, Quaternion.identity);
        //m_Animator.SetTrigger("hurt");
        if (health <= 0)
        {
            Die();
            
        }
        //Play animation
    }

    private void spawnVictory()
    {
        Instantiate(victoryObject, transform.position, Quaternion.identity);
        //goRightArrow.SetActive(true);
        StartCoroutine(lostBattle());
    }
    private IEnumerator lostBattle()
    {
        guitarRiff.SetActive(true);
        arenaWalls.SetActive(false);
        Time.timeScale = 0.8f;
        zoomCam.SetActive(true);
        //Vector3 spawnPosition = player.transform.position + new Vector3(0, 2f, 0);
        //Instantiate(victoryObject, spawnPosition, Quaternion.identity);
        CinemaMachineShake.Instance.ShakeCamera(40, .1f);
        yield return new WaitForSeconds(1.5f);
        zoomCam.SetActive(false);
        spriteRenderer.color = originalColor;
        
        portalCam.SetActive(true);
        Time.timeScale = 1;
        yield return new WaitForSeconds(1.5f);
        portalCam.SetActive(false);
        zoomCam.SetActive(false);
        mainCam.SetActive(true);
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
        foreach (GameObject enemy in summonedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        summonedEnemies.Clear(); 
        arenaWalls.SetActive(false);
    }

    private void loopingDeath()
    {
        m_Animator.SetBool("Loop", true);
    }
    public void destroy()
    {
        Destroy(gameObject);
    }

}
