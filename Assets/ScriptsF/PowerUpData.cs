using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpData : MonoBehaviour
{
    public float moveSpeed = 5;
    public int pointValue = 1000;
    public bool activateShield;
    public bool tripleShoot;
    public bool dashSkill;

    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.y -= moveSpeed * Time.fixedDeltaTime;

        transform.position = pos;
    }
}
