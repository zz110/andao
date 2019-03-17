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
    public class PerformanceAppraisalApp : BaseApp<PerformanceAppraisal>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryPerformanceAppraisalListReq request)
        {
             return new TableData
            {
                count = Repository.GetCount(null),
                data = Repository.Find(request.page, request.limit, "Id desc")
            };
        }


        public object page(int limit, int offset, PerformanceAppraisalQueryInput input)
        {
            User user = AuthUtil.GetCurrentUser().User;
            List<Relevance> rli = ReleManagerApp.Repository.Find(i => i.FirstId == user.Id && i.Key == "UserOrg").ToList<Relevance>();
            string orgids = "";
            for (int i = 0; i < rli.Count; i++)
            {
                orgids = orgids + "'" + rli[i].SecondId + "',";
            }
            if (orgids != "")
                orgids = orgids.Substring(0, orgids.Length - 1);
            else
                orgids = "''";

            offset += 1;
            string sql = $@"select top {limit} * from(


select row_number() over(order by c.Name) as num,

 a.[Id],isnull(AccessmentScore,0) AccessmentScore
      ,isnull(RatersId,'') RatersId,isnull(RatersName,'') RatersName,c.id JudgeId,c.Name as JudgeName,isnull(Optime,'') Optime
      , isnull(Datename(year,a.[Optime]),'') [State],b.Name DeptType 
 from( select *  
                                                      from PerformanceAppraisal a where 
                                                       (Datename(year,a.[Optime]) =@EvaluateYear) ) a 
                                                       right join [User] c
                                                       on a.JudgeId=c.Id inner join dbo.Relevance as r on r.firstid=c.id 
                                                       and r.[key]='UserOrg' inner join Org b
                                                       on b.Id=r.SecondId   inner join dbo.Relevance as r1 on r1.firstid=c.id 
                                                       and r1.[key]='UserRole' inner join Role ro
                                                       on ro.Id=r1.SecondId  
                                                       where  (b.id in ({orgids}) or {orgids}='') and 
                                                        (c.Name like '%'+@UserName+'%' or @UserName is null)
                                                       and (b.Name like '%'+@OrgName+'%' or @OrgName is null)
                and (ro.Name = '{ input.role }' or '{ input.role }' = '') and (b.BizCode='{input.DeptType}' or ('{input.DeptType}'='' or '{input.DeptType}' is null ))  
                                                       
                            ) as t where num > ({limit}*({offset}-1)) order by DeptType";

            var rows = Repository.ExecuteQuerySql<PerformanceAppraisalOutPut>(sql, input.ToParameters()).ToList<PerformanceAppraisalOutPut>();
            

            return new
            {
                total = rows.Count(),
                rows = rows
            };
        }

        public string Add(PerformanceAppraisal obj)
        {
            return Repository.AddAndReturnId(obj);
        }

        public void Update(PerformanceAppraisal obj)
        {
            UnitWork.Update<PerformanceAppraisal>(u => u.Id == obj.Id, u => new PerformanceAppraisal
            {
                //todo:要修改的字段赋值
                Optime = obj.Optime,
                AccessmentScore = obj.AccessmentScore
            });

        }

        public List<PerformanceAppraisalOutPut> List(string year,string type,string DeptType)
        {
            string sql = $@"select top 10000 JudgeId,JudgeName,
                           isnull( (select SUM(Score) from MonthlyAssessment 
                            where UserId = JudgeId 
                            and EvaluateYear='{ year }'),0) 
                            MonthlyAVG,isnull(max(m.Score),0) Score,
                            sum(
	                            case q1 
	                            when 10 then 1 
	                            when 11 then 0.7 
	                            when 12 then 0.3 
	                            else 0 
	                            end
                            ) q1,
                            sum(
	                            case q2 
	                            when 10 then 1 
	                            when 11 then 0.7 
	                            when 12 then 0.3 
	                            else 0 
	                            end
                            ) q2,
                            sum(
	                            case q3 
	                            when 10 then 1 
	                            when 11 then 0.7 
	                            when 12 then 0.3 
	                            else 0 
	                            end
                            ) q3,
                            sum(
	                            case q4 
	                            when 10 then 1 
	                            when 11 then 0.7 
	                            when 12 then 0.3 
	                            else 0 
	                            end
                            ) q4,
                            sum(
	                            case q5 
	                            when 10 then 1 
	                            when 11 then 0.7 
	                            when 12 then 0.3 
	                            else 0 
	                            end
                            ) q5,
                            sum(
	                            case q6
	                            when 10 then 1 
	                            when 11 then 0.8 
	                            when 12 then 0.6 
	                            when 13 then 0.3 
	                            else 0 
	                            end
                            ) q6,
                            sum(
	                            case 
	                            when q6 is not null then 1 
	                            else 0 
	                            end
                            ) q6count,sum(
	                            case 
	                            when q1 is not null then 1 
	                            else 0 
	                            end
                            ) q1count,sum(
	                            case 
	                            when q2 is not null then 1 
	                            else 0 
	                            end
                            ) q2count,sum(
	                            case 
	                            when q3 is not null then 1 
	                            else 0 
	                            end
                            ) q3count,sum(
	                            case 
	                            when q4 is not null then 1 
	                            else 0 
	                            end
                            ) q4count,sum(
	                            case 
	                            when q5 is not null then 1 
	                            else 0 
	                            end
                            ) q5count,
                            case when max(ro.name)<>'中层正职' then
                            isnull((select top 1 AccessmentScore from PerformanceAppraisal 
                            where JudgeId = t.JudgeId and YEAR(Optime) = 2018),0) 
							
							else 
							isnull(

							(select 要素*0.6+综合*0.4 from (
							select  要素=convert(decimal(18,1),case when (德_好+德_中+德_差)=0 or 
							                                                            (能_好+能_中+能_差)=0 or
							                                                            (勤_好+勤_中+勤_差)=0 or
							                                                            (绩_好+绩_中+绩_差)=0 or
							                                                            (廉_好+廉_中+廉_差)=0 then 0
						                                                        else 
							                                                            (德_好*1+德_中*0.7+德_差*0.3)*20/(德_好+德_中+德_差)+
							                                                            (能_好*1+能_中*0.7+能_差*0.3)*20/(能_好+能_中+能_差)+
							                                                            (勤_好*1+勤_中*0.7+勤_差*0.3)*20/(勤_好+勤_中+勤_差)+
							                                                            (绩_好*1+绩_中*0.7+绩_差*0.3)*20/(绩_好+绩_中+绩_差)+
							                                                            (廉_好*1+廉_中*0.7+廉_差*0.3)*20/(廉_好+廉_中+廉_差)
						                                                        end),综合=case when cnt=0 then 0 
								                        else 
								                        convert(decimal(18,1),(优秀*1+称职*0.8+基本称职*0.6+不称职*0.3)*100.0/cnt)
								                        end
														from (
							select 
							
											sum(case Q1 when 10 then 1 else 0 end) as '德_好', 
				                            sum(case Q1 when 11 then 0.7 else 0 end) as '德_中', 
				                            sum(case Q1 when 12 then 0.3 else 0 end) as '德_差', 

				                            sum(case Q2 when 10 then 1 else 0 end) as '能_好', 
				                            sum(case Q2 when 11 then 0.7 else 0 end) as '能_中', 
				                            sum(case Q2 when 12 then 0.3 else 0 end) as '能_差', 

				                            sum(case Q3 when 10 then 1 else 0 end) as '勤_好', 
				                            sum(case Q3 when 11 then 0.7 else 0 end) as '勤_中', 
				                            sum(case Q3 when 12 then 0.3 else 0 end) as '勤_差', 

				                            sum(case Q4 when 10 then 1 else 0 end) as '绩_好', 
				                            sum(case Q4 when 11 then 0.7 else 0 end) as '绩_中', 
				                            sum(case Q4 when 12 then 0.3 else 0 end) as '绩_差', 

				                            sum(case Q5 when 10 then 1 else 0 end) as '廉_好', 
				                            sum(case Q5 when 11 then 0.7 else 0 end) as '廉_中', 
				                            sum(case Q5 when 12 then 0.3 else 0 end) as '廉_差' ,
											count(Q6) cnt,
											 sum(case Q6 when 10 then 1 else 0 end) as '优秀', 
					                        sum(case Q6 when 11 then 1 else 0 end) as '称职', 
					                        sum(case Q6 when 12 then 1 else 0 end) as '基本称职', 
					                        sum(case Q6 when 13 then 1 else 0 end) as '不称职'
							
							 from Answer where JudgeId=t.JudgeId and PlanId = (select id from [plan] where [PlanName]='s领导测评')  )a) a)
							,0)

							 end  AccessmentScore 
                            from(
                              select row_number() over(order by a.Optime) as num,
                              a.RatersId,a.JudgeId,uj.Name as RatersName,ur.Name as JudgeName,
                              a.q1,a.q2,a.q3,a.q4,a.q5,a.q6 from Answer a 
                              inner join [user] ur on ur.Id = a.JudgeId 
                              inner join [user] uj on uj.Id = a.RatersId 
                              where YEAR(a.Optime) = { year } and a.State = '已提交' and PlanId <> (select id from [plan] where [PlanName]='s领导测评')
                            ) as t 
                            inner join Relevance r on r.FirstId = t.JudgeId  and r.[key]='UserRole' 
                            left join [Role] ro on ro.Id = r.SecondId 
							inner join Relevance r1 on r1.FirstId = t.JudgeId and r1.[key]='UserOrg' 
                            left join Org o on o.Id = r1.SecondId left join MonthlyEvaluation m on t.JudgeId=m.UserId and m.EvaluateYear={year}  
                            where num > 0 and (ro.Name = '{ type }' or '{ type }' = '') and (o.BizCode='{DeptType}' or ('{DeptType}'='' or '{DeptType}' is null ))  
                            group by JudgeId,JudgeName";
            var rows = Repository.ExecuteQuerySql<PerformanceAppraisalOutPut>(sql).ToList();

            return rows;
        }

    }
}