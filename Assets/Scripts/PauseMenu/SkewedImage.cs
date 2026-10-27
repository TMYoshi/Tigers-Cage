using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkewedImage : Image
{
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        Vector2 topLeftOffset = new Vector2(-100f, 0);
        Vector2 topRightOffset = new Vector2(100f, 0);
        Vector2 bottomRightOffset = Vector2.zero;
        Vector2 bottomLeftOffset = Vector2.zero;

        base.OnPopulateMesh(vh);
        var r = GetPixelAdjustedRect();
        var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
        Color32 color32 = color;
        vh.Clear();
        vh.AddVert(new Vector3(v.x + topLeftOffset.x, v.y + topLeftOffset.y), color32, new Vector2(0f, 0f));
        vh.AddVert(new Vector3(v.x + bottomLeftOffset.x, v.w + bottomLeftOffset.y), color32, new Vector2(0f, 1f));
        vh.AddVert(new Vector3(v.z + bottomRightOffset.x, v.w + bottomRightOffset.y), color32, new Vector2(1f, 1f));
        vh.AddVert(new Vector3(v.z + topRightOffset.x, v.y + topRightOffset.y), color32, new Vector2(1f, 0f));
        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);
    }
}
