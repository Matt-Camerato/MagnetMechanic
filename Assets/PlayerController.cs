using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _moveSpeed;

    [Header("Magnet Settings")]
    [SerializeField] private float _magnetThrowPower;
    [SerializeField] private GameObject _magnetPrefab;

    private Rigidbody2D _rb;

    private void Start() => _rb = GetComponent<Rigidbody2D>();
    private void FixedUpdate() => HandleMovement();
    private void Update() => HandleThrowing();

    //handles all player movement
    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x == 0 && y == 0) return; //don't continue unless there was input

        Vector3 moveDir = new Vector3(x, y, 0f);
        _rb.AddForce(moveDir * _moveSpeed);
    }

    //handles throwing magnets when LMB is pressed
    private void HandleThrowing()
    {
        if (!Input.GetMouseButtonDown(0)) return; //don't continue unless player presses LMB

        //create the magnet and give it reference to the player's transform
        GameObject magnet = Instantiate(_magnetPrefab, transform.position, Quaternion.identity);
        magnet.GetComponent<Magnet>().playerTransform = transform;

        //apply an impulse force to the magent in the direction of the player's mouse
        Vector2 forceDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        magnet.GetComponent<Rigidbody2D>()?.AddForce(forceDir.normalized * _magnetThrowPower, ForceMode2D.Impulse);
    }
}