using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _moveSpeed;

    public static event Action Throw;
    public static event Action<Transform> Attract;

    private Rigidbody2D _rb;

    private void Start() => _rb = GetComponent<Rigidbody2D>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Throw.Invoke(); //invoke throw event if LMB is pressed
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
        if (Input.GetKey(KeyCode.Space)) Attract.Invoke(transform); //invoke attract event if pressing space
    }

    private void HandlePlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x == 0 && y == 0) return; //don't continue unless there was input

        Vector3 moveDir = new Vector3(x, y, 0f);
        _rb.AddForce(moveDir * _moveSpeed);
    }
}