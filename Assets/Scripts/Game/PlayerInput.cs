using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private CharacterActions actions;
    private GameInput gameInput;

    private Vector2 input_direction;
    private Vector2 input_mouse_direction;

    private float delay = 1f;

    [SerializeField] private RectTransform movementArea; 
    [SerializeField] private RectTransform aimArea;
    
    public static event Action PlayerDead;

    private bool isDesktop;
    
    private void Awake()
    {
        gameInput = new GameInput();
        gameInput.Enable();
        
        actions = GetComponent<CharacterActions>();

        isDesktop = PlayerPrefs.GetString("device") == "desktop";
    }

    private void OnEnable()
    {
        gameInput.Keyboard.Shoot.performed += Shoot;
        //gameInput.Touchscreen.TouchPress.started += OnTouchPressStarted;
    }

    private void OnDisable()
    {
        PlayerDead?.Invoke();
        gameInput.Keyboard.Shoot.performed -= Shoot;
        //gameInput.Touchscreen.TouchPress.performed -= OnTouchPressStarted;
    }

    // Update is called once per frame
    private void Update()
    {
        if (delay > 0)
            delay -= Time.deltaTime;
        ReadAim();
        ReadMovement();
    }

    private void FixedUpdate()
    {
        Move();
        Aim();
        if (!isDesktop && delay <= 0)
        {
            actions.Shoot();
            delay = 0.5f;
        }
    }

    private void ReadMovement()
    {
        input_direction = gameInput.Touchscreen.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        actions.Move(input_direction);
    }

    private void ReadAim()
    {
        if (!isDesktop)
        {
            input_mouse_direction = gameInput.Touchscreen.Aim.ReadValue<Vector2>();
        }
        else
        {
            var mousePos = gameInput.Touchscreen.Aim.ReadValue<Vector2>();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            input_mouse_direction = (worldPos - transform.position).normalized;
        }
    }

    private void Aim()
    {
        if(input_mouse_direction != Vector2.zero) actions.Aim(input_mouse_direction);
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        if(delay <= 0)
        {
            actions.Shoot();
            delay = 0.75f;
        }
    }

    public void FakeDestroy()
    {
        gameObject.SetActive(false);
    }
}
