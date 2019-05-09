using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GreenCircle : MonoBehaviour, ICircle //do przemyslenia czy potrzebny interfejs??
{
    public float InitialTime { get; private set; }
    public float Timer { get; private set; }

    private Action _onTapAction;
    private Action _onEndAction;

    public void Initialize(Action onTapAction, Action oneEndAction)
    {
        _onTapAction = onTapAction;
        _onEndAction = oneEndAction;
    }

    public void CircleSpawned(float time)
    {
        InitialTime = time;
        Timer = InitialTime;
        StartCoroutine(WaitForTimer());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _onTapAction.Invoke();
        StopCoroutine(WaitForTimer());
    }

    private IEnumerator WaitForTimer()
    {
        while (Timer>0)
        {
            Timer -= Time.deltaTime;
            yield return null;
        }
        _onEndAction.Invoke();
    }

    private void Test() //przemysl czy potrzebne 2 object poole??
    {
        GetComponent<SpriteRenderer>().color=Color.green;
    }
}
