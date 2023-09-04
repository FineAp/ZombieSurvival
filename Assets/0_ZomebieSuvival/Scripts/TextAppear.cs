using UnityEngine;
using UnityEngine.UI;

public class TextAppear : MonoBehaviour
{
    public GameObject img;
    public GameObject[] texts = new GameObject[5];

    public Text text;

    void Start()
    {
        texts[0].SetActive(true);
        Invoke("OneText", 3f);
        Invoke("TwoText", 6f);
        Invoke("ThreeText", 9f);
        Invoke("FourText", 12f);
        Invoke("FiveText", 14f);

    }

    // Update is called once per frame
    void OneText()
    {
        Destroy(texts[0], 4f);
        texts[1].SetActive(true);
    
    }

    void TwoText()
    {
        Destroy(texts[1], 4f);
        texts[2].SetActive(true);
    }

    void ThreeText()
    {
        Destroy(texts[2], 4f);
        texts[3].SetActive(true);
    }

    void FourText()
    {
        Destroy(texts[3], 4f);
        texts[4].SetActive(true);
    }

    void FiveText()
    {
        Destroy(this,2.5f);
        Destroy(text, 2.5f);
        Destroy(img, 2.5f);
    }
}
