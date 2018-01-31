using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Events;
using System.Threading.Tasks;
using IdentityServer3.Core.Services.Default;

namespace Localink.UserCenter.IdentityServer
{
    /// <summary>
    /// 令牌发放事件
    /// </summary>
    public class CustomEventService : DefaultEventService
    {
        public override Task RaiseAsync<T>(Event<T> evt)
        {
            switch (evt.EventType)
            {
                //更新令牌刷新了或者授权成功
                case EventTypes.Success:
                    break;
                //授权失败，授权码赎回失败
                case EventTypes.Error:
                    break;
                //未处理异常
                case EventTypes.Failure:
                    break;
                //令牌发放和证书验证
                case EventTypes.Information:
                    HandleInformationEvent(evt);
                    break;
            }
            return base.RaiseAsync<T>(evt);
        }

        private void HandleInformationEvent<T>(Event<T> evt)
        {
            switch (evt.Category)
            {
                case "TokenService":
                    #region 访问令牌服务
                    //记录登录用户的设备ID
                    var deviceId = HttpContext.Current.Request.Headers["DeviceID"];
                    if (!string.IsNullOrEmpty(deviceId))
                    {
                        var userId = (evt.Details as IdentityServer3.Core.Events.AccessTokenIssuedDetails).SubjectId;
                        var cacheKey = "UserID_DeviceID_" + userId;
                        var deviceIdCache = HttpContext.Current.Cache.Get(cacheKey);
                        if (deviceIdCache == null)
                        {
                            HttpContext.Current.Cache.Insert(cacheKey, deviceId);
                        }
                        else
                        {
                            HttpContext.Current.Cache[cacheKey] = deviceId;
                        }
                    }
                    #endregion
                    break;
                default:
                    break;
            }
        }
    }
}