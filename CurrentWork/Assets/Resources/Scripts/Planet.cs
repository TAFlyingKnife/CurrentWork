using UnityEngine;

public class Planet : MonoBehaviour
{
    public void Update()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }
}
