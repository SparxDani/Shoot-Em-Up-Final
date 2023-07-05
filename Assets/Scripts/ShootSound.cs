using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSound : MonoBehaviour
{
    public float shootSoundLifeTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, shootSoundLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
