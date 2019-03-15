using Infrastructure;
using OpenAuth.Repository.Domain;
using OpenAuth.Repository.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.App
{
    /// <summary>
    /// 统计分析
    /// </summary>
    public class StatisticalAnalysisApp : BaseApp<StatisticalAnalysis>
    {
        #region 测评得分
        /// <summary>
        /// 测评得分
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public object get_evaluationscore_data(EvaluationscoreQueryInput input) {

            string sql = @"select ROW_NUMBER() over(order by 要素得分 desc) as Num,
                                           Name,
		                                   OrgName,isnull(XRank,'') as XRank,
		                                   德_好,德_中,德_差,
		                                   能_好,能_中,能_差,
		                                   勤_好,勤_中,勤_差,
		                                   绩_好,绩_中,绩_差,
		                                   廉_好,廉_中,廉_差,
		                                   要素得分
                                from (
	                                select u.Name,
		                                   t2.OrgName,t2.XRank,
		                                   德_好,德_中,德_差,
		                                   能_好,能_中,能_差,
		                                   勤_好,勤_中,勤_差,
		                                   绩_好,绩_中,绩_差,
		                                   廉_好,廉_中,廉_差,
		                                   要素得分=convert(decimal(18,2),case when (德_好+德_中+德_差)=0 or 
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
						                                end)
	                                 from (
			                                select JudgeId,
				                                sum(case Q1 when 10 then 1 else 0 end) as '德_好', 
				                                sum(case Q1 when 11 then 1 else 0 end) as '德_中', 
				                                sum(case Q1 when 12 then 1 else 0 end) as '德_差', 

				                                sum(case Q2 when 10 then 1 else 0 end) as '能_好', 
				                                sum(case Q2 when 11 then 1 else 0 end) as '能_中', 
				                                sum(case Q2 when 12 then 1 else 0 end) as '能_差', 

				                                sum(case Q3 when 10 then 1 else 0 end) as '勤_好', 
				                                sum(case Q3 when 11 then 1 else 0 end) as '勤_中', 
				                                sum(case Q3 when 12 then 1 else 0 end) as '勤_差', 

				                                sum(case Q4 when 10 then 1 else 0 end) as '绩_好', 
				                                sum(case Q4 when 11 then 1 else 0 end) as '绩_中', 
				                                sum(case Q4 when 12 then 1 else 0 end) as '绩_差', 

				                                sum(case Q5 when 10 then 1 else 0 end) as '廉_好', 
				                                sum(case Q5 when 11 then 1 else 0 end) as '廉_中', 
				                                sum(case Q5 when 12 then 1 else 0 end) as '廉_差'
			                                from Answer
			                                where State='已提交' and PlanId not in (select id from [Plan] where PlanName like '%s%') and year(Optime)=@EvaluateYear and (JudgeId in(
                                select distinct a.FirstId from Relevance a  join [Role] b
                                on a.SecondId=b.Id
                                where a.[Key]='UserRole' and b.Name=@role

                            ) or @role is null) 
			                                group by JudgeId
	                                ) as t1 left join [User] as u
	                                on t1.JudgeId=u.Id
	                                 join (
    
			                                select UserId,OrgName,BizCode,XRank from (
			                                select  ROW_NUMBER() over(partition by UserId order by num) as num,UserId,o.Name as OrgName,o.BizCode,u.XRank from (
			                                 select ROW_NUMBER() over(partition by SecondId order by OperateTime desc) as num,FirstId as UserId,SecondId as OrgId from Relevance where [Key]='UserOrg'
			                                ) as t
			                                 join [User] u
			                                on t.UserId=u.Id
			                                 join Org o
			                                on t.OrgId=o.Id
			                                ) as t
			                                where t.num=1 and (t.BizCode=@DeptType or @DeptType is null)
	                                ) t2
	                                on t1.JudgeId=t2.UserId and   (t2.OrgName like '%'+@OrgName+'%' or @OrgName is null) and 
                                                                    (u.Name like '%'+@UserName+'%' or @UserName is null)
                                ) as t";

            var rows = Repository.ExecuteQuerySql<EvaluationscoreOutput>(sql, input.ToParameters()).ToList();
            return new
            {
                rows = rows,
                total = 10000
            };
        }

        #endregion

        #region 综合评分
        /// <summary>
        /// 综合评分
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public object get_integration_score_data(EvaluationscoreQueryInput input) {

            string sql = @"select ROW_NUMBER() over(order by 综合得分 desc) as Num,
                                           Name,
		                                   OrgName,isnull(XRank,'') as XRank,
		                                   优秀,称职,基本称职,不称职,优秀称职率,不称职率,综合得分
		                                   
                                from (
	                                select u.Name,
		                                   t2.OrgName,t2.XRank,
		                                   优秀,称职,基本称职,不称职,
		                                   优秀称职率=convert(decimal(18,2),(convert(decimal(18,2),(优秀+称职))/convert(decimal(18,2),(优秀+称职+基本称职+不称职))*100)),
										   不称职率=convert(decimal(18,2),(convert(decimal(18,2),(不称职))/convert(decimal(18,2),(优秀+称职+基本称职+不称职))*100)),
										   综合得分=case when cnt=0 then 0 
														 else 
														   convert(decimal(18,2),(优秀*1+称职*0.8+基本称职*0.6+不称职*0.3)*100.0/cnt)
														 end

		                                  
	                                 from (
			                                select JudgeId,(select count(*) from Answer a where a.JudgeId=a1.JudgeId and State='已提交' and a.PlanId not in (select id from [Plan] where PlanName like '%s%')) as cnt,
												sum(case Q6 when 10 then 1 else 0 end) as '优秀', 
												sum(case Q6 when 11 then 1 else 0 end) as '称职', 
												sum(case Q6 when 12 then 1 else 0 end) as '基本称职', 
												sum(case Q6 when 13 then 1 else 0 end) as '不称职'
											from Answer as a1
											where State='已提交' and a1.PlanId not in (select id from [Plan] where PlanName like '%s%') and year(Optime)=@EvaluateYear and (JudgeId in(
                                select distinct a.FirstId from Relevance a  join [Role] b
                                on a.SecondId=b.Id
                                where a.[Key]='UserRole' and b.Name=@role

                            ) or @role is null) 
											group by JudgeId 
	                                ) as t1 left join [User] as u
	                                on t1.JudgeId=u.Id
	                                 join (
    
			                                select UserId,OrgName,BizCode,XRank from (
			                                select  ROW_NUMBER() over(partition by UserId order by num) as num,UserId,o.Name as OrgName,o.BizCode,u.XRank from (
			                                 select ROW_NUMBER() over(partition by SecondId order by OperateTime desc) as num,FirstId as UserId,SecondId as OrgId from Relevance where [Key]='UserOrg'
			                                ) as t
			                                 join [User] u
			                                on t.UserId=u.Id
			                                 join Org o
			                                on t.OrgId=o.Id
			                                ) as t
			                                where t.num=1 and (t.BizCode=@DeptType or @DeptType is null)
	                                ) t2
	                                on t1.JudgeId=t2.UserId and   (t2.OrgName like '%'+@OrgName+'%' or @OrgName is null) and 
                                                                    (u.Name like '%'+@UserName+'%' or @UserName is null)
                                ) as t";

            var rows = Repository.ExecuteQuerySql<EvaluationIntegrationOutput>(sql, input.ToParameters()).ToList();
            return new
            {
                rows = rows,
                total = 10000
            };
        }

        #endregion

        #region 总分
        /// <summary>
        /// 获取总分
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public object get_totalscore_data(EvaluationscoreQueryInput input) {

            string sql = $@"select ROW_NUMBER() over(order by t1.要素*0.6+t2.综合*0.4 desc) as Num,t1.Name,t1.OrgName,isnull(t1.XRank,'') as XRank,t1.要素,t2.综合,convert(decimal(18,1),t1.要素*0.6+t2.综合*0.4) as 总分 from (
                           select 
                                                                    Name,
		                                                            OrgName,XRank,
		                                                            要素
                                                        from (
	                                                        select u.Name,
		                                                            t2.OrgName,
																	t2.XRank,
		                                                            要素=convert(decimal(18,1),case when (德_好+德_中+德_差)=0 or 
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
						                                                        end)
	                                                            from (
			                                                        select JudgeId,
				                                                        sum(case Q1 when 10 then 1 else 0 end) as '德_好', 
				                                                        sum(case Q1 when 11 then 1 else 0 end) as '德_中', 
				                                                        sum(case Q1 when 12 then 1 else 0 end) as '德_差', 

				                                                        sum(case Q2 when 10 then 1 else 0 end) as '能_好', 
				                                                        sum(case Q2 when 11 then 1 else 0 end) as '能_中', 
				                                                        sum(case Q2 when 12 then 1 else 0 end) as '能_差', 

				                                                        sum(case Q3 when 10 then 1 else 0 end) as '勤_好', 
				                                                        sum(case Q3 when 11 then 1 else 0 end) as '勤_中', 
				                                                        sum(case Q3 when 12 then 1 else 0 end) as '勤_差', 

				                                                        sum(case Q4 when 10 then 1 else 0 end) as '绩_好', 
				                                                        sum(case Q4 when 11 then 1 else 0 end) as '绩_中', 
				                                                        sum(case Q4 when 12 then 1 else 0 end) as '绩_差', 

				                                                        sum(case Q5 when 10 then 1 else 0 end) as '廉_好', 
				                                                        sum(case Q5 when 11 then 1 else 0 end) as '廉_中', 
				                                                        sum(case Q5 when 12 then 1 else 0 end) as '廉_差'
			                                                        from Answer
			                                                        where State='已提交' and PlanId not in (select id from [Plan] where PlanName like '%s%') and year(Optime)=@EvaluateYear and (JudgeId in(
                                select distinct a.FirstId from Relevance a  join [Role] b
                                on a.SecondId=b.Id
                                where a.[Key]='UserRole' and b.Name=@role

                            ) or @role is null) 
			                                                        group by JudgeId
	                                                        ) as t1 left join [User] as u
	                                                        on t1.JudgeId=u.Id
	                                                            join (
    
			                                                        select UserId,OrgName,BizCode,XRank from (
			                                                        select  ROW_NUMBER() over(partition by UserId order by num) as num,UserId,o.Name as OrgName,o.BizCode,u.XRank from (
			                                                            select ROW_NUMBER() over(partition by SecondId order by OperateTime desc) as num,FirstId as UserId,SecondId as OrgId from Relevance where [Key]='UserOrg'
			                                                        ) as t
			                                                            join [User] u
			                                                        on t.UserId=u.Id
			                                                            join Org o
			                                                        on t.OrgId=o.Id
			                                                        ) as t
			                                                        where t.num=1 and (t.BizCode=@DeptType or @DeptType is null) 
	                                                        ) t2
	                                                        on t1.JudgeId=t2.UserId and 
                                                                     (t2.OrgName like '%'+@OrgName+'%' or @OrgName is null) and 
                                                                    (u.Name like '%'+@UserName+'%' or @UserName is null)
                                                        ) as t
	                        ) as t1
	                        join (

                            select 
				                        Name,
				                        OrgName,XRank,
				                        综合
		                                   
	                        from (
		                        select u.Name,
				                        t2.OrgName,
		                                t2.XRank,
				                        综合=case when cnt=0 then 0 
								                        else 
								                        convert(decimal(18,1),(优秀*1+称职*0.8+基本称职*0.6+不称职*0.3)*100.0/cnt)
								                        end

		                                  
			                        from (
				                        select JudgeId,(select count(*) from Answer a where a.JudgeId=a1.JudgeId and State='已提交' and a.PlanId not in (select id from [Plan] where PlanName like '%s%')) as cnt,
					                        sum(case Q6 when 10 then 1 else 0 end) as '优秀', 
					                        sum(case Q6 when 11 then 1 else 0 end) as '称职', 
					                        sum(case Q6 when 12 then 1 else 0 end) as '基本称职', 
					                        sum(case Q6 when 13 then 1 else 0 end) as '不称职'
				                        from Answer as a1
				                        where State='已提交' and a1.PlanId not in (select id from [Plan] where PlanName like '%s%') and year(Optime)=@EvaluateYear and (JudgeId in(
                                select distinct a.FirstId from Relevance a  join [Role] b
                                on a.SecondId=b.Id
                                where a.[Key]='UserRole' and b.Name=@role

                            ) or @role is null) 
				                        group by JudgeId 
		                        ) as t1 left join [User] as u
		                        on t1.JudgeId=u.Id
			                        join (
    
				                        select UserId,OrgName,BizCode,XRank from (
				                        select  ROW_NUMBER() over(partition by UserId order by num) as num,UserId,o.Name as OrgName,o.BizCode,u.XRank from (
					                        select ROW_NUMBER() over(partition by SecondId order by OperateTime desc) as num,FirstId as UserId,SecondId as OrgId from Relevance where [Key]='UserOrg'
				                        ) as t
					                        join [User] u
				                        on t.UserId=u.Id
					                        join Org o
				                        on t.OrgId=o.Id
				                        ) as t
				                        where t.num=1 and (t.BizCode=@DeptType or @DeptType is null)  
		                        ) t2
		                        on t1.JudgeId=t2.UserId and 
                                                                    (t2.OrgName like '%'+@OrgName+'%' or @OrgName is null) and 
                                                                    (u.Name like '%'+@UserName+'%' or @UserName is null)
	                        ) as t
                        ) as t2
                        on t1.Name=t2.Name and t1.OrgName=t2.OrgName";
            var rows = Repository.ExecuteQuerySql<EvaluationTotalscoreOutput>(sql, input.ToParameters()).ToList();
            return new
            {
                rows = rows,
                total = 10000
            };
        }


        public List<EvaluationTotalscoreOutput> get_totalscore_data_all(EvaluationscoreQueryInput input)
        {

            string sql = @"select ROW_NUMBER() over(order by t1.要素*0.6+t2.综合*0.4 desc) as Num,Id,t1.Name,t1.OrgName,isnull(t1.XRank,'') as XRank,t1.要素,t2.综合,convert(decimal(18,1),t1.要素*0.6+t2.综合*0.4) as 总分,优秀率,总票数 from (
                           select 
                                                                    Name,
		                                                            OrgName,XRank,
		                                                            要素,Id,总票数
                                                        from (
	                                                        select u.Id,u.Name,
		                                                            t2.OrgName,
																	t2.XRank,总票数,
		                                                            要素=convert(decimal(18,1),case when (德_好+德_中+德_差)=0 or 
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
						                                                        end)
	                                                            from (
			                                                        select JudgeId,count(*) 总票数,
				                                                        sum(case Q1 when 10 then 1 else 0 end) as '德_好', 
				                                                        sum(case Q1 when 11 then 1 else 0 end) as '德_中', 
				                                                        sum(case Q1 when 12 then 1 else 0 end) as '德_差', 

				                                                        sum(case Q2 when 10 then 1 else 0 end) as '能_好', 
				                                                        sum(case Q2 when 11 then 1 else 0 end) as '能_中', 
				                                                        sum(case Q2 when 12 then 1 else 0 end) as '能_差', 

				                                                        sum(case Q3 when 10 then 1 else 0 end) as '勤_好', 
				                                                        sum(case Q3 when 11 then 1 else 0 end) as '勤_中', 
				                                                        sum(case Q3 when 12 then 1 else 0 end) as '勤_差', 

				                                                        sum(case Q4 when 10 then 1 else 0 end) as '绩_好', 
				                                                        sum(case Q4 when 11 then 1 else 0 end) as '绩_中', 
				                                                        sum(case Q4 when 12 then 1 else 0 end) as '绩_差', 

				                                                        sum(case Q5 when 10 then 1 else 0 end) as '廉_好', 
				                                                        sum(case Q5 when 11 then 1 else 0 end) as '廉_中', 
				                                                        sum(case Q5 when 12 then 1 else 0 end) as '廉_差'
			                                                        from Answer a 
			                                                        where State='已提交' and NOT exists (select id from [Plan] where id=a.PlanId and PlanName like '%s%') and year(Optime)=@EvaluateYear and (JudgeId in(
                                select distinct a.FirstId from Relevance a  join [Role] b
                                on a.SecondId=b.Id
                                where a.[Key]='UserRole' and b.Name=@role

                            ) or @role is null) 
			                                                        group by JudgeId
	                                                        ) as t1 left join [User] as u
	                                                        on t1.JudgeId=u.Id
	                                                            join (
    
			                                                        select UserId,OrgName,BizCode,XRank from (
			                                                        select  ROW_NUMBER() over(partition by UserId order by num) as num,UserId,o.Name as OrgName,o.BizCode,u.XRank from (
			                                                            select ROW_NUMBER() over(partition by SecondId order by OperateTime desc) as num,FirstId as UserId,SecondId as OrgId from Relevance where [Key]='UserOrg'
			                                                        ) as t
			                                                            join [User] u
			                                                        on t.UserId=u.Id
			                                                            join Org o
			                                                        on t.OrgId=o.Id
			                                                        ) as t
			                                                        where t.num=1 and (t.BizCode=@DeptType or @DeptType is null)
	                                                        ) t2
	                                                        on t1.JudgeId=t2.UserId
                                                        ) as t
	                        ) as t1
	                        join (

                            select 
				                        Name,
				                        OrgName,XRank,
				                        综合,优秀率
		                                   
	                        from (
		                        select u.Name,
				                        t2.OrgName,
		                                t2.XRank,
				                        综合=case when cnt=0 then 0 
								                        else 
								                        convert(decimal(18,1),(优秀*1+称职*0.8+基本称职*0.6+不称职*0.3)*100.0/cnt)
								                        end,优秀率=convert(decimal(18,1),(convert(decimal(18,1),(优秀+称职))/convert(decimal(18,1),(优秀+称职+基本称职+不称职))*100))

		                                  
			                        from (
				                        select JudgeId,(select count(*) from Answer a where a.JudgeId=a1.JudgeId and State='已提交' and a.PlanId not in (select id from [Plan] where PlanName like '%s%')) as cnt,
					                        sum(case Q6 when 10 then 1 else 0 end) as '优秀', 
					                        sum(case Q6 when 11 then 1 else 0 end) as '称职', 
					                        sum(case Q6 when 12 then 1 else 0 end) as '基本称职', 
					                        sum(case Q6 when 13 then 1 else 0 end) as '不称职'
				                        from Answer as a1
				                        where State='已提交' and NOT exists (select id from [Plan] where id=a1.PlanId and PlanName like '%s%') and year(Optime)=@EvaluateYear and (JudgeId in(
                                select distinct a.FirstId from Relevance a  join [Role] b
                                on a.SecondId=b.Id
                                where a.[Key]='UserRole' and b.Name=@role

                            ) or @role is null) 
				                        group by JudgeId 
		                        ) as t1 left join [User] as u
		                        on t1.JudgeId=u.Id
			                        join (
    
				                        select UserId,OrgName,BizCode,XRank from (
				                        select  ROW_NUMBER() over(partition by UserId order by num) as num,UserId,o.Name as OrgName,o.BizCode,u.XRank from (
					                        select ROW_NUMBER() over(partition by SecondId order by OperateTime desc) as num,FirstId as UserId,SecondId as OrgId from Relevance where [Key]='UserOrg'
				                        ) as t
					                        join [User] u
				                        on t.UserId=u.Id
					                        join Org o
				                        on t.OrgId=o.Id
				                        ) as t
				                        where t.num=1 and (t.BizCode=@DeptType or @DeptType is null)
		                        ) t2
		                        on t1.JudgeId=t2.UserId
	                        ) as t
                        ) as t2
                        on t1.Name=t2.Name and t1.OrgName=t2.OrgName";
            
            List<EvaluationTotalscoreOutput> li = Repository.ExecuteQuerySql<EvaluationTotalscoreOutput>(sql, input.ToParameters()).ToList();

            return li;

        }

        #endregion
    }
}
