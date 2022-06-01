using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text text;
    [SerializeField] private bool isBoss;
    [SerializeField] private string[] phrases;

    void Start()
    {
        StartCoroutine(randomCall());        
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            int index = Random.Range(0, phrases.Length - 1);
            print(phrases.Length - 1);
            ChangeText(phrases[index]);
            panel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            panel.SetActive(false);
        }
        */
    }
    void LateUpdate()
    {
        Camera camera = (Camera) FindObjectOfType(typeof(Camera));
        //Camera camera = Camera.main;
        transform.LookAt(camera.transform.position, Vector3.up);

    }
    public void ChangeText(string a)
    {
        text.text = a;
    }

    private IEnumerator randomCall()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(10, 20));
            changePhrase();
            yield return new WaitForSeconds(3);
            panel.SetActive(false);
        }
    }

    private void changePhrase()
    {
        int index = Random.Range(0, phrases.Length - 1);
        print(phrases.Length - 1);
        ChangeText(phrases[index]);
        panel.SetActive(true);
    }

    public IEnumerator getFired()
    {
        if (isBoss)
        {
            ChangeText("I swear I'll fire him!");
            panel.SetActive(true);
            yield return new WaitForSeconds(3);
            panel.SetActive(false);
        }
    }
}
