using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleSpawner : MonoBehaviour
{
    private float _circleSpawnDelay;
    private float _spawnTimer;
    private float _minCircleTimeToExplosion;
    private float _maxCircleTimeToExplosion;

    private CircleType[] _circleTypes;

    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;
    private float _circleRadius;

    private IPrefabPool _circlePool;

    private Action _gameOver;

    public void Initialize(float circleSpawnDelay, float minCircleTimeToExplosion, float maxCircleTimeToExplosion, IPrefabPool circlePool,float circleRadius, Action gameOver)
    {
        _circleSpawnDelay = circleSpawnDelay;
        _spawnTimer = _circleSpawnDelay;
        _circlePool = circlePool;
        _gameOver = gameOver;
        _minCircleTimeToExplosion = minCircleTimeToExplosion;
        _maxCircleTimeToExplosion = maxCircleTimeToExplosion;
        _circleRadius = circleRadius;
        _circleTypes = new[]
        {
            new CircleType(PositiveAction, NegativeAction, Color.green),
            new CircleType(NegativeAction, PositiveAction, Color.black, false, Color.black),
        };
        
        _minX = GetComponent<RectTransform>().rect.xMin+_circleRadius;
        _maxX = GetComponent<RectTransform>().rect.xMax-_circleRadius;
        _minY = GetComponent<RectTransform>().rect.yMin+_circleRadius;
        _maxY = GetComponent<RectTransform>().rect.yMax-_circleRadius;
    }

    void Update()
    {
        if (_spawnTimer >= _circleSpawnDelay)
        {
           InitializeCircle(RandomCircleType());
           _spawnTimer = 0;
        }
        else
        {
            _spawnTimer += Time.deltaTime;
        }
    }

    private void InitializeCircle(CircleType circleType)
    {
        GameObject circle = _circlePool.Get();
        circle.transform.SetParent(transform);
        circle.transform.localScale=Vector3.one;
        //circle.transform.localPosition=new Vector3(10000,10000,0);
        do
        {
            SetPosition(circle);
        } while (IsCollidingWithOther(circle));
        circle.GetComponent<Circle>().Initialize(circleType,RandomCircleTimeToExplosion(circleType));
    }

    private void PositiveAction(GameObject circle)
    {
        DecreaseSpawnDelay();
        ReturnToPool(circle);
    }

    private void NegativeAction(GameObject circle)
    {
        //_gameOver.Invoke();
    }

    private void DecreaseSpawnDelay()
    {
        _circleSpawnDelay *= 0.98f;
        _minCircleTimeToExplosion *= 0.98f;
        _maxCircleTimeToExplosion *= 0.98f;
    }

    private void SetPosition(GameObject circle)
    {
        circle.transform.localPosition=new Vector3(RandomXPosition(),RandomYPosition(),0);
    }

    private bool IsCollidingWithOther(GameObject circle)
    {
        int layerMask = 1 << 8;
        if (Physics2D.OverlapCircle(circle.transform.localPosition, _circleRadius, layerMask) == null)
        {
            Debug.Log(true);
        }
        return Physics.CheckSphere(circle.transform.localPosition, _circleRadius*2);
    }
    
    private CircleType RandomCircleType()
    {
        if (Random.Range(0, 100) < 90)
        {
            return _circleTypes[0];
        }
        else
        {
            return _circleTypes[1];
        }
    }

    private float RandomXPosition()
    {
        return Random.Range(_minX, _maxX);
    }
    
    private float RandomYPosition()
    {
        return Random.Range(_minY, _maxY);
    }

    private float RandomCircleTimeToExplosion(CircleType circleType)
    {
        if (circleType.CircleColor == Color.green)
        {
            return Random.Range(_minCircleTimeToExplosion, _maxCircleTimeToExplosion);
        }
        else return 3.0f;
    }

    private void ReturnToPool(GameObject circle)
    {
        _circlePool.Return(circle);
    }
}
