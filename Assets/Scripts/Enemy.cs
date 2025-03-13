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
    public Animator animator;
    
    
    public delegate void EnemyDied(int points, bool isUFO);
    public static event EnemyDied OnEnemyDied;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        if (!isDead)
        {
            isDead = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
            Destroy(collision.gameObject);
            animator.Play("EnemyDestroyed");
            OnEnemyDied?.Invoke(points,isUFO);
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    void UFOEvent()
    {
        Destroy(gameObject, 4.5f);
        points = 500;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.linearVelocity = Vector2.right * 2; 
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
