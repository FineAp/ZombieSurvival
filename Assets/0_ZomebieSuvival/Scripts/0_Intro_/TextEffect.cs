using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    public string m_text;

    public float textSpeed;

    public Text tx;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("typing");   
    }

    IEnumerator typing()
    {
        yield return new WaitForSeconds(0f);
        for (int i = 0; i<= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);   
        }
    
    }


}