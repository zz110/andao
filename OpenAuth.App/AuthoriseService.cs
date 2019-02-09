// ***********************************************************************
// Assembly         : OpenAuth.Domain
// Author           : yubaolee
// Created          : 04-21-2016
//
// Last Modified By : yubaolee
// Last Modified On : 04-21-2016
// Contact : Microsoft
// File: AuthenService.cs
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using OpenAuth.Repository.Domain;

namespace OpenAuth.App
{
    /// <summary>
    /// 领域服务
    /// <para>用户授权服务</para>
    /// </summary>
    public class AuthoriseService :BaseApp<User>
    {
        
        protected User _user;

        private List<string> _userRoleIds;    //用户角色GUID

        public List<Module> Modules
        {
            get { return GetModulesQuery().ToList(); }
        }

        public List<Role> Roles
        {
            get { return GetRolesQuery().ToList(); }
        }

        public List<ModuleElement> ModuleElements
        {
            get { return GetModuleElementsQuery().ToList(); }
        }

        public List<Resource> Resources
        {
            get { return GetResourcesQuery().ToList(); }
        }

        public List<Org> Orgs
        {
            get { return GetOrgsQuery().ToList(); }
        }

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                _userRoleIds = UnitWork.Find<Relevance>(u => u.FirstId == _user.Id && u.Key == Define.USERROLE).Select(u => u.SecondId).ToList();
            }
        }

        public void Check(string userName, string password)
        {
            var _user = Repository.FindSingle(u => u.Account == userName);
            if (_user == null)
            {
                throw new Exception("用户帐号不存在");
            }
            _user.CheckPassword(password);
        }

        /// <summary>
        /// 用户可访问的机构
        /// </summary>
        /// <returns>IQueryable&lt;Org&gt;.</returns>
        public virtual IQueryable<Org> GetOrgsQuery()
        {
            var orgids = UnitWork.Find<Relevance>(
                u =>
                    (u.FirstId == _user.Id && u.Key == Define.USERORG) ||
                    (u.Key == Define.ROLEORG && _userRoleIds.Contains(u.FirstId))).Select(u => u.SecondId);
            return UnitWork.Find<Org>(u => orgids.Contains(u.Id));
        }

        /// <summary>
        /// 当前登录用户可访问机构下所有用户信息
        /// </summary>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        public virtual IQueryable<User> GetUsersQueryByOrgIds(string orgIds, bool exclude_self = false)
        {
            var userIds = UnitWork.Find<Relevance>(
                               u => u.Key == Define.USERORG && orgIds.Contains(u.SecondId)).Select(u => u.FirstId);

            var userList = UnitWork.Find<User>(u => userIds.Contains(u.Id));
            if (exclude_self)
            {
                userList = userList.Where(u => !u.Id.Equals(_user.Id));
            }
            return userList;
        }

        /// <summary>
        /// 通过用户id获取用户所属的机构信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual IQueryable<Org> GetOrgByUserId(string userId)
        {
            var orgids = UnitWork.Find<Relevance>(
                  u => u.FirstId == userId && u.Key == Define.USERORG).Select(u => u.SecondId);

            return UnitWork.Find<Org>(u => orgids.Contains(u.Id));
        }


        /// <summary>
        /// 通过组织、角色获取用户列表
        /// </summary>
        /// <param name="orgid"></param>
        /// <param name="role_name"></param>
        /// <returns></returns>
        public virtual IQueryable<User> GetUserByOrgAndRole(string orgid, string role_name) {

            string sql = @"select * from [User] where id in(
	                            select distinct t1.UserId from (
	                              select a.FirstId as UserId,a.SecondId as OrgId from Relevance as a
	                              where a.[Key]='UserOrg'
	                            ) as t1
	                            inner join (
	                              select a.FirstId as UserId,a.SecondId as RoleId from Relevance a
	                              where a.[Key]='UserRole'
	                            ) t2
	                            on t1.UserId=t2.UserId
	                            where t1.OrgId=@orgid and t2.RoleId in(
	                               select RoleId from [Role] where Name=@role_name
	                            )
                            )";

            return Repository.ExecuteQuerySql<User>(sql, new List<System.Data.SqlClient.SqlParameter> {
                new System.Data.SqlClient.SqlParameter("@orgid",orgid),
                new System.Data.SqlClient.SqlParameter("@role_name",role_name)
            }.ToArray());
        }


        /// <summary>
        /// 通过角色名称获取组织结构
        /// </summary>
        /// <param name="role_name"></param>
        /// <returns></returns>
        public virtual IQueryable<Org> GetOrgByRole(string role_name)
        {

            string sql = @"select * from org o where exists(
                           select a.* from Relevance a  join [Role] b
                           on a.FirstId=b.Id
                           where [Key]='RoleOrg' and a.SecondId=o.Id and b.Name=@role_name)";

            return Repository.ExecuteQuerySql<Org>(sql, new System.Data.SqlClient.SqlParameter("@role_name", role_name));

        }


        /// <summary>
        /// 获取用户可访问的资源
        /// </summary>
        /// <returns>IQueryable&lt;Resource&gt;.</returns>
        public virtual IQueryable<Resource> GetResourcesQuery()
        {
            var resourceIds = UnitWork.Find<Relevance>(
                u =>
                    (u.FirstId == _user.Id && u.Key == Define.USERRESOURCE) ||
                    (u.Key == Define.ROLERESOURCE && _userRoleIds.Contains(u.FirstId))).Select(u => u.SecondId);
            return UnitWork.Find<Resource>(u => resourceIds.Contains(u.Id));
        }

        /// <summary>
        /// 模块菜单权限
        /// </summary>
        public virtual IQueryable<ModuleElement> GetModuleElementsQuery()
        {
            var elementIds = UnitWork.Find<Relevance>(
                u =>
                    (u.FirstId == _user.Id && u.Key == Define.USERELEMENT) ||
                    (u.Key == Define.ROLEELEMENT && _userRoleIds.Contains(u.FirstId))).Select(u => u.SecondId);
            return UnitWork.Find<ModuleElement>(u => elementIds.Contains(u.Id));
        }

        /// <summary>
        /// 得出最终用户拥有的模块
        /// </summary>
        public virtual IQueryable<Module> GetModulesQuery()
        {
            var moduleIds = UnitWork.Find<Relevance>(
                u =>
                    (u.FirstId == _user.Id && u.Key == Define.USERMODULE) ||
                    (u.Key == Define.ROLEMODULE && _userRoleIds.Contains(u.FirstId))).Select(u => u.SecondId);
            return UnitWork.Find<Module>(u => moduleIds.Contains(u.Id)).OrderBy(u => u.SortNo);
        }

        //用户角色
        public virtual IQueryable<Role> GetRolesQuery()
        {
            return UnitWork.Find<Role>(u => _userRoleIds.Contains(u.Id));
        }
    }
}