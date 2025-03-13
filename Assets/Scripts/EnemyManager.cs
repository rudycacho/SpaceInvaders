using System;
using UnityEngine;
using Random = System.Random;

public class EnemyManager : MonoBehaviour {
    [Header("Enemy Properties")]
    public float stepSize = 0.25f;  // Adjust the step size for movement
    public float moveInterval = 0.05f;  // How often they should move (seconds)
    private float timer = 0f;
    private float ufotimer = 0f;
    private int direction = 1;
    [Header("Enemy Types")]
    public GameObject topAlien;
    public GameObject midAlien;
    public GameObject bottomAlien;
    public GameObject ufo;
    
    private float orignalmoveInterval;
    private int enemyColumns = 11;
    private int enemyRows = 5;
    private Vector3 enemyStartPosition = new Vector3(-2.5f, 3.35f, 0);
    private int enemyTotal;
    private Transform leftmostAlien;
    private Transform rightmostAlien;
    private Transform enemyGroupTransform;
    private int round = 0;
    private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        orignalmoveInterval = moveInterval;
        Enemy.OnEnemyDied += EnemyOnEnemyDied;
        enemyTotal = enemyColumns * enemyRows;
        SpawnAliens();
    }
    
    // onDestroy is called before the object is destroyed.
    void OnDestroy() {
        Enemy.OnEnemyDied -= EnemyOnEnemyDied;
    }
    
    // Update is called once per frame
    void Update() {
        enemyGroupTransform = this.transform;
        // Enemy Movement
        timer += Time.deltaTime;
        ufotimer += Time.deltaTime;
        
        if (timer >= moveInterval)
        {
            timer = 0f;  // Reset the timer
            MoveEnemies();
        }

        if (ufotimer >= 15)
        {
           int coinFlip = UnityEngine.Random.Range(0, 1);
           if (coinFlip == 0)
           {
               ufotimer = 0;
               SpawnUFO();
           }
           ufotimer = 0;
        }
    }
    void MoveEnemies()
    {
        enemyGroupTransform.position += new Vector3(stepSize * direction, 0, 0);
        if (leftmostAlien != null && direction == -1) {
            if (leftmostAlien.position.x < -3.35f) {
                Invoke("ChangeDirection",0.1f);
            }
        }
        if (rightmostAlien != null && direction == 1) {
            if (rightmostAlien.position.x > 3.35f) {
                Invoke("ChangeDirection",0.1f);
            }
        }
    }
    public void ChangeDirection()
    {
        enemyGroupTransform.position += new Vector3(0, -stepSize, 0);
        direction *= -1;
    }
    
    // Alien spawn logic
    void SpawnAliens() {
        moveInterval = orignalmoveInterval;
        this.transform.position = new Vector3(0, 0, 0);
        GameObject alien;
        Vector3 spawnPosition = enemyStartPosition;
        for (int i = 0; i < enemyRows; i++ ) {
            // Select alien
            if (i == 0) {
                alien = topAlien;
            }
            else if (i <= 2) {
                alien = midAlien;
            }
            else {
                alien = bottomAlien;
            }
            // Alien column spawning
            for (int j = 0; j < enemyColumns; j++) {
                GameObject newAlien = Instantiate(alien, spawnPosition, Quaternion.identity);
                newAlien.transform.parent = transform;
                spawnPosition.x += .5f;
            }
            spawnPosition.x = enemyStartPosition.x;
            spawnPosition.y -= .5f;
        }
        spawnPosition.x += .5f;
        GetFurthestAliens();
    }
    
    // Checks once an enemy dies
    void EnemyOnEnemyDied(int points, bool isUFO) {
        if (!isUFO)
        {
            enemyTotal--;
            Debug.Log($"Enemies Remaining: {enemyTotal}");
            // Respawn aliens if set to zero.
            if (enemyTotal == 0)
            {
                enemyTotal = enemyColumns * enemyRows;
                round++;
                Invoke("SpawnAliens", 1f);
            }
            // Check for new alien bounds and become faster
            else
            {
                GetFurthestAliens();
                moveInterval -= .01f;
            }
        }
    }
    
    // Check for the furthest aliens on both sides.
    void GetFurthestAliens() {
        // Left alien
        Transform leftmost = transform.GetChild(0);
        foreach (Transform alien in transform) {
            if (alien.position.x < leftmost.position.x) 
                leftmost = alien;
        }
        leftmostAlien = leftmost;
        
        // Right alien
        Transform rightmost = transform.GetChild(0);
        foreach (Transform alien in transform) {
            if (alien.position.x > rightmost.position.x)
                rightmost = alien;
        }
        rightmostAlien = rightmost;
        
        // Debug type stuff
        Debug.Log($"{leftmost.position}  {rightmost.position}");
    }

    void SpawnUFO()
    {
        GameObject newufo = Instantiate(ufo, new Vector3(-5f, 3.5f, 0), Quaternion.identity);
    }
}
