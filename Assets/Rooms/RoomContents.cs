using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContents : MonoBehaviour 
{
    int maxEnemyPlace = 0;
    int maxRewardsPlace = 0;
    public bool canFlipXAxis;

    public List<Transform> enemySpots = new List<Transform>();
    public GameObject[] enemyPrefabs;

    public List<Transform> rewardSport = new List<Transform>();
    public GameObject[] rewardPrefabs;

    void Awake()
    {
        int maxN = ManagePrefs.MP.easyMode ? 2 : 1;
        Debug.Log(maxN);
        maxEnemyPlace = enemySpots.Count - maxN; // @TODO difficulty adjust
        maxRewardsPlace = rewardSport.Count - 1;
    }

    void Start()
    {
        DepositInRoom(enemySpots, enemyPrefabs, maxEnemyPlace);
        DepositInRoom(rewardSport, rewardPrefabs, maxRewardsPlace);      
    }

    void DepositInRoom (List<Transform> _spots, GameObject[] _objects, int _repeat)
    {
        while (_repeat >= 1)
        {
            if (_spots.Count > 0)
            {
                int randSpot = Random.Range(0, _spots.Count);
                int randObj = Random.Range(0, _objects.Length);
        
                Instantiate(_objects[randObj], _spots[randSpot].position, Quaternion.identity, transform);
            
                _spots.RemoveAt(randSpot);
            }
            _repeat --;
        }
    }

    public Vector2? GetDepositPoint (List<Transform> _spots, bool _removePoint)
    {
        Vector2 point;
        int randSpot = Random.Range(0, _spots.Count);

        if (_spots.Count > 0)
        {            
            point = _spots[randSpot].position;
        }
        else
        {
            Debug.LogError("GetDepositPoint: no " 
                + _spots.Count + " available points in "
                + this.gameObject.name);
            return null;
        }

        if (_removePoint)
        {
            _spots.RemoveAt(randSpot);
        }

        return point;
    }

    public void DepositAtRewardPoint (GameObject _object, bool _removePoint)
    {
        int randSpot = Random.Range(0, rewardSport.Count);        
        if (rewardSport.Count > 0)
        {
            Instantiate(_object, rewardSport[randSpot].position, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogError("DepositAtRewardPoint: Error");
        }

        if (_removePoint)
        {
            rewardSport.RemoveAt(randSpot);
        }
    }

}
