using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using OpenAuth.Repository.Domain;
using OpenAuth.Repository.Dto;

namespace OpenAuth.App
{
    public class AnswerApp : BaseApp<Answer>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }

        public object page(int limit, int offset, MonthlyPostAssessmentQueryInput input)
        {
            
            string sql = $@"select top {limit} * from(
                              select row_number() over(order by a.id) as num,a.*,c.Name as RatersName,c1.Name as JudgeName
                                                       from Answer a 
                                                       left join [User] c
                                                       on a.RatersId=c.Id left join [User] c1 on a.JudgeId = c1.Id 
                                                       left join Relevance r on r.FirstId = a.JudgeId and r.[key]='UserOrg' 
                                                       inner join Org o on o.id = r.SecondId 
                                                       left join Relevance r1 on r1.FirstId = a.JudgeId and r1.[key]='UserRole' 
                                                       inner join Role ro on ro.id = r1.SecondId 
                                                       where (Datename(year,a.[Optime])=@EvaluateYear or @EvaluateYear is null)
                                    and (c1.Name like '%'+@UserName+'%'  or @UserName is null)
                                    and (o.Name like '%'+@OrgName+'%' or @OrgName is null)
                                    and (ro.Name=@role or @role is null)
                                    and (o.BizCode=@DeptType or (@DeptType='' or @DeptType is null or @DeptType='')) 
                                    and (a.PlanName like '%'+@PlanName+'%' or @PlanName is null or @PlanName='') 
                            ) as t where num > {offset}";

            var rows = Repository.ExecuteQuerySql<AnswerSearch>(sql, input.ToParameters()).ToList();

            sql = @"select count(*) from Answer a 
                                                       left join [User] c
                                                       on a.RatersId=c.Id left join [User] c1 on a.JudgeId = c1.Id 
                                                       left join Relevance r on r.FirstId = a.JudgeId and r.[key]='UserOrg' 
                                                       inner join Org o on o.id = r.SecondId 
                                                       left join Relevance r1 on r1.FirstId = a.JudgeId and r1.[key]='UserRole' 
                                                       inner join Role ro on ro.id = r1.SecondId 
                                                       where (Datename(year,a.[Optime])=@EvaluateYear or @EvaluateYear is null)
                                    and (c1.Name like '%'+@UserName+'%'  or @UserName is null)
                                    and (o.Name like '%'+@OrgName+'%' or @OrgName is null)
                                    and (ro.Name=@role or @role is null)
                                    and (o.BizCode=@DeptType or (@DeptType='' or @DeptType is null or @DeptType='')) and (a.PlanName like '%'+@PlanName+'%' or @PlanName is null or @PlanName='') ";

            int total = Repository.ExecuteQuerySql<int>(sql, input.ToParameters()).FirstOrDefault();

            return new
            {
                total = total,
                rows = rows
            };
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryAnswerListReq request)
        {
             return new TableData
            {
                count = Repository.GetCount(null),
                data = Repository.Find(request.page, request.limit, "Id desc")
            };
        }

        public void Add(Answer obj)
        {
            Repository.Add(obj);
        }
        
        public void Update(Answer obj)
        {
            UnitWork.Update<Answer>(u => u.Id == obj.Id, u => new Answer
            {
                //todo:要修改的字段赋值
                RatersId = obj.RatersId,
                JudgeId = obj.JudgeId,
                Q1 = obj.Q1,
                Q2 = obj.Q2,
                Q3 = obj.Q3,
                Q4 = obj.Q4,
                Q5 = obj.Q5,
                Q6 = obj.Q6,
                Optime = obj.Optime,
                State = obj.State,
                PlanName = obj.PlanName,
            });

        }

        public object NoAnswers(int limit, int offset, AnswerSearch input)
        {

            offset += 1;
            string sql = $@"  
  
  
  select u.Name,u.Account,o.name Password ,u.[Id]
      ,u.[Sex]
      ,u.[Status]
      ,u.[BizCode]
      ,u.[CreateTime]
      ,u.[CrateId]
      ,u.[TypeName]
      ,u.[TypeId]
      ,u.[Age]
      ,u.[XRank]
      ,u.[ZRank]
      ,[CardId],'' Politicalaffiliation,'' Position,'' DegreeEdu,'' Nation,'' Officetime from [user] u inner join dbo.Relevance r on u.id=r.firstid inner join dbo.Org o on o.id=r.secondid
  where u.id in (
 select * from temp 
  ) order by o.name
  
  ";

            var rows = Repository.ExecuteQuerySql<User>(sql, input.ToParameters()).ToList();


            return new
            {
                total = rows.Count(),
                rows = rows
            };
        }

    }
}