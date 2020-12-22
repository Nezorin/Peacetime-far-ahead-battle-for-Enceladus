using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingTank : MonoBehaviour
{
	public Transform target;
	public GameObject targetObject;
	public Transform PartToRotate;
	public Transform PartToRotateUpDown;
	public Transform Compensator;
	public Transform bulletPlace;
	public GameObject bulletprefab;

	public string enemyTag = "Defense";
	private float turnSpeed = 10f;

	[Header("Attributes")]
	public float range = 15f;
	public float fireRate = 1f;
	public float fireCountdown = 0f;
	public float damage = 5f;

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	// Use this for initialization
	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetObject = nearestEnemy;
		}
		else
		{
			target = null;
			targetObject = null;
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (target == null)
		{
			return;
		}
		LockOnTarget();
		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}
	private void Shoot()
	{
		targetObject.GetComponent<Health>().ModifyHealth(-damage);
		GameObject go = GameObject.Instantiate(bulletprefab, bulletPlace.position, Compensator.rotation * Quaternion.Euler(-90, 0, 0)) as GameObject;
		Destroy(go, 1.5f);
	}

	void LockOnTarget()
	{
		if (TankTrackController.IsMoving)
		{
			return;
		}
		//Debug.Log("x: " + Compensator.position.x.ToString() + "  y: " + Compensator.position.y.ToString() + "  z:  " + Compensator.position.z.ToString());
		Vector3 dir = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, target.position.z - transform.position.z);
		Vector3 dir1 = new Vector3(target.position.x - Compensator.GetComponent<Renderer>().bounds.center.x, target.position.y - Compensator.GetComponent<Renderer>().bounds.center.y, target.position.z - Compensator.GetComponent<Renderer>().bounds.center.z);
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Quaternion lookRotation1 = Quaternion.LookRotation(dir1);
		Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		Vector3 rotationUpDown = Quaternion.Lerp(PartToRotateUpDown.rotation, lookRotation1, Time.deltaTime * turnSpeed).eulerAngles;
		PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
		PartToRotateUpDown.rotation = Quaternion.Euler(rotationUpDown.x, rotation.y, 0f);
	}

}
