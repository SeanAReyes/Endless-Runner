using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float LeftEdge;

    private void Start()
    {
        LeftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }
    private void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

        if (transform.position.x < LeftEdge)
        {
            Destroy(gameObject);
        }
    }
}
