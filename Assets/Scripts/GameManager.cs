using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player playerPrefabs;
    public Platform platformPrefabs;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;
    public CameraController mainCam;
    public float powerBarup;
    Player player;
    int score;
    bool m_isGameStarted = true;

    public bool IsGameStarted { get => m_isGameStarted; }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        base.Start();

        Debug.Log("Best Score at game start: " + PlayerPrefs.GetInt(Prefconsts.BEST_SCORE, 0));

        GameGUIManager.Ins.UpdateScoreCounting(score);

        GameGUIManager.Ins.UpdatePowerBar(0, 1);

        AudioController.Ins.PlayBackgroundMusic();

    }

    public void PlayGame()
    {
        StartCoroutine(PlatformTnit());

        GameGUIManager.Ins.ShowGameGUI(true);

    }

    // Khởi Tạo Platform
    private IEnumerator PlatformTnit()
    {
        Platform platformClone = null;

        if (platformPrefabs)
        {
            //Instantiate: Phương thức này tạo một bản sao (instance) của platformPrefabs trong trò chơi.
            //Quaternion.identity: Tham số này đặt bản sao nền tảng ở góc quay mặc định(0, 0, 0).
            platformClone = Instantiate(platformPrefabs, new Vector2(0, Random.Range(minSpawnY, maxSpawnY)), Quaternion.identity);
            platformClone.id = platformClone.gameObject.GetInstanceID();
        }

        yield return new WaitForSeconds(0.5f);

        if (playerPrefabs)
        {
            player = Instantiate(playerPrefabs, Vector3.zero, Quaternion.identity);
            player.lastPlatformId = platformClone.id;
        }

        if (platformPrefabs)
        {
            //Điều này giúp đảm bảo nền tảng mới sẽ được tạo ra một khoảng cách nhất định từ vị trí hiện tại của người chơi.
            float spanwX = player.transform.position.x + minSpawnX;

            //Điều này đảm bảo rằng nền tảng mới sẽ được tạo ra ở một vị trí Y ngẫu nhiên trong khoảng xác định.
            float spanwY = Random.Range(minSpawnY, maxSpawnY);

            Platform platformClone02 = Instantiate(platformPrefabs, new Vector2(spanwX, spanwY), Quaternion.identity);
            platformClone02.id = platformClone02.gameObject.GetInstanceID();
        }

        yield return new WaitForSeconds(0.5f);

        m_isGameStarted = true;
    }

     // Để người chơi có thể nhảy từ nền này sang nền khác. Việc tạo các nền tảng mới có thể diễn ra ở nhiều nơi trong mã nguồn để đảm bảo người chơi luôn có nơi để nhảy tới.
    //Đoạn mã CreatePlatform() được thiết kế để tạo ra một nền tảng mới khi cần thiết, chẳng hạn khi người chơi đã nhảy lên nền tảng hiện tại và cần một nền tảng mới để tiếp tục trò chơi.
    public void CreatePlatform()
    {
        if (!platformPrefabs || !player) return;

        float spanwX = Random.Range(player.transform.position.x + minSpawnX, player.transform.position.x + maxSpawnX);
        float spanwY = Random.Range(minSpawnY, maxSpawnY);

        Platform platformClone = Instantiate(platformPrefabs, new Vector2(spanwX, spanwY), Quaternion.identity);
        platformClone.id = platformClone.gameObject.GetInstanceID();

        Debug.Log("Platform created at: " + spanwX + ", " + spanwY); // Thêm thông báo gỡ lỗi
    }
    
    public void CreatePlatformAndLerp(float playerXpos)
    {
        if (mainCam)
            mainCam.LerpTrigger(playerXpos + minSpawnX);

        CreatePlatform();
    }

    public void AddScore()
    {
        score++;
        Prefs.bestScore = score;
        GameGUIManager.Ins.UpdateScoreCounting(score);
        AudioController.Ins.PlaySound(AudioController.Ins.getScore);
        Debug.Log("Current Best Score: " + PlayerPrefs.GetInt(Prefconsts.BEST_SCORE, 0));

    }
}

