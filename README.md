# Re-dying Dungeon - Mobile Multiplayer Action Game

Re-dying Dungeon là một tựa game **hành động sinh tồn kết hợp khám phá hầm ngục nhiều người chơi** được phát triển trên nền tảng **Unity** dành cho thiết bị di động. Dự án được thực hiện trong khuôn khổ môn học Đồ Án 1 tại Đại học Bách Khoa Hà Nội.

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
- **Hệ thống kỹ năng và vật phẩm đa dạng**.
- **Cơ chế phản bội** lấy cảm hứng từ Among Us.

## Tính năng đã triển khai

- Tạo/tìm/phòng chơi online (Photon)
  ![CreateRoom](https://github.com/user-attachments/assets/783aaf50-7f3e-43fe-91e1-22d45a985ba9)

- Đăng ký, đăng nhập, quên mật khẩu (PlayFab)
  ![AuthScreen](https://github.com/user-attachments/assets/bc5712e0-1f67-48ed-a2fe-cb77dd9c6fac)

- Sinh bản đồ ngẫu nhiên bằng Random Walk, BSP, BFS
  ![MapGen](https://github.com/user-attachments/assets/f152fe1f-1d7b-4e03-b9c0-14a3b662a8fe)

- Giao diện người dùng thân thiện (UI/UX phù hợp mobile)
- AI đơn giản cho quái (Patrol/Chase Player)
  ![NPCBehaviorGraph](https://github.com/user-attachments/assets/c6637ff8-6f2c-4e89-91c6-d72b50079c9b)

- Nhân vật có thể tấn công, di chuyển, tương tác
- Đồng bộ mạng trạng thái gameplay

