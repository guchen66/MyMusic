using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Music.Core.Components
{
    public class YitIdInitComponent
    {
        //C:\Users\liuxin\.nuget\packages\yitter.idgenerator\1.0.14\
        public const string RegWorkerId_DLL_NAME = "Yitter.IdGenerator.dll";
        //根据文档定义三个接口
        [DllImport(RegWorkerId_DLL_NAME, EntryPoint = "RegisterOne", CallingConvention = CallingConvention.Cdecl, ExactSpelling = false)]
        // 注册一个 WorkerId，会先注销所有本机已注册的记录
        // ip: redis 服务器地址
        // port: redis 端口
        // password: redis 访问密码，可为空字符串“”
        // maxWorkerId: 最大 WorkerId
        private static extern ushort RegisterOne(string ip, int port, string password, int maxWorkerId);

        [DllImport(RegWorkerId_DLL_NAME, EntryPoint = "UnRegister", CallingConvention = CallingConvention.Cdecl, ExactSpelling = false)]
        // 注销本机已注册的 WorkerId
        private static extern void UnRegister();

        [DllImport(RegWorkerId_DLL_NAME, EntryPoint = "Validate", CallingConvention = CallingConvention.Cdecl, ExactSpelling = false)]
        // 检查本地WorkerId是否有效（0-有效，其它-无效）
        private static extern int Validate(int workerId);

        public static long NextId()
        {
            //这个if判断在高并发的情况下可能会有问题
            if (YitIdHelper.IdGenInstance == null)
            {
                UnRegister();

                // 如果不用自动注册WorkerId的话，直接传一个数值就可以了
                var workerId = RegisterOne("127.0.0.1", 6379, "", 63);
                // 创建 IdGeneratorOptions 对象，可在构造函数中输入 WorkerId：
                var options = new IdGeneratorOptions(workerId);
                // options.WorkerIdBitLength = 10; // 默认值6，限定 WorkerId 最大值为2^6-1，即默认最多支持64个节点。
                // options.SeqBitLength = 6; // 默认值6，限制每毫秒生成的ID个数。若生成速度超过5万个/秒，建议加大 SeqBitLength 到 10。
                // options.BaseTime = Your_Base_Time; // 如果要兼容老系统的雪花算法，此处应设置为老系统的BaseTime。
                // ...... 其它参数参考 IdGeneratorOptions 定义。

                // 保存参数（务必调用，否则参数设置不生效）：
                YitIdHelper.SetIdGenerator(options);

                // 以上过程只需全局一次，且应在生成ID之前完成。
            }

            return YitIdHelper.NextId();
        }
    }

}
