using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace Utils
{
    public class Encrypt
    {
        public string str2 = "";//记录加密后的数值

        public Encrypt(string str)
        {

            MD5 md5 = new MD5CryptoServiceProvider();//创建MD5对象（MD5类为抽象类不能被实例化）

            byte[] date = System.Text.Encoding.Default.GetBytes(str);//将字符串编码转换为一个字节序列

            byte[] date1 = md5.ComputeHash(date);//计算data字节数组的哈希值（加密）

            md5.Clear();//释放类资源

            for (int i = 0; i < date1.Length - 1; i++)//遍历加密后的数值到变量str2
            {

                str2 += date1[i].ToString("X");//（X为大写时加密后的数值里的字母为大写，x为小写时加密后的数值里的字母为小写）

            }

        }

    }
}
