using Infrastructure;
using OpenAuth.App;
using OpenAuth.App.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenAuth.Mvc.Controllers
{
    public class AuthoriseController : Controller
    {
        public AuthoriseService _AuthoriseService { get; set; }

        /// <summary>
        /// 通过组织、角色获取用户列表
        /// </summary>
        /// <param name="orgid"></param>
        /// <param name="role_name"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUserByOrgAndRole(string orgid, string role_name) {

            Response<object> response = new Response<object>("服务器错误");
            try
            {
                response.Code = Response<object>.SUCCESS_CODE;
                response.Message = "";
                var result = _AuthoriseService.GetUserByOrgAndRole(orgid, role_name).Select(s => new
                {
                    s.Id,
                    s.Name
                }).ToList();
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 通过角色名称获取用户
        /// </summary>
        /// <param name="role_name"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetOrgByRole(string role_name) {

            Response<object> response = new Response<object>("服务器错误");
            try
            {
                response.Code = Response<object>.SUCCESS_CODE;
                response.Message = "";
                var result = _AuthoriseService.GetOrgByRole(role_name).Select(s => new
                {
                    s.Id,
                    s.Name
                }).ToList();
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);

        }



        /// <summary>
        /// 通过当前登录用户组织id获取用户列表
        /// </summary>
        /// <param name="exclude_self">0 返回所有，1 返回结果中排除当前登陆用户</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUserListByCurrentUserOrgIds(int exclude_self = 0) {

            Response<object> response = new Response<object>("服务器错误");
            try
            {
                var user = AuthUtil.GetCurrentUser();
                if (user.Orgs != null && user.Orgs.Count == 0)
                {
                    response.Message = $"请为{user.User.Name}分配组织结构。";
                }
                else {
                    var orgIds = string.Join(",", user.Orgs.Select(s => s.Id));
                    response.Code = Response<object>.SUCCESS_CODE;
                    response.Message = "";
                    var result= _AuthoriseService.GetUsersQueryByOrgIds(orgIds, exclude_self == 1).Select(s => new
                    {
                        s.Id,
                        s.Name
                    }).ToList();
                    response.Result = result;
                }

            }
            catch (Exception ex) {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 通过用户id获取用户所属的机构信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetOrgByUserId(string id) {

            Response<object> response = new Response<object>("服务器错误");
            try
            {
                var user = AuthUtil.GetCurrentUser();
                if (user.Orgs != null && user.Orgs.Count == 0)
                {
                    response.Message = $"请为{user.User.Name}分配组织结构。";
                }
                else
                {
                    var orgIds = string.Join(",", user.Orgs.Select(s => s.Id));
                    response.Code = Response<object>.SUCCESS_CODE;
                    response.Message = "";
                    response.Result = _AuthoriseService.GetOrgByUserId(id).Select(s => new
                    {
                        s.Id,
                        s.Name
                    }).ToList();
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);

        }
    }
}