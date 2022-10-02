using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] Collider2D rbCollider;

    HashSet<Collider2D> wallColliders = new HashSet<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            wallColliders.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            wallColliders.Remove(collision);

            if (wallColliders.Count <= 0)
                rbCollider.enabled = true;
        }
    }
}
