// Perlin noise script based on Unity's perlin noise generator.
// Modifications made to generate prefab asteroids based on the pixel values across the x and y axis of the generated perlin noise texture
// Rob Hill - Pixel Dot Studios, LLC

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

    // Variables for the perlin noise texture, color, and renderer
    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;

    // Variable to store a vector3 array of positional data.
    // To be used to store the x,y,z transform.position of generated asteroids
    private Vector3[] asteroidArray;

    // Gameobject 'go' from initial testing, used for creating primative spheres.  sphereScale variable is the size (scale) of the sphere, in all three directions
    // Leaving in as placeholder if script is to be duplicated for other asteroid fields.
    // ******* Remove or comment out at final release -- Unneccessary creation of game objects
    private GameObject go;
    public float sphereScale = 10f;

    // Variables to increase the spread of asteroids in the XY plane, and the Z plane
    public int asteroidSpreadXY = 20;
    public float asteroidSpreadZ = 400f;
    
    // Asteroid prefab.  Iterate script to have multiple game objects, and generate different types of asteroids based on unused ranges in generateAsteroidArray function
    public GameObject asteroid1;


    void Awake()
    {
        // Create array of Vector3's called asteroidArray
        asteroidArray = new Vector3[255];

        // Old - Create game object that's a sphere, provide it a mesh component, and scale the sphere
        // ******* Remove or comment out at final release -- Unneccessary creation of game objects
        go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.GetComponent<MeshRenderer>();
        go.transform.localScale = new Vector3(sphereScale, sphereScale, sphereScale);
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


    // Nested loop to assign a float value to each pixel.
    // Assign values to pixels starting at (0,0), to (pixWidth, 0)...
    // Increment to the next row starting at (0,1), to (pixWidth, 1)...
    // Finish at (pixWidth, pixHeight)
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


    // Walk through each pixel, using the same nested loop approach as the CalcNoise method
    // If the pixel has a value above 0.4F, instantiate an asteroid at the same x,y coordinate as the pixel in the texture2D.
    // Assign the z transform.position from the value of the pixel, with a multiplier due to the grayscale value being too small (0 -> 1)
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
                    Instantiate(asteroid1, asteroidArray[i], new Quaternion(0f, 0f, 0f, 1f));
                    x++;
                    i++;
                }
             x++;
            }
            y++;
        }
    }
}
