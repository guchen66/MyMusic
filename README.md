### MyMusic

这是我使用WPF写的一个简单的音乐播放器，

主要使用了Prism框架+SqlSugar框架+SqlServer数据库+IT.Tangdao(唐刀，我自研的框架)

播放音乐使用了NAudio框架，

使用HtmlAgilityPack从三大主流网站爬取音乐

使用Nlog写的日志功能

使用Mapster做了Dto的映射

前端组件库使用了MaterialDesign

使用Bogus添加了虚假数据，用作前期数据测试

使用Newtonsoft做了数据的持久化

使用NewLife的一些扩展做了简单的工具处理

里面的功能都很完善，可以用来学习WPF基础，后期Star数高了，我会在B站和抖音出一期视频，欢迎大家一起讨论，学习

#### 1、启动项 MyMusic

1、登录界面

![image-20240404124137284](C:\Users\liuxin\AppData\Roaming\Typora\typora-user-images\image-20240404124137284.png)

2、主界面展示

![image-20240404124231501](C:\Users\liuxin\AppData\Roaming\Typora\typora-user-images\image-20240404124231501.png)

3、使用YitIdHelper配置雪花ID

```c#
 // 配置雪花Id算法机器码
 YitIdHelper.SetIdGenerator(new IdGeneratorOptions
 {
     WorkerId = 1,// 取值范围0~63,默认1
    // DataCenterId=1,//数据中心Id
 });
```

4、使用反射注册仓储、服务、以及视图

减少下面的书写

```
 // containerRegistry.Register<IMapper, Mapper>();
 // containerRegistry.RegisterScoped<IHttpClientService, HttpClientService>();
 // containerRegistry.RegisterScoped<IRegister, AsideMenuRegister>();
```

改为

```C#
foreach (var assemblyName in assemblies)
{
    //1、先是加载程序集
    var assembly = Assembly.Load(assemblyName);
    //2、找到类中标注了特性ScanningAttribute的所有类
    var typesToRegister = assembly
        .GetTypes()
        .Where(type => Attribute.IsDefined(type, typeof(ScanningAttribute)));

    // 3、创建一个字典，键是接口类型，值是实现类的类型 因为标注了特性ScanningAttribute的类只有一个接口
    var typeInterfaceDicts = typesToRegister.ToDictionary(
        type => type.GetInterfaces()[0],
        type => type);

    foreach (var typeInterDict in typeInterfaceDicts)
    {
        //4、找到关于类中特性的RegisterType
        var attribute = typeInterDict.Value.GetCustomAttribute<ScanningAttribute>(false);
        if (attribute != null)
        {
            switch (attribute.RegisterType)
            {
                case "Register":
                    Register(typeInterDict.Key, typeInterDict.Value);
                    break;
                case "RegisterScoped":
                    RegisterScoped(typeInterDict.Key, typeInterDict.Value);
                    break;
                case "RegisterSingleton":
                    RegisterScoped(typeInterDict.Key, typeInterDict.Value);
                    break;
                default:
                    break;
            }
        }
    }
}
```



表单：

1、播放列表

2、删除的列表

3、收藏的列表

4、

#### 2、Mapster的使用

需要增加字段但是实体类生成失败，只能手动增加

```c#
ALTER TABLE AsideCreateController ADD IsFullContent BIT NULL

```

全局注册Mapster

```
 /// <summary>
 /// 注册Mapster
 /// </summary>
 /// <param name="containerRegistry"></param>
 private void RegisterMapper(IContainerRegistry containerRegistry)
 {

     var config = new TypeAdapterConfig();
     var assembly = Assembly.Load("Music.System");
     config.Scan(assembly);

     // 注册单例实例
     containerRegistry.RegisterInstance(typeof(TypeAdapterConfig), config);

     // 创建并注册 Mapper 实例
     var mapper = new Mapper(config);
     containerRegistry.RegisterInstance(typeof(Mapper), mapper);
 }
```

查找到IRegister所在的程序集，Mapster会自动注入，注意互相转化的写法

```C#
 public class AsideCreateControllerRegister : IRegister
 {
     public void Register(TypeAdapterConfig config)
     {
         config.ForType<AsideCreateController, AsideCreateControllerDto>()
             .Map(dest => dest.Id, src => src.Id)
             .Map(dest=>dest.PlayListName, src=>src.Name)
             .Map(dest=>dest.IsExistContent,src=>src.IsFullContent);

         config.ForType<AsideCreateControllerDto, AsideCreateController>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.PlayListName)
            .Map(dest => dest.IsFullContent, src => src.IsExistContent);
     }     
 }
```

