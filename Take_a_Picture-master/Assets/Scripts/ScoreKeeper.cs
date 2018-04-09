using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public Text levelScore; //In the inspector drag the score number TEXT into this.

    public static int score = 0;
    public int currentScore; // Stores the current score. Future proof in case there is a level 2.


    void Start () {
        levelScore.text = score.ToString();
    }

    public void Score(int points)
    {
        score += points;//adds to what it currently equals or simply increased by.
        levelScore.text = score.ToString();
        //print(score);
    }

    public void saveScore()
    {
        currentScore = score;
    }

    public void Reset()
    {
        score = 0;
        print(score + " score reset");
    }

}
