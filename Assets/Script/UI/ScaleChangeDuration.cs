using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChangeDuration : MonoBehaviour
{
    [SerializeField] float minSpeed = 20f;
    [SerializeField] float maxSpeed = 30f;
    [SerializeField] float maxScale = 50f;
    void Update()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        transform.localScale += Vector3.one * speed * Time.deltaTime;
        if(transform.localScale.x >= maxScale)
        {
            Destroy(gameObject);
        }
    }
}