#### 3、NAudio使用

1、首先我点击播放，请求网易的服务器，

### 首页

首页查询出港台最新流行的20首歌曲

### 收藏

### 下载

下载应该扫描本地文件，然后真实本地文件的音乐

### 最近播放

设置缓存，将播放过的歌曲的缓存存在数据库，由这个界面展示

### 设置

模仿qq音乐设置具体参数

### 优化

## 1. 播放器核心架构优化

### 场景：播放器引擎与UI的解耦

**适用模式**：桥接模式(Bridge Pattern) + 外观模式(Facade Pattern)

csharp



复制



下载

```
// 桥接模式 - 分离抽象(播放控制)与实现(具体播放引擎)
public interface IAudioEngine
{
    void Play(string filePath);
    void Pause();
    void Stop();
    double Volume { get; set; }
    // 其他音频操作
}

public class NAudioEngine : IAudioEngine { /* NAudio实现 */ }
public class BASSAudioEngine : IAudioEngine { /* BASS音频库实现 */ }

// 外观模式 - 简化复杂子系统调用
public class MusicPlayerFacade
{
    private readonly IAudioEngine _engine;
    
    public MusicPlayerFacade(IAudioEngine engine)
    {
        _engine = engine;
    }
    
    public void PlayPlaylist(Playlist playlist)
    {
        // 封装复杂的播放逻辑
    }
}
```

**优化点**：

- 将音频处理引擎与UI逻辑完全分离
- 可以轻松切换不同的音频引擎(如NAudio、BASS等)
- 提供简化的高级接口给UI层使用

## 2. 播放列表管理优化

### 场景：动态播放列表处理

**适用模式**：组合模式(Composite Pattern) + 迭代器模式(Iterator Pattern)

csharp



复制



下载

```
// 组合模式 - 统一处理单个曲目和播放列表
public interface IPlaylistItem
{
    string Name { get; }
    IEnumerable<Song> GetSongs();
}

public class Song : IPlaylistItem { /* 单曲实现 */ }

public class Playlist : IPlaylistItem
{
    private readonly List<IPlaylistItem> _items = new();
    
    public void Add(IPlaylistItem item) => _items.Add(item);
    
    public IEnumerable<Song> GetSongs()
    {
        // 迭代器模式 - 递归遍历所有子项
        foreach (var item in _items)
        {
            foreach (var song in item.GetSongs())
            {
                yield return song;
            }
        }
    }
}
```

**优化点**：

- 统一处理单曲和播放列表
- 支持嵌套播放列表结构
- 简化遍历逻辑

## 3. 状态管理优化

### 场景：播放器状态转换(播放/暂停/停止)

**适用模式**：状态模式(State Pattern)

csharp



复制



下载

```
public interface IPlayerState
{
    void Play(MusicPlayer player);
    void Pause(MusicPlayer player);
    void Stop(MusicPlayer player);
}

public class PlayingState : IPlayerState { /* 实现播放状态行为 */ }
public class PausedState : IPlayerState { /* 实现暂停状态行为 */ }
public class StoppedState : IPlayerState { /* 实现停止状态行为 */ }

public class MusicPlayer
{
    private IPlayerState _currentState;
    
    public void TransitionTo(IPlayerState state)
    {
        _currentState = state;
    }
    
    public void Play() => _currentState.Play(this);
    public void Pause() => _currentState.Pause(this);
    public void Stop() => _currentState.Stop(this);
}
```

**优化点**：

- 消除复杂的条件判断语句
- 每个状态的行为封装在独立类中
- 方便添加新状态(如缓冲状态)

## 4. 音频效果处理优化

### 场景：动态添加/移除音频效果

**适用模式**：装饰者模式(Decorator Pattern)

csharp



复制



下载

