using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EnemyManagement

{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        if (collidedWithDestroyer)
        {
            DestroyDestructable();
            collidedWithDestroyer = false;
        }
    }
}
