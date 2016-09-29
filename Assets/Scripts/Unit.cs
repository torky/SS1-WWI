using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{

    Rigidbody unit;
    public float speed = 5;
    public Vector3 targetPoint;
    public GameObject target;
    public Unit enemy;
    public GameObject attacker;
    public GameObject bullet;

    public float range = 200F;
    public int damage = 10;
    public int health = 100;
    public float attackSpeed = 1F;
    bool reloaded = true;

    public List<List<GameObject>> listsContainingThis;

    public const float STANDARD_ARRIVAL_RANGE = 2F;
    public enum Mode : int
    {
        Attack,
        MoveToPoint,
        MoveToTarget,
        ScanForTarget,
        Dead,
    };

    public int state = (int)Mode.MoveToPoint;

    // Use this for initialization
    void Start()
    {
        unit = GetComponent<Rigidbody>();
        targetPoint = new Vector3(150, 0, 150);
        range = 30F;
        listsContainingThis = new List<List<GameObject>>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case (int)Mode.Attack:
                break;
            case (int)Mode.MoveToPoint:
                if (Movement(targetPoint, STANDARD_ARRIVAL_RANGE))
                {
                    Debug.Log("Arrived: " + state);
                    state = (int)Mode.ScanForTarget;
                }
                break;
            case (int)Mode.MoveToTarget:
                if (Movement(target.transform.position, range))
                {
                    state = (int)Mode.Attack;
                    enemy = target.GetComponent(typeof(Unit)) as Unit;
                    StartCoroutine(Attack());
                }
                break;
            case (int)Mode.ScanForTarget:

                break;
            case (int)Mode.Dead:
                //remove from collection
                foreach(List<GameObject> list in listsContainingThis)
                {
                    list.Remove(this.gameObject);
                }
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }

    void LateUpdate()
    {
        if (health <= 0)
        {
            state = (int)Mode.Dead;
        }
    }

    bool Movement(Vector3 point, float arrivalRange)
    {
        Vector3 direction = point - this.gameObject.transform.position;
        bool hasArrived = direction.magnitude < arrivalRange;

        Vector3 velocity = direction.normalized * speed;
        unit.velocity = velocity;
        return hasArrived;
    }

    bool InRange()
    {
        Vector3 direction = target.transform.position - this.transform.position;
        bool hasArrived = direction.magnitude < range;
        return hasArrived;
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackSpeed);
            if (enemy != null && enemy.health > 0 && this.health > 0)
            {
                //enemy.health -= damage;
                Vector3 direction = target.transform.position - this.transform.position;
                Vector3 creation = new Vector3(this.transform.position.x, 1, this.transform.position.z);
                creation = direction.normalized + creation;
                creation.y = 1;

                var createdBullet = (GameObject)Instantiate(bullet, creation, Quaternion.identity);
                Bullet b = createdBullet.GetComponent(typeof(Bullet)) as Bullet;
                b.SetRigidbody(b.GetComponent<Rigidbody>());
                b.SetDirection(direction.normalized);
                b.SetDamage(damage);
                Destroy(createdBullet, 3.0F);
            }else
            {
                StopCoroutine(Attack());
            }

            enemy.Agro(this);
            
            if (enemyDead())
            {
                state = (int)Mode.ScanForTarget;
                StopCoroutine(Attack());
            }

            if (state != (int)Mode.Attack)
            {
                StopCoroutine(Attack());
            }

            if (!InRange())
            {
                state = (int)Mode.MoveToTarget;
                StopCoroutine(Attack());
            }
        }
    }

    bool enemyDead()
    {
        return (enemy.state == (int)Mode.Dead);
    }

    public void Stop()
    {
        target = null;
        StopCoroutine(Attack());
    }

    public void Agro(Unit u)
    {
        if(state == (int)Mode.ScanForTarget)
        {
            state = (int)Mode.MoveToTarget;
            target = u.gameObject;
            enemy = u;
        }
    }
}
