using UnityEngine;

namespace Species
{
    public static class SpeciesPropertyFunctions
    {
        private const float MaxRedValue = 255f;

        public static Vector3 GetHeight(float height)
        {
            var localScale = new Vector3(1, height, 1);
            return localScale;
        }

        public static Color32 GetColorBySpeed(float speed)
        {
            var redValue = GetRedValue(speed);
            var materialColor = new Color32(redValue, 100, 0, 100);
            return materialColor;
        }

        private static byte GetRedValue(float speed)
        {
            // 1 - speed because I want it to be Greener when speed is higher
            return (byte) ((1 - speed) * MaxRedValue);
        }
    }
}