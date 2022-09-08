using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private static Dictionary<int, Sprite> dic;//�ֵ伯��
    /// <summary>
    /// ִֻ��һ�εľ�̬���캯��
    /// </summary>
     static ResourcesManager() {
        dic = new Dictionary<int, Sprite>();
       var sprites = Resources.LoadAll<Sprite>("pictures");//����
        foreach (var item in sprites)
        {
            dic.Add(int.Parse(item.name),item);
        }
    }
    public static Sprite LoadSprite(int num) {
        return dic[num];
    }
}
