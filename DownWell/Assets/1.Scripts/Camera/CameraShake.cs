using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = .2f;
    public float magnitude = 1f;

    public void Shake()
    {
        StartCoroutine(EShake());
    }

    public IEnumerator EShake()
    {
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float xMagnitude = magnitude;
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition += new Vector3(x, 0, 0);
            //transform.localPosition = new Vector3(xMagnitude * -1, transform.localPosition.y, transform.localPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = new Vector3(0, transform.position.y, transform.position.z);
    }
}
