using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshairImage;
    public Sprite defaultCrosshair;
    public Sprite interactableCrosshair;
    public float raycastLength = 10f;
    public LayerMask interactableLayer;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastLength, interactableLayer))
        {
            crosshairImage.sprite = interactableCrosshair;
        }
        else
        {
            crosshairImage.sprite = defaultCrosshair;
        }
    }
}
