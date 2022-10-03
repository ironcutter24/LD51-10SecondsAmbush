using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] GameObject gfx;
    [SerializeField] float rotationSpeed = 1f;

    void Update()
    {
        gfx.transform.Rotate(0f, 0f, rotationSpeed * Time.timeScale);
    }
}
