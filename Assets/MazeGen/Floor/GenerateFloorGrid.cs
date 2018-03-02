using UnityEngine;

public class GenerateFloorGrid : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] prefabs;

    [Header("Read-Only Floor Unit")]
    public float sideWall;
    public string floorTypeString;

    void Start ()
    {
        Invoke("GenerateUnits", 3f);      
    }

    void GenerateUnits()
    {
        sideWall = transform.localScale.x;
        transform.localScale = new Vector3(sideWall, sideWall, transform.localScale.z);
        floorTypeString = GetComponent<Floor>().tileTypeString;

        CalcStartPos();
        CreateGrid();
    }

    float CalcStartPos()
    {
        float calcOffset = (float)((sideWall / 2.0f + sideWall / 2.0f) / 2.0f) - (prefabs[0].transform.localScale.x * 0.5f);
        return -calcOffset;
    }

    void CreateGrid()
    {
        for (int i = 0; i < sideWall; i++)
        {
            for (int j = 0; j < sideWall; j++)
            {
                if (IsWall(i, j))
                    MakeFloorUnit(i, j);
            }
        }
    }

    private void MakeFloorUnit(int i, int j)
    {
        int randNum = Random.Range(0, 3);

        Vector3 pos = new Vector3(i, j, -1f) + new Vector3(CalcStartPos(), CalcStartPos(), -1.0f);

        GameObject floorUnit = Instantiate(prefabs[0], transform.position + pos, Quaternion.identity, this.transform);

        float fuscale = 1f / (float)sideWall;
        floorUnit.transform.localScale = new Vector3(fuscale, fuscale, 50f);
        floorUnit.GetComponent<FloorUnit>().SetColor(randNum);
        floorUnit.GetComponent<FloorUnit>().floorUnitID.x = i;
        floorUnit.GetComponent<FloorUnit>().floorUnitID.y = j;
        floorUnit.name = i.ToString() + " : " + j.ToString();

        // Special Considerations
        if (IsWall(i,j))
        {
            floorUnit.GetComponent<FloorUnit>().SetColor(10);
        }

    }

    private void MakeALatter(GameObject _floorUnit, float _fuscale, Vector3 _pos)
    {
        Ray ray = new Ray(_floorUnit.transform.position, -transform.up);
        float rayDist = 100f;

        Debug.DrawRay(_floorUnit.transform.position, -transform.up * rayDist, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(_floorUnit.transform.position, -transform.up, out hit, rayDist))
        {
            var latterUnit = Instantiate(prefabs[1], _floorUnit.transform.position + _pos, Quaternion.identity, this.transform);
            latterUnit.transform.localScale = new Vector3(_fuscale, hit.distance / 8f, 1f);
            var latterPos = _floorUnit.transform.position;
            latterPos.y = _floorUnit.transform.position.y - latterUnit.transform.lossyScale.y / 2;
            latterUnit.transform.position = latterPos;
        }
    }

    /// Identifies if the postion should be a wall
    private bool IsWall(int x, int y)
    {
        if (floorTypeString.Contains("T")  && y == sideWall - 1)
        {
            return true;
        }

        if (floorTypeString.Contains("R")  && x == sideWall - 1)
        {
            return true;
        }

        if (floorTypeString.Contains("D")  && y == 0)
        {
            return true;
        }

        if (floorTypeString.Contains("L") && x == 0)
        {
            return true;
        }
        return false;
    }
}
