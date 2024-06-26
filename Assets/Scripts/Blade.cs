
using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private SkinManager skinManager;
    public float minVelo = 0.1f;
    private Rigidbody2D rb;


    private AudioSource audioSource;
    private Renderer bladeRenderer;


    private Vector3 lastMousePos;
    private Vector3 mouseVelo;
    private Collider2D coll;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
        bladeRenderer = GetComponent<Renderer>();
        UpdateBladeSkin();
    }

    void Update()
    {
        SetBladeToMouse();
        UpdateBladeSkin();
    }
    

    // Sets the blade's position to follow the mouse cursor
    private void SetBladeToMouse(){
        var mousePos = Input.mousePosition;
        mousePos.z = 10;
        rb.position = Camera.main.ScreenToWorldPoint(mousePos);  
    }


    // Checks if the mouse is moving
    private bool IsMouseMoving(){
        Vector3 currMousePos = transform.position;
        float traveled = (lastMousePos - currMousePos).magnitude;
        lastMousePos = currMousePos;

        if(traveled > minVelo) return true;
        else return false;
    }


    // Updates the blade's appearance based on the selected skin
    public void UpdateBladeSkin()
    {
        Skin selectedSkin = skinManager.GetSelectedSkin(); // Get the currently selected skin
        if (bladeRenderer != null)
        {
            bladeRenderer.material = selectedSkin.material; // Apply the skin's material to the blade's renderer
        }
    }
}
