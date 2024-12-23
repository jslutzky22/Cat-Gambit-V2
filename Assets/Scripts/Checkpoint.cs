using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gm;
    Animator m_Animator;
    [SerializeField] private GameObject checkpointText;
    [SerializeField] private AudioClip checkpointNoise;
    private bool fired;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && fired == false)
        {
            fired = true;
            gm.LastCheckPointPos = transform.position;
            m_Animator.SetTrigger("checkpointHit");
            StartCoroutine(checkpointGot());
            AudioSource.PlayClipAtPoint(checkpointNoise, transform.position);
        }
    }


    private IEnumerator checkpointGot()
    {
        checkpointText.SetActive(true);
        yield return new WaitForSeconds(3f);
        checkpointText.SetActive(false);
    }
}
