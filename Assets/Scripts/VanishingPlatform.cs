using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingPlatform : MonoBehaviour
{
    //[SerializeField]
    [SerializeField] private Collider2D box;
    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private float vanishTime;
    [SerializeField] private float reappearTime;
    [SerializeField] private AudioClip platformTouchNoise;
    [SerializeField] private AudioClip platformVanishNoise;
    Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        //this.box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(platformVanish());
        AudioSource.PlayClipAtPoint(platformTouchNoise, transform.position);
    }
    private void breakApart()
    {
        this.box.enabled = false;
        //this.visual.enabled = false;
        AudioSource.PlayClipAtPoint(platformVanishNoise, transform.position);
    }

    private void assemble()
    {
        this.box.enabled = true;
        //this.visual.enabled = true;
        m_Animator.SetTrigger("Idle");
    }

    private IEnumerator platformVanish()
    {
        
        yield return new WaitForSeconds(vanishTime);
        m_Animator.SetTrigger("Breaking");

        yield return new WaitForSeconds(reappearTime);
        m_Animator.SetTrigger("Fixing");
    }
    private void EnableVisual()
    {
        this.visual.enabled = true;
    }
    private void disableVisual()
    {
        this.visual.enabled = false;
    }
}
