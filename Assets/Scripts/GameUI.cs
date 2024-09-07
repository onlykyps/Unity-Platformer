using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject endScreen;
    public TextMeshProUGUI endScreenHeader;
    public TextMeshProUGUI endScreenScoreText;

    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + GameManager.instance.score;
    }

    public void SetEndScreen(bool hasWon)
    {   
        endScreen.SetActive(true);
        endScreenScoreText.text = "<b>Score</b>\n" + GameManager.instance.score;

        if(hasWon)
        {
            endScreenHeader.text = "You win";
            endScreenHeader.color = Color.green;
        }
        else
        {
            endScreenHeader.text = "Game over";
            endScreenHeader.color = Color.red;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
