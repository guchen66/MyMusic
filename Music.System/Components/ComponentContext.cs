using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Components
{
    public sealed class ComponentContext
    {
        /// <summary>
        /// 组件类型
        /// </summary>
        public Type ComponentType { get; internal set; }

        /// <summary>
        /// 上级组件上下文
        /// </summary>
        public ComponentContext CalledContext { get; internal set; }

        /// <summary>
        /// 根组件上下文
        /// </summary>
        public ComponentContext RootContext { get; internal set; }

        /// <summary>
        /// 依赖组件列表
        /// </summary>
        public Type[] DependComponents { get; internal set; }

        /// <summary>
        /// 链接组件列表
        /// </summary>
        public Type[] LinkComponents { get; internal set; }

        /// <summary>
        /// 上下文数据
        /// </summary>
        private Dictionary<string, object> Properties { get; set; } = new();

        /// <summary>
        /// 是否是根组件
        /// </summary>
        internal bool IsRoot { get; set; } = false;

        /// <summary>
        /// 设置组件属性参数
        /// </summary>
        public Dictionary<string, object> SetProperty<TComponent>(object value) where TComponent : class, IPrismComponent, new()
        {
            return SetProperty(typeof(TComponent), value);
        }

        /// <summary>
        /// 设置组件属性参数
        /// </summary>
        public Dictionary<string, object> SetProperty(Type componentType, object value)
        {
            return SetProperty(componentType.FullName, value);
        }

        /// <summary>
        /// 设置组件属性参数
        /// </summary>
        public Dictionary<string, object> SetProperty(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var properties = RootContext == null ? Properties : RootContext.Properties;

            if (!properties.ContainsKey(key))
            {
                properties.Add(key, value);
            }
            else properties[key] = value;

            return properties;
        }

        /// <summary>
        /// 获取组件属性参数
        /// </summary>
        /// <returns></returns>
        public TComponentOptions GetProperty<TComponent, TComponentOptions>() where TComponent : class, IPrismComponent, new()
        {
            return GetProperty<TComponentOptions>(typeof(TComponent));
        }

        /// <summary>
        /// 获取组件属性参数
        /// </summary>
        /// <returns></returns>
        public TComponentOptions GetProperty<TComponentOptions>(Type componentType)
        {
            return GetProperty<TComponentOptions>(componentType.FullName);
        }

        /// <summary>
        /// 获取组件属性参数
        /// </summary>
        /// <returns></returns>
        public TComponentOptions GetProperty<TComponentOptions>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var properties = RootContext == null ? Properties : RootContext.Properties;

            return !properties.ContainsKey(key)
                ? default
                : (TComponentOptions)properties[key];
        }

        /// <summary>
        /// 获取组件所有依赖参数
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetProperties()
        {
            return RootContext == null ? Properties : RootContext.Properties;
        }
    }
}
