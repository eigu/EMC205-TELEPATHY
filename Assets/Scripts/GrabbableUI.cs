using UnityEngine;
using TMPro;

public class GrabbableUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI grabbableText;

    private void Update()
    {
        UpdateGrabbableText();
    }

    private void UpdateGrabbableText()
    {
        Ray ray = InputManager.Instance.GetCrosshairPoint();
        RaycastHit hit;

        Player playerScript = FindObjectOfType<Player>();

        if (playerScript.grabbedObject)
        {
            grabbableText.text = "Grabbed!";
            return;
        }

        if (playerScript.CheckIfPlayerGrabbedAnObject(ray, out hit))
        {
            grabbableText.text = "Grabbable!";
            return;
        }

        grabbableText.text = "";
    }
}
