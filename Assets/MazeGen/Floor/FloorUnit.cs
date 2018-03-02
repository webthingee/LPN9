using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorUnit : MonoBehaviour {

    [System.Serializable] public struct XYID
    {
        public int x;
        public float y;
    }
    public XYID floorUnitID = new XYID();

    public int colorChoice;

    public void SetColor(int _colorChoice)
    {
        switch (_colorChoice)
		{
        	case 0:
                this.GetComponent<Renderer>().material.color = Color.red;
                //this.GetComponent<Renderer>().material.SetColor("_TintColor", Color.red);
				break;
			case 1:
                this.GetComponent<Renderer>().material.color = Color.yellow;
                break;
			case 2:
                this.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 3:
                this.GetComponent<Renderer>().material.color = Color.red;
                break;
			default:
                this.GetComponent<Renderer>().material.color = Color.black;
                break;
		}
	}
}
