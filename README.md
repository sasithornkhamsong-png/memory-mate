[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/R0mj41T3)

**<ins>Note</ins>: Students must update this `README.md` file to be an installation manual or a README file for their own CS403 projects.**

**รหัสโครงงาน:** 68-1_38_lpp-r2

**ชื่อโครงงาน (ไทย):เมมโมรีเมท : แอปพลิเคชันเกมฝึกความจำ** 

**Project Title (Eng): MEMORY MATE : MEMORY TRAINING GAME APPLICATION** 

**อาจารย์ที่ปรึกษาโครงงาน:** ผู้ช่วยศาสตราจารย์ ดร.ลัมพาพรรณ พันธ์ชูจิตร์

**ผู้จัดทำโครงงาน:** 
1. นางสาวพัชรพร มณีฉาย  6309610084  patcharaporn.man@dome.tu.ac.th
2. นางสาวศศิธร ขำสงค์    6309681358  sasithorn.khams@dome.tu.ac.th
   
### เกี่ยวกับโครงงาน 
**MemoryMate** เป็นแอปพลิเคชันที่ออกแบบมาในรูปแบบเกมฝึกความจำระยะสั้น บนระบบปฏิบัติการ Android โดยอ้างอิงจากทฤษฎี **Spaced Petition** และ **Forgeting Curve** ของ Ebbinghaus โดยที่ผู้ใช้จะได้เลือกเล่นเกมที่หลากหลายรูปแบบ พร้อมภารกิจที่ได้รับมอบหมายภายในเกม อีกทั้งยังมีระบบติดตามพัฒนาการของผู้ใช้แบบ Real-time

---

### คุณสมบัติหลักของแอปพลิเคชัน
- รูปแบบเกมฝึกความจำที่ให้เลือกเล่นหลากหลาย ประกอบไปด้วย การจดจำตัวเลข, การจดจำคำศัพท์, การจดจำรูปภาพหรือไอคอน
- ระบบติดตามพัฒนาการของผู้เล่นและคะแนนสูงสุด
- ระบบภารกิจที่จะเพิ่มประสบการณ์ในการเล่นให้แก่ผู้ใช้งานมากยิ่งขึ้น
- ระบบแจ้งเตือนแบบเว้นระยะ เป็นการกระตุ้นให้ผู้ใช้งานกลับ่มาใช้งานระบบอย่างสม่ำเสมอ

--- 

### โปรแกรมและเครื่องมือที่ต้องติดต้ั้ง
| โปรแกรม | เวอร์ชัน | ลิงก์ดาวน์โหลด |
|---|---|---|
| Unity | 2022.3 LTS ขึ้นไป | https://unity.com/download |
| Android Build Support | (ติดตั้งผ่าน Unity Hub) | — |
| Visual Studio / VS Code | ล่าสุด | https://visualstudio.microsoft.com |

--- 

### โครงสร้างโฟลเดอร์
```
MemoryMate/
|--- Assets/
│   |--- Scenes/          # ไฟล์ Scene ทุกหน้าของเกม
│   |--- Script/          # C# scripts ทั้งหมด
│   │   |--- Game/        # Script ของแต่ละเกม
│   │   │   |--- HappyMarket/
│   │   │   |--- PartyGame/
│   │   |--- Toolsbar/    # Script ระบบ UI (Setting, Progress)
│   |--- Sprites/         # รูปภาพ และ UI assets
│   |--- Audio/           # ไฟล์เสียง BGM และ SFX
│   |--- Prefabs/         # Prefab objects
|--- Packages/
|--- ProjectSettings/
|--- README.md

```

---

### วิธีติดตั้งและเปิดโปรเจกต์

#### 1. Clone Project

```bash
git clone https://github.com/[your-repo]/memory-mate.git
cd memory-mate
```

#### 2. เปิดใน Unity Hub

1. เปิด **Unity Hub**
2. กด **Open** → **Add project from disk**
3. เลือกโฟลเดอร์ที่ clone มา
4. เลือก Unity version **2022.3 LTS** แล้วกด Open

#### 3. ติดตั้ง Android Build Support
 
1. เปิด **Unity Hub** > **Installs**
2. กดเมนู **setting** ข้างๆ version ที่ใช้
3. เลือก **Add modules**
4. ติ๊ก **Android Build Support** > Install

#### 4. ตั้งค่า Build Settings
 
1. ไปที่ **File > Build Settings**
2. เลือก **Android**
3. กด **Switch Platform**
4. ตรวจสอบว่า Scenes ทุก Scene อยู่ใน Build list ครบ

---
 
### วิธีรันโปรแกรม
 
#### รันใน Unity Editor (สำหรับทดสอบ)
 
1. เปิด Scene ***StartScene** ใน Assets/Scenes/
2. กดปุ่ม **Play** ด้านบน
 
#### Build ลงมือถือ Android
 
1. เชื่อมต่อมือถือ Android ผ่าน USB
2. เปิด **Developer Mode** และ **USB Debugging** ในมือถือ
3. ไปที่ **File → Build Settings → Build and Run**
4. รอ Build เสร็จ แอปจะติดตั้งลงมือถืออัตโนมัติ
 
---

 
### วิธีใช้งานแอปพลิเคชัน
 
1. เปิดแอป **Memory Mate** บนมือถือ
2. กด **เล่น** เพื่อเริ่มเกม
3. เล่นเกมให้ครบทุกด่านเพื่อรับคะแนนรวม
4. ดูพัฒนาการของตัวเองได้ที่หน้า **Progress**
5. รับภารกิจประจำวันได้ที่หน้า **Quest**
6. ระบบจะแจ้งเตือนให้กลับมาเล่นทุก 2 วัน ตามทฤษฎี Spaced Repetition
 
---
 
### ทฤษฎีที่ใช้
 
- **Spaced Repetition** — การทบทวนซ้ำในช่วงเวลาที่ห่างกันขึ้นเรื่อยๆ ช่วยให้จำได้นานขึ้น
- **Forgetting Curve (Ebbinghaus)** — ความจำลดลง ~50% ภายใน 2 วัน การแจ้งเตือนช่วยทบทวนก่อนลืม
- **Immediate Feedback Effect** — การให้ feedback ทันทีช่วยพัฒนาการเรียนรู้
