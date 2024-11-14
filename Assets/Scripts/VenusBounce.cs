using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusBounce : MonoBehaviour
{
    [SerializeField] private GameObject bite;
    [SerializeField] private GameObject bouncePad;
    [SerializeField] private float activationDelay; //How long till it fires
    [SerializeField] private float biteLastingTime; //How long it fires
    [SerializeField] private float bounceLastingTime;
    [SerializeField] private AudioClip biteNoise;
    private SpriteRenderer spriteRend;
    private bool triggered;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (active)
        {
            bite.SetActive(true);
        }
        if (!active)
        {
            bite.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(activateTrap());
            }
            if (active)
            {

                //Debug.Log("Murder time Baby wooo");
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    private IEnumerator activateTrap()
    {
       
        triggered = true;
        spriteRend.color = Color.red; 

       
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; 
        spriteRend.enabled = false;
        active = true;
        AudioSource.PlayClipAtPoint(biteNoise,transform.position);
        yield return new WaitForSeconds(biteLastingTime);
        active = false;
        bouncePad.SetActive(true);
        yield return new WaitForSeconds(bounceLastingTime);
        spriteRend.enabled = true;
        bouncePad.SetActive(false);
        triggered = false;
        
    }

    

    private IEnumerator deactivateTrap()
    {

        triggered = true;
        spriteRend.color = Color.red;

        
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; //turn the Sprite back
        spriteRend.enabled = false;
        active = true;
        yield return new WaitForSeconds(biteLastingTime);
        active = false;
        bouncePad.SetActive(true);
        yield return new WaitForSeconds(bounceLastingTime);
        spriteRend.enabled = true;
        bouncePad.SetActive(false);
        triggered = false;

    }
}