```
public abstract class AudioEffectDecorator : IAudioEngine
{
    protected IAudioEngine _wrappedEngine;
    
    protected AudioEffectDecorator(IAudioEngine engine)
    {
        _wrappedEngine = engine;
    }
    
    public virtual void Play(string filePath) => _wrappedEngine.Play(filePath);
    // 其他默认实现
}

public class ReverbEffect : AudioEffectDecorator
{
    public ReverbEffect(IAudioEngine engine) : base(engine) {}
    
    public override void Play(string filePath)
    {
        // 添加混响处理逻辑
        base.Play(filePath);
    }
}

// 使用
IAudioEngine engine = new ReverbEffect(new EchoEffect(new NAudioEngine()));
```

**优化点**：

- 动态添加效果而不修改原有代码
- 效果可以任意组合
- 符合开闭原则

## 5. 通知系统优化

### 场景：播放进度、状态变化通知

**适用模式**：观察者模式(Observer Pattern) + 事件聚合器(Event Aggregator)

csharp



复制



下载

```
// 事件聚合器
public class EventAggregator
{
    private readonly Dictionary<Type, List<object>> _subscribers = new();
    
    public void Subscribe<T>(Action<T> handler)
    {
        if (!_subscribers.ContainsKey(typeof(T)))
        {
            _subscribers[typeof(T)] = new List<object>();
        }
        _subscribers[typeof(T)].Add(handler);
    }
    
    public void Publish<T>(T message)
    {
        if (_subscribers.TryGetValue(typeof(T), out var handlers))
        {
            foreach (Action<T> handler in handlers.Cast<Action<T>>())
            {
                handler(message);
            }
        }
    }
}

// 定义事件
public class PlaybackProgressEvent
{
    public double Progress { get; set; }
    public TimeSpan CurrentTime { get; set; }
}
```

**优化点**：

- 松耦合的事件通知
- 避免直接依赖
- 易于扩展新的事件类型

## 6. 命令系统优化

### 场景：UI操作处理(播放、暂停、下一首等)

**适用模式**：命令模式(Command Pattern)

csharp



复制



下载

```
public interface ICommand
{
    void Execute();
    void Undo();
}

public class PlayCommand : ICommand
{
    private readonly MusicPlayer _player;
    private readonly string _filePath;
    
    public PlayCommand(MusicPlayer player, string filePath)
    {
        _player = player;
        _filePath = filePath;
    }
    
    public void Execute() => _player.Play(_filePath);
    public void Undo() => _player.Stop();
}

// 在ViewModel中使用
public class PlayerViewModel
{
    private readonly ICommand _playCommand;
    
    public PlayerViewModel()
    {
        _playCommand = new PlayCommand(player, selectedFile);
    }
    
    public ICommand PlayCommand => _playCommand;
}
```

**优化点**：

- 将操作封装为对象
- 支持撤销/重做功能
- 统一命令处理

## 7. 插件系统设计

### 场景：支持扩展功能(歌词、可视化等)

**适用模式**：依赖注入(DI) + 策略模式(Strategy Pattern)

csharp



复制



下载

```
// 插件接口
public interface IPlayerPlugin
{
    string Name { get; }
    void Initialize(MusicPlayer player);
}

// 插件管理器
public class PluginManager
{
    private readonly List<IPlayerPlugin> _plugins = new();
    
    public void RegisterPlugin(IPlayerPlugin plugin)
    {
        _plugins.Add(plugin);
    }
    
    public void InitializeAll(MusicPlayer player)
    {
        foreach (var plugin in _plugins)
        {
            plugin.Initialize(player);
        }
    }
}

// 具体插件实现
public class LyricsPlugin : IPlayerPlugin
{
    public string Name => "歌词插件";
    
    public void Initialize(MusicPlayer player)
    {
        player.PlaybackStarted += OnPlaybackStarted;
    }
    
    private void OnPlaybackStarted(string filePath)
    {
        // 加载歌词逻辑
    }
}
```

**优化点**：

- 系统可扩展性强
- 功能模块化
- 支持热插拔

## 重构建议步骤

1. **分析现有代码**：识别变化频繁的部分和稳定部分
2. **分层重构**：从底层核心功能开始(如音频引擎)
3. **逐步替换**：使用新设计逐步替换旧实现，而非一次性重写
4. **单元测试**：确保每次重构后功能正常
5. **性能测试**：特别是音频处理部分

## 应该避免的过度设计

1. 不要为简单的数据模型使用复杂模式
2. 避免创建过多的抽象层(保持平衡)
3. 只在真正需要扩展性的地方使用设计模式
4. 记住KISS原则(Keep It Simple, Stupid)
