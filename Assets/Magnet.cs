using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Magnet : MonoBehaviour
{
    [HideInInspector] public Transform playerTransform;

    [Header("Magnet Settings")]
    [SerializeField] private float _attractSpeed;
    [SerializeField] private float _collisionDelay;

    private Rigidbody2D _rb;

    private void Awake() => StartCoroutine(CollisionDelay());
    private void Start() => _rb = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.Space)) return; //don't do anything unless space is pressed

        //add force towards the player's position
        Vector2 moveDir = playerTransform.position - transform.position;
        _rb.AddForce(moveDir.normalized * _attractSpeed);
    }

    //enables the magnet's box collider after it first leaves the player's body (so it isn't destroyed instantly)
    private IEnumerator CollisionDelay()
    {
        yield return new WaitForSeconds(_collisionDelay);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) Destroy(gameObject);
    }
}