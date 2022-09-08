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
    private AudioSource audioSource;//声音
    public AudioClip clip;
    public GameObject lose, win;//失败、胜利界面
    public TextMeshProUGUI text;
    /// <summary>
    /// 初始化界面
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
        //检查源代码界面是否更新
        if (core.IsChange && !core.loseFlag && !core.successFlag)
        {
            UpdateMap();//UI界面更新
            SoundEffects();
            CreateRandomNum();//产生随机数
            core.Score();
            core.Check();  //检查游戏是否结束
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
    /// 更新主界面
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
    /// 初始化所有位置
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
    /// 初始化单个位置
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    private void SingleCube(int i, int j)
    {
        GameObject go = new GameObject(i.ToString() + j.ToString());
        go.AddComponent<Image>();//添加组件Image
        SingleSprite singleSprite = go.AddComponent<SingleSprite>();//添加组件SingleSprite
        sprites[i, j] = singleSprite;
        singleSprite.SetSprite(0);//设置0图片
        go.transform.SetParent(this.transform);//设置父物体
    }
    /// <summary>
    /// 随机产生2或4图片
    /// </summary>
    private void CreateRandomNum()
    {
        int? a;
        Index? index;
        core.RandomEmpty(out index, out a);
        sprites[index.Value.RIndex, index.Value.CIndex].SetSprite(a.Value);
        sprites[index.Value.RIndex, index.Value.CIndex].CreateEffect();
    }
    private Vector2 startPoint;//手指按下的坐标
    private bool button = false;
    /// <summary>
    /// 按下
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        startPoint = eventData.position;
        button = true;
    }
    /// <summary>
    /// 滑动
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
    /// 音效
    /// </summary>
    private void SoundEffects()
    {
        audioSource.PlayOneShot(clip);
    }
    /// <summary>
    /// 重新开始
    /// </summary>
    public void ReStart()
    {
        core.ReStart();//底层逻辑清0
        CleanScreen();//上层ui清0
        CreateRandomNum();
        CreateRandomNum();
    }
    /// <summary>
    /// 界面清0
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

