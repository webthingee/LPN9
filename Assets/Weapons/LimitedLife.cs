using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLife : MonoBehaviour {

	public float lifetime;

	void Start () 
	{
        StartCoroutine(EndOfLife(lifetime));
    }

	IEnumerator EndOfLife (float _delay)
	{
		yield return new WaitForSeconds(_delay);
		Destroy(this.gameObject);
	}
}
