using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    public int finalScore = 0;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        finalScore = FindObjectOfType<GameSession>().currentScore;
        audio = FindObjectOfType<GameSession>().GetComponent<AudioSource>();
        audio.Stop();
        scoreText.text = finalScore.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
