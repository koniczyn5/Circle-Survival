using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController: MonoBehaviour
{ 
    public void Awake()
    {
        GameObject.Find("HS_value_text").GetComponent<Text>().text=PlayerPrefs.GetFloat("HighScore", 0).ToString("#0.00")+"s";
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
