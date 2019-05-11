using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Circle : MonoBehaviour, IPointerDownHandler
{
    private float _initialTime;
    private float _timer;

    private Action<GameObject> _onTapAction;
    private Action<GameObject> _onExplosionAction;

    private Image _progress;
    private bool _isProgressActive;

    private void Awake()
    {
        _progress=transform.Find("Progress").GetComponent<Image>();
    }

    public void Initialize(CircleType circleType, float time)
    {
        _onTapAction = circleType.OnTapAction;
        _onExplosionAction = circleType.OnExplosionAction;
        GetComponent<Image>().color=circleType.CircleColor;
        _isProgressActive = circleType.IsProgressActive;
        _progress.color = circleType.ProgressColor;
        _progress.fillAmount = 0;
        _initialTime = time;
        _timer = 0;
        StartCoroutine(WaitForExplosion());
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _onTapAction.Invoke(gameObject);
        StopCoroutine(WaitForExplosion());
    }

    private IEnumerator WaitForExplosion()
    {
        while (_timer<_initialTime)
        {    
            _timer += Time.deltaTime;
            if(_isProgressActive) _progress.fillAmount = (_timer / _initialTime);
            yield return null;
        }
        _onExplosionAction.Invoke(gameObject);
    }
}
