using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Patterns;

public class InvisibleWalls : Singleton<InvisibleWalls>
{
    [SerializeField] GameObject wallNorth;
    [SerializeField] GameObject signNorth;
    [Space]
    [SerializeField] GameObject wallSouth;
    [SerializeField] GameObject signSouth;

    public void SetWalls(bool state)
    {
        //wallNorth.SetActive(state);
        //wallSouth.SetActive(state);
    }
}
