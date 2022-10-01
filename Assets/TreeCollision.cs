using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorForestScenery;

[RequireComponent(typeof(SceneryItem))]
[RequireComponent(typeof(BoxCollider2D))]
public class TreeCollision : MonoBehaviour
{
    SceneryItem tree;

    void Start()
    {
        tree = GetComponent<SceneryItem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        tree.TriggerParticle();
    }
}
