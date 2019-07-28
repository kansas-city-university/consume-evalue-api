using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using EValueApi;
using EValueApi.Business;
using EValueApi.Communication;
using EValueApi.Communication.PersonalRecords;
using EValueApi.EValuePersonalRecordsApi;

namespace EValueApi.SSISComponents
{
    public enum ExpirationSettingType
    {
        NA,
        Months_11,
        Years_1,
        Years_5,
        Years_10,
    }

    public static class LogProcessingResult
    {
        public const string STARTED = "Started";
        public const string COMPLETED = "Completed";
        public const string FAILED = "Failed";
    }

    public static class RowProcessingResult
    {
        public const string STARTED = "Started";
        public const string SKIPPED_NO_VALUE_IN_INPUT_COLUMN = "Skipped (no EvalueId value in input file)";
        public const string SKIPPED_ERROR_RETRIEVING_USER_PERSONAL_RECORDS = "Skipped (error occurred retrieving personal records from E*Value)";
        public const string FINISHED = "Finished processing all columns";
    }

    public static class ColumnProcessingResult
    {
        public const string STARTED = "Started";
        public const string SKIPPED_NO_VALUE_IN_INPUT_COLUMN = "Skipped (no input column value)";
        public const string SKIPPED_NO_CHANGES_DETECTED = "Skipped (no changes detected)";
        public const string EVALUE_UPDATE_SUCCESS = "E*Value update succeeded";
        public const string EVALUE_UPDATE_FAILED = "E*Value update failed";
        public const string FAILED_EXCEPTION_OCCURRED = "Failed (Exception occurred)";
        public const string FINISHED = "Finished";
    }

    public class RowProcessingResources
    {
        public int EvalueUserId { get; set; } = 0;
        public IList<PersonalRecord> ExistingEvalueRecordsForUser = new List<PersonalRecord>();
        public PersonalRecordsImportRow Inputs { get; set; } = new PersonalRecordsImportRow();
    }

    public class PersonalRecordsImportRow
    {
        public int? EvalueId { get; set; } = null;
        public string Student { get; set; } = string.Empty;
        public object HepBVaccine1 { get; set; } = null;
        public object HepBVaccine2 { get; set; } = null;
        public object HepBVaccine3 { get; set; } = null;
        public object PriorHepB1 { get; set; } = null;
        public object PriorHepB2 { get; set; } = null;
        public object PriorHepB3 { get; set; } = null;
        public object HepBTiter { get; set; } = null;
        public object HepBTiterResult { get; set; } = null;
        public object FluShot { get; set; } = null;
        public object MeningitisVaccine { get; set; } = null;
        public object MMR1 { get; set; } = null;
        public object MMR2 { get; set; } = null;
        public object MMRBooster { get; set; } = null;
        public object MeaslesTiter { get; set; } = null;
        public object MeaslesTiterResult { get; set; } = null;
        public object MumpsTiter { get; set; } = null;
        public object MumpsTiterResult { get; set; } = null;
        public object RubellaTiter { get; set; } = null;
        public object RubellaTiterResult { get; set; } = null;
        public object TDAPVaccine { get; set; } = null;
        public object PPD1 { get; set; } = null;
        public object PPD1Result { get; set; } = null;
        public object PPD2 { get; set; } = null;
        public object PPD2Result { get; set; } = null;
        public object ChestXRay { get; set; } = null;
        public object ChestXRayResult { get; set; } = null;
        public object TSpot { get; set; } = null;
        public object TSpotResult { get; set; } = null;
        public object QuatiferonGold { get; set; } = null;
        public object QuatiferonGoldResult { get; set; } = null;
        public object Varicella1 { get; set; } = null;
        public object Varicella2 { get; set; } = null;
        public object VaricellaTiter { get; set; } = null;
        public object VaricellaTiterResult { get; set; } = null;
        public object IPV1 { get; set; } = null;
        public object IPV2 { get; set; } = null;
        public object IPV3 { get; set; } = null;
        public object OPV1 { get; set; } = null;
        public object OPV2 { get; set; } = null;
        public object OPV3 { get; set; } = null;
        public object PolioBooster { get; set; } = null;
        public object TBNote { get; set; } = null;
        public object BA { get; set; } = null;
        public object BB { get; set; } = null;
        public object BC { get; set; } = null;
    }

