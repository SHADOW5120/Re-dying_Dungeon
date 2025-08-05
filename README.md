# Re-dying Dungeon - Mobile Multiplayer Action Game

Re-dying Dungeon là một tựa game **hành động sinh tồn kết hợp khám phá hầm ngục nhiều người chơi** được phát triển trên nền tảng **Unity** dành cho thiết bị di động.

## Nền tảng & Công nghệ

- **Engine**: Unity (2D Top-down)
- **Ngôn ngữ**: C#
- **Multiplayer**: Photon Realtime
- **Backend**: Microsoft PlayFab
- **AI**: Unity Behavior Graph
- **Mobile Target**: Android

## Gameplay chính

- **Nhiều người chơi** (multiplayer online).
- **Đồ họa 2D** với góc nhìn từ trên xuống.
- **Khám phá dungeon** ngẫu nhiên (Random Map Generation).

## Sơ đồ phân cấp chức năng
![LevelDiagram](https://github.com/user-attachments/assets/fb555a3f-c5ae-40bf-951c-41e807f92c26)

## Sơ đồ thực thể liên kết

![ER](https://github.com/user-attachments/assets/1ee467ba-72a2-4557-8c6b-af23faf8067a)

## Tính năng đã triển khai
- Màn hình chính

![MainMenuScreen](https://github.com/user-attachments/assets/873ceb61-306c-451b-b263-e42cc863acc5)

- Màn hình Gameplay

![GamePlayScreen](https://github.com/user-attachments/assets/43daf88d-83b8-4af4-bf7c-21a8a42273e1)

- Tạo/tìm/phòng chơi online (Photon)

  ![CreateRoom](https://github.com/user-attachments/assets/783aaf50-7f3e-43fe-91e1-22d45a985ba9)

  ![PlayerInRoom](https://github.com/user-attachments/assets/b927ac70-1ba2-4069-a9ae-6402b1055fa1)

- Đăng ký, đăng nhập, quên mật khẩu (PlayFab)

  ![AuthScreen](https://github.com/user-attachments/assets/bc5712e0-1f67-48ed-a2fe-cb77dd9c6fac)

- Sinh bản đồ ngẫu nhiên bằng Random Walk, BSP, BFS

  ![MapGen](https://github.com/user-attachments/assets/f152fe1f-1d7b-4e03-b9c0-14a3b662a8fe)

- AI đơn giản cho quái (Patrol/Chase Player)

  ![NPCBehaviorGraph](https://github.com/user-attachments/assets/c6637ff8-6f2c-4e89-91c6-d72b50079c9b)

- Nhân vật có thể tấn công, di chuyển, tương tác
- Đồng bộ mạng trạng thái gameplay

