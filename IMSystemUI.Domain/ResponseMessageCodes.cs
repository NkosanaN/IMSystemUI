using System.Collections.Immutable;

namespace IMSystemUI.Domain
{
    public static class ResponseMessageCodes
    {
        public static ImmutableDictionary<string, string> ErrorDictionary => Dictionary.ToImmutableDictionary();

        public static ImmutableDictionary<string, string> SuccefullDictionary => HappyPath.ToImmutableDictionary();

        public const string Success = "SUCCESS";
        public const string Unauthorized = "UNAUTHORIZED";
        public const string UserAlreadyExists = "USER_ALREADY_EXISTS";
        public const string InvalidCredentials = "INVALID_CREDENTIALS";
        public const string UserNotFound = "USER_NOT_FOUND";
        public const string PermissionDenied = "PERMISSION_DENIED";
        public const string ErrorMsg = "ERROR_MSG";
        public const string SuccessDepartment = "SUCCESS_DEPARTMENT";

        public const string IssuerDenied = "ISSUER_DENIED";

        private static readonly Dictionary<string, string> Dictionary = new()
        {
            { Unauthorized, "User not authorized, please, sign in." },
            { UserAlreadyExists, "User already exists in the system." },
            { UserNotFound, "User not found in the system." },
            { PermissionDenied, "You are not authorized to perform this action." },
            { InvalidCredentials, "Invalid credentials. Please, enter valid username and password." },
            { IssuerDenied, "Issuer cannot be the same as Receiver." },
            { ErrorMsg, "Oops Error Occurred during Processing." }
        };

        private static readonly Dictionary<string, string> HappyPath = new()
        {
            { SuccessDepartment, "Successful created department." },
         
        };


    }


}
