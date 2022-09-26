namespace Global.Shared.Commons
{
    public static class Constant
    {
        public const string DATE_TIME_FORMAT_MMddyyyy = "MM/dd/yyyy";
        public const string DATE_TIME_FORMAT_ddMMMyy = "dd-MMM-yy";
        public const string ID_CAN_NOT_EMPTY_NOTICE = "Id can not be empty";
        public const string RETURN_NULL_NOTICE = "Can't update Report because Server return null";
        public const string CAN_NOT_UPDATE_REPORT_NOTICE = "Can't update Report";
        public const string UPDATE_REPORT_SUCCESSFULLY_NOTICE = "Update Report succesfully";
        public const string COURSECODE_CAN_NOT_EMPTY_NOTICE = "CourseCode can not be empty";
        public const string CREATE_REPORT_SUCCESSFULLY_NOTICE = "Create Report succesfully";
        public const string CAN_NOT_CREATE_REPORT_NOTICE = "Can't create Report";
        public const string FILENAME_DELIVERY_TEMPLATE = "EmployeeTrainingDelivery.xlsx";
        public const string FILENAME_HISTORY_TEMPLATE = "EmployeeTrainingHistory.xlsx";

        public const string EMPLOYEE_INFO_HEADER = "Employee Info";
        public const string COURSE_INFO_HEADER = "Course Info";
        public const string TRAINEE_INFO_HEADER = "Trainee Info";
        public const string VALIDATION_AND_SUPPORT_INFO_HEADER = "Validation & Supporting Info";

        public const string WORKSHEET_RECORD_OF_CHANGES = "Record of changes";
        public const string WORKSHEET_UNI_AND_FALCULTY_LIST = "Uni& Faculty_List";
        public const string WORKSHEET_FINANCE_OBLIGATION = "Finance obligation";
        public const string WORKSHEET_GUIDELINE = "Guideline";
        public const string WORKSHEET_COURSES_SEMINARS_WORKSHOPS = "Courses, seminars, workshops";
        public const string WORKSHEET_EXAMS_AND_CERTIFICATE_SUPPORT = "Exams & Certificate support";

        public const string EXPORT_FILENAME_PREFIX_HISTORY = "FA_HCM_Employee Training History Database_";
        public const string EXPORT_FILENAME_PREFIX_DELIVERY = "FA_HCM_Employee Training Delivery Database_";
        public const string EXPORT_FILE_EXTENSION = ".xlsx";

        public const string EXCEPTION_NOT_FOUND_FEEDBACK = "Not found Feedback with id";
        public const string EXCEPTION_NOT_FOUND_FEEDBACK_ANSWER = "Not found Feedback Answer with id";
        public const string EXCEPTION_NOT_FOUND_FEEDBACK_QUESTION = "Not found Feedback Question with id";
        public const string EXCEPTION_REMOVE_FAILED = "Can't remove this object";

        public const string CAN_NOT_CREATE_ATTENDANCE = "Can't create attendance";
        public const string CAN_NOT_UPDATE_ATTENDANCE = "Can't update attendance";
        public const string EXCEPTION_NOT_FOUND_ATTENDANCE = "Not found Attendance";
        public const string EXCEPTION_ATTENDANCE_ALREADY_EXIST = "Attendance already exist";
        public const string SUCCESSFUL_ATTENDANCE = "Successful attendance";
        public const string FAILED_ATTENDANCE = "Failed attendance";
        public const string ALREADY_ATTENDED = "Already attended";
        public const string REPORT_DOES_NOT_EXIST = "Report does not exist";
        public const string REPORT_ALREADY_EXIST = "Report already exist";
        public const string INVALID_LINK = "Invalid link";

        public const string DOMAIN_EMAIL_FSOFT = "@fsoft.com.vn";
        public const string IMPORT_FAIL = "Import fail!";
        public const string EXCEPTION_NOT_FOUND_FRESHER = "Fresher not found";
        public const string EXCEPTION_CREATE_CLASS_FAIL = "Create class fail!";
        public const string EXCEPTION_CLASS_NOT_FOUND = "CLass Fresher not found";
        public const string EXCEPTION_UPDATE_CLASS_FAIL = "Update class fail!";
        public const string EXCEPTION_LIST_FRESHER_NOT_FOUND = "List fresher not found";
        public const string EXCEPTION_CHANGE_STATUS_FRESHER_FAIL = "Change staus for fresher fail!";
        public const string EXCEPTION_ID_FRESHER_EMPTY = "Id fresher is empty";
        public const string EXCEPTION_INVALID = "Invalid";
        public const string SEND_EMAIL_SUCCESSFUL = "Send email successful!";
        public const int VIETNAM_GMT_ADD7_HOUR = 7;
        public const int TOKEN_EXPIRATION_AFRER_30MINUTES = 30;
        public const int ATTENDANCE_TOKEN_LENGTH = 162;
        public const int SHA256_TOKEN_LENGTH = 64;
        public const int FIRST_INDEX_LENGTH = 0;
        public const string EXCEPTION_SHEETNAME_DOESNT_EXIST = "Sheet Name doesn't exist in this excel";
        public const string EXCEPTION_STATUS_FRESHER_NOT_CHANGE = "Status is not change";
        public const string LIST_CLASS_EXITED = "List class is exited";
        public const string UPDATE_STATUS_SUCCESS = "Update status success";
        public const string EXCEPTION_UPDATE_STATUS_FAIL = "Update status fail";
        public const string UPDATE_CLASS_INFO_SUCCESS = "Updated successful";
        public const int FIRST_ELEMENT = 0;
        public const int SECOND_ELEMENT = 1;
        public const int THIRD_ELEMENT = 2;
        public const int STATUS_NOT_FOUND = 404;
        public const int STATUS_CONFLICT = 409;
        public const int FIRST_ATTENDANCE = 1;
        public const int SECOND_ATTENDANCE = 2;

        public const string PROJECT_NAME = "fresher-management-system";
        public const string SOLUTION_NAME = "Apis";

        public const string LOCATION_JSON_FILENAME = "ClassLocations";
        public const string FRESHERS_JOBRANK_JSON_FILENAME = "FreshersJobRank";
        public const string FRESHERS_MAJOR_JSON_FILENAME = "FreshersMajor";

        public const string ADMIN_NAME_CAN_NOT_EMPTY_NOTICE = "Admin Name can not be empty";

        public const int INTERNAL_SERVER_ERROR = 500;
        public const string INTERNAL_SERVER_ERROR_MESSAGE = "Something went wrong";

        public const string CHART = "Chart";
        public const string COURSES = "Courses";
        public const string SCORE = "Score";
        public const string PIE_CHART = "Pie Chart";
        public const string FONT_NAME_ARIAL = "Arial";

        public const string EXCEPTION_NOT_FOUND_USER = "User not found";
        public const string EXCEL_TEMPLATE_ASSEMBLY = "Global.Shared";
        public const string EXCEL_TEMPLATE_SUBFOLDER_ASSEMBLY = "FileExcelTemplates";

        public const string EXCEPTION_UPDATE_MODULERESULT_FAIL = "Update module result fail";
        public const string EXCEPTION_SAVECHANGE_FAILED = "SaveChange Failed!";
        public const string EXCEPTION_UPDTAE_RELATED_TABLE_FAILED = "Update Related Table Failed!";
        public const string DATA_PLAN = "Plans";
        public const string DATA_MMODULE = "Modules";
        public const string DATA_TOPIC= "Topics";
        public const string DATA_CHAPTERSYLLABUS = "ChapterSyllabus";
        public const string DATA_LECTURECHAPTER = "LectureChapter";
        public const string EXCEPTION_MODULE_NOT_FOUND = "Module not found!";
        public const string EXCEPTION_LIST_MODULE_NOT_FOUND = "List module not found!";
        public const string EXCEPTION_PLAN_NOT_FOUND = "Plan not found!";
        public const string EXCEPTION_LIST_PLAN_NOT_FOUND = "List plan not found!";
        public const string EXCEPTION_TOPIC_NOT_FOUND = "Topic not found!";
        public const string EXCEPTION_LIST_TOPIC_NOT_FOUND = "List topic not found!";
        public const string EXCEPTION_CHAPTER_SYLLABUS_NOT_FOUND = "ChapterSyllabus not found!";
        public const string EXCEPTION_LIST_CHAPTER_SYLLABUS_NOT_FOUND = "List ChapterSyllabus not found!";
        public const string EXCEPTION_LECTURE_NOT_FOUND = "Lecture not found!";
        public const string EXCEPTION_PLANINFORMATION_NOT_FOUND = "PlanInformation not found!";
        public const string EXCEPTION_SYLLABUS_DETAIL_NOT_FOUND = "Syllabus not found!";
        public const string EXCEPTION_RRCODE_IS_NOT_VALID = "RRCode is not valid!";
        public const string EXCEPTION_ACCOUNT_NAME_IS_NOT_VALID = "Account Name is not valid!";

        public const string EXCEPTION_MODULE_NAME_IS_NOT_VALID = "Module Name is not valid!";
        public const string EXCEPTION_MODULE_SCORE_IS_NOT_VALID = "Module Score is not valid!";
        public const string EXCEPTION_FRESHER_IS_NOT_VALID = "This fresher is not valid!";
        public const string EXCEPTION_TYPE_SCORE_IS_NOT_NUMBER = "Type Score must be a number!";
        public const string EXCEPTION_TYPE_SCORE_IS_NOT_EXIST = "Type Score is not valid!";


        public const string INVALID_DATE_STRING = "Invalid date time";
        /// <summary>
        /// Expirition with hour units
        /// </summary>
        public const int JWT_TOKEN_EXPIRITION = 8;

    }
}
