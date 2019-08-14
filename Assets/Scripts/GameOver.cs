using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    public int finalScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        finalScore = FindObjectOfType<GameSession>().currentScore;
        AudioSource audio = FindObjectOfType<GameSession>().GetComponent<AudioSource>();
        audio.Stop();
        scoreText.text = finalScore.ToString();
    }
}
