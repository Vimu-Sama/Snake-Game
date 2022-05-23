using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItself : MonoBehaviour
{
    [SerializeField] float maxLife= 10f;
    private void Start()
    {
        StartCoroutine(DestroyFood());
    }


    IEnumerator DestroyFood()
    {
        yield return new WaitForSeconds(maxLife);
        Destroy(gameObject);
    }
}
