using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraCaptureManager : MonoBehaviour
{
    [SerializeField] RawImage _rawImage;

    private WebCamTexture _webCamTexture;
    
    private void Start()
    {
        _webCamTexture = new WebCamTexture();
        _rawImage.material.mainTexture = _webCamTexture;
    }

    public void TakePhoto()
    {
        StartCoroutine(TakePhotoAsync()); 
    }

    public void StartCamera()
    {
        StartCoroutine(StartcameraAsync()); 
    }
    
    private IEnumerator StartcameraAsync()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("webcam found");
            _webCamTexture.Play();
        }
        else
        {
            Debug.Log("webcam not found");
        }

    }

    public void StopCamera()
    {
        _webCamTexture.Stop(); 
    } 
    
    IEnumerator TakePhotoAsync()
    {
        yield return new WaitForEndOfFrame();
        Texture2D photo = new Texture2D(_webCamTexture.width, _webCamTexture.height);
        photo.SetPixels(_webCamTexture.GetPixels());
        photo.Apply();
    }
}
