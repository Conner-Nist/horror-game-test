using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AutoTileFloor : MonoBehaviour
{
    public Vector2 baseTiling = Vector2.one;

    void Update()
    {
        Renderer r = GetComponent<Renderer>();
        Vector3 s = transform.lossyScale;

        r.material.mainTextureScale = new Vector2(
            baseTiling.x * s.x, // width
            baseTiling.y * s.z  // depth
        );
    }
}