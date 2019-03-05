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
    public class AnnualExaminationRegistrationApp : BaseApp<AnnualExaminationRegistration>
    {


        public object page(int limit, int offset, AnnualExaminationRegistrationQueryInput input)
        {

            offset += 1;
            string sql = $@"select top {limit} * from(
                              select row_number() over(order by a.created) as num,a.*,b.Name as OrgName,c.Name as UserName 
                                                       from AnnualExaminationRegistration a left join Org b
                                                       on a.OrgId=b.Id
                                                       left join [User] c
                                                       on a.UserId=c.Id 
                                                       where a.Creator=@Creator 
                                                       and (year(a.Created)=@EvaluateYear or @EvaluateYear is null)

                            ) as t where num > ({limit}*({offset}-1))";

            var rows = Repository.ExecuteQuerySql<AnnualExaminationRegistrationOutput>(sql, input.ToParameters()).ToList();

            sql = @"select count(*) from AnnualExaminationRegistration a left join [User] c  on a.UserId=c.Id 
                    left join Org b
                    on a.OrgId=b.Id
                    where a.Creator=@Creator 
                    and (year(a.Created)=@EvaluateYear or @EvaluateYear is null)";

            int total = Repository.ExecuteQuerySql<int>(sql, input.ToParameters()).FirstOrDefault();

            return new
            {
                total = total,
                rows = rows
            };
        }



        public AnnualExaminationRegistrationOutput get(string id)
        {

            var model = this.Get(id);
            if (model == null)
            {
                return new AnnualExaminationRegistrationOutput();
            }
            else
            {
                AnnualExaminationRegistrationOutput result = model.MapTo<AnnualExaminationRegistrationOutput>();
              
                if (!string.IsNullOrEmpty(result.OrgId))
                {
                    result.OrgName = UnitWork.Find<Org>(w => w.Id.Equals(result.OrgId))?.FirstOrDefault()?.Name;
                }
                return result;
            }
        }

        public bool Exists(AnnualExaminationRegistration obj)
        {

            return Repository.Find(w =>
                          w.UserId.Equals(obj.UserId) &&
                          w.OrgId.Equals(obj.OrgId) &&
                          w.Created.Value.Year.Equals(DateTime.Now.Year)).Count() > 0;

        }




        public string Add(AnnualExaminationRegistration obj)
        {
            return Repository.AddAndReturnId(obj);
        }
        
        public void Update(AnnualExaminationRegistration obj)
        {
            UnitWork.Update<AnnualExaminationRegistration>(u => u.Id == obj.Id, u => new AnnualExaminationRegistration
            {
                //todo:要修改的字段赋值
                Name = obj.Name,
                UserId = obj.UserId,
                Birthday = obj.Birthday,
                Conclusion = obj.Conclusion,
                DegreeEdu = obj.DegreeEdu,
                EvaluationCount = obj.EvaluationCount,
                FactorScore = obj.FactorScore,
                HRAdvice = obj.HRAdvice,
                HRTime = obj.HRTime,
                IsPenalty = obj.IsPenalty,
                IsReward = obj.IsReward,
                Nation = obj.Nation,
                Notes = obj.Notes,
                Officetime = obj.Officetime,
                Officialadvice = obj.Officialadvice,
                OfficialName = obj.OfficialName,
                OfficialTime = obj.OfficialTime,
                OrgId = obj.OrgId,
                Penalty = obj.Penalty,
                PenaltyReasons = obj.PenaltyReasons,
                Politicalaffiliation = obj.Politicalaffiliation,
                Position = obj.Position,
                Rank = obj.Rank,
                Rate = obj.Rate,
                RegistrationTime = obj.RegistrationTime,
                Reward = obj.Reward,
                RewardReasons = obj.RewardReasons,
                Scope = obj.Scope,
                Sex = obj.Sex,
                UnitAdvice = obj.UnitAdvice,
                UnitTime = obj.UnitTime,
                Updated = DateTime.Now,
                PenaltyTime = obj.PenaltyTime,
                RewardTime = obj.RewardTime
            });

        }

    }
}