using UnityEngine;
using TMPro;

public class GuardianState : MonoBehaviour
{
    public Transform arCamera;
    public TMP_Text statusText;
    public Renderer cubeRenderer;

    static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    static readonly int GlowID = Shader.PropertyToID("_GlowIntensity");

    void Update()
    {
        float distance = Vector3.Distance(arCamera.position, transform.position);

        if (distance < 0.2f)
        {
            cubeRenderer.material.SetColor(BaseColorID, Color.red);
            cubeRenderer.material.SetFloat(GlowID, 4f);
            statusText.text = "CRITICAL HALT // BACK AWAY";
        }
        else if (distance < 0.5f)
        {
            cubeRenderer.material.SetColor(BaseColorID, Color.yellow);
            cubeRenderer.material.SetFloat(GlowID, 2.5f);
            statusText.text = "WARNING: RESTRICTED AREA";
        }
        else
        {
            cubeRenderer.material.SetColor(BaseColorID, Color.green);
            cubeRenderer.material.SetFloat(GlowID, 1.2f);
            statusText.text = "SYSTEM ARMED";
        }
    }
}
