using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boundary {
    public float xMinimun, xMaximun, yMinimun, yMaximun;
}
public class PlayerController : MonoBehaviour
{
    //public Mover moverComponent;
    public float speed;
    public Boundary boundary;
    public Transform shootOrigin;
    public GameObject shootPrefab;
    public GameObject shootSound;
    private float lastFire = 0.0f; 
    public float fireDelay = 0.5f;
    private void Start ()
    {
        //moverComponent.speed = speed;
        //InputProvider.OnHasShoot += OnHasShoot;
        InputProvider.OnDirection += OnDirection;
    }
    private void OnDirection(Vector3 direction)
    {
        //moverComponent.direction = direction;
    }
    private void OnHasShoot()
    {
        Instantiate(shootPrefab, shootOrigin, false);
    }
    void Update()
    {
        //X =-8.3, 8.3 Y = -4.5, 4.5
        float x = Mathf.Clamp(transform.position.x, boundary.xMinimun, boundary.xMaximun);
        float y = Mathf.Clamp(transform.position.y, boundary.yMinimun, boundary.yMaximun);
        transform.position = new Vector3(x, y);

        if(Input.GetButton("Shoot") && Time.time > lastFire)
        {
            lastFire = Time.time + fireDelay;
            GameObject clone = Instantiate(shootPrefab, shootOrigin.position, transform.rotation) as GameObject;
            Instantiate(shootSound);
        }
        
        
    }
}