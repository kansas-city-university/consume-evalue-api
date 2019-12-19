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

                // User - all
                //var institutionPSJ = new InstitutionApi(clientId, password, subUnitId, "https://api.e-value.net/Institution_1_0.cfc");
                //var userListPSJ = institutionPSJ.GetSubUnitUsers(1,3);
                //userListPSJ = institutionPSJ.GetSubUnitUsers(1, 4);
                //userListPSJ = institutionPSJ.GetSubUnitUsers(1, 5);
                //userListPSJ = institutionPSJ.GetSubUnitUsers(1, 6);
                //userListPSJ = institutionPSJ.GetSubUnitUsers(1, 29);
                //userListPSJ = institutionPSJ.GetSubUnitUsers(1, 81);
                //userListPSJ = institutionPSJ.GetSubUnitUsers(1, 241);

                //TestEvaluationImportRoutine();

                //DoAllSchedules
                //DoAllSchedules(clientId, password, subUnitId);

                // Use this to only test a single evaluation - uncomment for fixing this.
                DoSingleEvaluationOnly(clientId, password, subUnitId);

                // Use this to only test evaluations - uncomment for fixing this.
                DoAllEvaluations(clientId, password, subUnitId);

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

            var evaluationItems = eValueEval.GetResponses("359011", new DateTime(2019, 7, 1), new DateTime(2019, 9, 28), 1);
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} : SubjectUserId {evaluationItem.SubjectUserId} : EvaluatorUserId {evaluationItem.EvaluatorUserId} ");
                if ((evaluationItem.SubjectUserId == 1229965) | (evaluationItem.EvaluatorUserId == 1229965))
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }


            evaluationItems = eValueEval.GetResponses("359011", new DateTime(2019, 7, 1), new DateTime(2019, 9, 28), 1, 275194, DateTime.Now.AddDays(-60));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.SubjectUserId == 1193817)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }


            evaluationItems = eValueEval.GetResponses("359011", new DateTime(2019, 7, 1), new DateTime(2019, 9, 28), 1, 280052, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.SubjectUserId == 1193817)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }

            evaluationItems = eValueEval.GetResponses("359011", new DateTime(2019, 7, 1), new DateTime(2019, 9, 28), 1, 245552, DateTime.Now.AddDays(-30));
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.SubjectUserId == 1193817)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }






            // This call uses the subject user id in the parameter list - something new from EValue - yay!!
            evaluationItems = eValueEval.GetResponsesUsingSubjectId("359011", new DateTime(2019, 7, 1), new DateTime(2019, 8, 30), 1, 275194, 1229965);
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} Name: {evaluationItem.Name} Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.SubjectUserId == 1229965)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }


            /******************************      Figure out issues in Aug 2019 based on changes/additions to evaluation form types **************************************************/
            // Student: Desiree Albano: 1229965

            // 275194	Student Evaluation of Clerkship
            evaluationItems = eValueEval.GetResponsesUsingEvaluatorId("359011", new DateTime(2019, 7, 1), new DateTime(2019, 8, 30), 1, 275194, 1229965);
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} Name: {evaluationItem.Name} Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.EvaluatorUserId == 1229965)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }

            // 279811	Student Evaluation of Preceptor
            evaluationItems = eValueEval.GetResponsesUsingEvaluatorId("359011", new DateTime(2019, 7, 1), new DateTime(2019, 8, 30), 1, 279811, 1229965);
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} Name: {evaluationItem.Name} Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.EvaluatorUserId == 1229965)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }

            // 280052	Clinical Competency Assessment - better not get any!!!!!!! - this one was not valid - it was expired at the time
            evaluationItems = eValueEval.GetResponsesUsingEvaluatorId("359011", new DateTime(2019, 7, 1), new DateTime(2019, 8, 30), 1, 280052, 1229965);
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} Name: {evaluationItem.Name} Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.EvaluatorUserId == 1229965)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }

            // 284652	Clinical Competency Assessment - better not get any!!!!!!! - this one was valid at the time, but Desiree is not the evaluator
            evaluationItems = eValueEval.GetResponsesUsingEvaluatorId("359011", new DateTime(2019, 7, 1), new DateTime(2019, 8, 30), 1, 284652, 1229965);
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} Name: {evaluationItem.Name} Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.EvaluatorUserId == 1229965)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }

            // 246139	Student ECR MS4
            evaluationItems = eValueEval.GetResponsesUsingEvaluatorId("359011", new DateTime(2019, 7, 1), new DateTime(2019, 8, 30), 1, 246139, 1229965);
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} Name: {evaluationItem.Name} Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.EvaluatorUserId == 1229965)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }

            // 245552	Student ECR MS3
            evaluationItems = eValueEval.GetResponsesUsingEvaluatorId("359011", new DateTime(2019, 7, 1), new DateTime(2019, 8, 30), 1, 245552, 1229965);
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} Name: {evaluationItem.Name} Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.EvaluatorUserId == 1229965)
                {
                    Console.WriteLine("Here she is!!!!!");
                }
            }

            // 246405	Enrollment Verification
            evaluationItems = eValueEval.GetResponsesUsingEvaluatorId("359011", new DateTime(2019, 7, 1), new DateTime(2019, 8, 30), 1, 246405, 1229965);
            foreach (var evaluationItem in evaluationItems.EvaluationItems)
            {
                Console.WriteLine($"----- ---- Evaluation Item: {evaluationItem.SiteName} Name: {evaluationItem.Name} Activity: {evaluationItem.ActivityId}-{evaluationItem.Abbreviation} EIMnum: {evaluationItem.Eimnum} FormType: {evaluationItem.EvaluationFormTypeId} ");
                if (evaluationItem.EvaluatorUserId == 1229965)
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


        public static void TestEvaluationImportRoutine()
        {

            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string password = ConfigurationManager.AppSettings["ClientPassword"];
            string subUnitId = ConfigurationManager.AppSettings["SubUnitId"];

            const int completeStatusValue = 1;              // Always getting COMPLETE evaluations

            try
            {

                string evaluationItemApiUrl = "https://api.e-value.net/Evaluation_1_0b.cfc";
                var evaluationItemApi = new EvaluationApi(clientId, password, subUnitId, evaluationItemApiUrl);

                // now get the evaluation items for this schedule, status and user id
                var evaluationItems = evaluationItemApi.GetResponsesUsingSubjectId(359011.ToString(), new DateTime(2019, 7, 8), new DateTime(2019, 8, 2), 1, 275194, 1229965);
//                evaluationItems = evaluationItemApi.GetResponsesUsingSubjectId(activityId, scheduleBeginDate, scheduleEndDate, 1=StatusId, evaluationFormTypeId:, subjectUserId:)
                if (evaluationItems.Status)
                {
                    foreach (var evaluationItem in evaluationItems.EvaluationItems)
                    {
                        OutputRecordsBuffer outputRecordsBuffer = new OutputRecordsBuffer();

                        outputRecordsBuffer.ActivityId = evaluationItem.ActivityId;
                        outputRecordsBuffer.EvaluatorUserId = evaluationItem.EvaluatorUserId;

                        if ((evaluationItem.SubjectUserId ?? 0) == 0)
                        {
                            outputRecordsBuffer.SubjectUserId_IsNull = true;
                        }
                        else
                        {
                            outputRecordsBuffer.SubjectUserId = (int)evaluationItem.SubjectUserId;
                        }

                        outputRecordsBuffer.StatusId = evaluationItem.StatusId;
                        outputRecordsBuffer.SiteId = evaluationItem.SiteId;
                        outputRecordsBuffer.SiteName = evaluationItem.SiteName;
                        outputRecordsBuffer.Name = evaluationItem.Name;
                        outputRecordsBuffer.Abbreviation = evaluationItem.Abbreviation;
                        outputRecordsBuffer.Choice = evaluationItem.Choice;

                        if (!string.IsNullOrEmpty(evaluationItem.EssayText))
                        {
                            outputRecordsBuffer.EssayText = evaluationItem.EssayText;
                        }

                        outputRecordsBuffer.EvaluatorExternalId = evaluationItem.EvaluatorExternalId;
                        outputRecordsBuffer.EvaluatorExternalIdLabel = evaluationItem.EvaluatorExternalIdLabel;
                        outputRecordsBuffer.NumericAnswer = evaluationItem.NumericAnswer;
                        outputRecordsBuffer.IsConfidential = evaluationItem.IsConfidential;
                        outputRecordsBuffer.IsMandatory = evaluationItem.IsMandatory;

                        outputRecordsBuffer.QuestionId = evaluationItem.QuestionId;

                        outputRecordsBuffer.QuestionText = evaluationItem.QuestionText;
                        outputRecordsBuffer.QuestionTopic = evaluationItem.QuestionTopic;
                        outputRecordsBuffer.QuestionTypeId = evaluationItem.QuestionTypeId;
                        outputRecordsBuffer.QuestionTypeDesc = evaluationItem.QuestionTypeDesc;
                        outputRecordsBuffer.RowId = evaluationItem.RowId;
                        outputRecordsBuffer.SortOrder = evaluationItem.SortOrder;
                        outputRecordsBuffer.SubjectExternalId = evaluationItem.SubjectExternalId;
                        outputRecordsBuffer.SubjectExternalIdLabel = evaluationItem.SubjectExternalIdLabel;
                        outputRecordsBuffer.SubjectLegacyExternalId = evaluationItem.SubjectLegacyExternalId;
                        outputRecordsBuffer.EvaluatorLegacyExternalId = evaluationItem.EvaluatorLegacyExternalId;
                        outputRecordsBuffer.Eimnum = evaluationItem.Eimnum;
                        outputRecordsBuffer.EvaluationRequestId = evaluationItem.EvaluationRequestId;
                        outputRecordsBuffer.EvaluationFormTypeId = evaluationItem.EvaluationFormTypeId;
                        outputRecordsBuffer.TimeFrameId = evaluationItem.TimeFrameId;
                        outputRecordsBuffer.TimeFrameLabel = evaluationItem.TimeFrameLabel;

                    }
                }
            }
            catch (TimeoutException ex)
            {

                //if (!EventLog.SourceExists(eventSource))
                //{
                //    EventSourceCreationData mySourceData = new EventSourceCreationData(eventSource, eventLog);
                //    EventLog.CreateEventSource(mySourceData);
                //}

                //EventLog.WriteEntry(eventSource,string.Format("Timeout Exception. Message: {0}; ActivityId: {1}; Form Type id: {2}", ex.Message, Row.ActivityId, Row.EvaluationFormTypeId), EventLogEntryType.Error, 101, 1);

            }
            catch (System.Net.WebException ex)
            {

                //if (!EventLog.SourceExists(eventSource))
                //{
                //    EventSourceCreationData mySourceData = new EventSourceCreationData(eventSource, eventLog);
                //    EventLog.CreateEventSource(mySourceData);
                //}

                //EventLog.WriteEntry(eventSource, string.Format("System.Net.WebException. Message: {0}; ActivityId: {1}; Form Type id: {2}", ex.Message, Row.ActivityId, Row.EvaluationFormTypeId), EventLogEntryType.Error, 101, 1);

            }
            catch (Exception ex)
            {

                //if (!EventLog.SourceExists(eventSource))
                //{
                //    EventSourceCreationData mySourceData = new EventSourceCreationData(eventSource, eventLog);
                //    EventLog.CreateEventSource(mySourceData);
                //}

                //EventLog.WriteEntry(eventSource, string.Format("General Exception. Message: {0}; ActivityId: {1}; Form Type id: {2}", ex.Message, Row.ActivityId, Row.EvaluationFormTypeId), EventLogEntryType.Error, 101, 1);

                bool cancel;
                //ComponentMetaData.FireError(999, Variables.TaskName, ex.Message, string.Empty, 0, out cancel);
            }
        }

    }


    public class OutputRecordsBuffer
    {
        //public TYPE Type { get; set; }

        public int ActivityId { get; set; }
        public int EvaluatorUserId { get; set; }
        public int? SubjectUserId { get; set; }
        public bool SubjectUserId_IsNull { get; set; }

        public int StatusId { get; set; }
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Choice { get; set; }
        public string EssayText { get; set; }

        public string EvaluatorExternalId { get; set; }
        public string EvaluatorExternalIdLabel { get; set; }
        public string NumericAnswer { get; set; }
        public bool IsConfidential { get; set; }
        public bool IsMandatory { get; set; }
        public int? QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionTopic { get; set; }
        public int? QuestionTypeId { get; set; }
        public string QuestionTypeDesc { get; set; }
        public string RowId { get; set; }
        public int? SortOrder { get; set; }
        public string SubjectExternalId { get; set; }
        public string SubjectExternalIdLabel { get; set; }
        public string SubjectLegacyExternalId { get; set; }
        public string EvaluatorLegacyExternalId { get; set; }
        public int Eimnum { get; set; }
        public int EvaluationRequestId { get; set; }
        public int EvaluationFormTypeId { get; set; }
        public int TimeFrameId { get; set; }
        public string TimeFrameLabel { get; set; }

    }


}

