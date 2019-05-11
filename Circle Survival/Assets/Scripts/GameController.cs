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
    private const int CirclePoolSize = 3;

    //CircleSpawner Variables
    private readonly float _circleSpawnDelay=1.0f;
    private readonly float _minCircleTimeToExplosion = 2.0f;
    private readonly float _maxCircleTimeToExplosion = 4.0f;
    private CircleSpawner _circleSpawner;
    
    
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
        _circleSpawner=new CircleSpawner(GameObject.Find("Board"),_circleSpawnDelay,
            _minCircleTimeToExplosion,_maxCircleTimeToExplosion,new PrefabPool(CirclePoolSize,circlePrefab),
            circlePrefab.GetComponent<RectTransform>().rect.width/2,GameOver);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isStarted) return;
        _time += Time.deltaTime;
        _timeText.text = _time.ToString("#0.00") + "s";
        _circleSpawner.Update();
    }

    private void GameOver()
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
