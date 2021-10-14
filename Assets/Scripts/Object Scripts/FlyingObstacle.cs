using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObstacle : MonoBehaviour
{

    public Vector2 SpeedRange;

    public Transform ExplosionPrefab;

    private Transform self;
    private Rigidbody2D selfRigidbody;

    private void Awake()
    {

        self = transform;
        selfRigidbody = GetComponent<Rigidbody2D>();

        self.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ExplosionPrefab)
                Instantiate(ExplosionPrefab, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);
        GameManager.GM.OpenMenu();
    }

    public void SetInMotion(Vector2 direction)
    {
        if (selfRigidbody)
            selfRigidbody.velocity = direction * Random.Range(SpeedRange.x, SpeedRange.y) * Time.deltaTime;
    }

}
