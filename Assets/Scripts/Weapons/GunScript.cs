﻿
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public bool damage = false;
    public float range = 100f;
    public float power = 1000;
    public float fireRate = 15f;
    public GameObject gameOver;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    // temp
    public GameObject Player;
    public GameObject bullet;
    CustomCarolForce fire;

    private float nextFireTime = 0f;

    //BULLET VARIABLES
    public float bulletStartingForceMagnitude;
    public float bulletForceApplicationTime;

    // Update is called once per frame
    void Update()
    {
        if (gameOver.activeSelf)
            return;
        //Checks if player hits the fire button (right mouse click), if so execute Shoot()
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 5f / fireRate;
            Shoot();
            FindObjectOfType<AudioManager>().Play("Gunshot"); //Plays death sound after death
        }
    }

    //Shoots the gun
    //Plays muzzleFlash animation and uses Raycast to hit object
    void Shoot()
    {
        muzzleFlash.Play();

        GameObject bulletObj;
        bulletObj = Instantiate(bullet, fpsCam.transform.position + fpsCam.transform.forward * 0.05f, fpsCam.transform.rotation);
        bulletObj.transform.forward = fpsCam.transform.forward;

        CustomTraditionalForce force = bulletObj.AddComponent<CustomTraditionalForce>();

        force.Force = fpsCam.transform.forward * bulletStartingForceMagnitude;

        force.ApplyForce(bulletObj.GetComponent<ForceObject>(), true, bulletForceApplicationTime);
        


        //RaycastHit hit;
        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out hit, range))
        //{
        //    Debug.Log(hit.transform.name);
        //    // generates a bullet

        //    // the death hit
        //    // if (hit.rigidbody != null) {
        //    //     hit.rigidbody.AddForceAtPosition(ray.direction * power, hit.point);
        //    // }

        //    Target target = hit.transform.GetComponent<Target>();
        //    if (target != null) //If target is hit, turn damage on and send bool to GetHit under Target
        //    {
        //        damage = true;
        //        target.GetHit(damage);
        //    }
        //}
    }
}
