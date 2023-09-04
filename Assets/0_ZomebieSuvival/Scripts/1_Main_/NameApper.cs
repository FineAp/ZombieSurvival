using UnityEngine;

public class NameApper : MonoBehaviour
{
    public GameObject img;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Apper", time);
    }

    void Apper()
    {
        img.SetActive(true);
    }

}
