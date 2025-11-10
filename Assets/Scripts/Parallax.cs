using UnityEngine;

public class Parallax : MonoBehaviour
{
   private MeshRenderer meshRenderer;
   public float animationSeed = 0.15f;

    private void Awake()
    {
            meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(animationSeed * Time.deltaTime, 0);
    }

}
