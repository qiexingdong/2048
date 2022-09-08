using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private static Dictionary<int, Sprite> dic;//字典集合
    /// <summary>
    /// 只执行一次的静态构造函数
    /// </summary>
     static ResourcesManager() {
        dic = new Dictionary<int, Sprite>();
       var sprites = Resources.LoadAll<Sprite>("pictures");//数组
        foreach (var item in sprites)
        {
            dic.Add(int.Parse(item.name),item);
        }
    }
    public static Sprite LoadSprite(int num) {
        return dic[num];
    }
}
