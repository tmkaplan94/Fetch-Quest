using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAchievement : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.J))
        {
            ChangeText("hello");
            panel.SetActive(true);
    
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            panel.SetActive(false);
        }*/

        
    }
    public void ChangeText(string a)
    {
        text.text = a;
    }

    
}
