using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject starPrefab;
    public int radius;
    public int count;
    public int scale;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject star = Instantiate(starPrefab);
            star.transform.position = Random.onUnitSphere * Random.Range(radius * 0.8f, radius * 1.2f);
            star.transform.parent = transform;
            star.transform.localScale = Vector3.one * scale * Random.Range(0.5f, 1.5f);
        }
    }
}
