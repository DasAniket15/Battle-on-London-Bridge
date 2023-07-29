using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Utils
{
    public static class UtilsClass
    {
        public const int sortingOrderDefault = 5000;


        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;

            return vec;
        }


        public static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }


        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        }


        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);

            return worldPosition;
        }


        public static void ShakeCamera(float intensity, float timer)
        {
            Vector3 lastCameraMovement = Vector3.zero;

            FunctionUpdater.Create(
                delegate ()
                {
                    timer -= Time.unscaledDeltaTime;

                    Vector3 randomMovement = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * intensity;
                    Camera.main.transform.position = Camera.main.transform.position - lastCameraMovement + randomMovement;
                    lastCameraMovement = randomMovement;

                    return timer <= 0f;
                },

            "CAMERA_SHAKE");
        }


        public static Vector3 GetVectorFromAngle(int angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);

            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }


        public static Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);

            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }


        public static Vector3 GetVectorFromAngleInt(int angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);

            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }


        public static float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }


        public static float GetAngleFromVectorFloatXZ(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }


        public static int GetAngleFromVector(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            int angle = Mathf.RoundToInt(n);

            return angle;
        }


        public static int GetAngleFromVector180(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            int angle = Mathf.RoundToInt(n);

            return angle;
        }

        public static Color GetColorFromString(string color)
        {
            float red = Hex_to_Dec01(color.Substring(0, 2));
            float green = Hex_to_Dec01(color.Substring(2, 2));
            float blue = Hex_to_Dec01(color.Substring(4, 2));
            float alpha = 1f;

            if (color.Length >= 8)
            {
                alpha = Hex_to_Dec01(color.Substring(6, 2));
            }

            return new Color(red, green, blue, alpha);
        }


        public static float Hex_to_Dec01(string hex)
        {
            return Hex_to_Dec(hex) / 255f;
        }


        public static int Hex_to_Dec(string hex)
        {
            return Convert.ToInt32(hex, 16);
        }

        public static GameObject CreateWorldSprite(string name, Sprite sprite, Vector3 position, Vector3 localScale, int sortingOrder, Color color)
        {
            return CreateWorldSprite(null, name, sprite, position, localScale, sortingOrder, color);
        }


        public static GameObject CreateWorldSprite(Transform parent, string name, Sprite sprite, Vector3 localPosition, Vector3 localScale, int sortingOrder, Color color)
        {
            GameObject gameObject = new GameObject(name, typeof(SpriteRenderer));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            transform.localScale = localScale;
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = sortingOrder;
            spriteRenderer.color = color;

            return gameObject;
        }


        public static FunctionUpdater CreateWorldTextUpdater(Func<string> GetTextFunc, Vector3 localPosition, Transform parent = null, int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
        {
            TextMesh textMesh = CreateWorldText(GetTextFunc(), parent, localPosition, fontSize, color, textAnchor, textAlignment, sortingOrder);
            
            return FunctionUpdater.Create(() => {
                textMesh.text = GetTextFunc();

                return false;
            },
            
            "WorldTextUpdater");
        }


        public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
        {
            if (color == null) color = Color.white;

            return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
        }


        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

            return textMesh;
        }


        public static ButtonSprite CreateWorldSpriteButton(string name, Sprite sprite, Vector3 localPosition, Vector3 localScale, int sortingOrder, Color color)
        {
            return CreateWorldSpriteButton(null, name, sprite, localPosition, localScale, sortingOrder, color);
        }


        public static ButtonSprite CreateWorldSpriteButton(Transform parent, string name, Sprite sprite, Vector3 localPosition, Vector3 localScale, int sortingOrder, Color color)
        {
            GameObject gameObject = CreateWorldSprite(parent, name, sprite, localPosition, localScale, sortingOrder, color);
            gameObject.AddComponent<BoxCollider2D>();
            ButtonSprite buttonSprite = gameObject.AddComponent<ButtonSprite>();

            return buttonSprite;
        }
    }
}
