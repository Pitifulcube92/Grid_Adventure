using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRandomScarling : MonoBehaviour
{
    [SerializeField] private GameObject Scarlings;
    [SerializeField] private BoxCollider2D Spawnarea;
    [SerializeField] private float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, maxDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ShootScarlingPos()
    {
       Vector2 tmp_ =  Random.insideUnitCircle.normalized * maxDistance;
        //Instantiate(Scarlings,);
    }
}
