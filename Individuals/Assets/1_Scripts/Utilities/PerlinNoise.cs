using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    public float scale = 20;

    public float xOffset = 100f;
    public float yOffset = 100f;


    void Start()
    {
        xOffset = Random.Range(0f, 99999f);
        yOffset = Random.Range(0f, 99999f);
    }

    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;  
    }

    Color CalculateColor (int x, int y)
    {
        float xCoord = (float)x / width * scale + xOffset;
        float yCoord = (float)y / height * scale + yOffset;
        
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color (sample, sample, sample);
    }
}
