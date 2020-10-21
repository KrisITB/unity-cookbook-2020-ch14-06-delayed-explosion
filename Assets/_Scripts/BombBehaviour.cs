using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public GameObject explosionPrefab;

    void Start()
    {
        // after 3 seconds instantiate the explosion
        float delay = 3;
        Invoke(nameof(Explode), delay);

    }

    private void Explode()
    {        
        // create explosion at same location as this player
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        
        // destroy particle system after 1 second
        Destroy(explosion, 1);
        
        // --- do any collision logic for bomb here -----
        Destroy(gameObject);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach(Collider coll in hitColliders)
        {
            //if(coll.gameObject.layer== 8) // better approach IMHO but it requiers a custom layer added and assigned to objects that are destructable
            if(coll.transform.parent != null)//requiered otherwise foreach loop seems to break when object has no parent
            {
                if (coll.transform.parent.transform.name == "GeometryDynamic") //all Dynamic objects in this scene i.e.: Pickup and BoxSmall
                {
                    Destroy(coll.gameObject);
                    //Even more fun would probably be to call a method from the script on destroyable objects in range, something like: 
                    //coll.gameObject.GetComponent<BombBehaviour>().Explode() could be called instead of Destroy engine method, if != null
                    //but this solution is quick and easy while BombBehaviour invokes Explode at the Start() so it would need some other script all together  
                }
            }
            
        }
    }
}
