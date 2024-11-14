using System.Collections;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private GameObject arenaIntroText;
    [SerializeField] private GameObject arenaFightText;
    [SerializeField] private GameObject[] wave1;           //Array of GameObjects for wave 1
    [SerializeField] private GameObject[] wave1Portals;    //Array of portals for wave 1
    [SerializeField] private GameObject[] wave2;           //Array of GameObjects for wave 2
    [SerializeField] private GameObject[] wave2Portals;    //Array of portals for wave 2
    [SerializeField] private GameObject[] wave3;           //Array of GameObjects for wave 3
    [SerializeField] private GameObject[] wave3Portals;    //Array of portals for wave 3
    private bool shouldSpawn = false;                      //indicate spawning should start
    [SerializeField] private GameObject arenaWalls;        //Arena walls object
    [SerializeField] private float portalDelay;            //Delay before activating portals
    //[SerializeField] private float arenaStartDelay;

    private bool firstWaveComplete = false;                
    private bool secondWaveComplete = false;
    private bool thirdWaveComplete = false;
    private bool fought = false;

    void Update()
    {
        if (shouldSpawn && !firstWaveComplete && AllEnemiesDestroyed(wave1))
        {
            firstWaveComplete = true;
            StartCoroutine(secondWave()); 
        }
        if (firstWaveComplete && !secondWaveComplete && AllEnemiesDestroyed(wave2))
        {
            secondWaveComplete = true;
            StartCoroutine(thirdWave());  
        }
        if (secondWaveComplete && !thirdWaveComplete && AllEnemiesDestroyed(wave3))
        {
            Debug.Log("ThirdWaveComplete!");
            thirdWaveComplete = true;
            arenaWalls.SetActive(false); 
        }

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
        arenaIntroText.SetActive(true);
        shouldSpawn = true;
        arenaWalls.SetActive(true);
        fought = true;
        StartCoroutine(firstWave());
        //Do Opening Cuphead Intro
        yield return new WaitForSeconds(portalDelay - 0.5f);
        GetComponent<BoxCollider2D>().enabled = false;
    }




    private IEnumerator firstWave()
    {
        
        foreach (GameObject portal in wave1Portals)
        {
            portal.SetActive(true);  
        }

        yield return new WaitForSeconds(portalDelay);
        arenaIntroText.SetActive(false);
        arenaFightText.SetActive(true);
        foreach (GameObject portal in wave1Portals)
        {
            portal.SetActive(false);  
        }

        // Activate all enemies in wave1
        foreach (GameObject enemy in wave1)
        {
            if (enemy != null) enemy.SetActive(true);  
        }
        yield return new WaitForSeconds(0.5f);
        arenaFightText.SetActive(false);
    }
   
    private IEnumerator secondWave()
    {
        
        foreach (GameObject portal in wave2Portals)
        {
            portal.SetActive(true); 
        }

        yield return new WaitForSeconds(portalDelay);  
        foreach (GameObject portal in wave2Portals)
        {
            portal.SetActive(false);  
        }


       
        foreach (GameObject enemy in wave2)
        {
            if (enemy != null) enemy.SetActive(true); 
        }
    }

    private IEnumerator thirdWave()
    {
     
        foreach (GameObject portal in wave3Portals)
        {
            portal.SetActive(true);  
        }

        yield return new WaitForSeconds(portalDelay);  

        foreach (GameObject portal in wave3Portals)
        {
            portal.SetActive(false);  
        }

       
        foreach (GameObject enemy in wave3)
        {
            if (enemy != null) enemy.SetActive(true); 
        }
    }
    private bool AllEnemiesDestroyed(GameObject[] enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null) return false;  
        }
        return true; 
    }
}
