using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleSprite : MonoBehaviour
{
    private Image image;
    private void Awake()
    {
        image = transform.GetComponent<Image>();//��ȡ����image���
    }
    public void SetSprite(int num)
    {
        image.sprite = ResourcesManager.LoadSprite(num);
    }
    //����Ч��
    public void CreateEffect()
    {
        iTween.ScaleFrom(this.gameObject, Vector2.zero, 0.4f);
    }
    //�ƶ�Ч��
    public void MoveEffect(Vector3 pos)
    {
        //pos�������Ŀ��
        iTween.MoveTo(this.gameObject, pos, 0.4f);
    }
    //�ϲ�Ч��
    public void MergeEffect()
    {
        iTween.ScaleFrom(this.gameObject, new Vector3(1.3f, 1.3f, 1.3f), 0.4f);
    }
}
