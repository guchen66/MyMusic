using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Music.ToolKit.Extensions
{

    public class RegisterExtension
    {
        IContainerRegistry container;
        public RegisterExtension(IContainerRegistry containerRegistry)
        {

            container = containerRegistry;

        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <returns></returns>
       /* public RegisterExtension RegisterService()
        {
            var assembly = Assembly.GetExecutingAssembly(); // 获取当前执行的程序集
            var types = assembly.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && typeof(IPrismService).IsAssignableFrom(myType)); // 找到所有实现了IPrismComponent的非抽象类

            foreach (var type in types)
            {
                container.RegisterScoped(typeof(IPrismService), type); // 将找到的类型注册为IPrismComponent的实现

            }
            return this;
        }
*/
        /// <summary>
        /// 注册仓储
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <returns></returns>
      /*  public RegisterExtension RegisterRepository()
        {
            var assembly = Assembly.GetExecutingAssembly(); // 获取当前执行的程序集
            var types = assembly.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && typeof(IPrismRepository).IsAssignableFrom(myType)); // 找到所有实现了IPrismComponent的非抽象类

            foreach (var type in types)
            {
                container.RegisterScoped(typeof(IPrismRepository), type); // 将找到的类型注册为IPrismComponent的实现

            }
            return this;
        }*/

        /// <summary>
        /// 注册视图
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <returns></returns>
        public RegisterExtension RegisterForNavigation()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var viewModelsNamespace = "ViewModels";
            var viewsNamespace = "Views";

            var viewModelTypes = assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.Contains(viewModelsNamespace) && t.Name.EndsWith("ViewModel"))
                .ToList();

            var viewTypes = assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.Contains(viewsNamespace) && t.Name.EndsWith("View"))
                .ToList();

            foreach (var viewModelType in viewModelTypes)
            {
                var viewType = viewTypes.FirstOrDefault(v => v.Name == viewModelType.Name.Replace("ViewModel", "View"));
                if (viewType != null)
                {
                    container.RegisterForNavigation(viewType, viewModelType.ToString());
                }
            }
            return this;
        }

        /// <summary>
        /// 注册弹窗
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <returns></returns>
        public RegisterExtension RegisterDialog()
        {
            var assembly = Assembly.GetExecutingAssembly(); // 获取当前执行的程序集

            var dialogVMTypes = assembly.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && typeof(IDialogAware).IsAssignableFrom(myType));

            var doalogTypes = assembly.GetTypes()
               .Where(t => t.Namespace != null && t.Name.EndsWith("Dialog"))
               .ToList();
            foreach (var type in dialogVMTypes)
            {
                var viewType = doalogTypes.FirstOrDefault(v => v.Name == type.Name.Replace("ViewModel", "View"));
                if (viewType != null)
                {
                    container.RegisterForNavigation(viewType, type.ToString());
                }

            }
            return this;
        }
    }
}
