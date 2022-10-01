using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorForestScenery;

public class WorldBounds : MonoBehaviour
{
    [SerializeField] LayerMask treesMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bounds collision");

        var colliders = Physics2D.OverlapCircleAll(collision.GetContact(0).point, 2f, treesMask);

        Debug.Log("Colls: " + colliders.Length);

        foreach (var c in colliders)
        {
            

            if (c.gameObject.layer == LayerMask.NameToLayer("Trees"))
            {
                Debug.Log("Layer: " + c.gameObject.layer);

                var tree = c.gameObject.GetComponent<SceneryItem>();
                if (tree != null)
                    tree.TriggerParticle();
            }
        }
    }
}
