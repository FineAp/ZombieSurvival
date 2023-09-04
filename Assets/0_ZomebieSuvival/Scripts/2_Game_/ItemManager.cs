using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float x =0f;
    public float y =0f;
    public float z =0f;


    void Update()
    {
        Rotations();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            print("check");
            Destroy(gameObject);
        }
    }

    void Rotations()
    {
        transform.Rotate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime );
    }
}
