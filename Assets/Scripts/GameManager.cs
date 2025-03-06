using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI highScoreText;
    
    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy.OnEnemyDied += EnemyOnEnemyDied;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EnemyOnEnemyDied(int points)
    {
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
    }
}
