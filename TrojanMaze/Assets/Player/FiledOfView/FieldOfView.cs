using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private const float normalViewDistance = 1.5f;
    private const float boostViewDistance = 2f;

    [SerializeField] LayerMask layerMask;
    private Mesh mesh;
    private Vector3 origin;
    float viewDistance;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        viewDistance = normalViewDistance;
    }

    void LateUpdate(){
        float fov = 360f;
        int rayCount = 100;
        float angle = 0f;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i=0;i <= rayCount; i++){
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if(raycastHit2D.collider == null){
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                // Debug.Log("sadsda");
            }else{
                // Debug.Log("name:"+raycastHit2D.collider.tag);
                vertex = raycastHit2D.point;
                // Debug.Log("111");
            }
            vertices[vertexIndex] = vertex;

            if(i > 0){
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex +=3;
            }


            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one *1000f);
    }

    Vector3 GetVectorFromAngle(float angle){
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void SetOrigin(Vector3 origin){
        this.origin = origin;
    }

    public void ResetViewDistance(Vector3 origin){
        this.viewDistance = normalViewDistance;
    }

    public void BoostViewDistance(Vector3 origin){
        this.viewDistance = boostViewDistance;
    }
}
