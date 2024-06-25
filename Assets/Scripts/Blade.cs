
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
        //GetComponent<SpriteRenderer>().sprite = skinManager.GetSelectedSkin().sprite;
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

    private void SetBladeToMouse(){
        var mousePos = Input.mousePosition;
        mousePos.z = 10;
        rb.position = Camera.main.ScreenToWorldPoint(mousePos);  
    }

    private bool IsMouseMoving(){
        Vector3 currMousePos = transform.position;
        float traveled = (lastMousePos - currMousePos).magnitude;
        lastMousePos = currMousePos;

        if(traveled > minVelo) return true;
        else return false;
    }

    public void UpdateBladeSkin()
    {
        Skin selectedSkin = skinManager.GetSelectedSkin();
        //GetComponent<SpriteRenderer>().sprite = selectedSkin.sprite;
        if (bladeRenderer != null)
        {
            bladeRenderer.material = selectedSkin.material;
        }
    }
}
