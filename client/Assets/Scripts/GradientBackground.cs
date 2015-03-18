using UnityEngine;

public class GradientBackground : MonoBehaviour
{

  public Color topColor = Color.blue;
  public Color bottomColor = Color.white;
  public int gradientLayer = 7;

  private int currentSkin = 0;
  private Mesh backgroundMesh;

  void Awake()
  {
    DrawGradientBackground();
  }

  void Update()
  {
    if (currentSkin != PlayerPrefs.GetInt("MapSkin", 0))
    {
      UpdateBackground();
    }
  }

  void UpdateColors()
  {
    currentSkin = PlayerPrefs.GetInt("MapSkin", 0);

    if (currentSkin == 0)
    {
      topColor = new Color(0.58f, 0.77f, 0.83f);
      bottomColor = new Color(0.77f, 0.87f, 0.91f);
    }
    else if (currentSkin == 1)
    {
      topColor = new Color(0.7f, 0.86f, 1f);
      bottomColor = new Color(0.7f, 0.86f, 1f);
    }
    else if (currentSkin == 2)
    {
      topColor = Color.white;
      bottomColor = Color.white;
    }
  }

  void DrawGradientBackground()
  {
    UpdateColors();

    gradientLayer = Mathf.Clamp(gradientLayer, 0, 31);
    if (!GetComponent<Camera>())
    {
      Debug.LogError("Must attach GradientBackground script to the camera");
      return;
    }

    GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
    GetComponent<Camera>().cullingMask = GetComponent<Camera>().cullingMask & ~(1 << gradientLayer);
    Camera gradientCam = new GameObject("Gradient Cam", typeof(Camera)).GetComponent<Camera>();
    gradientCam.depth = GetComponent<Camera>().depth - 1;
    gradientCam.cullingMask = 1 << gradientLayer;

    backgroundMesh = new Mesh();
    backgroundMesh.vertices = new Vector3[4] { new Vector3(-100f, .577f, 1f), new Vector3(100f, .577f, 1f), new Vector3(-100f, -.577f, 1f), new Vector3(100f, -.577f, 1f) };

    backgroundMesh.colors = new Color[4] { topColor, topColor, bottomColor, bottomColor };

    backgroundMesh.triangles = new int[6] { 0, 1, 2, 1, 3, 2 };

    Material mat = new Material("Shader \"Vertex Color Only\"{Subshader{BindChannels{Bind \"vertex\", vertex Bind \"color\", color}Pass{}}}");
    GameObject gradientPlane = new GameObject("Gradient Plane", typeof(MeshFilter), typeof(MeshRenderer));

    ((MeshFilter)gradientPlane.GetComponent(typeof(MeshFilter))).mesh = backgroundMesh;
    gradientPlane.GetComponent<Renderer>().material = mat;
    gradientPlane.layer = gradientLayer;
  }

  void UpdateBackground()
  {
    UpdateColors();
    backgroundMesh.colors = new Color[4] { topColor, topColor, bottomColor, bottomColor };
  }

}
