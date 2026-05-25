# CLAUDE.md

本文件为Claude Code (claude.ai/code)提供在此仓库中操作代码的指导。

## 项目概述

**MyHealingTown** 是一个基于Unity的自然治疗游戏（产品名称：NatureTherapy），专注于农场模拟和放松体验。该项目似乎是一个治疗性游戏，玩家可以在轻松的环境中种植作物和管理农场。

## 项目结构

### 核心架构
- **场景进度制**：游戏通过编号场景进行流程控制（0.Start, 1.Intro, 2.Farm）
- **数据驱动设计**：使用ScriptableObjects进行物品/种子数据管理
- **组件化架构**：标准Unity MonoBehaviour架构，配备专门的管理器

### 关键系统
1. **场景管理**：StartManager处理场景转换和游戏退出
2. **农场系统**：CropBehaviour管理植物通过状态生长（种子 → 幼苗 → 开花 → 可收获）
3. **库存系统**：HandInventorySlot和数据类（ItemData, SeedData, EquipmentData）
4. **时间系统**：GameTimestamp工具类用于时间转换（天到时/分）

### 重要文件位置
- **主要脚本**：`Assets/Scripts/[场景编号].[场景名称]/`
- **核心游戏逻辑**：`Assets/Scripts/2.Farm/`（农场机制）
- **数据定义**：`Assets/Scripts/2.Farm/Inventory/Data/`（ScriptableObject数据类）
- **主要场景**：`Assets/Scenes/0.Start.unity`, `1.Intro.unity`, `2.Farm.unity`

## 开发命令

### Unity编辑器操作
- 在Unity Hub中打开项目：导航到`MyHealingTownProject/`文件夹
- 构建项目：使用Unity编辑器的构建设置（文件 → 构建设置）
- 在编辑器中运行：按Unity编辑器中的播放按钮

### 场景导航
- 开始场景：`0.Start.unity`（主菜单）
- 介绍场景：`1.Intro.unity`（教程/介绍）
- 主要游戏：`2.Farm.unity`（农场游戏玩法）

## 关键代码模式

### 数据管理
- 游戏数据的ScriptableObject模式（ItemData, SeedData, EquipmentData）
- 用于简化编辑器创建的CreateAssetMenu属性
- 数据与行为的分离

### 游戏状态管理
- 基于枚举的状态机（CropState用于植物生长）
- 基于场景的游戏流程
- 通过Unity序列化进行组件通信

### 时间系统
- 自定义GameTimestamp类进行时间计算
- 生长机制与时间进度相关联

## 依赖项

### 第三方资源
- **SteamVR**：VR支持集成
- **AutoHand**：VR手部交互系统
- **AllSkyFree**：天空盒/照明包，用于营造氛围
- **NaughtyAttributes**：增强编辑器属性

## 常见开发任务

### 添加新物品
1. 使用ItemData类创建ScriptableObject
2. 分配缩略图和3D模型引用
3. 通过适当的数据管理器添加到库存系统

### 创建新作物
1. 创建SeedData ScriptableObject
2. 定义生长阶段（幼苗，开花，可收获预制件）
3. 设置生长时间（daysToGrow属性）
4. 链接到可收获的ItemData

### 场景开发
1. 遵循编号场景命名约定
2. 使用现有管理器模式（例如，StartManager作为模板）
3. 在相应的`Scripts/[场景编号].[场景名称]/`文件夹中实现场景特定脚本

## 性能考虑
- 作物生长阶段的对象池（CropBehaviour中的Instantiate调用）
- 高效的时间基础生长计算
- 通过SteamVR集成进行VR优化

## 测试
- 测试场景位于`Assets/Scenes/Test/`（Player.unity, TestGrab.unity）
- 使用Unity的播放模式进行即时测试
- VR功能需要VR硬件设置

## 资源组织
- 遵循现有文件夹结构：`Assets/Scripts/[场景编号].[场景名称]/`
- 按类型组织数据资源（物品，种子，装备）
- 使用Unity的CreateAssetMenu创建数据对象

## 构建配置
- 产品名称："NatureTherapy"
- 默认场景分辨率：1024x768
- 色彩空间：线性（为了更好的照明效果）
- 通过SteamVR集成启用VR支持