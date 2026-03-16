using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private int coinValue = 50;

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
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        return;

        GameManager.Instance.AddScore(coinValue);
        AudioManager.instance.PlaySoundFXClip(coinClip, transform, 0.7f);
        Destroy(gameObject);
    }
}