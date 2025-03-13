using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI highScoreText;
    [Header("Sounds")]
    public AudioClip invaderDeath;
    public AudioClip playerDeath;
    
    private int score = 0;
    private int highScore = 0;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Enemy.OnEnemyDied += EnemyOnEnemyDied;
        Player.OnPlayerDied += PlayerOnPlayerDead;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EnemyOnEnemyDied(int points, bool isUFO)
    {
        audioSource.PlayOneShot(invaderDeath);
        int scoreLength = 4;
        score += points;
        string scoreString = score.ToString();
        int numZeros = scoreLength - scoreString.Length;
        
        string newScoreString ="";
        
        for(int i = 0; i < numZeros; i++){
            newScoreString += "0";
        }
        newScoreString += scoreString;
        p1ScoreText.text = "Score <1>\n" + newScoreString;
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "HI-SCORE\n" + newScoreString;
        }
    }

    void PlayerOnPlayerDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
