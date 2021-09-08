using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = .2f;
    public float magnitude = 1f;

    public IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition += new Vector3(x, 0, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = new Vector3(0, transform.position.y, transform.position.z);
    }
}
