using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    private float _circleSpawnDelay;
    private float _spawnTimer;

    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;

    private IPrefabPool _greenCirclePool;
    private IPrefabPool _blackCirclePool;

    public void Initialize(float circleSpawnDelay, IPrefabPool greenCirclePool, IPrefabPool blackCirclePool)
    {
        _circleSpawnDelay = circleSpawnDelay;
        _greenCirclePool = greenCirclePool;
        _blackCirclePool = blackCirclePool;

        Vector3[] boardCorners = new Vector3[4];
        GetComponent<RectTransform>().GetLocalCorners(boardCorners);
        _minX = boardCorners[0].x;
        _maxX = boardCorners[2].x;
        _minY = boardCorners[0].y;
        _maxY = boardCorners[2].y;

    }

    void Update()
    {
        if (_spawnTimer < _circleSpawnDelay)
        {
            if (RandomCircleType() < 9)
            {
                InitializeGreenCircle();
            }
            else
            {
                InitializeBlackCircle();
            }
            _spawnTimer = 0;
        }
        else
        {
            _spawnTimer += Time.deltaTime;
        }
    }

    private void InitializeGreenCircle()
    {
       // throw new System.NotImplementedException();
    }

    private void InitializeBlackCircle()
    {
       // throw new System.NotImplementedException();
    }

    private void DecreaseSpawnDelay()
    {
       // throw new System.NotImplementedException();
        //jakas funkcja matematyczna
    }
    
    private int RandomCircleType()
    {
        return Random.Range(0, 9);
    }

    private float RandomXPosition()
    {
        return Random.Range(_minX, _maxX);
    }
    
    private float RandomYPosition()
    {
        return Random.Range(_minY, _maxY);
    }

    private void ReturnToGreenPool(GameObject greenCircle)
    {
        _greenCirclePool.Return(greenCircle);
    }
    
    private void ReturnToBlackPool(GameObject blackCircle)
    {
        _blackCirclePool.Return(blackCircle);
    }
}
