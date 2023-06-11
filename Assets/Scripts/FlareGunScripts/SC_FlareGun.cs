using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FlareGun : UsableItem
{
	public Rigidbody flareBullet;
	public Transform barrelEnd;
	public GameObject muzzleParticles;
	public int bulletSpeed = 2000;

	public override void PhsyicsUpdate () 
	{
		if (tapInput)
		{
			UsedTap();
			if (Ammo > 0)
			{
				Shoot();
			}
		}
	}
	private void Awake()
	{
		Ammo = maxAmmo;
	}

	public override void Enter()
	{
		base.Enter();

		this.gameObject.SetActive(true);
	}

	public override void Exit()
	{
		base.Exit();

		//flashlightToggle = false;

		this.gameObject.SetActive(false);
	}
	
	void Shoot()
	{
		Ammo -= 1f;
		Debug.Log("BANGS");
		
		Rigidbody bulletInstance;			
			bulletInstance = Instantiate(flareBullet,barrelEnd.position,barrelEnd.rotation) as Rigidbody; //INSTANTIATING THE FLARE PROJECTILE
			
			bulletInstance.AddForce(barrelEnd.forward * bulletSpeed); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE
			
			Instantiate(muzzleParticles, barrelEnd.position,barrelEnd.rotation);	//INSTANTIATING THE GUN'S MUZZLE SPARKS*/
	}
}
