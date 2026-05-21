using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Sightseeing : MonoBehaviour
{

    public float zoomOutCamSize = 10f;
    public float zoomTime = 1f;
    private float originalCamSize;
    private CinemachineVirtualCamera  virtualCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();  
        originalCamSize = virtualCamera.m_Lens.OrthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.activeInHierarchy) return;
        if (other.gameObject.CompareTag("Player") && isActiveAndEnabled)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothZoom(virtualCamera.m_Lens.OrthographicSize, zoomOutCamSize, zoomTime));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!gameObject.activeInHierarchy) return;
        if (isActiveAndEnabled)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothZoom(virtualCamera.m_Lens.OrthographicSize, originalCamSize, zoomTime));
        }
    }

    IEnumerator SmoothZoom(float start, float end, float zoomTime)
    {
        for (float t = 0.0f; t < zoomTime; t += Time.deltaTime)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(start, end, t/zoomTime);
            yield return null;
        }
    }
}