    public class PersonalRecordsImportComponent
    {
        private Hashtable _vars;
        private PersonalRecordApi _api;
        private Log _log;

        private RequirementTypeOptionsResponse _requirementTypeOptionsResponse;
        private RequirementOptionsResponse _requirementOptionsResponse;
        private RequirementStatusOptionsResponse _requirementStatusOptionsResponse;

        public PersonalRecordsImportComponent(Hashtable variables)
        {
            _vars = variables;
            _log = new SSISComponents.Log((int)_vars["BatchId"]);
            _api = new PersonalRecordApi(
                _vars["EValueApiClientId"].ToString(),
                _vars["EValueApiClientPassword"].ToString(),
                _vars["EValueApiSubUnitId"].ToString(),
                _vars["EValueApiPersonalRecordsEndpointUrl"].ToString());

            var optionsApi = new PersonalRecordOptionApi(
                _vars["EValueApiClientId"].ToString(),
                _vars["EValueApiClientPassword"].ToString(),
                _vars["EValueApiSubUnitId"].ToString(),
                _vars["EValueApiPersonalRecordsOptionsEndpointUrl"].ToString()
                );

            _log = new Log((int)_vars["BatchId"]);
            _log.Start();

            _requirementTypeOptionsResponse = optionsApi.GetAllRequirementTypeOptions();
            _requirementOptionsResponse = optionsApi.GetAllRequirementOptions();
            _requirementStatusOptionsResponse = optionsApi.GetAllRequirementStatusOptions();
        }

