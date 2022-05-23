using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text text;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ChangeText("hello");
            panel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            panel.SetActive(false);
        }

    }
    void LateUpdate()
    {
        Camera camera = Camera.main;
        transform.LookAt(Camera.main.transform.position, Vector3.up);

    }
    public void ChangeText(string a)
    {
        text.text = a;
    }
}
