using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Localink.UserCenter.Common
{
    /// <summary>
    /// 路径操作类
    /// </summary>
    public class PathUtils
    {

        /// <summary>
        /// 确保是相对路径
        /// </summary>
        /// <param name="path"></param>
        public static string EnsureRelativePath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                path = HttpContext.Current.Request.MapPath(path);
            }
            return path;
        }

        /// <summary>
        /// 确保是绝对路径
        /// </summary>
        /// <param name="path"></param>
        public static string EnsureAbsolutePath(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                path = HttpContext.Current.Server.MapPath(path);
            }
            return path;
        }

        /// <summary>
        /// 当路径不存在时创建
        /// </summary>
        /// <param name="path"></param>
        public static void CreateWhenPathIsNotExists(string path)
        {
            path = EnsureAbsolutePath(path);
            var directory = Path.GetFullPath(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// 获取网络路径
        /// </summary>
        /// <param name="path"></param>
        public static string GetNetPath(string path)
        {
            if (path.StartsWith("~/"))
                path = path.Replace("~/", "");
            path = ConfigurationManager.AppSettings["IdentityServerRootAddress"] + path;
            path = path.Replace("//", "/");
            return path;
        }
    }
}
