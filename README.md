# Realtime Entity Server

**A Unity Transport lab server that spawns networked balloon entities** and notifies clients when balloons are popped.

---

## Status

Networking coursework / lab. Companion to `realtimeentityclient-main`. Early prototype with CSV-style string messages over Unity Transport.

## Tech stack

| Area | What it uses |
|---|---|
| Engine | Unity |
| Networking | **Unity Transport** |
| Language | **C#** |

## What's in the project

| System | Key files |
|---|---|
| Transport server | `realtimeentityserver-main/Assets/NetworkServer.cs` |
| Message routing | `realtimeentityserver-main/Assets/NetworkServerProcessing.cs` |
| Balloon spawn / pop logic | `realtimeentityserver-main/Assets/GameLogic.cs` |

## About this repository

Educational networking sample. For a cleaner movement-focused Transport pair, see `realtime-movement-server` / `realtime-movement-client`.
