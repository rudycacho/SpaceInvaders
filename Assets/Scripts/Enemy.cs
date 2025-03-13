using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    
    [Header("Alien Properties")]
    public int points = 0;
    public bool canShoot;
    public bool isUFO = false;
    public GameObject bulletPrefab;
    
    private float timer = 0;
    private AudioSource audioSource;
    public AudioClip clip;
    
    
    public delegate void EnemyDied(int points, bool isUFO);
    public static event EnemyDied OnEnemyDied;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (isUFO)
        {
            UFOEvent();
        }
    }

    void Update()
    {
        if (canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= 10)
            {
                timer = 0;
                int coinFlip = UnityEngine.Random.Range(0, 10);
                if (coinFlip == 1)
                {
                    audioSource.PlayOneShot(clip);
                    GameObject shot = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    Destroy(shot, 3f);
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
      Destroy(collision.gameObject);
      Destroy(gameObject);

      OnEnemyDied?.Invoke(points,isUFO);
    }

    void UFOEvent()
    {
        Destroy(gameObject, 4.5f);
        points = 500;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.linearVelocity = Vector2.right * 2; 
    }
}
