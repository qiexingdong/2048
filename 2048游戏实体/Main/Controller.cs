using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using _2048;
using MoveDirection = _2048.MoveDirection;


public class Controller : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private GameCore core;
    private SingleSprite[,] sprites;
    private AudioSource audioSource;//����
    public AudioClip clip;
    public GameObject lose, win;//ʧ�ܡ�ʤ������
    public TextMeshProUGUI text;
    /// <summary>
    /// ��ʼ������
    /// </summary>
    private void Start()
    {
        core = new GameCore();
        sprites = new SingleSprite[4, 4];
        CreateZero();
        CreateRandomNum();
        CreateRandomNum();
        audioSource = transform.GetComponent<AudioSource>();
        core.Score();
    }
    private void Update()
    {
        text.text = core.score.ToString();
        //���Դ��������Ƿ����
        if (core.IsChange && !core.loseFlag && !core.successFlag)
        {
            UpdateMap();//UI�������
            SoundEffects();
            CreateRandomNum();//���������
            core.Score();
            core.Check();  //�����Ϸ�Ƿ����
            core.IsChange = false;
        }
        if (core.successFlag && !win.activeInHierarchy)
        {
            win.SetActive(true);
            win.GetComponent<Win>().In();
        }
        if (core.loseFlag && !lose.activeInHierarchy)
        {
            lose.SetActive(true);
            lose.GetComponent<Lose>().In();
        }
    }
    /// <summary>
    /// ����������
    /// </summary>
    private void UpdateMap()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                sprites[i, j].SetSprite(core.Arr[i, j]);
            }
        }
    }
    /// <summary>
    /// ��ʼ������λ��
    /// </summary>
    private void CreateZero()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                SingleCube(i, j);
            }
        }
    }
    /// <summary>
    /// ��ʼ������λ��
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    private void SingleCube(int i, int j)
    {
        GameObject go = new GameObject(i.ToString() + j.ToString());
        go.AddComponent<Image>();//������Image
        SingleSprite singleSprite = go.AddComponent<SingleSprite>();//������SingleSprite
        sprites[i, j] = singleSprite;
        singleSprite.SetSprite(0);//����0ͼƬ
        go.transform.SetParent(this.transform);//���ø�����
    }
    /// <summary>
    /// �������2��4ͼƬ
    /// </summary>
    private void CreateRandomNum()
    {
        int? a;
        Index? index;
        core.RandomEmpty(out index, out a);
        sprites[index.Value.RIndex, index.Value.CIndex].SetSprite(a.Value);
        sprites[index.Value.RIndex, index.Value.CIndex].CreateEffect();
    }
    private Vector2 startPoint;//��ָ���µ�����
    private bool button = false;
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        startPoint = eventData.position;
        button = true;
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (button)
        {
            Vector2 offset = eventData.position - startPoint;
            float x = Mathf.Abs(offset.x);
            float y = Mathf.Abs(offset.y);
            MoveDirection? dir = null;
            if (x > y && x > 60)
            {
                dir = offset.x > 0 ? MoveDirection.Right : MoveDirection.Left;
            }
            if (y > x && y > 60)
            {
                dir = offset.y > 0 ? MoveDirection.Up : MoveDirection.Down;
            }
            if (dir != null)
            {
                core.Move(dir.Value);
                button = false;
            }
        }
    }
    /// <summary>
    /// ��Ч
    /// </summary>
    private void SoundEffects()
    {
        audioSource.PlayOneShot(clip);
    }
    /// <summary>
    /// ���¿�ʼ
    /// </summary>
    public void ReStart()
    {
        core.ReStart();//�ײ��߼���0
        CleanScreen();//�ϲ�ui��0
        CreateRandomNum();
        CreateRandomNum();
    }
    /// <summary>
    /// ������0
    /// </summary>
    private void CleanScreen()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                sprites[i, j].SetSprite(0);
            }
        }

    }
}

