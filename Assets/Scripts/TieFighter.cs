using UnityEngine;

public class TieFighter : MonoBehaviour


{
    public GameObject Falcon;
    public GameObject Fighter;

    public float thrust = 0.5f;

    public float FalconValue;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FalconValue = Vector3.Distance(Falcon.transform.position, transform.position);
        if (Falcon != null)
        {
            if (Fighter.activeSelf)
            {
                Vector2 direction = (Falcon.transform.position - transform.position).normalized;
                transform.up = direction;
                rb.AddForce(direction * thrust);
            }
        }        
    }
}
