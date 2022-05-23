using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private bool _lethal;
    // Start is called before the first frame update
    private void Update()
    {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        AIController ai =  other.gameObject.GetComponentInChildren<AIController>();
        if (ai != null && _lethal)
        {
            Debug.Log("zombie attempt");
            ai.Zombify();
        }
        Destroy(this.gameObject);
    }
}
