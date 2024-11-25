using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private GameObject popUp; 
    
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void enableText()
    {
        popUp.SetActive(true);
    }
}
