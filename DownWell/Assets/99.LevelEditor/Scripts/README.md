# 주요 코드 라인

## [JsonIO.cs](https://github.com/ComeBiga/DownWellGame/blob/CatDown_README/DownWell/Assets/99.LevelEditor/Scripts/JsonIO.cs)

이 파일에서는 에디터에서 생성하는 레벨을 Json파일로 저장하고 불러오는 코드들을 담고 있습니다.

```C#
private void SaveToTextFile(string path, string fileName)
{
    // Level 객체를 Json에 맞는 string으로 변환
    var jsonStr = LevelToJson(fileName);
    
    // Text파일로 쓰기
    File.WriteAllText(path, jsonStr);
}
```

__SaveToTextFile__ 함수는 string을 Text파일로 쓰기를 담당하고 있습니다.

__LevelToJson__ 함수는 Level객체를 Json에 맞게 string으로 변환해줍니다. 아래는 __LevelToJson__ 함수 입니다.

```C#
string LevelToJson(string fileName = "")
{
    // 레벨 객체 생성
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
    // Text파일 읽기
    var jsonStr = File.ReadAllText(path);

    // Level 객체로 변환
    var level = JsonToLevel<T>(jsonStr);

    return level;
}
```

## JsonIOEditor.cs

불러온 레벨들을 UI에 리스트로 보여주는 함수 스크립트입니다. 

```c#
void DisplayLevelList()
{
    EditorGUILayout.BeginVertical("Helpbox");
    
    // 검색창을 띄웁니다.
    searchName = EditorGUILayout.TextField(searchName, (GUIStyle)"SearchTextField");
    // 검색된 Json리스트 중에 해당되는 Level들을 찾습니다.
    var searchList = jsonIO.levelDB.jsonLevelDBs.FindAll(lv => lv.filename.ToLower().Contains(searchName.ToLower()));

    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(100));

    foreach (var db in searchList)
    {
        DrawJsonRow(db);
    }

    EditorGUILayout.EndScrollView();

    EditorGUILayout.EndVertical();
}
```
