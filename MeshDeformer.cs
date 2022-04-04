using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshCollider))]
public class MeshDeformer : MonoBehaviour
{
   // If checked normals of the mesh will be recalculated after every deformation
   public bool recalculateNormals;

   // If checked MeshCollider will be updated after every deformation
   public bool collisionDetection;

   Mesh mesh;
   //MeshCollider meshCollider;
   List<Vector3> vertices;

   // Start is called before the first frame update
   void Start () {
       mesh = GetComponent<MeshFilter> ().mesh;
      //meshCollider = GetComponent<MeshCollider> ();
       vertices = mesh.vertices.ToList ();
   }

/* Deforms this mesh
   point: The point from which deformation of the mesh starts
   radius: The maximum radius to which the deformation affects
   stepRadius: The small step value of the maximum radius
   strength: The maximum strength of the deformation
   stepStrength: The small step value of the maximum strength
   direction: The direction of the deformation relative to mesh
*/

  public void Deform (Vector3 point) {
        Vector3 Projectpoint = new Vector3(point.x, 0.0f, point.z);// Peak point = projection of the peak point onto xz plane
       // Debug.Log(point.x);
        //Debug.Log(point.z);
        for (int i = 0; i < vertices.Count; i++) {

            float radius = 5f; //stepRadius= 0.05f, strength= 0.5f, stepStrength = 0.05f;
            Vector3 direction = Vector3.down;
            Vector3 vi = transform.TransformPoint (vertices[i]);
            Vector3 Projectvi = new Vector3(vi.x, 0.0f, vi.z);//Vector3 Projected = Remove the y coordinate from vi just xz plane;
           // Debug.Log(vi.x);
            //Debug.Log(vi.z);
            float distance = Vector3.Distance (Projectpoint, Projectvi);// projected points and vi
          float s = point.y;
            // s = value of the y coordinate of the peak point
           // if(s<0)
            //{
              //  direction = Vector3.up;
            //}
          //for (float r = 0.0f; r < radius; r += stepRadius) {

              if (distance < radius) {
                  Vector3 deformation = direction * ((distance-radius)/radius)*s;                
                  vertices[i] = transform.InverseTransformPoint (Projectvi + deformation); // Just changes y value will need to change xz values 
                //Debug.Log();
                
              
            //  s -= stepStrength;
          }
      }
      if (recalculateNormals)
          mesh.RecalculateNormals ();

     // if (collisionDetection) {
       //   meshCollider.sharedMesh = null;
       //   meshCollider.sharedMesh = mesh;   
      //}           
      mesh.SetVertices (vertices);
       // Debug.Log("HIT!");
    }
}


/*

using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{

public float springForce = 20f;
public float damping = 5f;

Mesh deformingMesh;
Vector3[] originalVertices, displacedVertices;
Vector3[] vertexVelocities;

float uniformScale = 1f;
    

    void Start()
{
    deformingMesh = GetComponent<MeshFilter>().mesh;
    originalVertices = deformingMesh.vertices;
    displacedVertices = new Vector3[originalVertices.Length];
    for (int i = 0; i < originalVertices.Length; i++)
    {
        displacedVertices[i] = originalVertices[i];
    }
    vertexVelocities = new Vector3[originalVertices.Length];
}

void Update()
{
    uniformScale = transform.localScale.x;
    for (int i = 0; i < displacedVertices.Length; i++)
    {
        UpdateVertex(i);
    }
    deformingMesh.vertices = displacedVertices;
    deformingMesh.RecalculateNormals();
}

void UpdateVertex(int i)
{
    Vector3 velocity = vertexVelocities[i];
    Vector3 displacement = displacedVertices[i] - originalVertices[i];
    displacement *= uniformScale;
    velocity -= displacement * springForce * Time.deltaTime;
    velocity *= 1f - damping * Time.deltaTime;
    vertexVelocities[i] = velocity;
    displacedVertices[i] += velocity * (Time.deltaTime / uniformScale);
}

public void AddDeformingForce(Vector3 point, float force)
{
       
    point = transform.InverseTransformPoint(point);
    for (int i = 0; i < displacedVertices.Length; i++)
    {
        AddForceToVertex(i, point, force);
    }
}

void AddForceToVertex(int i, Vector3 point, float force)
{
    Vector3 pointToVertex = displacedVertices[i] - point;
    pointToVertex *= uniformScale;
    float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
    float velocity = attenuatedForce * Time.deltaTime;
    vertexVelocities[i] += pointToVertex.normalized * velocity;
}
}

*/