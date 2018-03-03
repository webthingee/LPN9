using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContents : MonoBehaviour 
{
    public int maxEnemyPlace = 0;
    public int maxRewardsPlace = 0;
    public int maxLightsToPlace = 0;

    public List<Transform> enemySpots = new List<Transform>();
    public GameObject[] enemyPrefabs;

    public List<Transform> rewardSport = new List<Transform>();
    public GameObject[] rewardPrefabs;

    public List<Transform> lightSpots = new List<Transform>();
    public GameObject[] lightPrefabs;

    void Start()
    {
        DepositAnEnemy(enemySpots, enemyPrefabs, maxEnemyPlace);
        if (GetComponentInParent<RoomControl>().isOnCompletionPath)
        {
            DepositAnEnemy(lightSpots, lightPrefabs, 1);
        }
    }

    void DepositAnEnemy (List<Transform> _spots, GameObject[] _objects, int _repeat)
    {
        while (_repeat >= 1)
        {
            int randSpot = Random.Range(0, _spots.Count);
            int randObj = Random.Range(0, _objects.Length);
        
            Instantiate(_objects[randObj], _spots[randSpot].position, Quaternion.identity);
            
            _spots.RemoveAt(randSpot);
            //_objects.RemoveAt(randObj); 

            _repeat --;
        }
    }

}
