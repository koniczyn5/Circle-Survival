using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController: MonoBehaviour
{
    private Text _highScoreText;
    public void Awake()
    {
        _highScoreText=GameObject.Find("HS_value_text").GetComponent<Text>();
        _highScoreText.text=PlayerPrefs.GetFloat("HighScore", 0).ToString("#0.00")+"s";
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
