using UnityEngine;

public class BarrierLogic : MonoBehaviour
{
    [Header("Sprite")]
    public Sprite[] sprites;
    private int damageState = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        if (damageState == 5)
        {
            Destroy(gameObject);
        }
        else
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[damageState];
            damageState++;
        }
    }
}
