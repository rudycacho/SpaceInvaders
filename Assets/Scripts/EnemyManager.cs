using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemyColumns = 11;
    public int enemyRows = 5;
    public Vector3 enemyStartPosition = new Vector3(-2.5f, 3.35f, 0);
    public float originalEnemySpeed = .25f;
    public float currentEnemySpeed;
    
    public GameObject topAlien;
    public GameObject midAlien;
    public GameObject bottomAlien;
    public GameObject specialAlien;

    private int enemyTotal;
    private Transform leftmostAlien;
    private Transform rightmostAlien;

    private Transform enemyGroupTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentEnemySpeed = originalEnemySpeed;
        Enemy.OnEnemyDied += EnemyOnEnemyDied;
        enemyTotal = enemyColumns * enemyRows;
        SpawnAliens();
    }
    void OnDestroy()
    {
        Enemy.OnEnemyDied -= EnemyOnEnemyDied;
    }
    

    // Update is called once per frame
    void Update()
    {
        // Enemy Movement
        enemyGroupTransform = this.transform;
        Vector3 newenemyGroupPosition = enemyGroupTransform.position + new Vector3(currentEnemySpeed * Time.deltaTime, 0f, 0f);
      
        enemyGroupTransform.position = newenemyGroupPosition;

        if (leftmostAlien != null)
        {
            if (leftmostAlien.position.x < -3.5f)
            {
                Debug.Log("TOO FAR");
                Vector3 newEnemyGroupPosition = enemyGroupTransform.position;
                newEnemyGroupPosition.y -= .25f;
                enemyGroupTransform.position = newEnemyGroupPosition;
                currentEnemySpeed = -currentEnemySpeed;
            }
        }
        if (rightmostAlien != null)
        {
            if (rightmostAlien.position.x > 3.5f)
            {
                Debug.Log("TOO FAR");
                Vector3 newEnemyGroupPosition = enemyGroupTransform.position;
                newEnemyGroupPosition.y -= .25f;
                enemyGroupTransform.position = newEnemyGroupPosition;
                currentEnemySpeed = -currentEnemySpeed;
            }
        }
    }

    void SpawnAliens()
    {
        currentEnemySpeed = originalEnemySpeed;
        currentEnemySpeed = Mathf.Abs(currentEnemySpeed);
        this.transform.position = new Vector3(0, 0, 0);
        GameObject alien;
        Vector3 spawnPosition = enemyStartPosition;
        for (int i = 0; i < enemyRows; i++ )
        {
            // Select alien
            if (i == 0)
            {
                alien = topAlien;
            }
            else if (i <= 2)
            {
                alien = midAlien;
            }
            else
            {
                alien = bottomAlien;
            }
            // Alien column spawning
            for (int j = 0; j < enemyColumns; j++)
            {
                GameObject newAlien = Instantiate(alien, spawnPosition, Quaternion.identity);
                newAlien.transform.parent = transform;
                spawnPosition.x += .5f;
            }
            spawnPosition.x = enemyStartPosition.x;
            spawnPosition.y -= .5f;
        }
        GameObject specialAlien = Instantiate(this.specialAlien, new Vector3(0f,5f,0f), Quaternion.identity);
        specialAlien.transform.parent = transform;
        spawnPosition.x += .5f;
        GetFurthestAliens();
    }
    
    void EnemyOnEnemyDied(int points)
    {
        enemyTotal--;
        Debug.Log($"Enemies Remaining: {enemyTotal}");
        if (enemyTotal == 0)
        {
            enemyTotal = enemyColumns * enemyRows;
            SpawnAliens();
        }
        else
        {
            GetFurthestAliens();
            currentEnemySpeed = currentEnemySpeed * 1.05f;
        }
    }
    
    void GetFurthestAliens()
    {
        // Left alien
        Transform leftmost = transform.GetChild(0);
        foreach (Transform alien in transform)
        {
            if (alien.position.x < leftmost.position.x) 
                leftmost = alien;
        }
        leftmostAlien = leftmost;
        // Right alien
        Transform rightmost = transform.GetChild(0);
        foreach (Transform alien in transform)
        {
            if (alien.position.x > rightmost.position.x)
                rightmost = alien;
        }
        rightmostAlien = rightmost;
        Debug.Log($"{leftmost.position}  {rightmost.position}");
    }
}
