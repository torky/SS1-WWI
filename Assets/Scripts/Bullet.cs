using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    Vector3 direction;
    Rigidbody rb;
    int damage;
    int timer = 0;
    int speed = 20;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Unit"))
        {
            Unit u = collision.gameObject.GetComponent(typeof(Unit)) as Unit;
            u.health -= damage;
            Debug.Log(u.state);
        }
        Destroy(this.gameObject);
    }

    public void SetDamage(int damageDealt)
    {
        this.damage = damageDealt;
    }

    public void SetDirection(Vector3 directionNormalized)
    {
        direction = directionNormalized;
    }

    public void SetRigidbody(Rigidbody rigidbody)
    {
        rb = rigidbody;
    }
}
