# Realtime Entity Server (balloon pop)

**Unity Transport server for a small shared-entity lab:** the server periodically spawns “balloon” entities at random screen positions, tracks them by ID, and replicates spawn/pop events to connected clients. Clients report pops; the server removes the ID and notifies everyone.

Nested Unity project path: `realtimeentityserver-main/`.

**Paired repository:** [realtimeentityclient-main](https://github.com/PapiChulllo/realtimeentityclient-main)

---

## How it works

1. Server binds UDP **9001** (`AnyIpv4`) with Unity Transport pipelines (reliable sequenced + unused fire-and-forget).
2. Roughly once per second (`Time.time % 1 < Time.deltaTime`), `GameLogic` picks a random screen-space position, assigns `nextBalloonID++`, stores it, and sends `SpawnBalloon` signifier messages to all clients.
3. On client connect, `SendUnpoppedBalloons` replays the live set.
4. Clients send `BalloonPopped,<id>`; server deletes the ID if present and broadcasts `BalloonPopped` to all.

**Status / limitations:** educational networking sample. Spawn timing uses a frame-sensitive modulo check (not a precise 1 Hz timer). Positions are generated from the **server process’s** `Screen.width/height` (Editor/game view size), which may not match client resolutions. No auth, prediction, or persistence. No automated tests; Unity not re-verified in this documentation pass.

## Tech stack

| Area | What it uses |
|---|---|
| Engine | **Unity** `2022.3.5f1` |
| Networking | **Unity Transport** `1.3.4` |
| Port | **9001** |
| Protocol | Integer **signifiers** + CSV (`Encoding.Unicode`, length-prefixed) |

Signifiers (from processing scripts): client→server `BalloonPopped = 1`; server→client `SpawnBalloon = 1`, `BalloonPopped = 2`.

## What's in the project

| System | Key files |
|---|---|
| Driver, connections, framing | `realtimeentityserver-main/Assets/NetworkServer.cs` |
| Signifier parse / connect hooks | `realtimeentityserver-main/Assets/NetworkServerProcessing.cs` |
| Balloon spawn, pop, late-join sync | `realtimeentityserver-main/Assets/GameLogic.cs` |

Three authored scripts (~10 KB). Folder nesting (`realtimeentityserver-main/realtimeentityserver-main/`) reflects a zip-style import of the Unity project.

### Code / system highlights

- **Authoritative entity IDs:** only the server allocates balloon IDs and decides whether a pop is valid.
- **Late join:** new connections receive every still-unpopped balloon as spawn messages.
- **Broadcast pop:** successful pop removes dictionary entry then fans out to all connection IDs.

## Scenes

Open the nested Unity project and use its `Assets/Scenes/SampleScene.unity` (standard sample scene wiring for the server components). Enter Play mode before clients connect.

## Third-party assets

Unity packages only (Transport, etc.). No art packs required for the server.

## About this repository

Public educational / portfolio networking sample under **PapiChulllo**. Pair with [realtimeentityclient-main](https://github.com/PapiChulllo/realtimeentityclient-main). Related (movement, not balloons): `realtime-movement-server` / `realtime-movement-client`.
