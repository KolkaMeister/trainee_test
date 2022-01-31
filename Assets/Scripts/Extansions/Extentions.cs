using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static bool IsInLayer(this GameObject go, LayerMask layer)
    {

        return layer == (layer | 1 << go.layer);
    }
}
