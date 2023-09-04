using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public string str;
    public static int score;
    
    private Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        score = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        text.text = str + score;  
    }
}
