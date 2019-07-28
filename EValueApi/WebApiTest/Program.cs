using System;
using EValueApi;
using System.Configuration;
using System.Linq;
using EValueApi.Business;

namespace WebApiTest
{
    class Program
    {

        static void Main(string[] args)
        {
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string password = ConfigurationManager.AppSettings["ClientPassword"];
            string subUnitId = ConfigurationManager.AppSettings["SubUnitId"];

            if (args != null && args.Length > 0)
            {
                switch (args[0])
                {
                    case "personal-records":

                        new PersonalRecordsTest(clientId, password, subUnitId);
                        break;

                    default:

                        break;
                }
            }
            else
            {
                //DoAllSchedules
                DoAllSchedules(clientId, password, subUnitId);

                // Use this to only test a single evaluation - uncomment for fixing this.
                DoSingleEvaluationOnly(clientId, password, subUnitId);

                // Use this to only test evaluations - uncomment for fixing this.
                //DoAllEvaluations(clientId, password, subUnitId);

                // Use this to only test teams
                DoTeamOnly(clientId, password, subUnitId);

                // EvaluationActions
                var eValueEvaluationActions = new ScheduleApi(clientId, password, subUnitId, "https://test-api.e-value.net/Schedule_1_0b.cfc");

                foreach (var options in eValueEvaluationActions.GetEvaluationActions().EvaluationActions)
                {
                    Console.WriteLine($"(^&)(^&) Evaluation Actions: {options.EvaluationActionId} {options.Description} ");
                }


                // Personal Records Options
                var eValuePersonalRecordOptions = new PersonalRecordOptionApi(clientId, password, subUnitId, "https://test-api.e-value.net/IandCOptions_1_0.cfc");

                foreach (var options in eValuePersonalRecordOptions.GetAllRequirementOptions().RequirementOptions)
                {
                    Console.WriteLine($"~!~!~! Personal Records Option: {options.RequirementId} {options.RequirementLabel} ");
                }

                foreach (var options in eValuePersonalRecordOptions.GetAllRequirementStatusOptions().RequirementStatusOptions)
                {
                    Console.WriteLine($"~!~!~!~!~! Personal Records Status Option: {options.StatusId} {options.StatusLabel} ");
                }

                foreach (var options in eValuePersonalRecordOptions.GetAllRequirementTypeOptions().RequirementTypeOptions)
                {
                    Console.WriteLine($"~!~!~!~!~!~!~! Personal Records Type Option: {options.TypeId} {options.TypeLabel} ");
                }

                // Statuses
                var eValuePeopleGroup = new PeopleGroupApi(clientId, password, subUnitId, "https://test-api.e-value.net/PeopleGroup_1_0.cfc");

                foreach (var groups in eValuePeopleGroup.GetAll().PeopleGroups)
                {
                    Console.WriteLine($"%%%% People Groups: {groups.GroupId} {groups.Name} ");

                    var peopleInGroup = eValuePeopleGroup.GetUsers(groups.GroupId.ToString());

                    foreach (var institutionUser in peopleInGroup.IntitutionUsers ?? Enumerable.Empty<InstitutionUser>())
                    {
                        Console.WriteLine($"%%%% %%%% People In the Group: {institutionUser.UserId} {institutionUser.FirstName} {institutionUser.LastName} ");
                    }
                }

                // Statuses
                var eValueStatus = new StatusApi(clientId, password, subUnitId, "https://test-api.e-value.net/Status_1_0.cfc");

                foreach (var status in eValueStatus.GetAll().Statuses)
                {
                    Console.WriteLine($"@@@@ Statuses: {status.StatusId} {status.Label} ");
                }

                // Ranks
                var eValueRanks = new RankApi(clientId, password, subUnitId, "https://api.e-value.net/Rank_1_0.cfc");

                foreach (var rank in eValueRanks.GetAll().Ranks)
                {
                    Console.WriteLine($"==== Ranks: {rank.RankId} {rank.Label} ");
                }

                // Roles
                var eValueRoles = new RoleApi(clientId, password, subUnitId, "https://api.e-value.net/Role_1_0.cfc");

                foreach (var role in eValueRoles.GetAll().Roles)
                {
                    Console.WriteLine($"++++ Roles: {role.RoleId} {role.Label} ");
                }

                // Schedules - used later in User info
                var eValueShedules = new ScheduleApi(clientId, password, subUnitId, "https://test-api.e-value.net/Schedule_1_0b.cfc");

                // Evaluations - used later in User info
                var eValueEval = new EvaluationApi(clientId, password, subUnitId, "https://test-api.e-value.net/Evaluation_1_0b.cfc");

                // Activities
                var eValueActivities = new ActivityApi(clientId, password, subUnitId, "https://test-api.e-value.net/Activity_1_0b.cfc");
                var activityMonkey = eValueActivities.GetAllActivities();

                // Teams - are a child of an activity - must be part of an activity
                var eValueTeams = new TeamApi(clientId, password, subUnitId, "https://test-api.e-value.net/Team_1_0.cfc");
                foreach (var activity in activityMonkey.Activities)
                {
                    Console.WriteLine($"~~~~ Activities: {activity.ActivityId} {activity.Abbreviation} {activity.Name} {activity.SiteId} ");
                    var teamForActivity = eValueTeams.GetAllTeams(activity.ActivityId.ToString());
                }

                // Time Frames
                var eValueTimeFrame = new TimeFrameApi(clientId, password, subUnitId, "https://test-api.e-value.net/TimeFrame_1_0.cfc");
                var timeFrameInfo = eValueTimeFrame.GetAllTimeFrames(DateTime.MinValue, DateTime.MaxValue);

                foreach (var timeFrame in timeFrameInfo.TimeFrames)
                {
                    var frame = eValueTimeFrame.Get(timeFrame.TimeFrameId);

                    Console.WriteLine($">>>> Time Frames: {frame.TimeFrame.TimeFrameId} {frame.TimeFrame.TimeFrameLabel} {frame.TimeFrame.TimeFrameBegin} {frame.TimeFrame.TimeFrameEnd} ");
                }

                // User - individual
                var eValueUser = new UserApi(clientId, password, subUnitId, "https://test-api.e-value.net/User_1_0b.cfc");
                var user = eValueUser.Get("79491668");

                // Sites - all
                var eValueSiteApi = new SiteApi(clientId, password, subUnitId, "https://test-api.e-value.net/Site_1_0.cfc");
                var sites = eValueSiteApi.GetAllSites();

                foreach (var site in sites.Sites)
                {
                    Console.WriteLine($"^^^^^ sites: {site.SiteId} {site.SiteName} ");
                }

                // User - all
                var institution = new InstitutionApi(clientId, password, subUnitId, "https://test-api.e-value.net/Institution_1_0.cfc");
                var userList = institution.GetSubUnitUsers(1);

                // *************************************
                // User and Personal Records
                // *************************************
                var eValuePersonalRecords = new PersonalRecordApi(clientId, password, subUnitId, "https://test-api.e-value.net/IandC_1_0.cfc");
                var record = eValuePersonalRecords.GetUserPersonalRecords("1193570");       // Haley Artz - should have a lot of them - test this in production or on Tuesday after transfer has occurred.

                foreach (var monkeyUser in userList.IntitutionUsers)
                {


                    Console.WriteLine($"***************************");
                    Console.WriteLine($"User List Item: {monkeyUser.UserId}-{monkeyUser.FirstName} {monkeyUser.LastName} : {monkeyUser.RankLabel}");
                    Console.WriteLine($"***************************");
                    Console.WriteLine(String.Empty);

                    // *************************************
                    // Get the personal records for the given user
                    // *************************************
                    var records = eValuePersonalRecords.GetUserPersonalRecords(monkeyUser.UserId.ToString());

                    foreach (var personalRecord in records.PeronalRecords)
                    {
                        Console.WriteLine($"##### ##### Personal Records: {personalRecord.RequirementId} {personalRecord.EventDate} {personalRecord.StatusId} ");
                    }

                    // *************************************
                    // Get the schedules for the given user
                    // *************************************
                    var schedule = eValueShedules.GetAll(monkeyUser.UserId.ToString());

                    foreach (var scheduleItem in schedule.Schedules)
                    {
                        Console.WriteLine($"----- Schedule Item: {scheduleItem.ActivityId}-{scheduleItem.BeginDate} {scheduleItem.EndDate} {scheduleItem}");
                    }

                    // *************************************
                    // Get the schedules for the given user in the year 2017 - this time to get the evaluations.
                    // *************************************
                    var scheduleByDate = eValueShedules.GetAll(new DateTime(2017, 1, 1), new DateTime(2017, 12, 31),
                        monkeyUser.UserId.ToString());

                    int[] evalStatusList = { -1, 0, 1, 2, 3, 4, 5, 6, 7, 8 };

                    foreach (var scheduleItem in scheduleByDate.Schedules)
                    {

                        foreach (int statusValue in evalStatusList)
                        {

                            // Updated in the last 30 days
                            var evaluationItems = eValueEval.GetResponses(scheduleItem.ActivityId.ToString(), (DateTime)scheduleItem.BeginDate, (DateTime)scheduleItem.EndDate, statusValue, DateTime.Now.AddDays(-30));

                            foreach (var evaluationItem in evaluationItems.EvaluationItems)
                            {
                                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId}");
                            }

                        }

                    }
                }
            }
        }


