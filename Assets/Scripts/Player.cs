using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private bool isTutorial;
    private int tutorialValue;
    private bool pauseOn;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerSlowedSpeed;
    private float maxSpeed;
    private float vertical;
    private float horizontal;
    //[SerializeField] private GameObject playerBoot;
    //[SerializeField] private Collider2D playerBootCollider;
    private bool canThrow = true;
    [SerializeField] private float throwDelay;
    private bool restartPressed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpSlowForce;
    private float maxJump;
    //private float jumpBufferTime = 0.4f;
    //private float jumpBufferCounter;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private bool jumpPressed;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int health;
    [SerializeField] private float healingRats;
    [SerializeField] private bool holdingCharge;
    [SerializeField] private float chargeAmount;
    [SerializeField] private float chargeDuration;
    [SerializeField] private float guardTime;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject health1;
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;
    [SerializeField] private GameObject health4;
    [SerializeField] private GameObject hurtCanvas;
    [SerializeField] private GameObject healthCanvas;
    [SerializeField] private GameObject dragonSpawn;
    [SerializeField] private GameObject knightSpawn;
    [SerializeField] private GameObject fairySpawn;
    [SerializeField] private GameObject werewolfSpawn;
    [SerializeField] private GameObject guardKnight;
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject dustParticles;
    //[SerializeField] private ParticleSystem dust;
    //[SerializeField] private GameObject upArrowBlack;
    [SerializeField] private GameObject upArrowRed;
    [SerializeField] private GameObject upMonsterReady;
    //[SerializeField] private GameObject upMonsterUsed;
    //[SerializeField] private GameObject rightArrowBlack;
    [SerializeField] private GameObject rightArrowRed;
    [SerializeField] private GameObject rightMonsterReady;
    //[SerializeField] private GameObject rightMonsterUsed;
    //[SerializeField] private GameObject downArrowBlack;
    [SerializeField] private GameObject downArrowRed;
    //[SerializeField] private GameObject downMonsterUsed;
    [SerializeField] private GameObject downMonsterReady;
    //[SerializeField] private GameObject leftArrowBlack;
    [SerializeField] private GameObject leftArrowReady;
    [SerializeField] private GameObject leftMonsterReady;
    //[SerializeField] private GameObject leftMonsterUsed;
    Animator m_Animator;
    private bool isFacingRight = true;
    private bool canLWolf = true;
    private bool arenaStart;
    [SerializeField] private float leftWerewolfCooldown;
    private bool canRWolf = true;
    [SerializeField] private float rightWerewolfCooldown;
    private bool canDKnight = true;
    [SerializeField] private float downKnightCooldown;
    private bool canUFairy = true;
    [SerializeField] private float upFairyCooldown;
    private bool canNDragon = true;
    [SerializeField] private float neutralDragonCooldown;
    [SerializeField] private float dashingCooldown;
    private bool canDash = true;
    private bool canJump = true;
    private bool isDashing;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float verticalLimit;
    private bool isdead;
    //[SerializeField] private float arenaDelayTime;
    [SerializeField] private float screenShakeAmount1Damage;
    [SerializeField] private float screenShakeAmount2Damage;
    [SerializeField] private TMP_Text ratsText;
    [SerializeField] private CardSpawner rightCard;
    [SerializeField] private CardSpawner leftCard;
    [SerializeField] private CardSpawner upCard;
    [SerializeField] private CardSpawner downCard;
    [SerializeField] private GameObject dragon;
    [SerializeField] private AudioClip cardNoise1;
    [SerializeField] private AudioClip cardNoise2;
    [SerializeField] private AudioClip jumpNoise;
    [SerializeField] private AudioClip hurtNoise;
    [SerializeField] private AudioClip ratNoise;
    [SerializeField] private AudioClip webNoise;
    [SerializeField] private AudioClip spillNoise;
    [SerializeField] private UnityEngine.UI.Image dragonBar;
    [SerializeField] private GameObject unleashedDragon;
    private bool bounced;


    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 3;
        if (isTutorial == false)
        {
            {
                tutorialValue = 10;
            }
        }
        m_Animator = gameObject.GetComponent<Animator>();
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        maxSpeed = playerSpeed;
        maxJump = jumpForce;
        ratsText.text = "Rats: " + healingRats;
        dragonBar.fillAmount = chargeAmount / 100f;
        if (health >= 4)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(true);
        }
        if (health == 3)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(true);
            health4.SetActive(false);
        }
        if (health == 2)
        {
            health1.SetActive(false);
            health2.SetActive(true);
            health3.SetActive(false);
            health4.SetActive(false);
        }
        if (health == 1)
        {
            health1.SetActive(true);
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.activeInHierarchy == false)
        {
            pauseOn = false;
        }

        if (horizontal != 0)
        {
            m_Animator.SetBool("run", true);
            if (isGrounded())
            {
                dustParticles.SetActive(true);
                //dust.Play();
            }
            else
            {
                //dust.Stop();
                dustParticles.SetActive(false);
            }
            
        }
        else
        {
            m_Animator.SetBool("run", false);
            dustParticles.SetActive(false);
        }
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        /*if (jumpPressed == true)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            //jumpPressed = false;
            jumpBufferCounter -= Time.deltaTime;
        }*/
        if (isDashing != true && arenaStart == false)
        {
            rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
        }
        // rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
        if (rb.velocity.y > verticalLimit && bounced == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, verticalLimit);
        }

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bounce")
        {
            StartCoroutine(Bouncing());
            //player feet collider
            /*if (collision.gameObject.tag == "Spike")
            {
                StartCoroutine(takeDamage());
            }*/
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tutorial")
        {
            //StartCoroutine(takeDamage());
            tutorialValue += 1;
        }
        //normalHurtbox
        if (collision.gameObject.tag == "Spike")
        {
            //StartCoroutine(takeDamage());
            takeDamage(1);
            AudioSource.PlayClipAtPoint(spillNoise, transform.position);
        }
        if (collision.gameObject.tag == "Web")
        {
            playerSpeed = playerSlowedSpeed;
            jumpForce = jumpSlowForce;
            rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y);
            AudioSource.PlayClipAtPoint(webNoise, transform.position);
        }
        if (collision.gameObject.tag == "Rat")
        {
            healingRats++;
            ratsText.text = "Rats: " + healingRats;
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(ratNoise, transform.position);
        }
        if (collision.gameObject.tag == "Arena")
        {
            m_Animator.SetBool("Arena", true);
            arenaStart = true;
            rb.velocity = new Vector2(0, 0);
            playerSpeed = 0f;
            horizontal = 0;


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arena")
        {
            rb.velocity = new Vector2(0, 0);
            horizontal = 0;
            m_Animator.SetBool("Arena", false);
            //Debug.Log("PlayerSpeedCorrected");
            playerSpeed = maxSpeed;
            arenaStart = false;
        }
        if (collision.gameObject.tag == "Spike")
        {
            //playerSpeed /= 2;
        }
        if (collision.gameObject.tag == "Web")
        {
            playerSpeed = maxSpeed;
            jumpForce = maxJump;
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
        if (arenaStart == false)
        {
            horizontal = context.ReadValue<Vector2>().x;
            vertical = context.ReadValue<Vector2>().y;
        }
        //horizontal = context.ReadValue<Vector2>().x;
       // vertical = context.ReadValue<Vector2>().y;
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (arenaStart == false)
        {
            if (context.canceled)
            {
                return;
            }
            if (canDash == true && isdead != true && pauseOn == false)
            {
   
                StartCoroutine(Dash());
            }
           
        }

    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && coyoteTimeCounter > 0f && arenaStart == false && canJump == true && pauseOn == false)
        {
            AudioSource.PlayClipAtPoint(jumpNoise, transform.position);
            
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);                                                   
        }
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            m_Animator.SetBool("jump", false);
            return true;
        }
        else
        {
            m_Animator.SetBool("jump", true);
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
    public void Quit(InputAction.CallbackContext context)
    {
        if (pauseOn == true)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            pauseOn = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            pauseOn = true;
        }
        //Application.Quit();
        /*Debug.Log("Pause");
        pauseMenu.SetActive(true);
        Time.timeScale = 0;*/
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if (pauseOn == false)
        {
            if (context.started && vertical == 0 && horizontal == 0 && canNDragon && arenaStart == false && tutorialValue >= 4)
            {
                // Debug.Log("NeutralCard");
                holdingCharge = true;
                StartCoroutine(Charging(chargeDuration));
                //holdingCharge = true;
                //Debug.Log("Firing");
            }
            if (context.started && vertical == 1 && canThrow == true && canUFairy && arenaStart == false && tutorialValue >= 2)
            {
                AudioSource.PlayClipAtPoint(cardNoise1, transform.position);
                //Debug.Log("UpCard");
                StartCoroutine(shooting());
                StartCoroutine(fairyCooldown());
                //Debug.Log("Firing");
            }
            if (context.started && vertical == -1 && canThrow == true && canDKnight && arenaStart == false && tutorialValue >= 3)
            {

                //Debug.Log("DownCard");
                StartCoroutine(shooting());
                StartCoroutine(knightGuard());
                StartCoroutine(knightCooldown());
                //Debug.Log("Firing");
            }
            if (context.started && horizontal == 1 && canThrow == true && canRWolf && arenaStart == false && tutorialValue >= 1)
            {
                //Debug.Log("RightCard");
                StartCoroutine(shooting());
                StartCoroutine(rWolfCooldown());
                //Debug.Log("Firing");
            }
            if (context.started && horizontal == -1 && canThrow == true && canLWolf && arenaStart == false && tutorialValue >= 2)
            {
                //Debug.Log("LeftCard");
                StartCoroutine(shooting());
                StartCoroutine(lWolfCooldown());
                //Debug.Log("Firing");
            }
            if (context.canceled)
            {
                //Debug.Log("released!!");
                if (chargeAmount == 100)
                {
                    AudioSource.PlayClipAtPoint(cardNoise2, transform.position);
                    //CardSpawner bullet = Instantiate(upCard, transform.position, quaternion.identity);
                    GameObject spawnedDragon = Instantiate(dragon, dragonSpawn.transform.position, Quaternion.identity);
                    {
                        if (!isFacingRight)
                        {
                            Vector3 dragonScale = spawnedDragon.transform.localScale;
                            dragonScale.x *= -1f;
                            spawnedDragon.transform.localScale = dragonScale;
                        }

                    }
                    m_Animator.SetTrigger("chargeCard");
                   // Debug.Log("ChargedCard!");
                    unleashedDragon.SetActive(true);
                    StartCoroutine(dragonCooldown());
                }
                else
                {
                    if (isFacingRight == true && canThrow == true && canRWolf && arenaStart == false && tutorialValue >= 1)
                    {
                        StartCoroutine(shooting());
                        StartCoroutine(rWolfCooldown());
                    }
                    if (isFacingRight == false && canThrow == true && canLWolf && arenaStart == false && tutorialValue >= 1)
                    {
                        StartCoroutine(shooting());
                        StartCoroutine(lWolfCooldown());
                    }
                }
                chargeAmount = 0;
                dragonBar.fillAmount = chargeAmount / 100f;
                canThrow = true;
                holdingCharge = false;
                //Debug.Log("Not firing");
            }
        }
        
    }
    public void Heal(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded() && arenaStart == false && pauseOn == false)
        {
            if (healingRats > 0 && health < 4)
            {
                m_Animator.SetTrigger("heal");
                health++;
                healingRats--;
                ratsText.text = "Rats: " + healingRats;
                if (health == 3)
                {
                    health1.SetActive(false);
                    health2.SetActive(false);
                    health3.SetActive(true);
                    health4.SetActive(false);
                }
                if (health == 2)
                {
                    health1.SetActive(false);
                    health2.SetActive(true);
                    health3.SetActive(false);
                    health4.SetActive(false);
                }
                if (health >= 4)
                {
                    health1.SetActive(false);
                    health2.SetActive(false);
                    health3.SetActive(false);
                    health4.SetActive(true);
                }
            }
        }
    }
    public void Restart(InputAction.CallbackContext context)
    {
        if (restartPressed == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            restartPressed = true;
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

        /*Vector3 dragonScale = dragonSpawn.transform.localScale;
        dragonScale.x *= -1f;
        dragonSpawn.transform.localScale = dragonScale;*/

    }
    /*private IEnumerator arenaDelay()
    {
        //ANIMATION FOR THE FIGHT INTRO
        arenaStart = true;
        playerSpeed = 0f;
        //yield return new WaitForSeconds(arenaDelayTime);
        playerSpeed = maxSpeed;
        arenaStart = false;
    }*/
    private IEnumerator shooting()
    {
        //Debug.Log("Firing");
        canThrow = false;
        yield return new WaitForSeconds(throwDelay);
        canThrow = true;
    }
    private IEnumerator fairyCooldown()
    {
        upArrowRed.SetActive(false);
        upMonsterReady.SetActive(false);
        AudioSource.PlayClipAtPoint(cardNoise2, transform.position);
        CardSpawner bullet = Instantiate(upCard, fairySpawn.transform.position, quaternion.identity);
        m_Animator.SetTrigger("upCard");
        canUFairy = false;
        yield return new WaitForSeconds(upFairyCooldown);
        upArrowRed.SetActive(true);
        upMonsterReady.SetActive(true);
        canUFairy = true;
    }
    private IEnumerator dragonCooldown()
    {
        canNDragon = false;
        chargeAmount = 0;
        dragonBar.fillAmount = chargeAmount / 100f;
        yield return new WaitForSeconds(neutralDragonCooldown);
        unleashedDragon.SetActive(false);
        canNDragon = true;
    }
    private IEnumerator lWolfCooldown()
    {
        leftArrowReady.SetActive(false);
        leftMonsterReady.SetActive(false);
        AudioSource.PlayClipAtPoint(cardNoise1, transform.position);
        CardSpawner bullet = Instantiate(leftCard, werewolfSpawn.transform.position, quaternion.identity);
        //m_Animator.SetTrigger("fowardCard");
        m_Animator.SetTrigger("sideCard");
        canLWolf = false;
        yield return new WaitForSeconds(leftWerewolfCooldown);
        leftArrowReady.SetActive(true);
        leftMonsterReady.SetActive(true);
        canLWolf = true;
    }
    private IEnumerator rWolfCooldown()
    {
        rightArrowRed.SetActive(false);
        rightMonsterReady.SetActive(false);
        AudioSource.PlayClipAtPoint(cardNoise1, transform.position);
        CardSpawner bullet = Instantiate(rightCard, werewolfSpawn.transform.position, quaternion.identity);
        m_Animator.SetTrigger("sideCard");
        //Debug.Log("Word");
        canRWolf = false;
        yield return new WaitForSeconds(rightWerewolfCooldown);
        rightMonsterReady.SetActive(true);
        rightArrowRed.SetActive(true);
        canRWolf = true;
    }
    private IEnumerator knightCooldown()
    {
        downArrowRed.SetActive(false);
        downMonsterReady.SetActive(false);
        CardSpawner bullet = Instantiate(downCard, knightSpawn.transform.position, quaternion.identity);
        m_Animator.SetTrigger("downCard");
        canDKnight = false;
        yield return new WaitForSeconds(downKnightCooldown);
        downArrowRed.SetActive(true);
        downMonsterReady.SetActive(true);
        canDKnight = true;
    }
    private IEnumerator knightGuard()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        guardKnight.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        //spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(guardTime);
        guardKnight.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Player");
        //spriteRenderer.color = Color.white;
    }
    private IEnumerator Charging(float chargeDuration)
    {
        float elapsedTime = 0;
        chargeAmount = 0;

        //Debug.Log("Charging started");

        while (holdingCharge == true && chargeAmount < 100)
        {
            elapsedTime += Time.deltaTime;
            dragonBar.fillAmount = chargeAmount / 100f;
            chargeAmount = Mathf.Lerp(0, 100, elapsedTime / chargeDuration);
            //Debug.Log("Charge Amount: " + chargeAmount);

            yield return null;
        }

        //Debug.Log("Charging finished");
    }
    private IEnumerator Bouncing()
    {
        bounced = true;
        yield return new WaitForSeconds(3);
        bounced = false;
        
    }
    private IEnumerator Dash()
    {
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        m_Animator.SetTrigger("dash");
        canDash = false;
        canJump = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        //Debug.Log("Dash Started");
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //Debug.Log("Dash Moved");
        //tr.emitting = true;
        //AudioSource.PlayClipAtPoint(dashSound, transform.position, GameManager.instance.volume);
        yield return new WaitForSeconds(dashingTime);
        gameObject.layer = LayerMask.NameToLayer("Player");
        //tr.emitting = false;
        rb.gravityScale = 3;
        //rb.gravityScale = originalGravity;
        isDashing = false;
        canJump = true;
        rb.velocity = new Vector2(transform.localScale.x, 0f);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        //Debug.Log("Dash Finished");
    }
    private IEnumerator tookDamage()
    {
        //Debug.Log("took Damage!");
        playerSpeed = maxSpeed;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        gameObject.layer = LayerMask.NameToLayer("Invincible");

        Color originalColor = spriteRenderer.color;

        // Reduce health
        //health --;

        
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.5f);
        hurtCanvas.SetActive(false);
        // Wait for 1 second 
        yield return new WaitForSeconds(1f);

        // Return to the original color
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    public void takeDamage(int damage)
    {
        
        m_Animator.SetTrigger("hurt");
        hurtCanvas.SetActive(true);
        if (damage < 2)
        {
            //Debug.Log("ShakingScreenlight");
            FindObjectOfType<GameManager>().Stop(0.2f);
            CinemaMachineShake.Instance.ShakeCamera(screenShakeAmount1Damage, .1f);
        }
        if (damage >= 2)
        {
            //Debug.Log("ShakingScreenHard");
            FindObjectOfType<GameManager>().Stop(0.5f);
            CinemaMachineShake.Instance.ShakeCamera(screenShakeAmount2Damage, .2f);
        }
        
        StartCoroutine(tookDamage());  // Start the coroutine properly
        health -= damage;  // Deduct health
        AudioSource.PlayClipAtPoint(hurtNoise, transform.position);
        if (health >= 4)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(true);
        }
        if (health == 3)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(true);
            health4.SetActive(false);
        }
        if (health == 2)
        {
            health1.SetActive(false);
            health2.SetActive(true);
            health3.SetActive(false);
            health4.SetActive(false);
        }
        if (health == 1)
        {
            health1.SetActive(true);
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(false);
        }
        if (health <= 0)
        {
            // Handle player death
            isdead = true;
            deathCanvas.SetActive(true);
            healthCanvas.SetActive(false);
            gameObject.layer = LayerMask.NameToLayer("Invincible");
            playerSpeed = 0;
            jumpForce = 0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the scene on death
        }

    }
}
