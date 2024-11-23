using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpot : MonoBehaviour
{
    //[SerializeField] private AudioClip Dialogue;
    private bool triggered;
    [SerializeField] private GameObject dialogueItems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && triggered == false)
        {
            //AudioSource.PlayClipAtPoint(Dialogue, transform.position);
            triggered = true;
            GetComponent<BoxCollider2D>().enabled = false;
            if (dialogueItems != null)
            {
                dialogueItems.SetActive(true);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
    }
}
