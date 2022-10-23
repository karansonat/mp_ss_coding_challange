using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

    private float nextFireTime;

	public void OnEnable () {
        nextFireTime = Time.time + delay;
	}

    public void Update () {
        if (Time.time >= nextFireTime) {
            nextFireTime += fireRate;
            Fire();
        }
    }

	private void Fire () {
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		GetComponent<AudioSource>().Play();
	}
}
