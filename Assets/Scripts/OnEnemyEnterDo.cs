using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnemyEnterDo : MonoBehaviour
{
    [SerializeField] private UnityEvent alwaysActions;
    [SerializeField] List<string> tagsToIgnore;
    [SerializeField] private  UnityEvent unignoredActions;

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        alwaysActions.Invoke();
        foreach(var ignoreTag in tagsToIgnore)
        {
            if (collision.tag == ignoreTag)
            {
                return;
            }
        }
        unignoredActions.Invoke();
    }

}
