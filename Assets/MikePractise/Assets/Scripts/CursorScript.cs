/*using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorClicked;

    private CursorControl controls;

    private void Awake()
    {
        //Cursor.visible = true;
        controls = new CursorControl();
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void Start()
    {
        controls.Mouse.Click.started += _ => StartedClick();
        controls.Mouse.Click.performed += _ => EndedClick();
    }
    private void ChangeCursor(Texture2D cursorType)
    {
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

    private void StartedClick()
    {
        ChangeCursor(cursorClicked);
    }
    
    private void EndedClick()
    {
        ChangeCursor(cursor);
    }
}

*/