# MapDisplay
![맵_1](https://user-images.githubusercontent.com/36800639/201475223-e4f60e16-1f0c-4579-925a-8c0d0d099ee7.PNG)

위 사진처럼 저장된 레벨 리스트 중에 랜덤으로 선택해 맵에 배치합니다.
## 주요 코드라인

### MapManager.cs
```c#
IEnumerator GenerateMap()
    {
        ...
        for (;(-currentYpos) < height;)
        {
            // 랜덤으로 불러온 레벨을 현재 y 위치에서 생성
            currentYpos -= mapDisplay.Display(RandomLevel(sm.CurrentStageIndex), currentYpos);
        }
        ...
    }
```

### MapDisplay.cs
Level을 오브젝트로 생성해주는 코드입니다.
```c#
public int Display(Level level, int Ypos)
{
    ...

    // Wall
    for (int y = 0; y < level.height; y++)
    {
        for (int x = 0; x < mapManager.width; x++)
        {
            int currentTile = level.tiles[y * mapManager.width + x];
            Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x, -y + offset.y + Ypos);
            
            // 코드에 맞는 타일을 생성
            if (currentTile >= 100 && currentTile <= 1000)
            {
                ...
                if (wallObject != null)
                {
                    wall = Instantiate(wallObject, tilePosition, Quaternion.identity, parent);
                    ...
                }
            }
            ...
        }
    }
    ...
}
```

### LoadLevel.cs
.Json으로 저장된 Level을 불러오는 코드입니다.
```c#
    List<Level> Load(string path)
    {
#if UNITY_EDITOR

        string[] directories = Directory.GetFiles(Application.dataPath + path + "/", "*.json");
        List<Level> lvList = new List<Level>();

        foreach (var dir in directories)
        {
            // Text파일에서 Json string 읽음
            string jsonStr = File.ReadAllText(dir);
            // string을 Level 객체로 변환
            var lvs = JsonToLevel<Level>(jsonStr);

            lvList.Add(lvs);
        }

        return lvList;
        
#elif UNITY_ANDROID || UNITY_STANDALONE_WIN

        var textDatas = Resources.LoadAll(path + "/", typeof(TextAsset));
        List<Level> lvList = new List<Level>();

        foreach (var textData in textDatas)
        {
            var lvs = JsonToLevel<Level>(textData.ToString());

            lvList.Add(lvs);
        }

        return lvList;
        
#endif
    }
```

게임 엔진에서 실행할 때와 빌드 된 파일에서 실행할 때, 리소스의 경로와 방식이 다르기 때문에, 전처리기를 이용해서 구분했습니다.
