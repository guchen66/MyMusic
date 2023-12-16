
namespace Music.System.Managers
{
    public class EncryptManager
    {
        Aes aes = Aes.Create(); // 在类级别创建 Aes 实例

        /// <summary>
        /// 获取加密的对象
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public string GetEncryObject(params string[] values)
        {
            return string.Join(",", values); // 将参数字符串连接起来
        }

        /// <summary>
        /// 设置加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string SetEncryPassword = nameof(SetEncryPassword);

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        public string SetAESEncrypt(string data, string pass)
        {
            var dataBytes = GetEncryObject(data).GetBytes(); // 调用 GetEncryObject 方法并转换为字节数组
            var passBytes = SetEncryPassword.GetBytes();
            var rs = aes.Encrypt(dataBytes, passBytes).ToBase64(); // 执行加密并转为 Base64 字符串
            return rs;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="pass"></param>
        public string GetAESEncrypt(string data)
        {
            data = GetEncryObject(data);
            var passBytes = SetEncryPassword.GetBytes();
            var encryptedBytes = SetAESEncrypt(data, SetEncryPassword).ToBase64();
            var txt = aes.Decrypt(encryptedBytes, passBytes).ToStr(); // 执行解密并转为字符串
            return txt;
        }
    }
}
