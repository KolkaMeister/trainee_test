using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : Human
{
    [Header("Movement")]
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float speed=1f;
    [SerializeField] Transform aim;
    [SerializeField] LayerMask mouseDetectionLayer;
    [SerializeField] Rigidbody rb;

    [Header("Weapons Inventory")]
    [SerializeField] private Weapon pistol;
    [SerializeField] private Weapon shotGun;
    [SerializeField] private Weapon assaultRifle;
    [SerializeField] private Weapon electroWeapon;

    
    private void Start()
    {
        onDieEvent += () =>
          {
              SceneManager.LoadScene(0);
          };
    }

    private void Update()
    {
        Move();
        RotateToMousePos();
        if (Input.GetKey(KeyCode.Mouse0)&& currentWeapon.IsAutomatic)
        {
            Shoot();
        }
    }
    //Movement and Rotation
    private void RotateToMousePos()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var isRayOnGround = Physics.Raycast(ray, out var hitInfo, 10000, mouseDetectionLayer, QueryTriggerInteraction.Ignore);
        if (isRayOnGround)
        {
            var direction = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z) - transform.position;
            direction.y = 0;
            Rotate(direction);
        }
    }
    private void Move()
    {
        var forceVector = new Vector3(moveDirection.x*speed,0,moveDirection.y * speed);
        rb.velocity = forceVector;
    }
    //Shoot and reload
    public void SetMoveDirection(Vector2 _direction)
    {
        moveDirection = _direction;
    }


    //////////////////////// Player Input ///////////////////////////
    public void SetDirectionPressed(InputAction.CallbackContext context)
    {
        SetMoveDirection(context.ReadValue<Vector2>());
    }
    
    public void ShootPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Shoot();
        }
        Debug.Log("PUE");
    }
    public void ReloadPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Reload();
        }
    }
    public void PistolSwitchPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwitchWeapon(pistol);
        }
    }
    public void SecondaryWeaponSwitchPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwitchWeapon(shotGun);
        }
    }
    public void ThirdWeaponSwitchPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwitchWeapon(assaultRifle);
        }
    }

    public void ElectroWeaponSwitchPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwitchWeapon(electroWeapon);
        }
    }
}
