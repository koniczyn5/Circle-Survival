using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleSpawner : MonoBehaviour
{
    //Spawning variables
    private float _circleSpawnDelay;
    private float _spawnTimer;
    //Circle timer variables
    private float _minCircleTimeToExplosion;
    private float _maxCircleTimeToExplosion;
    //Circle types
    private CircleType[] _circleTypes;
    
    private float _circleRadius;
    //Board bounds
    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;
    
    private HashSet<Vector3> _activeCirclesCenters;

    private IPrefabPool _circlePool;

    private Action _gameOver;

    private int _circlesDestroyed;

    public void Initialize(float circleSpawnDelay, float minCircleTimeToExplosion, 
        float maxCircleTimeToExplosion, IPrefabPool circlePool,float circleRadius, Action gameOver)
    {
        _circleSpawnDelay = circleSpawnDelay;
        _spawnTimer = _circleSpawnDelay;
        
        _minCircleTimeToExplosion = minCircleTimeToExplosion;
        _maxCircleTimeToExplosion = maxCircleTimeToExplosion;
        
        _circleRadius = circleRadius;
        
        _minX = GetComponent<RectTransform>().rect.xMin+_circleRadius;
        _maxX = GetComponent<RectTransform>().rect.xMax-_circleRadius;
        _minY = GetComponent<RectTransform>().rect.yMin+_circleRadius;
        _maxY = GetComponent<RectTransform>().rect.yMax-_circleRadius;
        
        Debug.Log(_circleRadius);
        
        _activeCirclesCenters=new HashSet<Vector3>();
        
        _circlePool = circlePool;
        
        _gameOver = gameOver;
        
        _circleTypes = new[]
        {
            new CircleType(PositiveAction, NegativeAction, Color.green),
            new CircleType(NegativeAction, PositiveAction, Color.black, false, Color.black),
        };
        
        
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
        
        SpawnCircle(circle);
        
        circle.GetComponent<Circle>().Initialize(circleType,RandomCircleTimeToExplosion(circleType));
    }

    private void PositiveAction(GameObject circle)
    {
        DecreaseValues();
        ReturnToPool(circle);
    }

    private void NegativeAction(GameObject circle)
    {
        _gameOver.Invoke();
    }

    private void DecreaseValues()
    {
        _circleSpawnDelay *= 0.98f;
        _minCircleTimeToExplosion *= 0.98f;
        _maxCircleTimeToExplosion *= 0.98f;
    }
    
    private bool IsCollidingWithOther(Vector3 circleCenter)
    {
        foreach (var t in _activeCirclesCenters)
        {
            if (Vector3.Distance(circleCenter, t) < _circleRadius * 2) return true;
        }

        return false;
    }

    private void SpawnCircle(GameObject circle)
    {
        int safety = 0;
        Vector3 circleCenter;
        do
        {
            safety++;
            circleCenter=new Vector3(RandomXPosition(),RandomYPosition(),0);
            if (safety > 50)
            {
                _gameOver.Invoke();
                throw new TimeoutException();
            }
        } while (IsCollidingWithOther(circleCenter));

        circle.transform.localPosition = circleCenter;
        _activeCirclesCenters.Add(circleCenter);
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
        _activeCirclesCenters.Remove(circle.transform.localPosition);
        _circlePool.Return(circle);
    }
}
