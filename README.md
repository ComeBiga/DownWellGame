
# 캣다운(CAT DOWN)

![CatDownLogo](https://user-images.githubusercontent.com/36800639/232320649-ee4e089a-27c8-41a6-a16d-f5d0ff3e404b.png)

이 게임은 2D 종스크롤 방식의 슈팅 게임입니다.

게임 '[다운웰(Downwell)](https://youtu.be/tpDONgfBuzk)'을 벤치마킹해서, 기존 게임의 속도감과 타격감을 살려서 개발했습니다.

__개발엔진__ : Unity3D  
__목표플랫폼__ : 모바일   
__플레이 영상__ : [Link](https://youtu.be/x36c1LxZNZ0)    
__구글플레이__ : [Link](https://play.google.com/store/apps/details?id=com.FourDX.CatDown)

***
## 게임 설명
<img src="https://user-images.githubusercontent.com/36800639/232319414-abdced62-3477-4459-9edf-c130d246485e.PNG"  width="250" height="550"/> <img src="https://user-images.githubusercontent.com/36800639/232319342-a25478bb-790e-4ce9-9780-5671d272ea95.jpg"  width="250" height="550"/> <img src="https://user-images.githubusercontent.com/36800639/232319944-ba6e70de-729c-407a-9c96-ccf98667760c.jpg"  width="250" height="550"/>

아래로 이어진 맵을 따라서 내려가면서 몬스터를 피해 내려가야 합니다.

**조작법**은 간단하게 _좌우 이동_, _점프_, _점프 중 발사_ 입니다. _몬스터를 밟는 경우_ 에도 잡을 수 있습니다.


<img src="https://user-images.githubusercontent.com/36800639/232320136-e30e1f88-771e-4641-9814-5873bbeaaf40.jpg"  width="250" height="550"/> <img src="https://user-images.githubusercontent.com/36800639/232320168-4f5239b3-4c22-43f1-a50c-60fc92e0dd26.jpg"  width="250" height="550"/> <img src="https://user-images.githubusercontent.com/36800639/232320315-87aebfa5-11b2-40e2-a91b-a066f6a9d402.jpg"  width="250" height="550"/>

아이템을 얻어 공격력을 강화할 수 있습니다.

강해진 공격으로 보스를 잡으면 됩니다.

***

## 주요 코드
 ### 맵 생성
+ #### [MapDisplay](https://github.com/ComeBiga/DownWellGame/tree/main/DownWell/Assets/1.Scripts/Map)
+ #### [ObjectPooler](https://github.com/ComeBiga/DownWellGame/blob/main/DownWell/Assets/1.Scripts/Map/ObjectPooler/ObjectPooler.cs)
 ### 맵 에디터
+ #### [LevelEditor](https://github.com/ComeBiga/DownWellGame/tree/main/DownWell/Assets/0.Scenes/LevelEditor)
 ### 몬스터 패턴
+ #### [BossBrain](https://github.com/ComeBiga/DownWellGame/tree/main/DownWell/Assets/1.Scripts/Enemy/Boss/Pattern/README.md)
+ #### [EnemyBrain](https://github.com/ComeBiga/DownWellGame/blob/main/DownWell/Assets/1.Scripts/Enemy/README.md)
 ### 충돌처리
+ #### [Collision](https://github.com/ComeBiga/DownWellGame/blob/main/DownWell/Assets/1.Scripts/Player/README.md)
