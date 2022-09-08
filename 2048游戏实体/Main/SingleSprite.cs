using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleSprite : MonoBehaviour
{
    private Image image;
    private void Awake()
    {
        image = transform.GetComponent<Image>();//获取自身image组件
    }
    public void SetSprite(int num)
    {
        image.sprite = ResourcesManager.LoadSprite(num);
    }
    //生成效果
    public void CreateEffect()
    {
        iTween.ScaleFrom(this.gameObject, Vector2.zero, 0.4f);
    }
    //移动效果
    public void MoveEffect(Vector3 pos)
    {
        //pos是移向的目标
        iTween.MoveTo(this.gameObject, pos, 0.4f);
    }
    //合并效果
    public void MergeEffect()
    {
        iTween.ScaleFrom(this.gameObject, new Vector3(1.3f, 1.3f, 1.3f), 0.4f);
    }
}
