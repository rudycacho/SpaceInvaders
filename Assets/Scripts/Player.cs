using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public delegate void PlayerDied();
  public static event PlayerDied OnPlayerDied;
  public GameObject bulletPrefab;
  public Transform shottingOffset;
  public float maxSpeed = 1f;

  private GameObject shot;
  private AudioSource audioSource;
  public AudioClip shotSound;
    // Update is called once per frame
    void Start()
    {
      audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space) && shot == null)
      {
        audioSource.PlayOneShot(shotSound);
        shot = Instantiate(bulletPrefab, shottingOffset.position, Quaternion.identity);
        Destroy(shot, 1.5f);
      }
      
    }

    void FixedUpdate()
    {
      float horizontal = Input.GetAxis("Horizontal");
      Transform playerTransform = this.transform;
      
      Vector3 newPlayerPosition = playerTransform.position + new Vector3(horizontal * maxSpeed * Time.deltaTime, 0f, 0f);
      newPlayerPosition.x = Mathf.Clamp(newPlayerPosition.x, -3.5f, 3.5f);
      
      playerTransform.position = newPlayerPosition;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
      Destroy(collision.gameObject);
      Destroy(gameObject);

      OnPlayerDied?.Invoke();
    }
}
