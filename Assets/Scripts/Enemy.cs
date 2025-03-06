using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int points = 0;
    public delegate void EnemyDied(int points);
    public static event EnemyDied OnEnemyDied;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
      Destroy(collision.gameObject);
      Destroy(gameObject);

      OnEnemyDied?.Invoke(points);
      
      // todo kill enemy
    }
}
