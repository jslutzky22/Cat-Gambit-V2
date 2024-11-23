using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();

    [SerializeField] private Transform destination;
    [SerializeField] private float portalDelay;
    [SerializeField] private bool portalReady;
    [SerializeField] private AudioClip portalNoise;
    Animator m_Animator;

    private void Start()
    {
       
        m_Animator = gameObject.GetComponent<Animator>();
        portalReady = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (portalObjects.Contains(collision.gameObject))
        {
            m_Animator.SetTrigger("portalUse");
            StartCoroutine(portalTime());
            return;
            
        }

        if (destination.TryGetComponent(out Portal destinationPortal) && portalReady == true && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "EDamage" && collision.gameObject.tag != "Spike")
        {
            destinationPortal.portalObjects.Add(collision.gameObject);
            m_Animator.SetTrigger("portalUse");
        }

        if (portalReady == true && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "EDamage" && collision.gameObject.tag != "Spike")
        {
            collision.transform.position = destination.position;
            m_Animator.SetTrigger("portalUse");
            StartCoroutine(portalTime());
        }
        //collision.transform.position = destination.position;

        AudioSource.PlayClipAtPoint(portalNoise, transform.position);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        portalObjects.Remove(collision.gameObject);
    }

    private IEnumerator portalTime()
    {
        m_Animator.SetBool("Cooldown", true);
        portalReady = false;
        yield return new WaitForSeconds(portalDelay);
        portalReady = true;
        m_Animator.SetBool("Cooldown", false);
    }

}
