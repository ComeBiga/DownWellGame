# 주요 코드 라인

## [JsonIO.cs]()

이 파일에서는 에디터에서 생성하는 레벨을 Json파일로 저장하고 불러오는 코드들을 담고 있습니다.

```C#
private void SaveToTextFile(string path, string fileName)
{
    var jsonStr = LevelToJson(fileName);

    File.WriteAllText(path, jsonStr);
}
```

__SaveToTextFile__ 함수는 string을 Text파일로 쓰기를 담당하고 있습니다.

__LevelToJson__ 함수는 Level객체를 Json에 맞게 string으로 변환해줍니다. 아래는 __LevelToJson__ 함수 입니다.

```C#
string LevelToJson(string fileName = "")
{
    Level level = new Level();
    level.tiles = new int[LevelEditorManager.instance.tiles.Count];

    level.name = fileName;

    for (int i = 0; i < level.tiles.Length; i++)
        level.tiles[i] = (int)LevelEditorManager.instance.tiles[i].GetComponent<TileInfo>().tileCode;

    level.width = (int)LevelEditorManager.instance.getCanvasSize().x;
    level.height = (int)LevelEditorManager.instance.getCanvasSize().y;

    level.AssignCorner();

    // 객체를 Json형태의 string으로 변환
    return JsonUtility.ToJson(level);
}
```

로드는 세이브의 역순입니다.

```C#
public T LoadFromTextFile<T>(string path)
{
    // Read From Text File
    var jsonStr = File.ReadAllText(path);

    // Convert into json
    var level = JsonToLevel<T>(jsonStr);

    return level;
}
```

## BrushManager.cs
