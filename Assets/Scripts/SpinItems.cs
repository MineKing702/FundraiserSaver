using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinItems : MonoBehaviour
{

    public float startY;
    public bool running;

    // Start is called before the first frame update
    void Start()
    {
        running = true;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            transform.Rotate(0, 0.1f, 0 * Time.deltaTime);
            float height = 0.2f;
            Vector3 pos = transform.position;
            float newY = Mathf.Sin(Time.time * 1f);
            transform.position = new Vector3(pos.x, ((newY * height) + startY), pos.z);
        }
    }
}
