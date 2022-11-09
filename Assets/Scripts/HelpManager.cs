using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    public static int ZoneIndex = 0;
    
    [SerializeField] private GameObject mainCam;
    [SerializeField] private float cameraDuration;
    
    [SerializeField] private List<GameObject> camerasZone1, camerasZone2, camerasZone3;
    
    private bool _isHelping;

    private const string HelpButton = "Help";
    
    private void Update()
    {
        if (!_isHelping && Input.GetButtonDown(HelpButton))
        {
            _isHelping = true;

            StartCoroutine(nameof(ShowCameras));
        }
    }

    private IEnumerator ShowCameras()
    {
        mainCam.SetActive(false);

        List<GameObject> cams = new List<GameObject>();

        switch (ZoneIndex)
        {
            case 1:
                cams = camerasZone1;
                break;
            case 2:
                cams = camerasZone2;
                break;
            case 3:
                cams = camerasZone3;
                break;
        }
        
        foreach (var cam in cams)
        {
            cam.SetActive(true);

            yield return new WaitForSeconds(cameraDuration);
            
            cam.SetActive(false);
        }
        
        mainCam.SetActive(true);
        _isHelping = false;
    }
}
