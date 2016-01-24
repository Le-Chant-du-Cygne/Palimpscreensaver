using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour
{
    public Transform corridor;
    public float corridorGenerationTime;
    public int minScale;
    public int maxScale;

    private Transform corridors;
    private Texture[] textures;

    private float corridorGenerationTimer;
    private Vector3 currentPosition;

	void Start ()
    {
        corridors = GameObject.Find("Corridors").transform;
        textures = Resources.LoadAll<Texture>("Textures/");

        corridorGenerationTimer = 0f;
        currentPosition = Vector3.zero;
	}
	
	void Update ()
    {
	    if (corridorGenerationTimer >= corridorGenerationTime && corridors.childCount < 100)
        {
            spawnRandomCorridor();
            corridorGenerationTimer = 0f;
        }
        corridorGenerationTimer += Time.deltaTime;
	}

    void spawnRandomCorridor()
    {
        Transform newCorridor = Instantiate(corridor, Vector3.zero, Quaternion.identity) as Transform;
        newCorridor.parent = corridors;
        Vector3 scale = new Vector3(1f, 1f, Random.Range(minScale, maxScale + 1));
        newCorridor.localScale = scale;
        newCorridor.position = currentPosition;
        currentPosition = new Vector3(0f, 0f, currentPosition.z);
        currentPosition += new Vector3(0f, 0f, 4f * scale.z);

        // Texture
        MeshRenderer[] renderers = newCorridor.GetComponentsInChildren<MeshRenderer>();
        Color color1 = Random.ColorHSV(),
            color2 = Random.ColorHSV(),
            color3 = Random.ColorHSV();
        Texture randomTexture1 = textures[Random.Range(0, textures.Length - 1)];
        Texture randomTexture2 = textures[Random.Range(0, textures.Length - 1)];
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material.SetColor("_Color1", color1);
            renderer.material.SetColor("_Color2", color2);
            renderer.material.SetColor("_Color3", color3);

            renderer.material.SetTexture("_Illum", randomTexture1);
            renderer.material.SetTexture("_Illum2", randomTexture2);
        }
    }
}
