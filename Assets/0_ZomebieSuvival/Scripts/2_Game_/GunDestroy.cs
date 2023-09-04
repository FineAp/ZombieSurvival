using UnityEngine;

public class GunDestroy : MonoBehaviour
{
    public float destroyTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

}