        public void ProcessRow(PersonalRecordsImportRow input)
        {
            _log.NewRow();
            _log.CurrentRow.Start();

            if (!input.EvalueId.HasValue)
            {
                _log.CurrentRow.ProcessingResult = RowProcessingResult.SKIPPED_NO_VALUE_IN_INPUT_COLUMN;
            }
            else
            {
                _log.CurrentRow.NewColumn("EValueId", input.EvalueId.Value);
                _log.CurrentRow.CurrentColumn.Start();

                var userRecordsResponse = _api.GetUserPersonalRecords(input.EvalueId.Value.ToString());

                _log.CurrentRow.CurrentColumn.Append(string.Format("{0} E*Value records found for User Id {1}", userRecordsResponse.PeronalRecords.Count, input.EvalueId.Value));
                _log.CurrentRow.CurrentColumn.ProcessingResult = ColumnProcessingResult.FINISHED;
                _log.CurrentRow.CurrentColumn.End();

                _log.CurrentRow.Attributes.Add("Student", input.Student);

                if (userRecordsResponse.Status)
                {
                    var userRecords = userRecordsResponse.PeronalRecords;

                    if (input.PriorHepB1 != null && input.PriorHepB2 != null && input.PriorHepB3 != null)
                    {
                        ProcessColumn(input.EvalueId.Value, "PriorHepB1", input.PriorHepB1, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Hepatitis_B_Series1_1, null, RequirementDurationType.Constants.One_time);
                        ProcessColumn(input.EvalueId.Value, "PriorHepB2", input.PriorHepB2, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Hepatitis_B_Series1_2, null, RequirementDurationType.Constants.One_time);
                        ProcessColumn(input.EvalueId.Value, "PriorHepB3", input.PriorHepB3, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Hepatitis_B_Series1_3, null, RequirementDurationType.Constants.One_time);

                        ProcessColumn(input.EvalueId.Value, "HepBVaccine1", input.HepBVaccine1, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Hepatitis_B_Series2_1, null, RequirementDurationType.Constants.One_time);
                        ProcessColumn(input.EvalueId.Value, "HepBVaccine2", input.HepBVaccine2, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Hepatitis_B_Series2_2, null, RequirementDurationType.Constants.One_time);
                        ProcessColumn(input.EvalueId.Value, "HepBVaccine3", input.HepBVaccine3, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Hepatitis_B_Series2_3, null, RequirementDurationType.Constants.One_time);
                    }
                    else
                    {
                        ProcessColumn(input.EvalueId.Value, "HepBVaccine1", input.HepBVaccine1, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Hepatitis_B_Series1_1, null, RequirementDurationType.Constants.One_time);
                        ProcessColumn(input.EvalueId.Value, "HepBVaccine2", input.HepBVaccine2, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Hepatitis_B_Series1_2, null, RequirementDurationType.Constants.One_time);
                        ProcessColumn(input.EvalueId.Value, "HepBVaccine3", input.HepBVaccine3, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Hepatitis_B_Series1_3, null, RequirementDurationType.Constants.One_time);
                    }

                    ProcessColumn(input.EvalueId.Value, "HepBTiter/HepBTiterResult", input.HepBTiter, input.HepBTiterResult, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Hepatitis_B_Surface_Antibody_Titer, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "FluShot", input.FluShot, null, ExpirationSettingType.Months_11, userRecords, RequirementTypeOption.Constants.Annual_Influenza, null, RequirementDurationType.Constants.Ongoing);
                    ProcessColumn(input.EvalueId.Value, "MeningitisVaccine", input.MeningitisVaccine, null, ExpirationSettingType.Years_5, userRecords, RequirementTypeOption.Constants.Meningococcal_Meningitis, null, RequirementDurationType.Constants.Ongoing);
                    ProcessColumn(input.EvalueId.Value, "MMR1", input.MMR1, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.MMR_Dose_1, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "MMR2", input.MMR2, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.MMR_Dose_2, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "MMRBooster", input.MMRBooster, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.MMR_Booster, null, RequirementDurationType.Constants.Not_required);
                    ProcessColumn(input.EvalueId.Value, "MeaslesTiter/MeaslesTiterResult", input.MeaslesTiter, input.MeaslesTiterResult, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.MMR_Measles_Titer, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "MumpsTiter/MumpsTiterResult", input.MumpsTiter, input.MumpsTiterResult, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.MMR_Mumps_Titer, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "RubellaTiter/RubellaTiterResult", input.RubellaTiter, input.RubellaTiterResult, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.MMR_Rubella_Titer, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "TDAP Vaccine", input.TDAPVaccine, null, ExpirationSettingType.Years_10, userRecords, RequirementTypeOption.Constants.Tdap_Vaccination, null, RequirementDurationType.Constants.Ongoing);
                    ProcessColumn(input.EvalueId.Value, "PPD 1", input.PPD1, input.PPD1Result, ExpirationSettingType.Years_1, userRecords, RequirementTypeOption.Constants.PPD1, null, RequirementDurationType.Constants.Ongoing);
                    ProcessColumn(input.EvalueId.Value, "PPD 2", input.PPD2, input.PPD2Result, ExpirationSettingType.Years_1, userRecords, RequirementTypeOption.Constants.PPD2, null, RequirementDurationType.Constants.Ongoing);
                    ProcessColumn(input.EvalueId.Value, "ChestXRay/ChestXRayResult", input.ChestXRay, input.ChestXRayResult, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.TB_Status__Chest_XRay, null, RequirementDurationType.Constants.Not_required);
                    ProcessColumn(input.EvalueId.Value, "TSpot/TSpotResult", input.TSpot, input.TSpotResult, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.TB_IGRA_Quantiferon_or_TSpot, null, RequirementDurationType.Constants.Not_required);
                    ProcessColumn(input.EvalueId.Value, "QuatiferonGold/QuatiferonGoldResult", input.QuatiferonGold, input.QuatiferonGoldResult, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.TB_IGRA_Quantiferon_or_TSpot, null, RequirementDurationType.Constants.Not_required);
                    ProcessColumn(input.EvalueId.Value, "Varicella1", input.Varicella1, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Varicella_1, null, RequirementDurationType.Constants.Not_required);
                    ProcessColumn(input.EvalueId.Value, "Varicella2", input.Varicella2, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Varicella_2, null, RequirementDurationType.Constants.Not_required);
                    ProcessColumn(input.EvalueId.Value, "VaricellaTiter/VaricellaTiterResult", input.VaricellaTiter, input.VaricellaTiterResult, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Varicella_Titer, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "IPV1", input.IPV1, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Polio_OPV_IPV_1, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "IPV2", input.IPV2, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Polio_OPV_IPV_2, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "IPV3", input.IPV3, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Polio_OPV_IPV_3, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "OPV1", input.OPV1, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Polio_OPV_IPV_1, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "OPV2", input.OPV2, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Polio_OPV_IPV_2, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "OPV3", input.OPV3, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Polio_OPV_IPV_3, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "PolioBooster", input.PolioBooster, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.Polio_Booster, null, RequirementDurationType.Constants.One_time);
                    ProcessColumn(input.EvalueId.Value, "TBNote", input.TBNote, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.TB_INH_treatment, null, RequirementDurationType.Constants.Not_required);

                    ProcessColumn(input.EvalueId.Value, "BA", input.BA, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.BA, null, RequirementDurationType.Constants.Not_required);
                    ProcessColumn(input.EvalueId.Value, "BB", input.BB, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.BB, null, RequirementDurationType.Constants.Not_required);
                    ProcessColumn(input.EvalueId.Value, "BC", input.BC, null, ExpirationSettingType.NA, userRecords, RequirementTypeOption.Constants.BC, null, RequirementDurationType.Constants.Not_required);
                }
                else
                {
                    _log.CurrentRow.CurrentColumn.ProcessingResult = RowProcessingResult.SKIPPED_ERROR_RETRIEVING_USER_PERSONAL_RECORDS;
                    _log.CurrentRow.CurrentColumn.Append("Error retrieving personal records from E*Value");
                }

                _log.CurrentRow.CurrentColumn.End();

                _log.CurrentRow.ProcessingResult = RowProcessingResult.FINISHED;
            }

            _log.CurrentRow.End();
        }

