using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.5f;
    private Rigidbody2D rb;
    private float mx;
    private float my;

    private float fireTimer;

    private Vector2 mousePos;
    private CameraBoundaries cameraBoundaries;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraBoundaries = FindObjectOfType<CameraBoundaries>();

        if (cameraBoundaries == null)
        {
            Debug.LogError("CameraBoundaries script not found in the scene.");
        }
    }

    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = rb.position + new Vector2(mx, my).normalized * speed * Time.fixedDeltaTime;
        if (cameraBoundaries != null)
        {
            newPosition = cameraBoundaries.GetClampedPosition(newPosition);
        }
        rb.MovePosition(newPosition);
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }
}
