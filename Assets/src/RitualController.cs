using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RitualController : MonoBehaviour, IEventListener
{
    public float startTimeToEclipse = 300;
    private float actualTimeToEclipse;

    public int woodNeeded = 300;
    public int initialWood = 150;
    private int actualWood;

    public int foodNeeded = 300;
    public int initialFood = 150;
    private int actualFood;

    public int woodConsuming = 5;
    public int foodConsuming = 5;
    public float timeToConsume = 10;
    private float actualConsumingTime;

    void Start()
    {
        actualTimeToEclipse = startTimeToEclipse;
        actualWood = initialWood;
        actualFood = initialFood;

        actualConsumingTime = 0;

        EventManager.instance.AddListener(this as IEventListener, "ResourceAdd");
    }

    // Update is called once per frame
    void Update()
    {
        actualTimeToEclipse -= Time.deltaTime;
        /// Cada X segundos descontamos recursos
        actualConsumingTime += Time.deltaTime;
        if (actualConsumingTime >= timeToConsume)
        {
            actualConsumingTime -= timeToConsume;
            actualFood = Math.Max(0, actualFood - foodConsuming);
            actualWood = Math.Max(0, actualWood - woodConsuming);
        }
    }

    void OnGUI()
    {
        string timeLeft = "Time Left: " + (int)GetTimeToEclipse();
        string wood = "Wood: " + GetCurrentWood() + "/" + woodNeeded;
        string food = "Food: " + GetCurrentFood() + "/" + foodNeeded;
        //Debug.Log(timeLeft + ", " + wood + ", " + food);
        Color original = GUI.contentColor;
        GUI.contentColor = Color.black;
        GUIStyle style = GUI.skin.customStyles[0];
        Vector2 screenPos = new Vector2(Screen.width, Screen.height);

        Vector2 size = style.CalcSize(new GUIContent(timeLeft));
        Vector2 finalPos = screenPos * 0.05f - size * 0.5f;
        Rect r = new Rect(finalPos.x, finalPos.y, size.x, size.y * 1.5f);
        DrawQuad(r, Color.white);
        GUI.Label(r, timeLeft);

        size = style.CalcSize(new GUIContent(wood));
        finalPos = new Vector2(screenPos.x * 0.05f,screenPos.y * 0.075f)  - size * 0.5f;
        r = new Rect(finalPos.x, finalPos.y, size.x, size.y * 1.5f);
        DrawQuad(r, Color.white);
        GUI.Label(r, wood);

        size = style.CalcSize(new GUIContent(food));
        finalPos = new Vector2(screenPos.x * 0.05f, screenPos.y * 0.1f) - size * 0.5f;
        r = new Rect(finalPos.x, finalPos.y, size.x, size.y * 1.5f);
        DrawQuad(r, Color.white);
        GUI.Label(r, food);
    }

    private void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

    public float GetTimeToEclipse()
    {
        return actualTimeToEclipse;
    }

    public int GetCurrentWood()
    {
        return actualWood;
    }

    public int GetCurrentFood()
    {
        return actualFood;
    }

    bool IEventListener.HandleEvent(IEvent evt)
    {
        KeyValuePair<ActionEnum, int> pair = (KeyValuePair<ActionEnum, int>)evt.GetData();
        if (pair.Key == ActionEnum.CHOP)
        {
            actualWood += pair.Value;
        }
        else if (pair.Key == ActionEnum.FARM)
        {
            actualFood += pair.Value;
        }
        return true;
    }


    internal bool IsReady()
    {
        return actualFood >= foodNeeded && actualWood >= woodNeeded;
    }
}
