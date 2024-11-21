using UnityEngine;

public class BlinkColors : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;     
    private bool isGreen = false;    

    [SerializeField] private float blinkInterval = 0.5f;
    private Color neonBlue = new Color(0.1f, 0.5f, 1f, 1f);
    [SerializeField] private float detectionRadius = 5f; 
    private AudioSource audioSource;
    private GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        objectRenderer = GetComponent<Renderer>();


        audioSource = GetComponent<AudioSource>();


        originalColor = objectRenderer.material.color;

       
        InvokeRepeating(nameof(Blink), 0f, blinkInterval);
    }

    private void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= detectionRadius)
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }


    private void Blink()
    {

        if (isGreen)
        {
            objectRenderer.material.color = originalColor;
        }
        else
        {
            objectRenderer.material.color = neonBlue;
        }
        isGreen = !isGreen;
    }

}
