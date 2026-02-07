# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

OCDisOCDat is a Unity children's educational game about OCD awareness/therapy. It consists of 18 scenes: a bootstrap scene, a menu, 15 level scenes across 5 themes (Slippers, Cereal, Bathroom, Gallery, Cars — each with 3 difficulty levels), and a credits scene.

**Unity Version:** 6000.3.5f2
**Solution file:** OCDisOCDat.sln

## Build & Run

Standard Unity project — open in Unity Editor or build via CLI:
```
Unity -projectPath . -buildTarget <target>
```
No custom build scripts, CI/CD, Makefile, or test harness exists. Unity Test Framework is included as a dependency but not actively used.

## Architecture

### Dependency Injection (Zenject)

Zenject 9.1.0 (embedded in `Assets/Plugins/Zenject/`) is the DI framework. Services are bound as singletons in MonoInstaller classes and injected via `[Inject]` attributes on MonoBehaviours.

- **BootstrapInstaller** (`Assets/Scripts/Installers/BootstrapInstaller.cs`) — binds core services: `SoundService`, `SceneManagerService`, `PromiseTimerService`
- **ControlsInstaller** (`Assets/Scripts/Installers/ControlsInstaller.cs`) — binds the main Camera instance
- Per-level installers bind level-specific `LevelResolver` instances

### Core Services

| Service | Purpose |
|---------|---------|
| `SceneManagerService` | Additive scene loading/unloading using `ScenesEnum` indices |
| `SoundService` | Manages 24 sound effects (enum-based, loaded from Resources) + background music via separate AudioSources |
| `PromiseTimerService` | Wraps RSG.PromiseTimer; implements Zenject's `ITickable` for frame updates; provides `WaitFor(seconds)` returning promises |

### Interaction Base Classes (`Assets/Scripts/Interactors/Abstract/`)

Three abstract classes define all player interaction patterns:

- **Draggable** — drag-and-drop with collision detection against `DragTarget` objects, axis locking, position revert on failure
- **Tappable** — single tap/click interactions
- **Rotatable** — mouse-based rotation toward target angles (5-degree tolerance)

Each provides virtual methods (`OnDrag`, `OnTap`, `OnStartRotate`, `OnRotateTarget`, etc.) that level-specific subclasses override.

### Level Resolution Pattern

**LevelResolver** is an abstract base class injected into interactors for multi-object levels. It checks via LINQ `All()` whether every interactive object meets completion criteria, then triggers sound + scene transition on success.

### Game Flow

1. **Bootstrap (Scene 0):** `GameStarter` initializes music, loads GameStart scene additively
2. **GameStart (Scene 1):** Menu — dragging the "L" triggers first level load
3. **Level Scenes (2–16):** Interactor components handle user input → check completion → play sound → `PromiseTimerService.WaitFor(1f)` → load next scene
4. **Credits (Scene 17):** `Restarter` component allows replay

### Key Dependencies

- **RSG.Promises 3.0.1** — from OpenUPM registry (`com.rsg.promise`), used for async scene transitions and timed callbacks
- **DOTween** — embedded in `Assets/Demigiant/DOTween/`, animation tweening
- **TextMesh Pro** — UI text rendering

## Code Organization

```
Assets/Scripts/
├── Installers/        # Zenject MonoInstallers (bootstrap + per-level)
├── Services/          # SoundService, PromiseTimerService, IPromiseTimerService
├── Interactors/
│   ├── Abstract/      # Draggable, Tappable, Rotatable, LevelResolver
│   ├── SlipperLevel1-3/
│   ├── CerealLevel1-3/
│   ├── BathroomLevel1-3/
│   ├── GalleryLevel1-3/
│   └── Parking/       # Cars levels
├── GameStarter.cs     # Bootstrap entry point
├── SceneManagerService.cs
├── ScenesEnum.cs      # Scene index enum (0-17)
└── Axis.cs            # X/Y axis constraint enum
```

## Conventions

- Each level theme gets its own subfolder under `Interactors/` with concrete subclasses of the abstract interaction types
- Sound effects are stored as audio clips in Resources and referenced by the `SoundService.SoundEffects` enum
- Scene transitions always follow: disable input → play sound → wait 1 second (promise) → load next scene
- All injectable services are bound as singletons via `BindInstance(...).AsSingle().NonLazy()`
