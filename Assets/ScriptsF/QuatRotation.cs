using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuatRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        float rotationAngle = rotationSpeed * Time.deltaTime;//Declarar y multiplicar
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);//Declarar, asignar Quaternion que crea una rotaci�n 3D, Se especifica un �ngulo de rotaci�n en el eje Z utilizando rotationAngle
        transform.rotation *= rotation;//Actualiza la rotaci�n del objeto al multiplicar su rotaci�n actual por la nueva rotaci�n
    }
}

