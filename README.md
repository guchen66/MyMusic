# MyMusic

这是我使用WPF写的一个简单的音乐播放器，

主要使用了Prism框架+SqlSugar框架+SqlServer数据库

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

