using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterDo : MonoBehaviour
{
    [SerializeField] private UnityEvent action;
    private GameObject collisionee;
    [SerializeField] private List<string> tagsToIgnore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ShouldIgnoreCollision(collision))
        {
            collisionee = collision.gameObject;
            action.Invoke();
        }
    }

    public void DestroyCollisionee()
    {
        if (collisionee != null)
        {
            Destroy(collisionee);
        }
    }

    private bool ShouldIgnoreCollision(Collider2D collider)
    {
        if (tagsToIgnore.Count == 0)
        {
            return false;
        }

        for (int i = 0; i < tagsToIgnore.Count; i++)
        {
            if (collider.CompareTag(tagsToIgnore[i]))
            {
                return true;
            }
        }

        return false;
    }
}
