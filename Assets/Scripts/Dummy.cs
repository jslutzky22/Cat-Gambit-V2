using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : EnemyHealth
{
    Animator m_Animator;
    private GameObject player;
    [SerializeField] private Color hitColor = Color.red;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioClip dummyHitNoise;
    [SerializeField] private AudioClip Compliment;
    [SerializeField] private GameObject hurtParticle;
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
        
    }

    public override void takeDamage(int damage)
    {
        base.takeDamage(damage);
        StartCoroutine(gotHit());
        m_Animator.SetTrigger("hurt");
        Instantiate(hurtParticle, gameObject.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(dummyHitNoise, transform.position);
        if (health <= 0)
        {
            m_Animator.SetBool("Dead", true);
            //EASTER EGG?
            //Die();
        }
        //Play animation
    }

    private void Revive()
    {
        health = 20;
        m_Animator.SetBool("Dead", false);
    }
    private IEnumerator gotHit()
    {
        spriteRenderer.color = hitColor;
        AudioSource.PlayClipAtPoint(Compliment, transform.position);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
        //PLAY VOICELINE HERE

    }
}
