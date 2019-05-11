using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //HUD variables
    private Text _timeText;
    private float _time;
    private bool _isStarted;
    
    
    //ObjectPool Variables
    [SerializeField] private GameObject circlePrefab;
    private int circlePoolSize = 1;

    //CircleSpawner Variables
    private float circleSpawnDelay=1.0f;
    private float minCircleTimeToExplosion = 2.0f;
    private float maxCircleTimeToExplosion = 4.0f;
    
    
    //GameOver screen variables
    private GameObject _gameOverPanel;
    void Awake()
    {
        _isStarted = true;
        _timeText=GameObject.Find("Time_text").GetComponent<Text>();
        _time = 0;
        _timeText.text = _time.ToString("0.00") + "s";
        
        _gameOverPanel=GameObject.Find("GameOver_panel");
        _gameOverPanel.SetActive(false);
        GameObject.Find("Board").GetComponent<CircleSpawner>().Initialize(
            circleSpawnDelay, minCircleTimeToExplosion, maxCircleTimeToExplosion,new PrefabPool(circlePoolSize,circlePrefab),
            circlePrefab.GetComponent<RectTransform>().rect.width/2,GameOver);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isStarted) return;
        _time += Time.deltaTime;
        _timeText.text = _time.ToString("#0.00") + "s";
    }

    void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _isStarted = false;
        GameObject.Find("Board").SetActive(false);
        GameObject newRecord =GameObject.Find("NewRecord_text");
        GameObject.Find("GameOver_score_value").GetComponent<Text>().text=_time.ToString("#0.00") + "s";
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);
        if (_time > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", _time);
            newRecord.SetActive(true);
            PlayerPrefs.Save();
        }
        else
        {
            newRecord.SetActive(false);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
