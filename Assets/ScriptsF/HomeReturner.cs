using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeReturner : MonoBehaviour
{
    public Canvas canvas;
    public void ReturnMenu()
    {
        canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