        static void DoSingleEvaluationOnly(string clientId, string password, string subUnitId)
        {

            // Evaluations - Use the test one I have there  need to debug some issues with this.
            var eValueEval = new EvaluationApi(clientId, password, subUnitId, "https://api.e-value.net/Evaluation_1_0b.cfc");


            // This call uses the subject user id in the parameter list - something new from EValue - yay!!
            var evaluationItems = eValueEval.GetResponsesUsingSubjectId("359140", new DateTime(2017, 1, 1), new DateTime(2018, 7, 31), 1, 263557, 1193954);
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} Name: {evaluationItem.Name} Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.SubjectUserId == 1193817)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }

            // This call uses the subject user id in the parameter list - something new from EValue - yay!!
            evaluationItems = eValueEval.GetResponsesUsingEvaluatorId("365729", new DateTime(2017, 1, 1), new DateTime(2018, 7, 31), 1, 246405, 1193575);
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {245328}");
                if (evaluationItem.SubjectUserId == 1193817)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }

            evaluationItems = eValueEval.GetResponses("358970", new DateTime(2017, 1, 1), new DateTime(2018, 4, 30), 1, 245328, DateTime.Now.AddDays(-7));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {245328}");
                if (evaluationItem.SubjectUserId == 1193817)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }


            evaluationItems = eValueEval.GetResponses("358970", new DateTime(2017, 9, 14), new DateTime(2017, 10, 16), 1, 245328, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {245328}");
            }



            // Enrollment Verification - PEDS301 (359046)
            evaluationItems = eValueEval.GetResponses("359046", new DateTime(2017, 9, 1), new DateTime(2017, 10, 30), 1, 246405, DateTime.Now.AddDays(-20));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {245328}");
            }

            // Compentency Assessments - PEDS301 (359046)
            evaluationItems = eValueEval.GetResponses("359046", new DateTime(2017, 9, 1), new DateTime(2017, 10, 30), 1, 245328, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {245328}");
            }

            // Student ECR MS4 - IMED302 (358971)
            evaluationItems = eValueEval.GetResponses("358971", new DateTime(2018, 1, 1), new DateTime(2018, 1, 31), 1, 246139, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {245328}");
            }

            // Compentency Assessments - IMED302 (358971)
            evaluationItems = eValueEval.GetResponses("358971", new DateTime(2018, 1, 1), new DateTime(2018, 1, 31), 1, 245328, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems.Where(xxx => xxx.SubjectUserId == 1193826))
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {245328}");
            }

            evaluationItems = eValueEval.GetResponses("358777", new DateTime(2017, 9, 20), new DateTime(2017, 10, 30), 1, 244774, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {244774}");
            }

            evaluationItems = eValueEval.GetResponses("358777", new DateTime(2017, 1, 1), new DateTime(2017, 12, 31), 1, 244775, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {244775}");
            }


            evaluationItems = eValueEval.GetResponses("358777", new DateTime(2017, 1, 1), new DateTime(2017, 12, 31), 1, 245552, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {245552}");
            }

            evaluationItems = eValueEval.GetResponses("358777", new DateTime(2017, 1, 1), new DateTime(2017, 12, 31), 1, 246139, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {246139}");
            }

            evaluationItems = eValueEval.GetResponses("358777", new DateTime(2017, 1, 1), new DateTime(2017, 12, 31), 1, 246405, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId} {evaluationItem.Eimnum} Type {246405}");
            }



            evaluationItems = eValueEval.GetResponses("358777", new DateTime(2017, 1, 1), new DateTime(2017, 12, 31), 1, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId}");
            }

            evaluationItems = eValueEval.GetResponses("358948", new DateTime(2017, 3, 1), new DateTime(2017, 5, 1), 1, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} {evaluationItem.Name} {evaluationItem.ActivityId}");
            }

        }

        static void DoAllEvaluations(string clientId, string password, string subUnitId)
        {

            // Schedules - used later in User info
            var eValueShedules = new ScheduleApi(clientId, password, subUnitId, "https://api.e-value.net/Schedule_1_0b.cfc");

            // Evaluations - Use the test one I have there  need to debug some issues with this.
            var eValueEval = new EvaluationApi(clientId, password, subUnitId, "https://api.e-value.net/Evaluation_1_0b.cfc");

            // User - all
            var institution = new InstitutionApi(clientId, password, subUnitId, "https://api.e-value.net/Institution_1_0.cfc");
            var userList = institution.GetSubUnitUsers(2);


            foreach (var monkeyUser in userList.IntitutionUsers)
            {

                Console.WriteLine($"***************************");
                Console.WriteLine(
                    $"User List Item: {monkeyUser.UserId}-{monkeyUser.FirstName} {monkeyUser.LastName} : {monkeyUser.RankLabel}");
                Console.WriteLine($"***************************");
                Console.WriteLine(String.Empty);

                // *************************************
                // Get the schedules for the given user in the year 2017 - this time to get the evaluations.
                // *************************************
                var scheduleByDate = eValueShedules.GetAll(new DateTime(2017, 1, 1), new DateTime(2017, 12, 31),
                    monkeyUser.UserId.ToString());

                int[] evalStatusList = {1};

                if (scheduleByDate.Schedules != null)
                {
                    foreach (var scheduleItem in scheduleByDate.Schedules)
                    {

                        foreach (int statusValue in evalStatusList)
                        {

                            var evaluationItems = eValueEval.GetResponses(scheduleItem.ActivityId.ToString(), (DateTime) scheduleItem.BeginDate, (DateTime) scheduleItem.EndDate, statusValue, DateTime.Now.AddDays(-30));

                            foreach (var evaluationItem in evaluationItems.EvaluationItems)
                            {
                                Console.WriteLine(
                                    $"----- ---- Evaluation Item: Site:{evaluationItem.SiteName} Name:{evaluationItem.Name} ActivityId:{evaluationItem.ActivityId} Subject:{evaluationItem.SubjectUserId}");
                            }

                        }

                    }
                }
            }
        }

        static void DoTeamOnly(string clientId, string password, string subUnitId)
        {
            // Activities
            var eValueActivities = new ActivityApi(clientId, password, subUnitId, "https://api.e-value.net/Activity_1_0b.cfc");
            var activityMonkey = eValueActivities.GetAllActivities();

            // Teams - are a child of an activity - must be part of an activity
            var eValueTeams = new TeamApi(clientId, password, subUnitId, "https://api.e-value.net/Team_1_0.cfc");
            foreach (var activity in activityMonkey.Activities)
            {
                Console.WriteLine($"~~~~ Activities: {activity.ActivityId} {activity.Abbreviation} {activity.Name} {activity.SiteId} ");
                var teamForActivity = eValueTeams.GetAllTeams(activity.ActivityId.ToString());

                foreach (var team in teamForActivity.Teams)
                {
                    Console.WriteLine($"~~~~ ~~~~ Team : {team.TeamId} {team.Name} ActivityId: {team.ActivityId} ");
                }
            }
        }

        static void DoAllSchedules(string clientId, string password, string subUnitId)
        {

            // User - all
            var institution = new InstitutionApi(clientId, password, subUnitId, "https://api.e-value.net/Institution_1_0.cfc");

            // Active Admins
            var userList2 = institution.GetSubUnitUsers(1, 3);
            var userList3 = institution.GetSubUnitUsers(1, 4);
            var userList4 = institution.GetSubUnitUsers(1, 5);
            var userList5 = institution.GetSubUnitUsers(1, 6);
            var userList6 = institution.GetSubUnitUsers(1, 29);
            var userList7 = institution.GetSubUnitUsers(1, 81);
            var userList8 = institution.GetSubUnitUsers(1, 241);

            var userList = institution.GetSubUnitUsers(1);

            // Schedules - used later in User info
            var eValueShedules = new ScheduleApi(clientId, password, subUnitId, "https://test-api.e-value.net/Schedule_1_0b.cfc");

            foreach (var monkeyUser in userList.IntitutionUsers)
            {

                Console.WriteLine($"-- Clinical User: {monkeyUser.UserId}-{monkeyUser.FirstName} {monkeyUser.LastName} {monkeyUser.RankLabel}");


                // *************************************
                // Get the schedules for the given user
                // *************************************
                var schedule = eValueShedules.GetAll(monkeyUser.UserId.ToString());

                foreach (var scheduleItem in schedule.Schedules)
                {
                    Console.WriteLine($"----- Schedule Item: {scheduleItem.ActivityId}-{scheduleItem.BeginDate} {scheduleItem.EndDate} {scheduleItem}");
                }
            }
        }

    }
}

