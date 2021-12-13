using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int TotalScore;
    public int Hp;
    public Text scoreText;
    public GameObject[] Life;
    private int NumberImg;
    private int Num;
    public bool deathCollision;

    public static GameController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Num = Hp;
    }

    public void UpdateScoreText(){
        scoreText.text = TotalScore.ToString();
    }

    public void UpdateHpImage(){
        
        NumberImg = Num - Hp;
        Life[NumberImg].SetActive(true);
        Hp--;
        if (Hp == 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
        deathCollision = false;
    }
}
