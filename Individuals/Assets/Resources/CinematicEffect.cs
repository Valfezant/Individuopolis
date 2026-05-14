using UnityEngine;

[CreateAssetMenu(menuName = "Grain", order = 1)]
public class CinematicEffect : BaseEffect
{
    [SerializeField] private float strenght = 0.1f;

    [SerializeField] private float aspectRatio = 1.777f;

    // Find the Cinematic shader source
    public override void OnCreate()
    {
        baseMaterial = new Material(Resources.Load<Shader>("Cinematic"));

        baseMaterial.SetFloat("_Strenght", strenght);
        baseMaterial.SetFloat("_Aspect", aspectRatio);
    }

    public override void Render(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, baseMaterial);
    }
}