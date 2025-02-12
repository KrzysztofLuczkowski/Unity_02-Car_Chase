using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GrayscaleEffect : MonoBehaviour
{
    // Materia³ wykorzystuj¹cy shader grayscale (przypisz w Inspectorze)
    public Material grayscaleMaterial;
    // Flaga okreœlaj¹ca, czy efekt ma byæ aktywny
    public bool effectEnabled = false;

    // Metoda przetwarzaj¹ca obraz kamery
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (effectEnabled && grayscaleMaterial != null)
        {
            Graphics.Blit(src, dest, grayscaleMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

    // Metoda do w³¹czania/wy³¹czania efektu
    public void SetEffect(bool enabled)
    {
        effectEnabled = enabled;
    }
}
