using UnityEngine;
using System.Collections;

public class Speaker : MonoBehaviour
{
    float showTime;
    public Vector2 textOffset;
    string showText;

    // Use this for initialization
    void Start()
    {
        showTime = 0;
        textOffset = new Vector2(0, 60);
        showText = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (showTime > 0)
        {
            showTime -= Time.deltaTime;
        }
    }

    public void SpeakUp(string text, float seconds = 2)
    {
        showTime = seconds;
        showText = text;
    }

    void OnGUI()
    {
        if (showTime > 0)
        {
            Color original = GUI.contentColor;
            GUI.contentColor = Color.black;
            Vector2 screenPos = GameObject.Find("Main Camera").camera.WorldToScreenPoint(transform.position);
            screenPos.y = Screen.height - screenPos.y;
            GUIStyle style = GUI.skin.customStyles[0];
            Vector2 size = style.CalcSize(new GUIContent(showText));
            Vector2 finalPos = screenPos - textOffset - size * 0.5f;
            Rect r = new Rect(finalPos.x, finalPos.y, size.x, size.y*1.5f);
            DrawQuad(r, Color.white);
            GUI.Label(r, showText);
            GUI.contentColor = original;
        }
    }
    
    private void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }
}