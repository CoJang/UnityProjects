using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float BlinkTime = 0.25f;
    MeshRenderer meshRenderer = null;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }


    public void Damaged()
    {
        Debug.Log("Damaged!");
        StartCoroutine(OnHit(2));
    }

    IEnumerator OnHit(int blinkCount)
    {
        for(int i = 0; i < blinkCount; i++)
        {
            meshRenderer.material.color = Color.red;
            yield return new WaitForSeconds(BlinkTime);
            meshRenderer.material.color = Color.white;
            yield return new WaitForSeconds(BlinkTime);
        }
    }
}
