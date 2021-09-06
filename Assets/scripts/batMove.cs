using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batMove : MonoBehaviour
{
    private float RotateSpeed = 5f;
    private float Radius = 1f;

    private Vector2 _centre;
    private float _angle;

    public bool dead = false;

    private void Start()
    {
        _centre = transform.position;
    }

    private void Update()
    {
        if(!dead)
        {
            _angle += RotateSpeed * Time.deltaTime;

            var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
            transform.position = _centre + offset;
        }
    }
}
