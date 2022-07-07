using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private GameObject light;

    public KeyCode activateKey = KeyCode.G;

    private bool _isLightActive;
    void Update()
    {
        if (Input.GetKeyDown(activateKey))
        {
            light.SetActive(_isLightActive);
            _isLightActive = !_isLightActive;
        }
    }
}
