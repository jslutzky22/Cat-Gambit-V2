using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArena : MonoBehaviour
{

    [SerializeField] private GameObject arenaIntroText;
    [SerializeField] private GameObject arenaFightText;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject arenaWalls;
    [SerializeField] private float startDelay;
    [SerializeField] private float introDelay;
    private bool fought = false;
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
        if (collision.gameObject.tag == "Player" && fought == false)
        {
            //GetComponent<BoxCollider2D>().enabled = false;
            //Debug.Log("ArenaStarted!");
            //shouldSpawn = true;                      
            //arenaWalls.SetActive(true);
            //fought = true; 
            StartCoroutine(arenaOpening());
        }
    }
    private IEnumerator arenaOpening()
    {
        fought = true;
        arenaWalls.SetActive(true);
        arenaIntroText.SetActive(true);
        boss.SetActive(true);
        yield return new WaitForSeconds(startDelay);
        arenaIntroText.SetActive(false);
        arenaFightText.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        arenaFightText.SetActive(false);
    }
}
