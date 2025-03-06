using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameObject bulletPrefab;
  public Transform shottingOffset;
  public float maxSpeed = 1f;
    // Update is called once per frame

    void Start()
    {
      
    }

    void OnDestroy()
    {
      
    }
    
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        GameObject shot = Instantiate(bulletPrefab, shottingOffset.position, Quaternion.identity);
        Debug.Log("Bang!");

        Destroy(shot, 3f);

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
}
