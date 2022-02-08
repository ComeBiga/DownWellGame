# Physics

![pc](https://user-images.githubusercontent.com/36800639/153020349-b327013a-2f9a-410d-902c-1c6730aee934.PNG)

바닥 끝에서 점프하는 것 처럼 세밀한 충돌 체크를 처리하기 위한 코드입니다.   
위 사진에 보이는 빨간 선이 충돌체크에 이용되는 ray입니다. 아래쪽 선 중 하나만 체크돼도 점프를 할 수 있는 상태가 됩니다.

## 주요 코드라인
### [PlayerController.cs](https://github.com/ComeBiga/DownWellGame/blob/CatDown_README/DownWell/Assets/1.Scripts/Player/PlayerController.cs)
```c#
// 경계점을 계산하기 위한 함수
public void UpdateRaycastOrigins()
{
    Bounds bounds = GetComponent<BoxCollider2D>().bounds;

    raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
    raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
    raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
}

// 얼마나 세밀하게 체크하는 지 계산하는 함수
public void CalculateRaySpacing()
{
    Bounds bounds = GetComponent<BoxCollider2D>().bounds;

    verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
    horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

    verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
}
```

```c#
public bool GroundCollision()
{
    for(int i = 0; i < verticalRayCount; i++)
    {
        Vector2 rayOrigin = raycastOrigins.bottomLeft;
        rayOrigin += Vector2.right * (verticalRaySpacing * i);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, .1f, groundLayerMask);

        Debug.DrawRay(rayOrigin, Vector2.down * .1f, Color.red);

        if (hit) return true;
    }

    return false;
}
```
