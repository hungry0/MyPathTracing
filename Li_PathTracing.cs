using UnityEngine;
using System.Collections;

public class Li_PathTracing : MonoBehaviour
{
    public Texture2D m_Texture;

    private Texture2D m_PathTracingTexture;

    void Awake()
    {
        m_PathTracingTexture = new Texture2D(512, 512, TextureFormat.ARGB32, false);
    }

	void Start ()
	{
	    PathTracing();
	}

    void PathTracing()
    {
        for (int i = 0; i < 512; i++)
        {
            for (int j = 0; j < 512; j++)
            {
                m_PathTracingTexture.SetPixel(i, j, FireOneRay(new Vector3(i / 512.0f - 0.5f, j / 512.0f-0.5f, 0.8f)));
            }
        }

        m_PathTracingTexture.Apply();
    }
	
	void Update () {
	
	}

    public void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), m_PathTracingTexture);
    }

    Color FireOneRay(Vector3 direction)
    {
        Color color = Color.black;

        Ray ray = new Ray(Camera.main.transform.position,direction);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            color = GetColliderGOColor(hit);
        }

        return color;
    }

    Color GetColliderGOColor(RaycastHit hit)
    {
        Texture2D texture2D = hit.collider.gameObject.GetComponent<Renderer>().material.mainTexture as Texture2D;
        Vector2 textureCoord = hit.textureCoord;

        if(texture2D)
            return texture2D.GetPixel((int)textureCoord.x,(int)textureCoord.y);

        return Color.white;
    }

    
}
