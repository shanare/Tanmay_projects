using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    float peakX = 0, peakY = 0, peakZ = 0;
    public float force = 100f;
    public float forceOffset = 1f;
    //private RaycastHit hit;
    public GameObject Fossa;
    MeshDeformer Deformer;

    // Use this for initialization  
    void Start()
    {


    }

    // Update is called once per frame  
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0.05f, 0f, 0f);
            peakX += 0.05f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-0.05f, 0f, 0f);
            peakX -= 0.05f;

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.0f, 0f, -0.05f);
            peakZ -= 0.05f;

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(0.0f, 0f, 0.05f);
            peakZ += 0.05f;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(0.0f, -0.05f, 0f);
            peakY -= 0.05f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(0.0f, 0.05f, 0f);
            peakY += 0.05f;
        }
        Vector3 point = new Vector3(peakX, peakY, peakZ);
        Camera.main.ScreenPointToRay(point);
       // if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        //{
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.blue);
            //Debug.Log("HIT!");
        //}
        if (Input.anyKey)
        {
            Deformer = Fossa.GetComponent<MeshDeformer>();
            //MeshDeformer Deformer = hit.transform.GetComponent<MeshDeformer>(); //Look for examples grabbing mesh
            if (!Deformer)
            { //Debug.Log("Deformer is made"); 
            }

                if (Deformer)
                {
                    Debug.Log("Deformer is executed");
                    Deformer.Deform(point);
                }
        }
            //Vector3 closest = new Vector3(0, 0, 0);

    }
}
  /*  public class ShowClosestPoint : MonoBehaviour
    {

        public Vector3 location;

        public void OnDrawGizmos()
        {
            var collider = GetComponent<Collider>();

            if (!collider)
            {
                return; // nothing to do without a collider
            }

            Vector3 closestPoint = collider.ClosestPoint(location);

            Gizmos.DrawSphere(location, 0.1f);
            Gizmos.DrawWireSphere(closestPoint, 0.1f);
        }
    }*/
   /* public void OnCollisionEnter(Collision collision)
    {


        if (collision.transform.gameObject.tag == "DeformableMesh")
        {
           
            ContactPoint[] contactPoints = new ContactPoint[collision.contactCount];
            collision.GetContacts(contactPoints);
            foreach (ContactPoint contactPoint in contactPoints)
            {
                
                meshDeformer.Deform(contactPoint.point);
            }
        }
    }

}
*/


/*
 * MeshDeformer Deformer = gameObject.GetComponent<MeshDeformer>();
        Vector3 closest = .ClosestPoint(peakX, peakY, peakZ);
        Deformer.Deform(closest);
 */