        public void ProcessColumn(
            int evalueUserId,
            string columnName,
            object columnValue,
            object noteValue,
            ExpirationSettingType expirationSetting,
            IList<PersonalRecord> userRecords,
            int requirementTypeId,
            int? requirementTypeIdFallback,
            int requirementDurationTypeId
            )
        {
            var log = new ColumnLog
            {
                InputColumnName = columnName,
                InputColumnValue = columnValue
            };

            log.Start();

            if (columnValue != null)
            {
                try
                {
                    var columnValueStr = columnValue.ToString();
                    var noteValueStr = noteValue != null ? noteValue.ToString() : string.Empty;
                    var changes = new List<string>();

                    var updateRecord = new PersonalRecord
                    {
                        UserId = evalueUserId,
                        StatusId = RequirementStatusOption.Constants.Not_Met,
                        RequirementId = requirementDurationTypeId,
                        TypeId = requirementTypeId, 
                    };
                    
                    DateTime dt;
                    if (DateTime.TryParse(columnValue.ToString(), out dt))
                    {
                        updateRecord.EventDate = dt;

                        switch (expirationSetting)
                        {
                            case ExpirationSettingType.Months_11:

                                updateRecord.ExpireDate = updateRecord.EventDate.Value.AddMonths(11);
                                break;
                            case ExpirationSettingType.Years_1:

                                updateRecord.ExpireDate = updateRecord.EventDate.Value.AddYears(1);
                                break;
                            case ExpirationSettingType.Years_5:

                                updateRecord.ExpireDate = updateRecord.EventDate.Value.AddYears(5);
                                break;
                            case ExpirationSettingType.Years_10:

                                updateRecord.ExpireDate = updateRecord.EventDate.Value.AddYears(10);
                                break;

                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(noteValueStr))
                        {
                            noteValueStr = columnValueStr;
                        }
                    }

                    if (!string.IsNullOrEmpty(noteValueStr))
                    {
                        updateRecord.Note = noteValueStr;
                    }

                    var existing = userRecords.Where(x => x == updateRecord).FirstOrDefault();

                    //a prerequisite check is required.  If a record with primary requirement type is already present, use the fallback type
                    if (existing != null &&
                        requirementTypeIdFallback.HasValue &&
                        existing.EventDate.HasValue &&
                        updateRecord.EventDate.HasValue &&
                        existing.EventDate.Value != updateRecord.EventDate.Value)
                    {
                        log.Append(string.Format("A prior requirement type [{0}] was found, using fallback type of [{1}]", requirementTypeId, requirementTypeIdFallback.Value));

                        updateRecord.TypeId = requirementTypeIdFallback.Value;
                        existing = userRecords.Where(x => x == updateRecord).FirstOrDefault();
                    }

                    if (existing == null)
                    {
                        log.Append("No E*Value record found");
                        changes.Add("All");
                    }
                    else
                    {
                        log.Append("E*Value record found");

                        updateRecord.IcId = existing.IcId;

                        if (updateRecord.EventDate.HasValue && (!existing.EventDate.HasValue || existing.EventDate.Value != updateRecord.EventDate.Value))
                        {
                            var existingDateStr = existing.EventDate.HasValue ? existing.EventDate.Value.ToShortDateString() : "null";

                            changes.Add("EventDate");
                            log.Append(string.Format("EventDate changed from {0} to {1}]", existingDateStr, updateRecord.EventDate.Value));
                        }
                        if (existing.RequirementId.HasValue && existing.RequirementId.Value != updateRecord.RequirementId.Value)
                        {
                            changes.Add("RequirementId");
                            log.Append(string.Format("RequirementId changed from {0} to {1}", existing.RequirementId.Value, updateRecord.RequirementId.Value));
                        }
                        if (existing.StatusId.HasValue && existing.StatusId.Value != updateRecord.StatusId.Value)
                        {
                            changes.Add("StatusId");
                            log.Append(string.Format("StatusId changed from {0} to {1}", existing.StatusId.Value, updateRecord.StatusId.Value));
                        }
                        if (string.IsNullOrEmpty(existing.Note))
                        {
                            existing.Note = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(updateRecord.Note) && existing.Note != updateRecord.Note)
                        {
                            changes.Add("Note");
                            log.Append(string.Format("Note changed from {0} to {1}", existing.Note, updateRecord.Note));
                        }
                    }

                    if (changes.Count == 0)
                    {
                        log.ProcessingResult = ColumnProcessingResult.SKIPPED_NO_CHANGES_DETECTED;
                        log.Append("No changes were detected.");
                    }
                    else
                    {
                        log.Append(string.Format("Updating E*Value due to changes in fields [{0}]", string.Join(",", changes.Select(x => x))));

                        PersonalRecord record;

                        if (updateRecord.IcId.HasValue)
                        {
                            var updateResponse = _api.Update(updateRecord);

                            log.Append("E*Value API Request", updateResponse.RequestXml);

                            if (updateResponse.Status)
                            {
                                log.Append("E*Value update record succeeded");
                                log.ProcessingResult = ColumnProcessingResult.EVALUE_UPDATE_SUCCESS;
                            }
                            else
                            {
                                log.Append("E*Value update record failed");
                                log.Append(updateResponse.ErrorMessage);
                                log.ProcessingResult = ColumnProcessingResult.EVALUE_UPDATE_FAILED;
                            }
                            record = updateResponse.PersonalRecord;
                        }
                        else
                        {
                            var createResponse = _api.Create(updateRecord);

                            log.Append("E*Value API Request", createResponse.RequestXml);

                            if (createResponse.Status)
                            {
                                log.Append("E*Value created succeeded");
                                log.ProcessingResult = ColumnProcessingResult.EVALUE_UPDATE_SUCCESS;
                            }
                            else
                            {
                                log.Append("E*Value update record failed");
                                log.Append(createResponse.ErrorMessage);
                                log.ProcessingResult = ColumnProcessingResult.EVALUE_UPDATE_FAILED;
                            }
                            record = createResponse.PersonalRecord;
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Append(ex.Message);
                    log.ProcessingResult = ColumnProcessingResult.FAILED_EXCEPTION_OCCURRED;
                }          
            }
            else
            {
                log.ProcessingResult = ColumnProcessingResult.SKIPPED_NO_VALUE_IN_INPUT_COLUMN;
            }

            _log.CurrentRow.Columns.Add(log);
        }

        public void End()
        {
            _log.ProcessingResult = LogProcessingResult.COMPLETED;
            _log.End();
        }

        public override string ToString()
        {
            return _log.GetXElement().ToString();
        }
    }
}
