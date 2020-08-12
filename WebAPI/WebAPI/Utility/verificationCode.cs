using System;

namespace WebAPI.Utility
{
    public class VerificationCode
    {
        /// <summary>
        /// 产生N位数验证码
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string code(int number)
        {
            var code = "";
            var random = new Random((int)DateTime.Now.Ticks);
            const string textArray = "23456789ABCDEFGHGKLMNPQRSTUVWXYZ";

            for (var i = 0; i < number; i++)
            {
                code = code + textArray.Substring(random.Next() % textArray.Length, 1);
            }

            return code;
        }

    }
}