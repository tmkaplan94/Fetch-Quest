using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAchievement : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text text;
    void Start()
    {
        
    }

    
    public void ChangeText(string a)
    {
        text.text = a;
    }

    
}
