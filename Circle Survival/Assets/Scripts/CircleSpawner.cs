using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleSpawner
{
    //Spawning variables
    private readonly float _startingCircleSpawnDelay;
    private float _circleSpawnDelay;
    private float _spawnTimer;
    //Circle timer variables
    private float _minCircleTimeToExplosion;
    private float _maxCircleTimeToExplosion;
    //Circle types
    private readonly CircleType[] _circleTypes;
    
    private readonly float _circleRadius;
    //Board bounds
    private readonly float _minX;
    private readonly float _maxX;
    private readonly float _minY;
    private readonly float _maxY;
    
    private readonly HashSet<Vector3> _activeCirclesCenters;

    private readonly IPrefabPool _circlePool;

    private readonly Action _gameOver;

    private int _circlesDestroyed;
    
    private readonly Transform _boardTransform;

    public CircleSpawner(GameObject board, float circleSpawnDelay, float minCircleTimeToExplosion, 
        float maxCircleTimeToExplosion, IPrefabPool circlePool,float circleRadius, Action gameOver)
    {
        _startingCircleSpawnDelay = circleSpawnDelay;
        _circleSpawnDelay = _startingCircleSpawnDelay;
        _spawnTimer = _circleSpawnDelay;
        
        _boardTransform = board.transform;
        
        _minCircleTimeToExplosion = minCircleTimeToExplosion;
        _maxCircleTimeToExplosion = maxCircleTimeToExplosion;
        
        _circleRadius = circleRadius;
        
        _minX = board.GetComponent<RectTransform>().rect.xMin+_circleRadius;
        _maxX = board.GetComponent<RectTransform>().rect.xMax-_circleRadius;
        _minY = board.GetComponent<RectTransform>().rect.yMin+_circleRadius;
        _maxY = board.GetComponent<RectTransform>().rect.yMax-_circleRadius;
        
        _activeCirclesCenters=new HashSet<Vector3>();
        
        _circlePool = circlePool;
        
        _gameOver = gameOver;
        
        _circleTypes = new[]
        {
            new CircleType(PositiveAction, NegativeAction, Color.green),
            new CircleType(NegativeAction, PositiveAction, Color.black, false, Color.black),
        };
    }

    public void Update()
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
        
        circle.transform.SetParent(_boardTransform);
        circle.transform.localScale=Vector3.one;
        
        SpawnCircle(circle);
        
        circle.GetComponent<Circle>().Initialize(circleType,RandomCircleTimeToExplosion(circleType));
    }

    private void PositiveAction(GameObject circle)
    {
        _circlesDestroyed++;
        DecreaseValues();
        ReturnToPool(circle);
    }

    private void NegativeAction(GameObject circle)
    {
        _gameOver.Invoke();
    }

    private void DecreaseValues()
    {
        float factor = (float) Math.Log(_circlesDestroyed);
        _circleSpawnDelay =Math.Min((_startingCircleSpawnDelay/factor),_startingCircleSpawnDelay);
        _minCircleTimeToExplosion *= 0.99f;
        _maxCircleTimeToExplosion *= 0.99f;
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
            if (safety <= 50) continue;
            _gameOver.Invoke();
            throw new TimeoutException();
        } while (IsCollidingWithOther(circleCenter));

        circle.transform.localPosition = circleCenter;
        _activeCirclesCenters.Add(circleCenter);
    }
    
    private CircleType RandomCircleType()
    {
        return Random.Range(0, 100) < 90 ? _circleTypes[0] : _circleTypes[1];
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
        return circleType.CircleColor == Color.green ? Random.Range(_minCircleTimeToExplosion, _maxCircleTimeToExplosion) : 3.0f;
    }

    private void ReturnToPool(GameObject circle)
    {
        _activeCirclesCenters.Remove(circle.transform.localPosition);
        _circlePool.Return(circle);
    }
}
