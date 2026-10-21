using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speed = 10f;
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
