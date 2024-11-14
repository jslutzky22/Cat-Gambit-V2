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
    // Start is called before the first frame update
    void Start()
    {
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

    private IEnumerator platformVanish()
    {
        
        yield return new WaitForSeconds(vanishTime);
        this.box.enabled = false;
        this.visual.enabled = false;
        AudioSource.PlayClipAtPoint(platformVanishNoise, transform.position);
        yield return new WaitForSeconds(reappearTime);
        this.box.enabled = true;
        this.visual.enabled = true;
    }
}
