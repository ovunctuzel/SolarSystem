using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    private Vector3 defaultPos;
    private bool shaking = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(shaking)
        {
            transform.localPosition = defaultPos + Random.insideUnitSphere * 0.5f;
        }
        else
        {
            transform.localPosition = defaultPos;
        }
    }

    public void ToggleShake(bool value)
    {
        shaking = value;
    }
}
