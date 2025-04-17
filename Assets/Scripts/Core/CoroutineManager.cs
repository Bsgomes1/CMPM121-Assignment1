using UnityEngine;
using System.Collections;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Run(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
