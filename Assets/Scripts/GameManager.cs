using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private Vector2 lastCheckPointPos;
    bool waiting;
    private AudioSource audioSource;
    [SerializeField] private GameObject menuMusic;
    [SerializeField] private GameObject level1Music;
    [SerializeField] private GameObject level2Music;
    [SerializeField] private GameObject level3Music;
    [SerializeField] private GameObject level4Music;
    [SerializeField] private GameObject level5Music;
    [SerializeField] private Player cosmo;
    [SerializeField] private GameObject gameMusic;
    [SerializeField] private GameObject deathMusic;
    [SerializeField] private GameObject pauseMusic;
    public Vector2 LastCheckPointPos { get => lastCheckPointPos; set => lastCheckPointPos = value; }
    public static GameManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
       
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            //audioSource.Stop();
            //audioSource.Pause();
            LastCheckPointPos = transform.position;
            gameMusic.SetActive(true);
            pauseMusic.SetActive(false);
            deathMusic.SetActive(false);
            menuMusic.SetActive(true);
            level1Music.SetActive(false);
            level2Music.SetActive(false);
            level3Music.SetActive(false);
            level4Music.SetActive(false);
            level5Music.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            menuMusic.SetActive(false);
            level1Music.SetActive(true);
            level2Music.SetActive(false);
            level3Music.SetActive(false);
            level4Music.SetActive(false);
            level5Music.SetActive(false);
            if (cosmo == null)
            {
                cosmo = FindObjectOfType<Player>();
            }
            if (cosmo.Isdead == true)
            {
                gameMusic.SetActive(false);
                deathMusic.SetActive(true);
            }
            else
            {
                gameMusic.SetActive(true);
                deathMusic.SetActive(false);
            }
            if (cosmo.PauseOn == true)
            {
                gameMusic.SetActive(false);
                pauseMusic.SetActive(true);
            }
            else
            {
                gameMusic.SetActive(true);
                pauseMusic.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            menuMusic.SetActive(false);
            level1Music.SetActive(false);
            level2Music.SetActive(true);
            level3Music.SetActive(false);
            level4Music.SetActive(false);
            level5Music.SetActive(false);
            if (cosmo == null)
            {
                cosmo = FindObjectOfType<Player>();
            }
            if (cosmo.Isdead == true)
            {
                gameMusic.SetActive(false);
                deathMusic.SetActive(true);
            }
            else
            {
                gameMusic.SetActive(true);
                deathMusic.SetActive(false);
            }
            if (cosmo.PauseOn == true)
            {
                gameMusic.SetActive(false);
                pauseMusic.SetActive(true);
            }
            else
            {
                gameMusic.SetActive(true);
                pauseMusic.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "Level3")
        {
            menuMusic.SetActive(false);
            level1Music.SetActive(false);
            level2Music.SetActive(false);
            level3Music.SetActive(true);
            level4Music.SetActive(false);
            level5Music.SetActive(false);
            if (cosmo == null)
            {
                cosmo = FindObjectOfType<Player>();
            }
            if (cosmo.Isdead == true)
            {
                gameMusic.SetActive(false);
                deathMusic.SetActive(true);
            }
            else
            {
                gameMusic.SetActive(true);
                deathMusic.SetActive(false);
            }
            if (cosmo.PauseOn == true)
            {
                gameMusic.SetActive(false);
                pauseMusic.SetActive(true);
            }
            else
            {
                gameMusic.SetActive(true);
                pauseMusic.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "Level4")
        {
            menuMusic.SetActive(false);
            level1Music.SetActive(false);
            level2Music.SetActive(false);
            level3Music.SetActive(false);
            level4Music.SetActive(true);
            level5Music.SetActive(false);
            if (cosmo == null)
            {
                cosmo = FindObjectOfType<Player>();
            }
            if (cosmo.Isdead == true)
            {
                gameMusic.SetActive(false);
                deathMusic.SetActive(true);
            }
            else
            {
                gameMusic.SetActive(true);
                deathMusic.SetActive(false);
            }
            if (cosmo.PauseOn == true)
            {
                gameMusic.SetActive(false);
                pauseMusic.SetActive(true);
            }
            else
            {
                gameMusic.SetActive(true);
                pauseMusic.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "Level5")
        {
            menuMusic.SetActive(false);
            level1Music.SetActive(false);
            level2Music.SetActive(false);
            level3Music.SetActive(false);
            level4Music.SetActive(false);
            level5Music.SetActive(true);
            if (cosmo == null)
            {
                cosmo = FindObjectOfType<Player>();
            }
            if (cosmo.Isdead == true)
            {
                gameMusic.SetActive(false);
                deathMusic.SetActive(true);
            }
            else
            {
                gameMusic.SetActive(true);
                deathMusic.SetActive(false);
            }
            if (cosmo.PauseOn == true)
            {
                gameMusic.SetActive(false);
                pauseMusic.SetActive(true);
            }
            else
            {
                gameMusic.SetActive(true);
                pauseMusic.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "WinScreen")
        {
            menuMusic.SetActive(true);
            gameMusic.SetActive(true);
            level1Music.SetActive(false);
            level2Music.SetActive(false);
            level3Music.SetActive(false);
            level4Music.SetActive(false);
            level5Music.SetActive(false);
            deathMusic.SetActive(false);
            pauseMusic.SetActive(false);
        }
        

    }

    public void Stop(float duration)
    {
        if (waiting)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(wait(duration));
    }

    IEnumerator wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
    public void UnparentWithDelay(Transform objectToUnparent)
    {
        StartCoroutine(UnparentAfterDelay(objectToUnparent));
    }

    private IEnumerator UnparentAfterDelay(Transform objectToUnparent)
    {
        yield return new WaitForEndOfFrame(); 
        if (objectToUnparent != null)
        {
            objectToUnparent.SetParent(null);
        }
    }

}
