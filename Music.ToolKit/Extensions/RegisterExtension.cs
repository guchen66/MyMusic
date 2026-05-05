using IT.Tangdao.Core.Attributes;
using IT.Tangdao.Core.Enums;
using Music.Shared.Attributes;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Music.ToolKit.Extensions
{
    public static class RegisterExtension
    {
        public static void RegisterScannedTypes(this IContainerRegistry containerRegistry)
        {
            var assemblies = new[] { "Music.SqlSugar", "Music.System", "MyMusic", };
            foreach (var assemblyName in assemblies)
            {
                //1、先是加载程序集
                var assembly = Assembly.Load(assemblyName);
                //2、找到类中标注了特性AutoRegisterAttribute的所有类
                var typesToRegister = assembly
                    .GetTypes()
                    .Where(type => Attribute.IsDefined(type, typeof(AutoRegisterAttribute)));

                // 3、创建一个字典，键是接口类型，值是实现类的类型 因为标注了特性AutoRegisterAttribute的类只有一个接口
                var typeInterfaceDicts = typesToRegister.ToDictionary(
                    type => type.GetInterfaces()[0],
                    type => type);

                foreach (var typeInterDict in typeInterfaceDicts)
                {
                    //4、找到关于类中特性的RegisterType
                    var attribute = typeInterDict.Value.GetCustomAttribute<AutoRegisterAttribute>(false);
                    if (attribute != null)
                    {
                        switch (attribute.Mode)
                        {
                            case RegisterMode.Transient:
                                Register(containerRegistry, typeInterDict.Key, typeInterDict.Value);
                                break;

                            case RegisterMode.Scoped:
                                RegisterScoped(containerRegistry, typeInterDict.Key, typeInterDict.Value);
                                break;

                            case RegisterMode.Singleton:
                                RegisterSingleton(containerRegistry, typeInterDict.Key, typeInterDict.Value);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 特性注册标注Register
        /// </summary>
        private static void Register(IContainerRegistry containerRegistry, Type interfaceType, Type implementationType)
        {
            containerRegistry.Register(interfaceType, implementationType);
        }

        /// <summary>
        /// 特性注册标注RegisterScoped
        /// </summary>
        private static void RegisterScoped(IContainerRegistry containerRegistry, Type interfaceType, Type implementationType)
        {
            containerRegistry.RegisterScoped(interfaceType, implementationType);
        }

        private static void RegisterSingleton(IContainerRegistry containerRegistry, Type interfaceType, Type implementationType)
        {
            containerRegistry.RegisterSingleton(interfaceType, implementationType);
        }
    }
}