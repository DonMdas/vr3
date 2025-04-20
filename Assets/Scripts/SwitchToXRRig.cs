using UnityEngine;

public class SwitchToXRRig : MonoBehaviour
{
    public GameObject ovrRig;
    public GameObject xrRig;

    private bool hasSwitched = false;

    // Call this method when you want to switch rigs (e.g., after timer ends)
    public void SwitchRig()
    {
        if (hasSwitched || ovrRig == null || xrRig == null) return;

        // Match position and rotation
        xrRig.transform.position = ovrRig.transform.position;
        xrRig.transform.rotation = ovrRig.transform.rotation;

        // Swap rigs
        ovrRig.SetActive(false);
        xrRig.SetActive(true);

        hasSwitched = true;
    }
}
