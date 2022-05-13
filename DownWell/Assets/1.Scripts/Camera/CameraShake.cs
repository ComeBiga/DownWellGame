using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = .2f;
    public float magnitude = 1f;

    private int dir = -1;

    private void Start()
    {
        //Shake(.02f, 100f);
    }

    private void LateUpdate()
    {
        //float xMagnitude = magnitude;
        //float x = Random.Range(0, 1f) * magnitude;

        //transform.localPosition = new Vector3(x * dir, transform.localPosition.y, transform.localPosition.z);
        //dir *= -1;
    }

    public void Shake(float _magnitude, float duration = .2f, bool vertical = false)
    {
        StartCoroutine(EShake(_magnitude, duration, vertical));
    }

    public IEnumerator EShake(float _magnitude, float duration, bool vertical = false)
    {
        float elapsed = 0.0f;
        int dir = -1;

        while(elapsed < duration)
        {
            float xMagnitude = _magnitude;
            float x = Random.Range(0, 1f) * _magnitude;
            //float x = _magnitude * -1;
            float y = (vertical == true)? Random.Range(-1f, 1f) * _magnitude : 0;
            
            //if(SettingMgr.instance != null && !SettingMgr.instance.gPaused)
            transform.position = new Vector3(x * dir, transform.position.y, transform.position.z);
            dir *= -1;
            //transform.localPosition = new Vector3(xMagnitude * -1, transform.localPosition.y, transform.localPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }
}
