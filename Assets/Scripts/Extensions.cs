using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Bounds OrtographicBounds(this Camera camera)
    {
        if(!camera.orthographic)
        {
            return new Bounds();
        }

        var t = camera.transform;
        var x = t.position.x;
        var y = t.position.y;
        var size = camera.orthographicSize * 2;
        var w = size * (float)Screen.width / Screen.height;
        var h = size;

        return new Bounds(new Vector3(x, y, 0), new Vector3(w, h, 0));
    }
}