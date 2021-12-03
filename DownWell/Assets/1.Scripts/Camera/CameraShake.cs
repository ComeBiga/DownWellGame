using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = .2f;
    public float magnitude = 1f;

    public void Shake(float _magnitude, float duration = .2f, bool vertical = false)
    {
        StartCoroutine(EShake(_magnitude, duration, vertical));
    }

    public IEnumerator EShake(float _magnitude, float duration, bool vertical = false)
    {
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float xMagnitude = _magnitude;
            float x = Random.Range(-1f, 1f) * _magnitude;
            //float x = _magnitude * -1;
            float y = (vertical == true)? Random.Range(-1f, 1f) * _magnitude : 0;
            
            //if(SettingMgr.instance != null && !SettingMgr.instance.gPaused)
                transform.localPosition += new Vector3(x, y, 0);
            //transform.localPosition = new Vector3(xMagnitude * -1, transform.localPosition.y, transform.localPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = new Vector3(0, transform.position.y, transform.position.z);
    }
}
