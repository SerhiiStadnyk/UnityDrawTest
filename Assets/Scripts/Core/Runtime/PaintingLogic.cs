using System.Collections.Generic;
using UnityEngine;

namespace Core.Runtime
{
    public static class PaintingLogic
    {
        public static IEnumerable<Vector2> GetPointsOnLine(Vector2 p1, Vector2 p2)
        {
            float distance = Vector2.Distance(p1, p2);
            int steps = Mathf.CeilToInt(distance);
            for (int i = 0; i <= steps; i++)
            {
                yield return Vector2.Lerp(p1, p2, i / distance);
            }
        }
        
        
        public static void DrawCircle(int centerX, int centerY, int radius, Texture2D texture, Color color)
        {
            int sqrRadius = radius * radius;
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    if (x * x + y * y <= sqrRadius)
                    {
                        int texX = centerX + x;
                        int texY = centerY + y;

                        if (texX >= 0 && texX < texture.width && texY >= 0 && texY < texture.height)
                        {
                            texture.SetPixel(texX, texY, color);
                        }
                    }
                }
            }
        }
    }
}
