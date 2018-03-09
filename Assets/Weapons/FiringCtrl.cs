using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FiringCtrl : MonoBehaviour 
{
	private Weapon weapon;
    private Weapon rope;    
    private bool canFire = true;
    private bool canFireRope = true;
    public Tilemap gameTilemap;

    void Awake()
    {
        weapon = GetComponent<WeaponInHand>().primaryWeapon;
        rope = GetComponent<WeaponInHand>().secondaryWeapon;
    }

    void Update()
    {
        /// Fire Projectile
		if (Input.GetButtonDown("Weapon") && canFire)
		{			
            canFire = false;
            PullTrigger();
		}

        /// Fire Rope
        // if (Input.GetAxisRaw("Vertical") > 0 && Input.GetButtonDown("Utility") && canFireRope)
		// {			
		// 	canFireRope = false;
        //     FireRope(transform.up, weapon.TimeBetweenHits);
		// }
    }

    public void PullTrigger ()
    {
        StartCoroutine(FireBullets(transform.right, weapon.TimeBetweenHits));
    }

	IEnumerator FireBullets (Vector3 _direction, float _waitTime)
	{			
		canFire = false;
		
		Quaternion _rotation = Quaternion.FromToRotation(Vector3.up, _direction); //same as : bullet.transform.up = direction;
		
		Vector3 _position = transform.position;
			_position.z = 0;
		
		GameObject bullet = Instantiate(weapon.projectilePrefab, _position, _rotation);
            bullet.GetComponent<Projectile>().FiringPoint = transform.position;
            bullet.GetComponent<Projectile>().projectileRange = weapon.Range;            

		yield return new WaitForSeconds(_waitTime);
		
		canFire = true;
	}

    void FireRope (Vector3 _direction, float _waitTime)
	{			
		CheckTilemap();
        
        canFireRope = false;
		
		Quaternion _rotation = Quaternion.FromToRotation(Vector3.up, _direction); //same as : bullet.transform.up = direction;
		
        Vector3Int cell = gameTilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = gameTilemap.GetCellCenterWorld(cell);
		
		GameObject hook = Instantiate(rope.projectilePrefab, cellCenterPos, _rotation);
            hook.GetComponent<Projectile>().FiringPoint = transform.position;

		canFireRope = true;
	}

    void CheckTilemap ()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 2f, 1 << LayerMask.NameToLayer("Walkable"));
        
        for (var i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Tilemap>() != null)
            {
                gameTilemap = hitColliders[i].GetComponent<Tilemap>();
                return;
            }
            
        }
    }
}
