using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocity;
    // Start is called before the first frame update
    private void Update()
    {
        transform.position += Vector3.forward * velocity * Time.deltaTime;
    }
}
