using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localink.UserCenter.Common
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileUtils
    {
        /// <summary>
        /// 如果文件存在，那么删除
        /// </summary>
        /// <param name="fileName"></param>
        public static void DeleteIfFileIsExists(string fileName)
        {
            fileName = PathUtils.EnsureAbsolutePath(fileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}
