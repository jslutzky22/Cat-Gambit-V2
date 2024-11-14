using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    //[SerializeField] private float pillarTime;
    [SerializeField] private AudioClip pillarNoise; 

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void pillarStart()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        AudioSource.PlayClipAtPoint(pillarNoise, transform.position);
    }
    private void pillarEnd()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
    private void end()
    {
        gameObject.SetActive(false);
    }

}
