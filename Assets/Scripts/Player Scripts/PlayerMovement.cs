using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed = 200.0f;

    public Vector2 StartingPosition;

    private Transform self;
    private Rigidbody2D selfRigidbody;

    private readonly float delta = 0.00025f;
    private float radius;

    private void Awake()
    {

        self = transform;
        selfRigidbody = GetComponent<Rigidbody2D>();

        self.position = StartingPosition;

        if (GetComponent<CircleCollider2D>())
            radius = GetComponent<CircleCollider2D>().radius;

    }

    private void Update()
    {
        if (GameManager.GM && GameManager.GM.InGame)
        {
            CheckKeyboardInput();
            CheckMobileInput();
        }
    }

    private void CheckKeyboardInput()
    {

        #if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR

        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (direction.magnitude != 0.0f)
            direction = direction.normalized;

        Move(direction);

        #endif

    }

    private void CheckMobileInput()
    {

        #if UNITY_ANDROID

        if (Input.touches.Length == 0)
            return;

        Touch touch = Input.touches[0];

        if (touch.phase == TouchPhase.Moved)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
            if (touchPosition.WithinCircle(self.position, radius) || Mathf.Abs(radius) < delta)
                self.position = touchPosition;
        }

        #endif

    }

    private void Move(Vector2 direction)
    {
        if (selfRigidbody)
            selfRigidbody.velocity = direction * Speed * Time.deltaTime;
    }

}
