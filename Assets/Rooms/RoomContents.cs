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
        DepositInRoom(enemySpots, enemyPrefabs, maxEnemyPlace);
        
        DepositInRoom(rewardSport, rewardPrefabs, maxRewardsPlace);
        
        if (GetComponentInParent<RoomControl>().isOnCompletionPath)
        {
            DepositInRoom(lightSpots, lightPrefabs, 1);
        }
    }

    void DepositInRoom (List<Transform> _spots, GameObject[] _objects, int _repeat)
    {
        while (_repeat >= 1)
        {
            if (_spots.Count > 0)
            {
                int randSpot = Random.Range(0, _spots.Count);
                int randObj = Random.Range(0, _objects.Length);
        
                Instantiate(_objects[randObj], _spots[randSpot].position, Quaternion.identity);
            
                _spots.RemoveAt(randSpot);
            }
            _repeat --;
        }
    }

}
