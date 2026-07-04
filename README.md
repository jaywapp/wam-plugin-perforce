# WAM Perforce Plugin

WAM의 첫 번째 공식 플러그인. Perforce의 구조(Depot·Stream·Workspace·Changelist)를
WAM 캔버스의 노드와 관계로 표현합니다.

- **PluginId**: `wam.plugin.perforce`
- **상태**: 스켈레톤 — 노드 타입·관계 타입·설정 페이지(저장 동작)까지 구현.
  실제 p4 서버 연동은 `Services/IPerforceClient` 뒤에 구현 예정.

## 제공 타입

| 노드 타입 | ID | 비고 |
|-----------|----|------|
| Depot | `perforce.depot` | Simple 뷰 기본 |
| Stream | `perforce.stream` | Simple 뷰 기본 |
| Workspace | `perforce.workspace` | |
| Changelist | `perforce.changelist` | 상태: Pending → Shelved → Submitted |

| 관계 타입 | ID | 의미 |
|-----------|----|------|
| Contains | `perforce.contains` | Depot → Stream, Stream → Workspace |
| Submitted To | `perforce.submitted-to` | Changelist → Stream |

## 설정

설정 창 → Plugins → Perforce. 저장 위치는 `%AppData%\WAM\plugins\wam.plugin.perforce.json`.

| 항목 | 대응 |
|------|------|
| 서버 | `P4PORT` (예: `ssl:p4.example.com:1666`) |
| 사용자 | `P4USER` |
| 워크스페이스 | `P4CLIENT` |
| 연결 타임아웃 | 초 단위, 기본 30 |

비밀번호는 저장하지 않습니다 — 인증은 `p4 login` 티켓을 사용합니다.

## 구조

```
perforce/
├── README.md
└── src/
    └── Wam.Plugins.Perforce/
        ├── PerforcePlugin.cs            # IWamPlugin: 타입·설정 페이지 선언
        ├── PerforceSettings.cs          # 영속화되는 설정 POCO
        ├── PerforceSettingsViewModel.cs # ISettingsPage (Apply에서 저장)
        └── Services/
            └── IPerforceClient.cs       # 향후 p4 연동 경계
```

현재는 WAM 레포 안에서 인큐베이션 중이며(`Wam.sln`의 `plugins` 솔루션 폴더),
외부 어셈블리 로딩이 도입되면 독립 repository로 분리합니다.
개발 규칙은 [plugins/README.md](../README.md)를 따릅니다.

## 로드맵

- [x] 노드/관계 타입 등록, 설정 페이지 + 영속화
- [ ] `IPerforceClient` p4 CLI 구현 (`p4 info` 연결 테스트 버튼)
- [ ] Depot/Stream 구조 가져오기 → 노드 자동 생성
- [ ] Changelist 조회·상태 동기화 (pending/submitted)
- [ ] 노드 더블클릭 액션 (P4V로 열기)
