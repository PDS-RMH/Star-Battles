using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldGenerator : MonoBehaviour
{
    // Width and height of the texture in pixels.
    public int pixWidth;
    public int pixHeight;

    // The origin of the sampled area in the plane.
    public float xOrg;
    public float yOrg;

    // The number of cycles of the basic noise pattern that are repeated
    // over the width and height of the texture.
    public float scale = 1.0F;

    public float pixelValue = 0f;

    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;

    private Vector3[] asteroidArray;
    private GameObject go;
    public int asteroidSpreadXY = 20;
    public float asteroidSpreadZ = 400f;
    public float asteroidSize = 10f;


    void Awake()
    {
        asteroidArray = new Vector3[255];
        go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.GetComponent<MeshRenderer>();
        go.transform.localScale = new Vector3(asteroidSize, asteroidSize, asteroidSize);
    }
    
    


    void Start()
    {
        rend = GetComponent<Renderer>();

        // Set up the texture and a Color array to hold pixels during processing.
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        rend.material.mainTexture = noiseTex;

        CalcNoise();
        GenerateAsteroidArray();
    }

    void CalcNoise()
    {
        // For each pixel in the texture...
        float y = 0.0F;

        while (y < noiseTex.height)
        {
            float x = 0.0F;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }

        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }

    void GenerateAsteroidArray()
    {

        int y = 0;
        while (y < pixHeight)
        {
            int x = 0;
            while (x < pixWidth)
            {
                int i = 0;
                if (noiseTex.GetPixel(x, y).grayscale > 0.4f)
                {
                    asteroidArray[i] = new Vector3(x * asteroidSpreadXY, y * asteroidSpreadXY, (noiseTex.GetPixel(x, y).grayscale * asteroidSpreadZ) - (asteroidSpreadZ/2));
                    Instantiate(go, asteroidArray[i], new Quaternion(0f, 0f, 0f, 1f));
                    x++;
                    i++;
                }
             x++;
            }
            y++;
        }

    }


    void Update()
    {

//        Debug.Log(noiseTex.GetPixel(20,21).grayscale);

    }
}
