using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    #region GetChildrenList
    public static List<T> GetChildren<T>(GameObject go)
    {
        List<T> list = new List<T>();
        if (go == null || go.transform.childCount == 0)
        {
            return list;
        }
        Stack<Transform> stack = new Stack<Transform>();
        stack.Push(go.transform);
        while (stack.Count > 0)
        {
            Transform t = stack.Pop();
            list.Add(t.GetComponent<T>());
            foreach (Transform child in t)
            {
                stack.Push(child);
            }
        }
        Debug.Log(string.Join(", ", list));
        return list;
    }
    public static List<T> GetChildren<T>(GameObject go, bool inChild)
    {
        List<T> list = new List<T>();
        if (go == null || go.transform.childCount == 0)
        {
            return list;
        }

        if (!inChild) 
        {
            foreach (Transform child in go.transform)
            {
                list.Add(child.GetComponent<T>());
            }
            return list;
        }
        else if (inChild) 
        {
            Stack<Transform> stack = new Stack<Transform>();
            stack.Push(go.transform);
            while (stack.Count > 0)
            {
                Transform t = stack.Pop();
                foreach (Transform child in t)
                {
                    stack.Push(child);
                    list.Add(t.GetComponent<T>());
                }
            }
        }
        Debug.Log(string.Join(", ", list));
        return list;
    }
    #endregion

    #region GetRandomItem
    public static GameObject GetRandomItem(List<GameObject> listToRandomize)
    {
        int randomNum = Random.Range(0, listToRandomize.Count);
        return listToRandomize[randomNum];
    }
    public static T GetRandomItem<T>(List<T> listToRandomize)
    {
        int randomNum = Random.Range(0, listToRandomize.Count);
        return listToRandomize[randomNum];
    }
    #endregion 

}